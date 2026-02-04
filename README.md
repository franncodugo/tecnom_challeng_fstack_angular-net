# Boxes - Sistema de Reserva de Turnos

Este proyecto es una solución Fullstack diseñada para la gestión de turnos en talleres mecánicos. La aplicación permite visualizar talleres disponibles a través de una integración con la API de Tecnom, realizar reservas y listar los turnos agendados en memoria.

## 🚀 Decisiones Arquitectónicas

### Backend (.NET 10)
- **Clean Architecture**: Se dividió la solución en capas (API, Application, Infrastructure, Domain) para garantizar un desacoplamiento real y facilitar el testing.
- **Resiliencia**: Se implementó `IMemoryCache` en el servicio de talleres para optimizar el consumo de la API externa y mejorar los tiempos de respuesta del frontend.
- **Seguridad**: Gestión de credenciales mediante `IConfiguration`, evitando el hardcoding de secretos en el código fuente.
- **Validaciones**: Uso de DataAnnotations y lógica de negocio para asegurar la integridad de los datos de contacto y vehículos antes de la persistencia.

### Frontend (Angular 20)
- **Signals & Zoneless**: Se optó por un enfoque moderno sin `zone.js`, utilizando Signals para una detección de cambios granular y de alto rendimiento.
- **Reactive Forms**: Formulario robusto con validaciones síncronas para campos obligatorios (nombre, email, fecha, taller).
- **UI/UX**: Interfaz construida con Tailwind CSS, enfocada en la usabilidad y la claridad visual.

## 🛠️ Instalación y Configuración

### Requisitos previos
- .NET 10 SDK
- Node.js (v22 recomendado)
- Angular CLI

### Configuración del Backend
1. Navegar a la carpeta del servidor.
2. En `appsettings.json`, configurar las credenciales de la API de Tecnom:
   ```json
   "TecnomApi": {
     "Username": "tu_usuario",
     "Password": "tu_password"
   }
