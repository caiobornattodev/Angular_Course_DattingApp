import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs';


export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busySevice = inject(BusyService);

  busySevice.busy();

  return next(req).pipe(
    delay(1000),
    finalize(() => {
      busySevice.idle()
    })
  );
};
