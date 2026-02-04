import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment';

@Component({
  selector: 'app-appointment-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './appointment-list.html'
})
export class AppointmentListComponent implements OnInit {
  // Signal inject to access saved appointments
  private service = inject(AppointmentService);
  appointments = this.service.savedAppointments;

  ngOnInit() {
    // Load all saved appointments on component initialization
    this.service.loadAll();
  }
}