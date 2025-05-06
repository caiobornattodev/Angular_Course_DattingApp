import { Component, inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Member } from '../../_models/member';
import { GalleryItem, ImageItem } from 'ng-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { MessageService } from '../../_services/message.service';
import { PresenceService } from '../../_services/presence.service';
import { AccountService } from '../../_services/account.service';
import { HubConnectionState } from '@microsoft/signalr';


@Component({
  selector: 'app-member-detail',
  standalone: false,
  
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit, OnDestroy {

  @ViewChild('memberTabs', { static : true }) memberTabs?: TabsetComponent;

  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private accountService = inject(AccountService);
  private messageService = inject(MessageService);
  presenceService = inject(PresenceService);
  member: Member = {} as Member;
  images: GalleryItem[] = [];
  activeTab?: TabDirective;


  ngOnInit(): void {

    this.route.data.subscribe({
      next: data => {
        this.member = data['member'];
        this.member && this.member.photos.map(photo => {
          this.images.push(new ImageItem({ src: photo.url, thumb: photo.url }))
        })
      }
    })

    this.route.paramMap.subscribe({
      next: _ => this.onRouteParamsChange()
    })

    this.route.queryParams.subscribe({
      next: params => {
        params['tab'] && this.selectTab(params['tab'])
      }
    })
  }



  selectTab(heading: string) {
    if (this.memberTabs) {
      const messageTab = this.memberTabs.tabs.find(x => x.heading === heading);
      if (messageTab) messageTab.active = true;
    }
  }

  onRouteParamsChange(){
    const user = this.accountService.currentUser();
    if(!user) return;
    if (this.messageService.hubConnection?.state === HubConnectionState.Connected && this.activeTab?.heading === 'Messages'){
      this.messageService.hubConnection.stop().then(() => {
        this.messageService.createHubConnection(user,this.member.userName);
      })
    }
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    this.router.navigate([],{
      relativeTo: this.route,
      queryParams: {tab : this.activeTab.heading },
      queryParamsHandling : 'merge'
    })
    if (this.activeTab.heading === 'Messages'  && this.member) {
      const currentUser = this.accountService.currentUser();
      if (!currentUser) return;
      this.messageService.createHubConnection(currentUser, this.member.userName);
    } else {
      this.messageService.stopHubConnection();
    }
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }
}
