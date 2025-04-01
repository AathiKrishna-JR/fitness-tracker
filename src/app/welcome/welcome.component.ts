import { Component, OnInit } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css'],
  imports: [FlexLayoutModule],
})
export class WelcomeComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
