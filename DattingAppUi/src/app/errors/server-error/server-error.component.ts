import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  standalone: false,
  
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})

export class ServerErrorComponent {
  

  public error: any;

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.['error'];
  }
}
