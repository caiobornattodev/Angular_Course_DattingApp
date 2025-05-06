import { Component, inject, Input, ViewChild  } from '@angular/core';
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


  messageService = inject(MessageService);
  messageContent = '';

  sendMessage() {
    this.messageService.sendMessage(this.username, this.messageContent).then(() => {
      this.messageForm?.reset();
    })
  }
}
