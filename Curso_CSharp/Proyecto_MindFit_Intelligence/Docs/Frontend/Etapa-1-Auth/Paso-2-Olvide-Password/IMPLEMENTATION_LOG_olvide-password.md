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

## Decisiones importantes

- Se extrajo la carga de gimnasios activos a `useActiveGyms` para reutilizar la misma logica en login, forgot password y reset password.
- Se mantuvo `idGym` fuera de los bodies y se envio siempre por header `X-Gym-Id` en `forgot-password` y `reset-password`, alineado con `TenantMiddleware`.
- Se reutilizo `GymSelect` para respetar el comportamiento searchable ya usado en login.
- Se agrego `AuthShowcase` para conservar una UI consistente entre las pantallas publicas de autenticacion sin duplicar la seccion visual.

## Integracion frontend/backend

- `authService.forgotPassword` envia `POST /Auth/forgot-password` con body `{ email }` y header `X-Gym-Id`.
- `authService.resetPassword` envia `POST /Auth/reset-password` con body `{ tokenPlano, newPassword }` y header `X-Gym-Id`.
- `ResetPasswordPage` toma el token desde la query string `?token=...`, que coincide con el link generado en `AuthService.cs`.
- Se agrego la ruta publica `/reset-password` en el router.

## Validaciones implementadas

- Forgot password:
  - gimnasio obligatorio
  - email obligatorio
  - formato de email valido
- Reset password:
  - gimnasio obligatorio
  - nueva contrasena obligatoria
  - token requerido desde query string

## Estados y UX

- Todas las pantallas manejan loading al enviar el formulario.
- `useActiveGyms` expone loading y error de carga de gimnasios.
- Forgot password muestra siempre un mensaje generico de exito para no revelar si el email existe.
- Reset password muestra el mensaje real del backend cuando el token es invalido o expiro.
- Se agrego navegacion de vuelta al login desde forgot password y reset password.

## Configuracion y headers

- No fue necesario cambiar interceptores globales.
- Para estos endpoints publicos se envia `X-Gym-Id` explicitamente desde `authService`, porque todavia no existe una sesion persistida al iniciar el flujo.

## Verificacion

- Se ejecuto `npm.cmd run build` en `Frontend` con resultado exitoso.

## TODOs o limitaciones detectadas

- Los textos de UI se dejaron en ASCII para mantener consistencia con la restriccion de edicion, aunque el proyecto ya tenia algunos archivos con acentos mal codificados.
- No se agrego politica de complejidad de contrasena en frontend porque el plan solo exige campo obligatorio y el backend es la fuente de verdad para reglas adicionales.
