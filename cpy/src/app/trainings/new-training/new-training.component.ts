import { Component, OnInit } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { TrainingService } from '../training.service';
import { Exercise } from '../exercise.model';
import { CommonModule } from '@angular/common';
import { FormsModule,NgForm } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ExerciseService } from '../exercise.service';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-new-training',
  imports: [MatCardModule,FlexLayoutModule,MatButtonModule,MatSelectModule,CommonModule,FormsModule,MatFormFieldModule,HttpClientModule],
  templateUrl: './new-training.component.html',
  styleUrl: './new-training.component.css'
})
export class NewTrainingComponent implements OnInit{
  
  exercises:Exercise[]=[];


  constructor(private trainingService:TrainingService,private exerciseService:ExerciseService){}

  ngOnInit() {
    this.exercises=this.trainingService.getAvailableExercises();
    this.exerciseService.getExercises().subscribe(
      (data) => {
        this.exercises = data;
        console.log(this.exercises);
      },
      (error: any) => {
        console.error('Error fetching exercises:', error.message);
      }
    );
  }
  onStartTraining(form:NgForm){
    this.trainingService.startExercise(form.value.exercise);
  }
}