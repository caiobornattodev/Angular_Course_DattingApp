import { Component, EventEmitter, Input, Output, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: false,

  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent {

  //Decorators
  @Output() cancelRegister = new EventEmitter<boolean>();

  //Props
  private accountService = inject(AccountService)
  private toastrService = inject(ToastrService)
  model: any = {}

  //Methods
  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {
        this.cancel();
      },
      error: error => {       
        this.toastrService.error(error)
      }
    }) 
  }

  cancel() {
    this.cancelRegister.emit(false)
  }
}
