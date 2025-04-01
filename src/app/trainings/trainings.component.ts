import { Component } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { CommonModule } from '@angular/common';
import { NewTrainingComponent } from './new-training/new-training.component';
import { PastTrainingsComponent } from './past-trainings/past-trainings.component';
import { CurrentTrainingComponent } from './current-traing/current-traing.component';
import { TrainingService } from './training.service';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { Route } from '@angular/router';
import{ RouterModule } from '@angular/router';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-trainings',
  standalone: true,
  imports: [
    CommonModule,
    MatTabsModule,
    NewTrainingComponent,
    PastTrainingsComponent,
    CurrentTrainingComponent,
    RouterModule
  ],
  templateUrl: './trainings.component.html',
  styleUrl: './trainings.component.css'
})
export class TrainingsComponent {
  ongoingTraining=false;
    exerciseSubscription!:Subscription;

    constructor(private trainingService: TrainingService) {}
    ngOnInit(){
      this.exerciseSubscription=this.trainingService.exerciseChanged.subscribe((exercise:any) => {
        if(exercise){
          this.ongoingTraining=true;
          console.log(exercise, this.ongoingTraining);
        }else{
          this.ongoingTraining=true;
          console.log(exercise, this.ongoingTraining);
        }
      });
    }

    onStartTraining(form:NgForm){
      this.trainingService.startExercise(form.value.exercise);
     
    }
    ngOnDestroy() {
      if (this.exerciseSubscription) {
        this.exerciseSubscription.unsubscribe();
      }
    }
    

}
