# Sistema de Login - MindFit Intelligence v4

## 🚀 Inicio Rápido

### 1. Configurar Backend

```powershell
cd backend

# Copiar archivo de entorno
Copy-Item .env.example .env

# Editar .env con tus credenciales de PostgreSQL
# Asegúrate de tener DATABASE_URL configurado correctamente

# Instalar dependencias (si no lo hiciste antes)
npm install

# Ejecutar migraciones
npm run prisma:migrate

# Ejecutar seed (crea usuario admin/admin123 en "Gym Olimpo")
npm run prisma:seed

# Iniciar servidor backend
npm run dev
```

El backend correrá en `http://localhost:3000`

### 2. Configurar Frontend

```powershell
cd frontend

# Copiar archivo de entorno
Copy-Item .env.example .env

# El archivo .env debe tener:
# VITE_API_URL=http://localhost:3000/api

# Instalar dependencias (si no lo hiciste antes)
npm install

# Iniciar aplicación frontend
npm run dev
```

El frontend correrá en `http://localhost:5173`

---

## 🔐 Credenciales de Prueba

- **Gimnasio:** Gym Olimpo
- **Usuario:** `admin`
- **Contraseña:** `admin123`

---

## 📁 Estructura del Proyecto

### Backend

```
backend/
├── src/
│   ├── config/
│   │   └── env.ts              # Variables de entorno
│   ├── controllers/
│   │   ├── auth.controller.ts  # Login y obtener usuario actual
│   │   └── gym.controller.ts   # Lista de gimnasios
│   ├── middleware/
│   │   ├── auth.middleware.ts  # Validación de JWT
│   │   └── error.middleware.ts # Manejo de errores
│   ├── routes/
│   │   ├── auth.routes.ts      # Rutas de autenticación
│   │   └── gym.routes.ts       # Rutas de gimnasios
│   ├── services/
│   │   ├── auth.service.ts     # Lógica de autenticación
│   │   └── gym.service.ts      # Lógica de gimnasios
│   ├── types/
│   │   └── jwt.types.ts        # Tipos TypeScript
│   ├── utils/
│   │   ├── jwt.util.ts         # Generación y verificación JWT
│   │   └── password.util.ts    # Bcrypt hash/compare
│   ├── prisma.ts               # Cliente Prisma con adapter PG
│   └── index.ts                # Servidor Express
```

### Frontend

```
frontend/
├── src/
│   ├── components/
│   │   ├── auth/
│   │   │   └── ProtectedRoute.tsx  # HOC para rutas protegidas
│   │   └── ui/
│   │       ├── Button.tsx          # Componente botón
│   │       ├── Input.tsx           # Componente input
│   │       └── Select.tsx          # Componente select
│   ├── context/
│   │   └── AuthContext.tsx         # Context global de autenticación
│   ├── pages/
│   │   ├── Login.tsx               # Página de login
│   │   └── Home.tsx                # Página principal
│   ├── services/
│   │   └── api.service.ts          # Servicios API (axios)
│   ├── types/
│   │   └── auth.types.ts           # Tipos TypeScript
│   ├── App.tsx                     # Router principal
│   └── App.css                     # Estilos
```

---

## 🔧 Endpoints API

### GET /api/gyms

Obtiene lista de gimnasios disponibles.

**Response:**

```json
{
  "gyms": [{ "gymId": 1, "nombre": "Gym Olimpo" }]
}
```

### POST /api/auth/login

Autentica un usuario.

**Request:**

```json
{
  "gymId": 1,
  "nombreUsuario": "admin",
  "password": "admin123"
}
```

