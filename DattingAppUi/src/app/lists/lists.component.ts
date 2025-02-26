import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { LikesService } from '../_services/likes.service';


@Component({
  selector: 'app-lists',
  standalone: false,
  
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit, OnDestroy {
  likesService = inject(LikesService);
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;

  ngOnInit(): void {
    this.loadLikes();
  }

  ngOnDestroy(): void {
    this.likesService.paginatedResult.set(null);
  }

  getTitle(): string {
    switch (this.predicate) {
      case 'liked': return 'Your likes';
      case 'likedBy': return 'Who likes you';
      default: return 'Mutual'
    }
  }

  loadLikes() {
    this.likesService.getLikes(this.predicate, this.pageNumber, this.pageSize);
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadLikes();
    }
  }
}
