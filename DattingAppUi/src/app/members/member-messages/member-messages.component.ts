import { AfterViewChecked, Component, inject, Input, ViewChild  } from '@angular/core';
import { MessageService } from '../../_services/message.service';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-member-messages',
  standalone: false,
  
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent implements AfterViewChecked {
  @ViewChild('messageForm') messageForm?: NgForm;
  @ViewChild('scrollMe') scrollContainer?: any;
  @Input({ required: true }) username: string;

  messageService = inject(MessageService);
  messageContent = '';

  sendMessage() {
    this.messageService.sendMessage(this.username, this.messageContent).then(() => {
      this.messageForm?.reset();
      this.scrollToBottom();
    })
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  private scrollToBottom(){
    if(this.scrollContainer){
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
    }
  }
}
