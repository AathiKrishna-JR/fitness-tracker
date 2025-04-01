import { Subject } from 'rxjs';
import { Exercise } from './exercise.model';
import { Injectable } from '@angular/core';
import { ExerciseService } from './exercise.service';
@Injectable({
  providedIn: 'root'
})
export class TrainingService {

  exerciseChanged = new Subject<Exercise | null>();
  
  private availableExercises: Exercise[] = []; 
  private runningExercise: Exercise | null = null;
  private exercises: Exercise[] = [];

  constructor(exercise :ExerciseService){
    exercise.getExercises().subscribe((exercises: Exercise[]) => {
      this.availableExercises = exercises;
    });
 }

  getAvailableExercises() {
    return [...this.availableExercises];
  }

  startExercise(selected: string) {
    const selectedExercise = this.availableExercises.find((ex) => {
      return ex.id == selected;  
      
    });
  
    console.log("available",this.availableExercises);
  
    if (!selectedExercise) {
      console.error('Selected exercise not found.');
      return;
    }
  
    this.runningExercise = selectedExercise;
    this.exerciseChanged.next({ ...this.runningExercise });
    console.log(this.runningExercise);
  
  }
  completeExercise() {
    if (!this.runningExercise) return;

    this.exercises.push({
      ...this.runningExercise,
      date: new Date(),
      state: 'completed'
    });
    this.runningExercise = null;
    this.exerciseChanged.next(null);
  }

  cancelExercise(progress: number) {
    if (!this.runningExercise) return;

    this.exercises.push({
      ...this.runningExercise,
      duration: this.runningExercise.duration * (progress / 100),
      calories: this.runningExercise.calories * (progress / 100),
      date: new Date(),
      state: 'cancelled'
    });
    this.runningExercise = null;
    this.exerciseChanged.next(null);
  }

  getRunningExercise(): Exercise | null {
    return this.runningExercise ? { ...this.runningExercise } : null;
  }

  getCompletedOrCancelledExercises() {
    return [...this.exercises];
  }
}
