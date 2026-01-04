# Endpoints API - MindFit Intelligence

## Índice

1. [Convenciones generales](#convenciones-generales)
2. [Autenticación](#autenticación)
3. [Sitio Público](#sitio-público)
4. [Módulo Socios](#módulo-socios)
5. [Módulo Turnos](#módulo-turnos)
6. [Módulo Rutinas](#módulo-rutinas)
7. [Módulo Gimnasio](#módulo-gimnasio)
8. [Módulo Seguridad](#módulo-seguridad)
9. [Módulo IA](#módulo-ia)
10. [Códigos de respuesta](#códigos-de-respuesta)

---

## Convenciones generales

### Base URL

```
Development: https://localhost:5001/api
Production: https://api.mindfit.com.ar/api
```

### Formato de respuestas

**Success Response:**

```json
{
  "success": true,
  "data": { ... },
  "message": "Operación exitosa"
}
```

**Error Response:**

```json
{
  "success": false,
  "error": {
    "code": "ERROR_CODE",
    "message": "Descripción del error",
    "details": { ... }
  }
}
```

**Paginación:**

```json
{
  "success": true,
  "data": {
    "items": [ ... ],
    "totalItems": 100,
    "currentPage": 1,
    "pageSize": 10,
    "totalPages": 10
  }
}
```

### Headers estándar

**Autenticación (portal):**

```
Authorization: Bearer <access_token>
Content-Type: application/json
```

**Sin autenticación (sitio público):**

```
Content-Type: application/json
```

---

## Autenticación

Base: `/api/auth`

### POST /auth/login

Iniciar sesión en el portal.

**Request:**

```json
{
  "gymId": 1,
  "username": "admin@gym",
  "password": "Password123!"
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "user": {
      "id": 1,
      "username": "admin@gym",
      "email": "admin@example.com",
      "nombre": "Juan",
      "apellido": "Pérez",
      "gymId": 1,
      "gymNombre": "FitZone Rosario"
    }
  }
}
```

**Response (401 Unauthorized):**

```json
{
  "success": false,
  "error": {
    "code": "INVALID_CREDENTIALS",
    "message": "Usuario o contraseña incorrectos"
  }
}
```

**Notas:**

- Refresh Token se envía en cookie HttpOnly (no visible en response)
- Access Token expira en 15 minutos
- Refresh Token expira en 7 días

---

### POST /auth/refresh

Renovar Access Token usando Refresh Token.

**Request:** (sin body, Refresh Token en cookie)

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIs..."
  }
}
```

**Response (401 Unauthorized):**

```json
{
  "success": false,
  "error": {
    "code": "INVALID_REFRESH_TOKEN",
    "message": "Token de refresco inválido o expirado"
  }
}
```

**Notas:**

- Nuevo Refresh Token se envía en cookie (rotación)
- Token anterior queda invalidado

---

### POST /auth/logout

Cerrar sesión (revocar Refresh Token).

**Request:** (sin body)

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Sesión cerrada exitosamente"
}
```

---

### POST /auth/forgot-password

Solicitar reseteo de contraseña.

**Request:**

```json
{
  "email": "usuario@example.com",
  "gymId": 1
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Se envió un correo con instrucciones para resetear la contraseña"
}
```

**Notas:**

- Siempre retorna 200 OK (no revelar si el email existe)
- Email contiene link con token: `https://app.mindfit.com/reset-password?token=abc123`
- Token expira en 30 minutos

---

### POST /auth/reset-password

Resetear contraseña con token recibido por email.

**Request:**

```json
{
  "token": "abc123def456",
  "newPassword": "NewPassword123!"
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Contraseña actualizada exitosamente"
}
```

**Response (400 Bad Request):**

```json
{
  "success": false,
  "error": {
    "code": "INVALID_OR_EXPIRED_TOKEN",
    "message": "El token es inválido o ha expirado"
  }
}
```

**Notas:**

- Token queda marcado como usado
- Todos los Refresh Tokens del usuario se revocan

---

### POST /auth/change-password

Cambiar contraseña (usuario autenticado).

**Request:**

```json
{
  "currentPassword": "OldPassword123!",
  "newPassword": "NewPassword123!"
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Contraseña actualizada exitosamente"
}
```

**Response (400 Bad Request):**

```json
{
  "success": false,
  "error": {
    "code": "INVALID_CURRENT_PASSWORD",
    "message": "La contraseña actual es incorrecta"
  }
}
```

**Requiere:** Autenticación (Bearer Token)

---

## Sitio Público

Base: `/api/public`

### POST /public/lead/demo-request

Solicitar demo (captura lead).

**Request:**

```json
{
  "nombreContacto": "Carlos Gómez",
  "emailContacto": "carlos@example.com",
  "telefonoContacto": "+54 341 1234567",
  "nombreGimnasio": "PowerGym",
  "ciudad": "Rosario",
  "mensaje": "Me interesa conocer más sobre la plataforma"
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "message": "Solicitud enviada exitosamente. Nos pondremos en contacto pronto."
}
```

---

### POST /public/contact

Enviar mensaje de contacto.

**Request:**

```json
{
  "nombre": "María López",
  "email": "maria@example.com",
  "asunto": "Consulta sobre precios",
  "mensaje": "Quisiera conocer los planes disponibles..."
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "message": "Mensaje enviado exitosamente"
}
```

---

### GET /public/gyms/search?query={query}

Buscar gimnasios (para selector de login).

**Query Params:**

- `query` (string): Término de búsqueda

**Request:**

```
GET /api/public/gyms/search?query=fit
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "nombre": "FitZone Rosario",
      "ciudad": "Rosario"
    },
    {
      "id": 2,
      "nombre": "FitCenter",
      "ciudad": "Rosario"
    }
  ]
}
```

**Notas:**

- Búsqueda case-insensitive por nombre de gimnasio
- Solo retorna gimnasios activos
- Limita a 10 resultados máximo

---

## Módulo Socios

Base: `/api/socios`  
**Requiere autenticación** (todas las operaciones)

### GET /socios

Listar socios del gimnasio.

**Query Params:**

- `page` (int, default: 1): Número de página
- `pageSize` (int, default: 10): Cantidad por página
- `search` (string, optional): Búsqueda por nombre, apellido, email o número de socio
- `activo` (bool, optional): Filtrar por activos/inactivos
- `cuotaAlDia` (bool, optional): Filtrar por cuota al día

**Request:**

```
GET /api/socios?page=1&pageSize=10&search=juan&activo=true
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "numeroSocio": "001",
        "nombre": "Juan",
        "apellido": "Pérez",
        "email": "juan@example.com",
        "telefono": "+54 341 1234567",
        "activo": true,
        "cuotaAlDia": true,
        "fechaInscripcion": "2024-01-15"
      }
    ],
    "totalItems": 1,
    "currentPage": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

**Permiso requerido:** `SOCIOS_VER`

---

### GET /socios/{id}

Obtener detalle de un socio.

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": 1,
    "numeroSocio": "001",
    "nombre": "Juan",
    "apellido": "Pérez",
    "email": "juan@example.com",
    "telefono": "+54 341 1234567",
    "fechaNacimiento": "1990-05-20",
    "direccion": "Calle Falsa 123",
    "dni": "35123456",
    "fechaInscripcion": "2024-01-15",
    "activo": true,
    "cuotaAlDia": true,
    "ultimaFechaPago": "2024-12-01",
    "observaciones": "Apto médico vigente"
  }
}
```

**Response (404 Not Found):**

```json
{
  "success": false,
  "error": {
    "code": "SOCIO_NOT_FOUND",
    "message": "Socio no encontrado"
  }
}
```

**Permiso requerido:** `SOCIOS_VER`

---

### POST /socios

Crear nuevo socio.

**Request:**

```json
{
  "numeroSocio": "002",
  "nombre": "María",
  "apellido": "González",
  "email": "maria@example.com",
  "telefono": "+54 341 7654321",
  "fechaNacimiento": "1995-08-10",
  "direccion": "Av. Siempre Viva 456",
  "dni": "40234567",
  "fechaInscripcion": "2025-01-01",
  "observaciones": null
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 2,
    "numeroSocio": "002",
    "nombre": "María",
    "apellido": "González",
    "email": "maria@example.com",
    "activo": true,
    "cuotaAlDia": true,
    "fechaInscripcion": "2025-01-01"
  }
}
```

**Response (400 Bad Request):**

```json
{
  "success": false,
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Errores de validación",
    "details": {
      "email": ["El email ya está registrado"],
      "numeroSocio": ["El número de socio ya existe"]
    }
  }
}
```

**Permiso requerido:** `SOCIOS_CREAR`

---

### PUT /socios/{id}

Actualizar socio.

**Request:**

```json
{
  "nombre": "María",
  "apellido": "González",
  "email": "maria.gonzalez@example.com",
  "telefono": "+54 341 7654321",
  "direccion": "Nueva Dirección 789",
  "cuotaAlDia": false,
  "observaciones": "Cuota adeudada mes de enero"
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": 2,
    "numeroSocio": "002",
    "nombre": "María",
    "apellido": "González",
    "email": "maria.gonzalez@example.com",
    "cuotaAlDia": false
  }
}
```

**Permiso requerido:** `SOCIOS_EDITAR`

---

### DELETE /socios/{id}

Eliminar socio (lógico).

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Socio eliminado exitosamente"
}
```

**Permiso requerido:** `SOCIOS_ELIMINAR`

---

### POST /socios/{id}/restore

Recuperar socio eliminado.

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Socio recuperado exitosamente"
}
```

**Permiso requerido:** `SOCIOS_RECUPERAR`

---

## Módulo Turnos

Base: `/api/turnos`  
**Requiere autenticación**

### GET /turnos

Listar turnos.

**Query Params:**

- `fecha` (date, optional): Filtrar por fecha (formato: YYYY-MM-DD)
- `socioId` (int, optional): Filtrar por socio
- `estado` (string, optional): Filtrar por estado (Reservado, Confirmado, Cancelado, Completado)
- `page` (int, default: 1)
- `pageSize` (int, default: 10)

**Request:**

```
GET /api/turnos?fecha=2025-01-05&estado=Reservado
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "socio": {
          "id": 1,
          "nombreCompleto": "Juan Pérez",
          "numeroSocio": "001"
        },
        "entrenador": {
          "id": 1,
          "nombreCompleto": "Carlos López"
        },
        "fechaTurno": "2025-01-05",
        "horaInicio": "10:00:00",
        "horaFin": "11:00:00",
        "estado": "Reservado",
        "fechaReserva": "2025-01-01T14:30:00"
      }
    ],
    "totalItems": 1,
    "currentPage": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

**Permiso requerido:** `TURNOS_VER`

---

### GET /turnos/disponibles

Consultar turnos disponibles (slots libres).

**Query Params:**

- `fecha` (date, required): Fecha a consultar
- `entrenadorId` (int, optional): Filtrar por entrenador

**Request:**

```
GET /api/turnos/disponibles?fecha=2025-01-05
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "horaInicio": "10:00:00",
      "horaFin": "11:00:00",
      "cuposDisponibles": 5,
      "entrenador": {
        "id": 1,
        "nombreCompleto": "Carlos López"
      }
    },
    {
      "horaInicio": "11:00:00",
      "horaFin": "12:00:00",
      "cuposDisponibles": 3,
      "entrenador": null
    }
  ]
}
```

**Permiso requerido:** `TURNOS_VER`

---

### POST /turnos

Reservar turno.

**Request:**

```json
{
  "socioId": 1,
  "entrenadorId": 1,
  "fechaTurno": "2025-01-05",
  "horaInicio": "10:00:00",
  "horaFin": "11:00:00",
  "observaciones": null
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 1,
    "socio": {
      "id": 1,
      "nombreCompleto": "Juan Pérez"
    },
    "fechaTurno": "2025-01-05",
    "horaInicio": "10:00:00",
    "horaFin": "11:00:00",
    "estado": "Reservado"
  },
  "message": "Turno reservado exitosamente. Se envió un correo de confirmación."
}
```

**Response (400 Bad Request):**

```json
{
  "success": false,
  "error": {
    "code": "NO_CUPOS_DISPONIBLES",
    "message": "No hay cupos disponibles para el horario seleccionado"
  }
}
```

**Permiso requerido:** `TURNOS_CREAR`

**Validaciones:**

- Socio activo y con cuota al día
- Horario dentro de los horarios de atención del gimnasio
- Cupos disponibles
- No tener otro turno en el mismo horario

**Notificación:**

- Email de confirmación al socio

---

### PUT /turnos/{id}/cancelar

Cancelar turno.

**Request:**

```json
{
  "motivo": "Enfermedad"
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Turno cancelado exitosamente"
}
```

**Permiso requerido:** `TURNOS_CANCELAR`

**Notificación:**

- Email de cancelación al socio

---

## Módulo Rutinas

Base: `/api/rutinas`  
**Requiere autenticación**

### GET /rutinas

Listar rutinas.

**Query Params:**

- `socioId` (int, optional): Filtrar por socio
- `activa` (bool, optional): Filtrar por activas/inactivas
- `page` (int, default: 1)
- `pageSize` (int, default: 10)

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "nombre": "Rutina Fuerza - Nivel 1",
        "socio": {
          "id": 1,
          "nombreCompleto": "Juan Pérez"
        },
        "entrenador": {
          "id": 1,
          "nombreCompleto": "Carlos López"
        },
        "objetivo": "Ganancia de fuerza",
        "fechaInicio": "2025-01-01",
        "fechaFin": null,
        "activa": true
      }
    ],
    "totalItems": 1,
    "currentPage": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

**Permiso requerido:** `RUTINAS_VER`

---

### GET /rutinas/{id}

Obtener detalle de rutina con ejercicios.

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": 1,
    "nombre": "Rutina Fuerza - Nivel 1",
    "descripcion": "Rutina enfocada en ganancia de fuerza muscular",
    "objetivo": "Ganancia de fuerza",
    "socio": {
      "id": 1,
      "nombreCompleto": "Juan Pérez"
    },
    "entrenador": {
      "id": 1,
      "nombreCompleto": "Carlos López"
    },
    "fechaInicio": "2025-01-01",
    "fechaFin": null,
    "activa": true,
    "ejercicios": [
      {
        "id": 1,
        "orden": 1,
        "ejercicio": {
          "id": 10,
          "nombre": "Press de banca",
          "grupoMuscular": "Pecho"
        },
        "series": 4,
        "repeticiones": "8-10",
        "peso": 60.0,
        "tiempoDescanso": 90,
        "observaciones": "Controlar técnica"
      },
      {
        "id": 2,
        "orden": 2,
        "ejercicio": {
          "id": 15,
          "nombre": "Sentadillas",
          "grupoMuscular": "Piernas"
        },
        "series": 4,
        "repeticiones": "10-12",
        "peso": 80.0,
        "tiempoDescanso": 120,
        "observaciones": null
      }
    ]
  }
}
```

**Permiso requerido:** `RUTINAS_VER`

---

### POST /rutinas

Crear rutina.

**Request:**

```json
{
  "socioId": 1,
  "nombre": "Rutina Hipertrofia",
  "descripcion": "Rutina para aumento de masa muscular",
  "objetivo": "Hipertrofia",
  "fechaInicio": "2025-01-10",
  "fechaFin": null,
  "ejercicios": [
    {
      "ejercicioId": 10,
      "orden": 1,
      "series": 4,
      "repeticiones": "8-12",
      "peso": 50.0,
      "tiempoDescanso": 60,
      "observaciones": null
    },
    {
      "ejercicioId": 15,
      "orden": 2,
      "series": 3,
      "repeticiones": "12-15",
      "peso": 70.0,
      "tiempoDescanso": 90,
      "observaciones": "Enfoque en técnica"
    }
  ]
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 2,
    "nombre": "Rutina Hipertrofia",
    "socio": {
      "id": 1,
      "nombreCompleto": "Juan Pérez"
    },
    "fechaInicio": "2025-01-10",
    "activa": true
  }
}
```

**Permiso requerido:** `RUTINAS_ASIGNAR`

---

### PUT /rutinas/{id}

Actualizar rutina.

**Request:** Similar a POST con campos actualizados.

**Response (200 OK):**

```json
{
  "success": true,
  "data": { ... }
}
```

**Permiso requerido:** `RUTINAS_EDITAR`

---

### DELETE /rutinas/{id}

Eliminar rutina.

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Rutina eliminada exitosamente"
}
```

**Permiso requerido:** `RUTINAS_ELIMINAR`

---

## Módulo Gimnasio

Base: `/api/gimnasio`  
**Requiere autenticación**

### Maquinas

#### GET /gimnasio/maquinas

Listar máquinas.

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "nombre": "Press de piernas",
      "marca": "Technogym",
      "estado": "Operativa",
      "activo": true
    }
  ]
}
```

**Permiso requerido:** `GIMNASIO_MAQUINAS_GESTIONAR` (lectura implícita)

---

#### POST /gimnasio/maquinas

Crear máquina.

**Request:**

```json
{
  "nombre": "Cinta de correr",
  "descripcion": "Cinta profesional",
  "marca": "Life Fitness",
  "modelo": "T3",
  "numeroSerie": "LF-T3-2024-001",
  "fechaCompra": "2024-12-01",
  "estado": "Operativa"
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 2,
    "nombre": "Cinta de correr",
    "estado": "Operativa"
  }
}
```

**Permiso requerido:** `GIMNASIO_MAQUINAS_GESTIONAR`

---

### Ejercicios

#### GET /gimnasio/ejercicios

Listar ejercicios.

**Query Params:**

- `grupoMuscular` (string, optional)
- `tipoEjercicio` (string, optional)

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": 10,
      "nombre": "Press de banca",
      "grupoMuscular": "Pecho",
      "tipoEjercicio": "Fuerza",
      "maquina": {
        "id": 3,
        "nombre": "Banco plano"
      }
    }
  ]
}
```

**Permiso requerido:** `GIMNASIO_EJERCICIOS_GESTIONAR` (lectura implícita)

---

#### POST /gimnasio/ejercicios

Crear ejercicio.

**Request:**

```json
{
  "nombre": "Dominadas",
  "descripcion": "Ejercicio de espalda con peso corporal",
  "grupoMuscular": "Espalda",
  "tipoEjercicio": "Fuerza",
  "maquinaId": null
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 20,
    "nombre": "Dominadas",
    "grupoMuscular": "Espalda"
  }
}
```

**Permiso requerido:** `GIMNASIO_EJERCICIOS_GESTIONAR`

---

### Horarios

#### GET /gimnasio/horarios

Obtener horarios de atención.

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "diaSemana": 1,
      "diaSemanaTexto": "Lunes",
      "horaApertura": "08:00:00",
      "horaCierre": "22:00:00",
      "activo": true
    },
    {
      "id": 2,
      "diaSemana": 2,
      "diaSemanaTexto": "Martes",
      "horaApertura": "08:00:00",
      "horaCierre": "22:00:00",
      "activo": true
    }
  ]
}
```

