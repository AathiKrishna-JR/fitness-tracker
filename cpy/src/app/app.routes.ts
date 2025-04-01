import { Routes } from '@angular/router';
import { WelcomeComponent } from './welcome/welcome.component';
import { SignupComponent } from './auth/signup/signup.component';
import { NewTrainingComponent } from './trainings/new-training/new-training.component';
import { LoginComponent } from './auth/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { TrainingsComponent } from './trainings/trainings.component';
import { CurrentTrainingComponent } from './trainings/current-traing/current-traing.component';

export const routes: Routes = [
  { path: '', component: WelcomeComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'login', component: LoginComponent  },
  { path: 'training', component: TrainingsComponent },
  
];
