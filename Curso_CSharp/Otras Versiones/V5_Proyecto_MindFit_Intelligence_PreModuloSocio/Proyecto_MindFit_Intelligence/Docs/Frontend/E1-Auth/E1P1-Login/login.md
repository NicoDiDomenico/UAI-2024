# Etapa 1 - Auth - Paso 1 - Login Form

## Objetivo

Implementar la primera pantalla de autenticación del sistema MindFit Intelligence utilizando React + TypeScript + Vite.

El frontend debe consumir la API .NET existente sin modificar backend.

---

# Base URL

```txt
https://localhost:7199
```

---

# Alcance de este paso

Este paso incluye únicamente:

- pantalla de login
- carga de gimnasios activos
- selección de gimnasio
- autenticación de usuario
- persistencia local de sesión
- rutas protegidas básicas
- estructura inicial de autenticación frontend

Este paso NO incluye:

- refresh automático de token
- recuperación real de contraseña
- autorización visual avanzada por permisos
- dashboard completo
- menú dinámico
- módulos funcionales posteriores

---

# Componentes UI requeridos

## 1. SelectBox de gimnasios

Características:

- searchable
- búsqueda interna por nombre
- muestra `nombreGym`
- almacena internamente `idGym`
- obligatorio para login

Estados:

- loading mientras carga
- error si falla endpoint
- disabled mientras no existan gimnasios

---

## 2. Campo Username

- text input
- obligatorio

---

## 3. Campo Password

- password input
- obligatorio

---

## 4. Botón "Ingresar"

Comportamiento:

- ejecuta login
- disabled durante submit
- muestra loading durante request

---

## 5. Link "Olvidé mi contraseña"

Por el momento:

- abrir una nueva pestaña/página en blanco
- mostrar texto:

```txt
Próximamente...
```

La funcionalidad real será implementada en el Paso 2 de la Etapa 1.

---

# Endpoints Backend

## A) Obtener gimnasios activos

### Endpoint

```txt
GET api/Gyms/activos
```

### Authorization

```txt
[AllowAnonymous]
```

### Response

Tipo:

```txt
IEnumerable<GymPublicoDTO>
```

Ejemplo JSON:

```json
[
  {
    "idGym": 0,
    "nombreGym": "string"
  }
]
```

### Uso Frontend

- cargar gimnasios al iniciar pantalla login
- poblar SelectBox
- guardar `idGym` seleccionado
- usar `idGym` para header de requests públicos y autenticados posteriores

---

## B) Login usuario

### Endpoint

```txt
POST api/Auth/login
```

### Authorization

```txt
[AllowAnonymous]
```

### Header requerido

El login debe enviar el gimnasio seleccionado en el header:

```txt
X-Gym-Id: {idGym}
```

El `idGym` NO va en el body del login.

### Request DTO

Tipo:

```txt
LoginUsuarioDto
```

Ejemplo JSON:

```json
{
  "username": "string",
  "password": "string"
}
```

### Response DTO

Tipo:

```txt
TokenResponseDto
```

Ejemplo JSON:

```json
{
  "accessToken": "string",
  "refreshToken": "string",
  "permisos": ["string"]
}
```

### Posibles respuestas

- `200 OK`: login exitoso
- `401 Unauthorized`: credenciales inválidas
- `500 Internal Server Error`: error del servidor
- `4xx`: errores de validación

---

# Header X-Gym-Id

El header requerido para requests públicos y autenticados posteriores es:

```txt
X-Gym-Id
```

Valor:

```txt
idGym seleccionado por el usuario
```

Implementación recomendada:

- configurar Axios centralizado
- usar interceptor o configuración común para agregar automáticamente `X-Gym-Id`
- tomar el valor desde `localStorage` cuando corresponda

---

# Persistencia Local

Guardar en `localStorage`:

- `idGym`
- `accessToken`
- `refreshToken`
- `permisos`

Los permisos deben guardarse exactamente como llegan desde backend para comparación exacta posterior.

---

# Flujo esperado

## Paso 1

Usuario ingresa a login.

## Paso 2

Frontend obtiene gimnasios activos.

## Paso 3

Usuario selecciona gimnasio.

## Paso 4

Usuario ingresa:

- username
- password

## Paso 5

Frontend ejecuta login enviando:

- body: `username`, `password`
- header: `X-Gym-Id`

## Paso 6

Si login es exitoso:

Guardar en `localStorage`:

- `idGym`
- `accessToken`
- `refreshToken`
- `permisos`

## Paso 7

Redireccionar a:

```txt
/dashboard
```

Puede ser una pantalla placeholder temporal.

---

# Manejo de errores

## Validaciones frontend

- gimnasio requerido
- username requerido
- password requerido

## Errores backend

Mostrar mensaje genérico para:

- credenciales inválidas
- error de servidor
- error de red
- errores de validación

## Error carga gimnasios

Mostrar estado de error si no pueden cargarse.

---

# Permisos

Los permisos retornados en `TokenResponseDto.permisos` son strings.

Ejemplo:

```json
["crear_usuario", "editar_usuario"]
```

En este paso:

- solamente almacenarlos
- no implementar todavía autorización visual avanzada

---

# Uso futuro de permisos

## Botones de Navegación

Endpoint relacionado:

```txt
GET /api/Formulario
```

Sistema futuro:

- traer el catálogo de formularios con sus permisos asociados
- mapear cada botón de navegación con los permisos del formulario correspondiente
- mostrar botón de navegación si existe al menos un permiso coincidente entre:
  - permisos del usuario
  - permisos asociados al formulario

Authorization:

```txt
[AllowAnonymous]
```

---

## Botones de Acción

Endpoint relacionado:

```txt
GET /api/Formulario
```

Sistema futuro:

- traer el catálogo de formularios con sus permisos asociados
- mapear cada botón de acción al `CodigoPermiso` correspondiente
- mostrar botón de acción si existe coincidencia exacta entre:
  - permiso del usuario
  - permiso requerido por la acción

Authorization:

```txt
[AllowAnonymous]
```

---

# Librería sugerida para SelectBox searchable

Para el SelectBox con búsqueda interna se recomienda usar:

```txt
react-select
```

Motivo:

- simple de implementar
- estable
- mantenible
- buen soporte con TypeScript
- evita crear desde cero un combo searchable

No agregar frameworks UI pesados para este paso.

---

# Archivos backend sugeridos como contexto para Codex

Al pedir implementación, abrir en VS Code y activar `/ide` con estos archivos:

```txt
AGENTS.md
frontend-skill.md
Docs/Frontend/Etapa-1-Auth/Pa so-1-Login/login.md
Backend/.../Controllers/AuthController.cs
Backend/.../Controllers/GymsController.cs
Backend/.../DTOs/LoginUsuarioDto.cs
Backend/.../DTOs/TokenResponseDto.cs
Backend/.../DTOs/GymPublicoDTO.cs
Backend/.../Program.cs
```

Codex debe leer estos archivos solo para confirmar contrato, rutas, headers, DTOs y configuración.
