import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { GalleryItem, ImageItem } from 'ng-gallery';


@Component({
  selector: 'app-member-detail',
  standalone: false,
  
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  member?: Member
  images: GalleryItem[] = [];

  ngOnInit(): void {
    this.loadMember()
  }

  loadMember() {
    const username = this.route.snapshot.paramMap.get('username');
    if (!username) return;
    this.memberService.getMember(username).subscribe({
      next: member => {
        this.member = member
        member.photos.map(photo => {
          this.images.push(new ImageItem({ src: photo.url, thumb: photo.url }))
        })
      }
    })
  }
}
