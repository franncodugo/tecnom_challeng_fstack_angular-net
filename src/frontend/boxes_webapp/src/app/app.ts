import { Component } from '@angular/core';
import { AppointmentFormComponent } from './components/appointment-form/appointment-form';
import { AppointmentListComponent } from './components/appointment-list/appointment-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [AppointmentFormComponent, AppointmentListComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class AppComponent {
  title = 'BoxesWeb';
}