import { Component , inject} from '@angular/core';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-home',
  standalone: false,

  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  {
  private acountService = inject(AccountService);
  currentUser = this.acountService.currentUser;
  registerMode = false;
  users: any = {}

  registerToogle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
