# MindFit Intelligence v2

Sistema integral de gestión de gimnasios desarrollado con las mejores prácticas de arquitectura de software moderna.

## 🏗️ Arquitectura del Proyecto

### Backend (.NET 8)

- **Clean Architecture** con separación de responsabilidades
- **CQRS Pattern** (Command Query Responsibility Segregation)
- **Repository Pattern** y **Unit of Work**
- **Entity Framework Core** para acceso a datos
- **MediatR** para manejo de comandos y queries
- **API RESTful** con Swagger/OpenAPI

### Frontend (React)

- **React 18** con Vite
- **Arquitectura modular** por features
- **Zustand** para state management
- **React Router** para navegación
- **Tailwind CSS** para estilos
- **Axios** para peticiones HTTP
- **Custom Hooks** para lógica reutilizable

### Base de Datos (SQL Server)

- **Diseño normalizado** con integridad referencial
- **Stored Procedures** para lógica compleja
- **Triggers** para automatización
- **Views** para consultas optimizadas
- **Índices** para mejor rendimiento

## 📁 Estructura del Proyecto

```
MindFit_Intelligence_v2/
├── src/
│   ├── Backend/
│   │   ├── MindFit.Domain/           # Entidades y lógica de negocio
│   │   ├── MindFit.Application/      # Casos de uso (CQRS)
│   │   ├── MindFit.Infrastructure/   # Implementación de repositorios
│   │   └── MindFit.API/              # Controllers y configuración API
│   ├── Frontend/
│   │   ├── src/
│   │   │   ├── components/          # Componentes reutilizables
│   │   │   ├── pages/               # Páginas de la aplicación
│   │   │   ├── services/            # Servicios API
│   │   │   ├── store/               # Estado global (Zustand)
│   │   │   └── hooks/               # Custom hooks
│   │   └── public/
│   └── Database/
│       ├── 01_CreateSchema.sql      # Creación de tablas
│       ├── 02_StoredProcedures.sql  # Procedimientos almacenados
│       ├── 03_SampleData.sql        # Datos de prueba
│       ├── 04_Triggers.sql          # Triggers
│       └── 05_Views.sql             # Vistas
└── README.md
```

## 🚀 Configuración e Instalación

### Requisitos Previos

- **.NET 8 SDK** o superior
- **Node.js 18+** y npm
- **SQL Server 2019+** o SQL Server Express
- Un IDE como **Visual Studio 2022** o **VS Code**

### 1. Configuración de la Base de Datos

```bash
# Ejecutar los scripts en orden desde SQL Server Management Studio
# o usando sqlcmd:

sqlcmd -S localhost -U sa -P YourPassword -i src/Database/01_CreateSchema.sql
sqlcmd -S localhost -U sa -P YourPassword -i src/Database/02_StoredProcedures.sql
sqlcmd -S localhost -U sa -P YourPassword -i src/Database/03_SampleData.sql
sqlcmd -S localhost -U sa -P YourPassword -i src/Database/04_Triggers.sql
sqlcmd -S localhost -U sa -P YourPassword -i src/Database/05_Views.sql
```

### 2. Configuración del Backend

```bash
# Navegar al directorio del backend
cd src/Backend

# Restaurar paquetes NuGet
dotnet restore

# Actualizar la cadena de conexión en appsettings.json
# Editar: MindFit.API/appsettings.json
# Cambiar la ConnectionString con tus credenciales de SQL Server

# Ejecutar migraciones (si es necesario)
cd MindFit.API
dotnet ef database update

# Ejecutar la API
dotnet run
# La API estará disponible en: https://localhost:5001
```

### 3. Configuración del Frontend

```bash
# Navegar al directorio del frontend
cd src/Frontend

# Instalar dependencias
npm install

# Crear archivo de variables de entorno
cp .env.example .env

# Editar .env si es necesario (ya está configurado por defecto)

# Ejecutar en modo desarrollo
npm run dev
# La aplicación estará disponible en: http://localhost:3000
```

## 🎯 Funcionalidades Principales

### Gestión de Miembros

- ✅ Registro completo de miembros con información de contacto
- ✅ Historial de asistencias
- ✅ Gestión de membresías activas
- ✅ Seguimiento de contactos de emergencia

### Gestión de Membresías

- ✅ Planes personalizables (Básico, Premium, Elite)
- ✅ Renovación automática
- ✅ Alertas de vencimiento
- ✅ Historial de pagos

### Gestión de Clases