**Response:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "usuario": {
    "usuarioId": 1,
    "nombreUsuario": "admin",
    "gym": { "gymId": 1, "nombre": "Gym Olimpo" },
    "persona": {
      "personaId": 1,
      "nombreYApellido": "Admin Olimpo",
      "email": "admin@olimpo.com"
    },
    "roles": ["ADMIN"],
    "permisos": ["USER_CREATE", "USER_UPDATE", ...]
  }
}
```

### GET /api/auth/me

Obtiene datos del usuario actual (requiere token).

**Headers:**

```
Authorization: Bearer <token>
```

**Response:**

```json
{
  "usuario": { ... }
}
```

---

## 🔑 JWT Payload

El token JWT contiene **solo** estos campos:

```json
{
  "usuarioId": 1,
  "gymId": 1,
  "iat": 1234567890,
  "exp": 1234567890
}
```

Los roles y permisos se calculan **dinámicamente** desde la base de datos en cada request a `/api/auth/me`.

---

## 🛡️ Seguridad

1. **Contraseñas hasheadas** con bcrypt (salt rounds = 10)
2. **JWT firmado** con secret (configurado en `.env`)
3. **Multi-tenant estricto**: Todas las consultas filtran por `gymId`
4. **CORS configurado** para solo permitir el frontend
5. **Validación de tokens** en middleware

---

## 🧪 Flujo de Prueba

1. Abrir `http://localhost:5173`
2. Seleccionar "Gym Olimpo"
3. Ingresar usuario: `admin`
4. Ingresar contraseña: `admin123`
5. Click en "INICIAR SESIÓN"
6. Serás redirigido a `/home`
7. Verás tu información de usuario
8. Click en "Cerrar Sesión" para salir

---

## ⚠️ Notas Importantes

### Prisma v7

El proyecto usa **Prisma v7** con el adapter de PostgreSQL. **NO usar** `new PrismaClient()` directo. Siempre importar desde `src/prisma.ts`:

```typescript
import { prisma } from "./prisma.js";
```

### Multi-tenant

**SIEMPRE** filtrar por `gymId`. Ejemplo:

```typescript
// ✅ CORRECTO
const usuario = await prisma.usuario.findUnique({
  where: {
    gymId_nombreUsuario: { gymId, nombreUsuario },
  },
});

// ❌ INCORRECTO (no filtra por gym)
const usuario = await prisma.usuario.findUnique({
  where: { nombreUsuario },
});
```

### JWT Mínimo

El token **NO contiene** roles ni permisos. Solo `usuarioId` y `gymId`. Esto permite:

- Revocar permisos sin invalidar tokens
- Actualizar roles sin relogin
- Tokens más pequeños

---

## 🐛 Troubleshooting

### Backend no inicia

- Verifica que PostgreSQL esté corriendo
- Verifica `DATABASE_URL` en `.env`
- Ejecuta `npm run prisma:migrate`

### Frontend no conecta con backend

- Verifica que el backend esté corriendo en puerto 3000
- Verifica `VITE_API_URL` en `.env` del frontend
- Abre DevTools > Network para ver errores

### Login falla

- Verifica que ejecutaste `npm run prisma:seed`
- Verifica que seleccionaste el gimnasio correcto
- Credenciales: `admin` / `admin123` (case-sensitive)

### Token expirado

- El token expira en 8 horas (configurable en backend `.env`)
- Cierra sesión y vuelve a iniciar

---

## 📚 Próximos Pasos

Una vez que el login funcione correctamente, puedes:

1. Implementar más páginas protegidas
2. Agregar más roles y permisos
3. Crear sistema de recuperación de contraseña
4. Agregar refresh tokens
5. Implementar rate limiting

---

## 🎯 Variables de Entorno

### Backend `.env`

```env
DATABASE_URL="postgresql://user:password@localhost:5432/mindfit"
JWT_SECRET="tu-secret-super-seguro-cambiar-en-produccion"
JWT_EXPIRATION="8h"
PORT=3000
FRONTEND_URL="http://localhost:5173"
```

### Frontend `.env`

```env
VITE_API_URL="http://localhost:3000/api"
```

---

¡Sistema de login implementado! 🎉
