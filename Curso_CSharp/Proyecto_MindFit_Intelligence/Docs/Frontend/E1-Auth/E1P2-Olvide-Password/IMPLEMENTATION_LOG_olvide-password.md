# Implementation Log - Paso 2 Olvide Password

## Archivos creados o modificados

- `Frontend/src/pages/auth/ForgotPasswordPage.tsx`
- `Frontend/src/pages/auth/ResetPasswordPage.tsx`
- `Frontend/src/pages/auth/LoginPage.tsx`
- `Frontend/src/routes/AppRouter.tsx`
- `Frontend/src/services/authService.ts`
- `Frontend/src/types/auth.ts`
- `Frontend/src/hooks/useActiveGyms.ts`
- `Frontend/src/components/auth/GymSelect.tsx`
- `Frontend/src/components/auth/AuthShowcase.tsx`
- `Frontend/src/utils/apiError.ts`
- `Frontend/src/App.css`

## Ajuste posterior aplicado

- `ResetPasswordPage` ya no solicita seleccion manual de gimnasio.
- Se agrego lectura de `gymId` desde la query string del enlace de recuperacion junto con `token`.
- Se valido `gymId` como entero positivo antes de habilitar el submit.
- Se agrego un segundo campo para repetir la nueva contrasena.
- Se valido en frontend que ambas contrasenas coincidan antes de enviar el formulario.
- Tras un reset exitoso, la pantalla ahora redirige automaticamente a `/login`.
- Se actualizaron los mensajes de error para distinguir:
  - token ausente
  - `gymId` ausente o invalido
- Se mantuvo `authService.resetPassword` enviando:
  - body `{ tokenPlano, newPassword }`
  - header `X-Gym-Id` con el `gymId` recibido desde la URL

## Decisiones importantes

- Se extrajo la carga de gimnasios activos a `useActiveGyms` para reutilizar la misma logica en login, forgot password y reset password.
- Se mantuvo `idGym` fuera de los bodies y se envio siempre por header `X-Gym-Id` en `forgot-password` y `reset-password`, alineado con `TenantMiddleware`.
- Se reutilizo `GymSelect` para respetar el comportamiento searchable ya usado en login.
- Se agrego `AuthShowcase` para conservar una UI consistente entre las pantallas publicas de autenticacion sin duplicar la seccion visual.
- Tras el ajuste del backend en `AuthService.cs`, `ResetPasswordPage` dejo de depender de `GymSelect` y de `useActiveGyms`, porque la sede ahora viaja resuelta en el link del email.

## Integracion frontend/backend

- `authService.forgotPassword` envia `POST /Auth/forgot-password` con body `{ email }` y header `X-Gym-Id`.
- `authService.resetPassword` envia `POST /Auth/reset-password` con body `{ tokenPlano, newPassword }` y header `X-Gym-Id`.
- `ResetPasswordPage` toma `token` y `gymId` desde la query string `?token=...&gymId=...`, que coincide con el link generado en `AuthService.cs`.
- Se agrego la ruta publica `/reset-password` en el router.

## Validaciones implementadas

- Forgot password:
  - gimnasio obligatorio
  - email obligatorio
  - formato de email valido
- Reset password:
  - nueva contrasena obligatoria
  - repetir nueva contrasena obligatoria
  - ambas contrasenas deben coincidir
  - token requerido desde query string
  - `gymId` requerido desde query string
  - `gymId` debe ser numerico y mayor a cero

## Estados y UX

- Todas las pantallas manejan loading al enviar el formulario.
- `useActiveGyms` expone loading y error de carga de gimnasios.
- Forgot password muestra siempre un mensaje generico de exito para no revelar si el email existe.
- Reset password muestra el mensaje real del backend cuando el token es invalido o expiro.
- Se agrego navegacion de vuelta al login desde forgot password y reset password.
- Reset password deshabilita el campo y el submit cuando el enlace no trae un `token` o `gymId` validos.
- Reset password muestra confirmacion de exito y luego navega automaticamente a login con un pequeno delay.

## Configuracion y headers

- No fue necesario cambiar interceptores globales.
- Para estos endpoints publicos se envia `X-Gym-Id` explicitamente desde `authService`, porque todavia no existe una sesion persistida al iniciar el flujo.
- En forgot password el `X-Gym-Id` sigue saliendo de la seleccion del usuario.
- En reset password el `X-Gym-Id` ahora sale exclusivamente del `gymId` de la URL.

## Verificacion

- Se ejecuto `npm.cmd run build` en `Frontend` con resultado exitoso.

## TODOs o limitaciones detectadas

- Los textos de UI se dejaron en ASCII para mantener consistencia con la restriccion de edicion, aunque el proyecto ya tenia algunos archivos con acentos mal codificados.
- No se agrego politica de complejidad de contrasena en frontend porque el plan solo exige campo obligatorio y el backend es la fuente de verdad para reglas adicionales.
