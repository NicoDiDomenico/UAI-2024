## 1. El Rol y la Persona (System / Role Context)

Ponte en el rol de un desarrollador frontend.

---

## 2. La Tarea Principal (Core Task)

Implementar el flujo frontend de recuperación de contraseña para el módulo de autenticación.

El login ya está implementado. Ahora se deben agregar dos pantallas públicas, sin requerir usuario autenticado.

Los endpoints públicos de este flujo también dependen del gimnasio seleccionado.

El backend utiliza el header:
X-Gym-Id

para resolver dinámicamente la conexión de base de datos mediante multitenancy.

Por lo tanto:

- Forgot Password
- Reset Password

también deben permitir seleccionar gimnasio y enviar el header `X-Gym-Id`.

---

## 1. SelectBox de gimnasios

Las pantallas de recuperación de contraseña deben reutilizar el mismo SelectBox searchable implementado en login.

### Endpoint

GET api/Gyms/activos

### Response DTO

IEnumerable<GymPublicoDTO>

### Ejemplo response

```json id="v6g2u1"
[
  {
    "idGym": 0,
    "nombreGym": "string"
  }
]
```

### Requerimientos

El SelectBox debe:

- ser obligatorio
- mostrar `nombreGym`
- almacenar internamente `idGym`
- reutilizar lógica/componentes existentes del login si ya existen
- manejar loading/error states

El `idGym` seleccionado debe enviarse mediante header:
X-Gym-Id: {idGym}

El `idGym` NO debe enviarse en el body.

---

## 2. Forgot Password

### Endpoint

`POST api/Auth/forgot-password`

### DTO

`ForgotPasswordRequestDto`

### Autorización

`[AllowAnonymous]`

### Header requerido

X-Gym-Id: {idGym}

### Request body

{
"email": "string"
}

### Response

`200 OK` con `string` como mensaje de confirmación.

### Frontend requerido

Crear un formulario público “Olvidé mi contraseña”.

El usuario debe:

- seleccionar gimnasio
- ingresar su email
- enviar el formulario
- recibir un mensaje genérico de confirmación

La pantalla debe:

- llamar al endpoint `api/Auth/forgot-password`
- enviar un objeto `ForgotPasswordRequestDto`
- enviar el header `X-Gym-Id`
- no revelar si el email existe o no
- mostrar siempre un mensaje genérico de éxito

Ejemplo:
Si el email ingresado está registrado, recibirás instrucciones para recuperar tu contraseña.

### Validaciones

- gimnasio obligatorio
- email obligatorio
- formato válido de email

### UX

Manejar:

- loading
- success state
- error state

---

## 3. Reset Password

### Endpoint

`POST api/Auth/reset-password`

### DTO

`ResetPasswordRequestDto`

### Autorización

`[AllowAnonymous]`

### Header requerido

X-Gym-Id: {idGym}

### Request body

{
"tokenPlano": "string",
"newPassword": "string"
}

### Response

`200 OK` con `string` como resultado.

### Frontend requerido

Crear una pantalla pública para definir una nueva contraseña.

La pantalla debe:

- obtener el token desde el link del email
- permitir seleccionar gimnasio
- permitir ingresar una nueva contraseña
- llamar al endpoint `api/Auth/reset-password`
- enviar un objeto `ResetPasswordRequestDto`
- enviar el header `X-Gym-Id`

### Comportamiento esperado

Si el token es válido:

- mostrar mensaje de éxito
- permitir volver al login

Si el token es inválido o expiró:

- mostrar mensaje claro indicando que el enlace no es válido o expiró

### Validaciones

- gimnasio obligatorio
- nueva contraseña obligatoria

### UX

Manejar:

- loading
- success state
- error state

---

## Navegación

- Continuar desde el link del login que dice "Olvidé mi contraseña"
- Agregar navegación al login luego de reset exitoso.

---

## 3. El Contexto (Background)

- `AuthController.cs`
  (rutas reales, autorización `[AllowAnonymous]`, status codes y mensajes)

- `IAuthService.cs`
  (contratos exactos de `ForgotPasswordAsync(ForgotPasswordRequestDto)` y `ResetPasswordAsync(ResetPasswordRequestDto)`)

- `AuthService.cs`
  (comportamiento real: token por query string, expiración, resultado cuando token inválido/expirado)

- `ForgotPasswordRequestDto.cs`
  (`Email`)

- `ResetPasswordRequestDto.cs`
  (`TokenPlano`, `NewPassword`)

Importante: los DTOs están en `DTOs/Usuarios`, no en `DTOs/Auth`.

- `TenantMiddleware.cs`
  (para aclarar `X-Gym-Id` en endpoints públicos)

- `Program.cs`
  (orden de middlewares + auth + multitenancy)

- `LoginPage.tsx`
  (ahí vive directamente el link “Olvidé mi contraseña”, que es el punto de partida exacto para continuar desde el login)

---

## 4. Reglas y Restricciones (Constraints / Guardrails)

- No modificar backend.
- No cambiar endpoints.
- No renombrar DTOs.
- No alterar nombres de propiedades.
- Backend es la fuente de verdad.
- Usar TypeScript.
- Usar Axios si ya existe configuración.
- Usar variables de entorno/configuración existente.
- No hardcodear URLs absolutas.
- Mantener UI consistente con el login actual.
- Reutilizar lógica/componentes existentes del login cuando sea posible.
- El header `X-Gym-Id` es obligatorio también en endpoints públicos.
- No enviar `idGym` en request body.
- Reutilizar el SelectBox de gimnasios ya existente si fue abstraído a componente reutilizable.
- Centralizar llamadas HTTP en `src/services`.
- Evitar duplicar lógica de carga de gimnasios entre pantallas.

## 5. Formato de Salida (Output Format)

Además de implementar el código solicitado, generar o actualizar:

```txt
IMPLEMENTATION_LOG_nombrePasoActual.md
```

El archivo debe crearse en la misma carpeta/ruta donde se encuentra este plan .md.

El log debe documentar brevemente:

- qué archivos fueron creados o modificados
- qué decisiones importantes se tomaron
- cómo se resolvió la integración frontend/backend
- validaciones implementadas
- manejo de estados/loading/errors
- configuración relevante de Axios/interceptors/context/hooks si aplica
- posibles TODOs o limitaciones detectadas

El objetivo es que futuras iteraciones de IA puedan entender rápidamente qué fue implementado y por qué.