**Permiso requerido:** `GIMNASIO_HORARIOS_GESTIONAR` (lectura implícita)

---

#### PUT /gimnasio/horarios/{id}

Actualizar horario.

**Request:**

```json
{
  "horaApertura": "07:00:00",
  "horaCierre": "23:00:00",
  "activo": true
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": { ... }
}
```

**Permiso requerido:** `GIMNASIO_HORARIOS_GESTIONAR`

---

### Entrenadores

#### GET /gimnasio/entrenadores

Listar entrenadores.

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "nombreCompleto": "Carlos López",
      "email": "carlos@gym.com",
      "especialidad": "Fuerza y acondicionamiento",
      "activo": true
    }
  ]
}
```

**Permiso requerido:** `GIMNASIO_ENTRENADORES_GESTIONAR` (lectura implícita)

---

#### POST /gimnasio/entrenadores

Crear entrenador.

**Request:**

```json
{
  "nombre": "Laura",
  "apellido": "Martínez",
  "email": "laura@gym.com",
  "telefono": "+54 341 9876543",
  "especialidad": "Fitness y entrenamiento funcional",
  "certificaciones": "NSCA-CPT, CrossFit Level 1",
  "fechaAlta": "2025-01-01"
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 2,
    "nombreCompleto": "Laura Martínez",
    "especialidad": "Fitness y entrenamiento funcional"
  }
}
```

**Permiso requerido:** `GIMNASIO_ENTRENADORES_GESTIONAR`

---

### Configuraciones

#### GET /gimnasio/configuraciones

Obtener configuraciones del gimnasio.

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "clave": "CuposMaximosPorTurno",
      "valor": "10",
      "tipo": "Number",
      "descripcion": "Cantidad máxima de cupos por turno"
    },
    {
      "clave": "DuracionTurnoMinutos",
      "valor": "60",
      "tipo": "Number",
      "descripcion": "Duración de cada turno en minutos"
    }
  ]
}
```

