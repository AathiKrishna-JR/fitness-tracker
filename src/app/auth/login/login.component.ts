import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule, Router } from '@angular/router';  // ✅ RouterModule added
import { AuthService } from '../auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    RouterModule 
  ],
  providers: [AuthService],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoading$!: Observable<boolean>;
  errorMessage: string = '';
  successMessage: string = '';
  constructor(
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });
  }

  onSubmit() {
    this.authService.loginUser({
      email: this.loginForm.get('email')?.value.trim(),  // Ensure no extra spaces
      password: this.loginForm.get('password')?.value.trim()
    }).subscribe({
      next: (response) => {
        this.successMessage = response.message;
        console.log(this.successMessage);
        alert(this.successMessage);
        setTimeout(() => {
          this.router.navigate(['training']);
        }, 1000);
      },
      error: (error) => {
        this.errorMessage = error.error.message || 'Login failed';
        console.error('Login failed', error.error.errors);
        alert(this.errorMessage);
      }
    });
  }
    
  navigateToSignup() {
    this.router.navigate(['/signup']);
  }
}