- ✅ Calendario de clases
- ✅ Reservas en línea
- ✅ Control de capacidad
- ✅ Múltiples categorías (Yoga, Cardio, Strength, etc.)

### Gestión de Entrenadores

- ✅ Perfiles completos con especializaciones
- ✅ Certificaciones
- ✅ Asignación de clases
- ✅ Tarifas por hora

### Pagos y Facturación

- ✅ Registro de pagos
- ✅ Múltiples métodos de pago
- ✅ Reportes de ingresos
- ✅ Historial de transacciones

### Dashboard y Reportes

- ✅ Estadísticas en tiempo real
- ✅ Indicadores clave (KPIs)
- ✅ Gráficos de ingresos
- ✅ Métricas de asistencia

## 🔐 Seguridad

- Autenticación con **JWT Tokens**
- Autorización basada en roles (Admin, Staff, Trainer)
- Validación de datos con **FluentValidation**
- Protección CORS configurada
- Encriptación de contraseñas

## 📊 Patrones y Mejores Prácticas Implementadas

### Backend

- ✅ **Clean Architecture**: Separación en capas (Domain, Application, Infrastructure, API)
- ✅ **CQRS**: Separación de comandos y queries
- ✅ **Repository Pattern**: Abstracción del acceso a datos
- ✅ **Unit of Work**: Gestión de transacciones
- ✅ **Dependency Injection**: Inyección de dependencias nativa de .NET
- ✅ **DTOs**: Separación entre entidades de dominio y respuestas API
- ✅ **MediatR**: Desacoplamiento de lógica de negocio

### Frontend

- ✅ **Component-Based Architecture**: Componentes reutilizables
- ✅ **Custom Hooks**: Lógica compartida y reutilizable
- ✅ **State Management**: Zustand para estado global
- ✅ **Service Layer**: Abstracción de llamadas API
- ✅ **Atomic Design**: Organización de componentes
- ✅ **Responsive Design**: Diseño adaptable con Tailwind CSS

### Base de Datos

- ✅ **Normalización**: Tercera forma normal (3NF)
- ✅ **Integridad Referencial**: Foreign Keys y Constraints
- ✅ **Índices**: Optimización de consultas frecuentes
- ✅ **Stored Procedures**: Lógica compleja en la base de datos
- ✅ **Triggers**: Automatización de tareas
- ✅ **Views**: Simplificación de consultas complejas

## 🧪 Testing

```bash
# Backend
cd src/Backend
dotnet test

# Frontend
cd src/Frontend
npm run test
```

## 📝 API Endpoints

### Members

- `GET /api/members` - Obtener todos los miembros
- `GET /api/members/{id}` - Obtener un miembro
- `POST /api/members` - Crear nuevo miembro
- `PUT /api/members/{id}` - Actualizar miembro
- `DELETE /api/members/{id}` - Eliminar miembro

### Classes

- `GET /api/classes` - Obtener todas las clases
- `POST /api/classes` - Crear nueva clase
- `GET /api/classes/schedule` - Obtener horarios

### Trainers

- `GET /api/trainers` - Obtener entrenadores
- `POST /api/trainers` - Crear entrenador

### Payments

- `GET /api/payments` - Obtener pagos
- `POST /api/payments` - Registrar pago
- `GET /api/payments/report` - Reporte de ingresos

## 🛠️ Tecnologías Utilizadas

### Backend

- .NET 8
- Entity Framework Core 8
- SQL Server
- MediatR
- AutoMapper
- FluentValidation
- Swagger/OpenAPI

### Frontend

- React 18
- Vite
- React Router v6
- Zustand
- Axios
- Tailwind CSS
- React Hot Toast
- Lucide Icons
- date-fns

### Base de Datos

- SQL Server 2019+
- T-SQL

## 📈 Roadmap Futuro

- [ ] Implementar notificaciones push
- [ ] Sistema de mensajería interna
- [ ] Integración con pasarelas de pago
- [ ] App móvil (React Native)
- [ ] Sistema de reservas online
- [ ] Análisis avanzado con BI
- [ ] Integración con dispositivos IoT

## 👥 Contribución

Este es un proyecto educativo. Las contribuciones son bienvenidas.

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 📧 Contacto

Para preguntas o sugerencias, contacta al equipo de desarrollo.

---

**MindFit Intelligence** - Sistema integral de gestión de gimnasios
Desarrollado con ❤️ usando las mejores prácticas de desarrollo de software