**Permiso requerido:** `GIMNASIO_CONFIGURACION_GESTIONAR` (lectura implícita)

---

#### PUT /gimnasio/configuraciones/{clave}

Actualizar configuración.

**Request:**

```json
{
  "valor": "15"
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "clave": "CuposMaximosPorTurno",
    "valor": "15"
  }
}
```

**Permiso requerido:** `GIMNASIO_CONFIGURACION_GESTIONAR`

---

## Módulo Seguridad

Base: `/api/seguridad`  
**Requiere autenticación**

### Usuarios

#### GET /seguridad/usuarios

Listar usuarios del gimnasio.

**Query Params:**

- `page`, `pageSize`, `search`, `activo`

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": 1,
        "username": "admin@gym",
        "email": "admin@example.com",
        "nombreCompleto": "Juan Pérez",
        "activo": true,
        "grupos": [
          {
            "id": 1,
            "nombre": "ADMIN_GYM"
          }
        ],
        "fechaAlta": "2024-01-01"
      }
    ],
    "totalItems": 1,
    "currentPage": 1,
    "pageSize": 10,
    "totalPages": 1
  }
}
```

**Permiso requerido:** `SEGURIDAD_USUARIOS_GESTIONAR`

---

#### POST /seguridad/usuarios

Crear usuario.

**Request:**

```json
{
  "username": "entrenador1",
  "email": "entrenador1@example.com",
  "password": "Password123!",
  "nombre": "Carlos",
  "apellido": "López",
  "telefono": "+54 341 1112233",
  "gruposIds": [3]
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 5,
    "username": "entrenador1",
    "email": "entrenador1@example.com",
    "nombreCompleto": "Carlos López",
    "activo": true
  }
}
```

**Permiso requerido:** `SEGURIDAD_USUARIOS_GESTIONAR`

---

#### PUT /seguridad/usuarios/{id}

Actualizar usuario.

**Request:**

```json
{
  "email": "carlos.lopez@example.com",
  "telefono": "+54 341 1112233",
  "gruposIds": [3, 4]
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": { ... }
}
```

**Permiso requerido:** `SEGURIDAD_USUARIOS_GESTIONAR`

---

#### DELETE /seguridad/usuarios/{id}

Eliminar usuario (desactivar).

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Usuario eliminado exitosamente"
}
```

