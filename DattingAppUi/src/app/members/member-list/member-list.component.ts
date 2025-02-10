import { Component, OnInit, inject } from '@angular/core';
import { MembersService } from '../../_services/members.service';

@Component({
  selector: 'app-member-list',
  standalone: false,
  
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  memberService = inject(MembersService);

  ngOnInit(): void {
    if (this.memberService.members().length === 0) {
     this.loadMembers();
    }
  }

  loadMembers() {
    this.memberService.getMembers()
  }
}
