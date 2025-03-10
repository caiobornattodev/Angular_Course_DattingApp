import { Component, EventEmitter, inject, Input, Output, ViewChild  } from '@angular/core';
import { Message } from '../../_models/message';
import { MessageService } from '../../_services/message.service';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-member-messages',
  standalone: false,
  
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent {

  @ViewChild('messageForm') messageForm?: NgForm;
  @Input({ required: true }) username: string;
  @Input({ required: true }) messages: Message[];
  @Output() updateMessages = new EventEmitter<Message>();

  private messageService = inject(MessageService);
  messageContent = '';

  sendMessage() {
    this.messageService.sendMessage(this.username, this.messageContent).subscribe({
      next: message => {
        this.updateMessages.emit(message);
        this.messageForm?.reset();
      }
    })
  }
}