**Permiso requerido:** `SEGURIDAD_USUARIOS_GESTIONAR`

---

### Grupos

#### GET /seguridad/grupos

Listar grupos del gimnasio.

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "nombre": "ADMIN_GYM",
      "descripcion": "Administrador del gimnasio",
      "esSistema": true,
      "cantidadUsuarios": 2,
      "cantidadPermisos": 15
    }
  ]
}
```

**Permiso requerido:** `SEGURIDAD_GRUPOS_GESTIONAR`

---

#### GET /seguridad/grupos/{id}

Obtener detalle de grupo con permisos.

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "id": 1,
    "nombre": "ADMIN_GYM",
    "descripcion": "Administrador del gimnasio",
    "esSistema": true,
    "activo": true,
    "permisos": [
      {
        "id": 1,
        "codigo": "SOCIOS_VER",
        "nombre": "Ver socios",
        "modulo": "Socios"
      },
      {
        "id": 2,
        "codigo": "SOCIOS_CREAR",
        "nombre": "Crear socios",
        "modulo": "Socios"
      }
    ]
  }
}
```

**Permiso requerido:** `SEGURIDAD_GRUPOS_GESTIONAR`

---

#### POST /seguridad/grupos

Crear grupo.

**Request:**

```json
{
  "nombre": "RECEPCION",
  "descripcion": "Personal de recepción",
  "permisosIds": [1, 2, 10, 11]
}
```

