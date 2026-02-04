import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AppointmentService } from '../../services/appointment';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-appointment-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './appointment-form.html'
})
export class AppointmentFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private service = inject(AppointmentService);

  // Signals to handle state
  workshops = this.service.workshops;
  loading = signal(false);

  // Reactive form
  appointmentForm = this.fb.group({
    placeId: [null, [Validators.required]],
    appointmentAt: ['', [Validators.required]],
    serviceType: ['', [Validators.required]],
    contact: this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required]]
    }),
    vehicle: this.fb.group({
      make: [''],
      model: [''],
      year: [''],
      licensePlate: ['']
    })
  });

  ngOnInit() {
    // Loading workshops on init
    this.service.getWorkshops();
  }

  async onSubmit() {
    if (this.appointmentForm.invalid) return;

    this.loading.set(true);
    try {
      await this.service.create(this.appointmentForm.value);
      alert('¡Turno reservado exitosamente!');
      this.appointmentForm.reset();
      this.service.loadAll(); // update the list of appointments
    } catch (error) {
      alert('Error al guardar el turno. Revisa la consola.');
    } finally {
      this.loading.set(false);
    }
  }
}