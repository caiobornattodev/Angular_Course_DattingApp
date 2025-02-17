import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

import { AbstractControl, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: false,

  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})

export class RegisterComponent implements OnInit {


  //Decorators
  @Output() cancelRegister = new EventEmitter<boolean>();

  //Props
  private accountService = inject(AccountService);

  private router = inject(Router);


  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date();
  validationErrors: string[] | undefined;


  //Methods
  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18)
  }

  machValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    }
  }

  initializeForm() {
    this.registerForm = new FormGroup({
      gender: new FormControl('male'),
      username: new FormControl('', Validators.required),
      knownAs: new FormControl('', Validators.required),
      city: new FormControl('', Validators.required),
      country: new FormControl('', Validators.required),
      dateOfBirth: new FormControl('', Validators.required),
      password: new FormControl('',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(8)
        ]),
      confirmPassword: new FormControl('', [Validators.required, this.machValues('password')]),
    });

    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  register() {
    const dob = this.getDateOnly(this.registerForm.get('dateOfBirth')?.value);
    this.registerForm.patchValue({ dateOfBirth: dob })
    this.accountService.register(this.registerForm.value).subscribe({
      next: _ => {
        this.router.navigateByUrl('/members')
      },
      error: error => {
        this.validationErrors = error;
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {

    if (!dob) return undefined;

    return new Date(dob).toISOString().slice(0, 10);
  }
}