**Response (201 Created):**

```json
{
  "success": true,
  "data": {
    "id": 6,
    "nombre": "RECEPCION",
    "descripcion": "Personal de recepción"
  }
}
```

**Permiso requerido:** `SEGURIDAD_GRUPOS_GESTIONAR`

---

#### PUT /seguridad/grupos/{id}

Actualizar grupo.

**Request:**

```json
{
  "descripcion": "Personal de recepción y atención al cliente",
  "permisosIds": [1, 2, 3, 10, 11]
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": { ... }
}
```

**Permiso requerido:** `SEGURIDAD_GRUPOS_GESTIONAR`

---

#### DELETE /seguridad/grupos/{id}

Eliminar grupo.

**Response (200 OK):**

```json
{
  "success": true,
  "message": "Grupo eliminado exitosamente"
}
```

**Response (400 Bad Request):**

```json
{
  "success": false,
  "error": {
    "code": "GRUPO_EN_USO",
    "message": "El grupo no puede eliminarse porque tiene usuarios asignados"
  }
}
```

**Permiso requerido:** `SEGURIDAD_GRUPOS_GESTIONAR`

**Restricción:** No se pueden eliminar grupos del sistema (`EsSistema = true`)

---

### Permisos

#### GET /seguridad/permisos

Listar todos los permisos del sistema (catálogo).

