import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Workshop, Appointment } from '../models/appointment.model';

@Injectable({ providedIn: 'root' })
export class AppointmentService {
  private http = inject(HttpClient);
  private readonly API_URL = 'http://localhost:5169/api/appointments';

  // Signals for auto-updating components.
  workshops = signal<Workshop[]>([]);
  savedAppointments = signal<any[]>([]);

  // 1. GET Workshops.
  async getWorkshops() {
    try {
      const data = await firstValueFrom(this.http.get<Workshop[]>(`${this.API_URL}/workshops`));
      this.workshops.set(data);
    } catch (e) {
      console.error("Error cargando talleres", e);
    }
  }

  // 2. POST Appointment.
  async create(appointment: any) {
    return firstValueFrom(this.http.post(this.API_URL, appointment));
  }

  // 3. GET All Appointments.
  async loadAll() {
    try {
      const data = await firstValueFrom(this.http.get<any[]>(this.API_URL));
      this.savedAppointments.set(data);
    } catch (e) {
      console.error("Error loading appointments", e);
    }
  }
}