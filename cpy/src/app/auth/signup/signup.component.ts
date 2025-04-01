import { Component } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, NgForm, NgModel, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';  


@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    FlexLayoutModule,
    MatButtonModule,
    FormsModule,
    MatDatepickerModule,
    MatProgressSpinnerModule,
    MatCheckboxModule  ,
 
    
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  maxDate!: Date;
  isLoading = false; 

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  onSubmit(form: NgForm) {
    if (form.invalid) return;

    this.isLoading = true;

    this.authService.registerUser({
      email: form.value.email,
      passwordHash: form.value.password,
      birthdate: form.value.date.toISOString(),  // Correct format
      CreatedOn: new Date().toISOString() 
});

    setTimeout(() => {
      this.isLoading = false;
      this.router.navigate(['/login']);
    }, 2000);  // Simulated async behavior
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }
}
