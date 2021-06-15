import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'WebDashboard';


  getCurrentYear():number
  {
    let currentYear = new Date().getFullYear();
    return currentYear;
  }
}
