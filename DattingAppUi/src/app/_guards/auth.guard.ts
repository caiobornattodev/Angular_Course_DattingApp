import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core'
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';


export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastrService = inject(ToastrService);  

  if (accountService.currentUser()) {
    return true;
  } else {
    toastrService.error('You must be loged id to accces that tab')
    return false;
  }
};
