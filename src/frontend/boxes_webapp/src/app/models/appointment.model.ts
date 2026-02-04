export interface Workshop {
  id: number;
  name: string;
  address: string;
}

export interface Appointment {
  place_id: number; 
  appointment_at: string; 
  service_type: string; 
  contact: {
    name: string;
    email: string; 
    phone: string; 
  };
  vehicle?: { 
    make: string;
    model: string; 
    year: number; 
    license_plate: string;
  };
}