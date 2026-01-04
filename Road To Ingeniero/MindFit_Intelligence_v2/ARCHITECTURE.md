# Arquitectura del Sistema - MindFit Intelligence

## 📐 Visión General

MindFit Intelligence sigue una arquitectura en capas con separación clara de responsabilidades, implementando patrones modernos de desarrollo de software.

## 🏗️ Arquitectura Backend (Clean Architecture)

### Capas

```
┌─────────────────────────────────────┐
│         Presentation Layer          │
│         (MindFit.API)               │
│  - Controllers                      │
│  - Middleware                       │
│  - DTOs Request/Response            │
└─────────────┬───────────────────────┘
              │
┌─────────────▼───────────────────────┐
│        Application Layer            │
│      (MindFit.Application)          │
│  - Commands (CQRS)                  │
│  - Queries (CQRS)                   │
│  - Handlers                         │
│  - Interfaces                       │
│  - DTOs                             │
│  - Validators                       │
└─────────────┬───────────────────────┘
              │
┌─────────────▼───────────────────────┐
│          Domain Layer               │
│        (MindFit.Domain)             │
│  - Entities                         │
│  - Value Objects                    │
│  - Domain Events                    │
│  - Business Rules                   │
└─────────────┬───────────────────────┘
              │
┌─────────────▼───────────────────────┐
│      Infrastructure Layer           │
│     (MindFit.Infrastructure)        │
│  - DbContext                        │
│  - Repositories                     │
│  - External Services                │
│  - Persistence                      │
└─────────────────────────────────────┘
```

### Flujo de una Request

```
Cliente → Controller → MediatR → Command/Query Handler → Repository → Database
                                          ↓
                                        Domain
```

## 🎨 Arquitectura Frontend

### Estructura por Features

```
src/
├── components/           # Componentes reutilizables
│   ├── Layout/          # Header, Sidebar, Footer
│   ├── Common/          # Buttons, Inputs, Cards
│   └── Modals/          # Diálogos y modales
├── pages/               # Páginas principales
│   ├── Dashboard/
│   ├── Members/
│   ├── Classes/
│   └── ...
├── services/            # Servicios API
│   ├── api.js          # Cliente Axios configurado
│   ├── memberService.js
│   └── ...
├── store/               # Estado global (Zustand)
│   ├── memberStore.js
│   └── ...
├── hooks/               # Custom hooks
│   ├── useAuth.js
│   └── ...
└── utils/               # Utilidades
    ├── formatters.js
    └── validators.js
```

### Flujo de Datos

```
UI Component → Store Action → Service → API → Backend
      ↑                                          ↓
      └──────────── Store Update ←──────────────┘
```

## 🗄️ Arquitectura de Base de Datos

### Diseño Relacional

```
Members ──┬── Memberships ── MembershipPlans
          ├── ClassBookings ── ClassSchedules ── Classes ── Trainers
          ├── Payments
          └── Attendances

Users (Sistema)
```

### Capas de Abstracción

1. **Physical Layer**: Tablas, índices, constraints
2. **Logic Layer**: Stored Procedures, Functions, Triggers
3. **View Layer**: Views para consultas complejas
4. **Application Layer**: Entity Framework en .NET

## 🔄 Patrones Implementados

### Backend Patterns

1. **CQRS (Command Query Responsibility Segregation)**

   - Commands: Modifican estado
   - Queries: Solo lectura
   - Separación clara de responsabilidades

2. **Repository Pattern**

   - Abstracción del acceso a datos
   - Facilita testing
   - Desacoplamiento

3. **Unit of Work**

   - Gestión de transacciones
   - Múltiples repositorios en una transacción
   - Garantiza consistencia

4. **Dependency Injection**

   - Inversión de control
   - Facilita testing
   - Código más mantenible

5. **Mediator Pattern (MediatR)**
   - Desacoplamiento de handlers
   - Pipeline de comportamientos
   - Fácil extensión

### Frontend Patterns

1. **Component Composition**

   - Componentes pequeños y enfocados
   - Reutilización
   - Fácil mantenimiento

2. **Custom Hooks**

   - Lógica reutilizable
   - Separación de concerns
   - Testing simplificado

3. **Service Layer**

   - Abstracción de API calls
   - Centralización de configuración
   - Fácil mockeo para tests

4. **State Management**
   - Zustand para estado global
   - React Context para temas/auth
   - Local state con useState

## 🔐 Seguridad

### Autenticación y Autorización

```
Client → JWT Token → API → Validate → Authorize → Execute
                      ↓
                   Database
```

### Capas de Seguridad

1. **Frontend**

   - Validación de formularios
   - Sanitización de inputs
   - Token storage seguro

2. **Backend**

   - JWT Authentication
   - Role-based Authorization
   - Input validation (FluentValidation)
   - SQL Injection protection (EF Core)

3. **Database**
   - Stored Procedures
   - Constraints y validaciones
   - Encriptación de datos sensibles

## 📈 Escalabilidad

### Horizontal Scaling

- Load balancer para múltiples instancias de API
- Stateless API (JWT)
- Database clustering

### Vertical Scaling

- Optimización de queries
- Índices en base de datos
- Caching (Redis)
- CDN para frontend

## 🧪 Testing Strategy

```
Frontend Tests
├── Unit Tests (Components)
├── Integration Tests (Pages)
└── E2E Tests (User flows)

Backend Tests
├── Unit Tests (Handlers, Services)
├── Integration Tests (Repositories)
└── API Tests (Controllers)

Database Tests
├── Schema validation
├── Stored Procedures
└── Performance tests
```

## 📊 Monitoreo y Logging

### Application Insights

- Performance monitoring
- Error tracking
- User analytics

### Logging Levels

```
TRACE → DEBUG → INFO → WARN → ERROR → FATAL
```

## 🚀 Deployment

```
Development → Testing → Staging → Production
     ↓           ↓         ↓          ↓
   Docker    Docker    Docker     Docker
   Compose   Compose   + K8s       + K8s
```

## 📝 Mejores Prácticas

### Código

- SOLID Principles
- DRY (Don't Repeat Yourself)
- KISS (Keep It Simple, Stupid)
- YAGNI (You Aren't Gonna Need It)

### Git Workflow

```
feature/xxx → develop → staging → main
```

### Code Review

- Pull Requests requeridos
- Al menos 1 aprobación
- Tests pasando
- Sin conflictos

---

Esta arquitectura permite:

- ✅ Fácil mantenimiento
- ✅ Escalabilidad
- ✅ Testing efectivo
- ✅ Desarrollo paralelo
- ✅ Despliegue continuo
