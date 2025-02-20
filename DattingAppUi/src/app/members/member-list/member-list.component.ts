import { Component, OnInit, inject } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { AccountService } from '../../_services/account.service';
import { UserParams } from '../../_models/userParams';

@Component({
  selector: 'app-member-list',
  standalone: false,
  
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {


  memberService = inject(MembersService);

  genderList = [
    { value: 'male', display: 'Males' },
    { value: 'female', display: 'Females' }
  ];

  ngOnInit(): void {
    console.log

    if (!this.memberService.paginatedResult()) {
      this.loadMembers();
    }
  }

  loadMembers() {
    this.memberService.getMembers()
  }

  resetFilters() {
    this.memberService.resetUserParams();
    this.loadMembers();
  }

  pageChanged(event: any) {
    const userParams = this.memberService.userParams();
    if (userParams.pageNumber !== event.page) {
      userParams.pageNumber = event.page;
      this.loadMembers();
    }
  }
}