**Response (200 OK):**

```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "codigo": "SOCIOS_VER",
      "nombre": "Ver socios",
      "modulo": "Socios",
      "descripcion": "Permite visualizar el listado y detalle de socios"
    },
    {
      "id": 2,
      "codigo": "SOCIOS_CREAR",
      "nombre": "Crear socios",
      "modulo": "Socios",
      "descripcion": "Permite dar de alta nuevos socios"
    }
  ]
}
```

**Permiso requerido:** `SEGURIDAD_PERMISOS_VER`

---

## Módulo IA

Base: `/api/ia`  
**Requiere autenticación**

### POST /ia/chat

Enviar mensaje al asistente IA (mock inicial).

**Request:**

```json
{
  "socioId": 1,
  "mensaje": "¿Qué ejercicios me recomiendas para ganar masa muscular en pecho?"
}
```

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "respuesta": "Para ganar masa muscular en pecho, te recomiendo los siguientes ejercicios: Press de banca (4 series de 8-10 repeticiones), Press inclinado con mancuernas (3 series de 10-12 repeticiones), y Fondos en paralelas (3 series hasta el fallo). Recuerda mantener una técnica adecuada y descansar 60-90 segundos entre series.",
    "timestamp": "2025-01-01T10:30:00"
  }
}
```

**Permiso requerido:** `IA_USAR`

**Notas:**

- Implementación inicial: respuestas predefinidas o mock
- Futura integración: API de GPT u otro LLM

---

### GET /ia/recomendaciones/{socioId}

Obtener recomendaciones personalizadas para un socio.

**Response (200 OK):**

```json
{
  "success": true,
  "data": {
    "socioId": 1,
    "recomendaciones": [
      "Considera agregar ejercicios de pierna a tu rutina actual",
      "Tu última visita fue hace 5 días, te recomendamos mantener consistencia",
      "Basado en tus objetivos, podrías aumentar la intensidad de tu cardio"
    ],
    "generadoEn": "2025-01-01T10:30:00"
  }
}
```

**Permiso requerido:** `IA_USAR`

---

## Códigos de respuesta

### Códigos HTTP estándar

| Código                    | Descripción                                  |
| ------------------------- | -------------------------------------------- |
| 200 OK                    | Operación exitosa                            |
| 201 Created               | Recurso creado exitosamente                  |
| 204 No Content            | Operación exitosa sin contenido de respuesta |
| 400 Bad Request           | Errores de validación o datos inválidos      |
| 401 Unauthorized          | No autenticado o token inválido              |
| 403 Forbidden             | Autenticado pero sin permisos                |
| 404 Not Found             | Recurso no encontrado                        |
| 409 Conflict              | Conflicto (ej: registro duplicado)           |
| 500 Internal Server Error | Error interno del servidor                   |

### Códigos de error de negocio

| Código                   | Descripción                                        |
| ------------------------ | -------------------------------------------------- |
| INVALID_CREDENTIALS      | Credenciales inválidas en login                    |
| INVALID_REFRESH_TOKEN    | Refresh Token inválido o expirado                  |
| INVALID_OR_EXPIRED_TOKEN | Token de reseteo inválido o expirado               |
| INVALID_CURRENT_PASSWORD | Contraseña actual incorrecta en cambio             |
| SOCIO_NOT_FOUND          | Socio no encontrado                                |
| SOCIO_NO_ACTIVO          | Socio no activo                                    |
| SOCIO_CUOTA_VENCIDA      | Socio con cuota vencida (no puede reservar turnos) |
| NO_CUPOS_DISPONIBLES     | Sin cupos para el turno seleccionado               |
| TURNO_NO_CANCELABLE      | Turno fuera del plazo de cancelación               |
| GRUPO_EN_USO             | Grupo con usuarios asignados (no puede eliminarse) |
| VALIDATION_ERROR         | Errores de validación (con detalles)               |
| INSUFFICIENT_PERMISSIONS | Permisos insuficientes                             |
| DUPLICATE_ENTRY          | Entrada duplicada (email, username, etc.)          |

---

## Resumen de endpoints

| Módulo        | Cantidad de endpoints                                                             |
| ------------- | --------------------------------------------------------------------------------- |
| Autenticación | 6                                                                                 |
| Sitio Público | 3                                                                                 |
| Socios        | 6                                                                                 |
| Turnos        | 4                                                                                 |
| Rutinas       | 5                                                                                 |
| Gimnasio      | 15+ (Máquinas, Ejercicios, Equipamiento, Horarios, Entrenadores, Configuraciones) |
| Seguridad     | 10+ (Usuarios, Grupos, Permisos)                                                  |
| IA            | 2                                                                                 |
| **Total**     | **51+ endpoints**                                                                 |

---

**Última actualización:** 01/01/2026
