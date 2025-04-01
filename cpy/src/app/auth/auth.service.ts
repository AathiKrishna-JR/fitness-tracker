import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './user.model';
import { AuthData } from './auth-data.model';
import { Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  API_URL ='http://localhost:5052/api/FitnessUser';
  authChange = new Subject<boolean>();
  private user: User | null = null;
  constructor(private router: Router , private http : HttpClient) {}
  registerUser(authData: AuthData) {
   
    // this.uiService.LoadingStateChanged.next(true);
    console.log('Auth Data:', authData);
    this.http.post('http://localhost:5052/api/FitnessUser/AddFitnessUser', authData, {
      headers: { 'Content-Type': 'application/json' },
      responseType: 'text'  // <-- Important
    }).subscribe({
          next: () => {
            alert('Registration Successful! Please login.');
            console.log('Auth Data:', authData);
            this.router.navigate(['/login']);
            
          
          },
          error: (error) => {
           
            console.log('Auth Data:', authData);
            console.error('Error creating user:', error.error)
          }
        });
      }
  

      loginUser(user: { email: string; password: string }) {
        return this.http.post<{ message: string; token: string }>(
          'http://localhost:5052/api/FitnessUser/Login',
          user, {
          headers: { 'Content-Type': 'application/json' },
       } );
      }

  logout() {
    this.user = null;
    this.authChange.next(false);
  }

  getUser() {
    return { ...this.user };
  }

  
  isAuth() {
    return this.user != null;
  }

  private authSuccessfully() {
    this.authChange.next(true);
    this.router.navigate(['/training']);
  }
}