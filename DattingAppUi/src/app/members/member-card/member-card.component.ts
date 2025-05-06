import { Component, computed, inject, Input } from '@angular/core';
import { Member } from '../../_models/member';
import { LikesService } from '../../_services/likes.service';
import { PresenceService } from '../../_services/presence.service';

@Component({
  selector: 'app-member-card',
  standalone: false,
  
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'

})
export class MemberCardComponent {
  @Input({ required: true }) member: Member;

  private presenceService = inject(PresenceService);
  private likeService = inject(LikesService);
  hasLiked = computed(() => this.likeService.likeIds().includes(this.member.id))
  isOnline = computed(() => this.presenceService.onlineUsers().includes(this.member.userName))

  toggleLike() {
    this.likeService.toggleLike(this.member.id).subscribe({
      next: () => {
        if (this.hasLiked()) {
          this.likeService.likeIds.update(ids => ids.filter(x => x !== this.member.id))
        } else {
          this.likeService.likeIds.update(ids => [...ids, this.member.id])
        }
      }
    });
  }
}
