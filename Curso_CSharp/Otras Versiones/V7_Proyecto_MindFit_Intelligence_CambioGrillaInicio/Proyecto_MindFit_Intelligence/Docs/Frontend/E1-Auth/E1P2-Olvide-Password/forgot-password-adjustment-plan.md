# Forgot Password Adjustment Plan

## Motivo del ajuste

El plan original requería seleccionar manualmente el gimnasio en la pantalla de Reset Password.

Luego del cambio realizado en AuthService.cs, el enlace de recuperación ahora incluye:

- token
- gymId

Por lo tanto, el gimnasio ya puede determinarse automáticamente desde la URL y no debe volver a solicitarse al usuario.

## Objetivo

Ajustar el flujo de recuperación de contraseña para que la sede se resuelva desde la URL del enlace de recuperación, sin mostrar ni pedir selección manual de gimnasio en la pantalla de reset.

## Contexto de backend

El backend ya fue ajustado en AuthService.cs para que el enlace de recuperación incluya gymId como query param:

- token: tokenPlano
- gymId: Id del gimnasio que debe viajar en la URL

El endpoint POST /api/Auth/reset-password debe seguir recibiendo:

- body: TokenPlano y NewPassword
- header: X-Gym-Id con el gymId leído desde la URL del front

## Cambios sobre la implementación actual

### 1. Leer gymId desde la URL en ResetPasswordPage

Archivo objetivo:
ResetPasswordPage.tsx

- Tomar gymId desde useSearchParams junto con token.
- Validar que gymId exista y sea numérico.
- Si gymId no existe o es inválido, mostrar un mensaje claro de error.
- Guardar gymId en estado local o derivarlo directamente desde la URL.
- No depender de selección manual del usuario para esta pantalla.

### 2. Eliminar el selector de gimnasio de la pantalla de reset

Archivo objetivo:
ResetPasswordPage.tsx

- Quitar la importación de GymSelect.
- Quitar useActiveGyms.
- Quitar el estado idGym del formulario.
- Quitar validaciones asociadas a idGym.
- Quitar el bloque visual que renderiza el campo Gimnasio.
- Mantener solo el campo Nueva contrasena y el botón de submit.

### 3. Enviar X-Gym-Id automáticamente al hacer reset

Archivo objetivo:
authService.ts

- Mantener el body del reset con tokenPlano y newPassword.
- Cambiar el método resetPassword para que reciba gymId desde la pantalla.
- Usar ese gymId para enviar el header X-Gym-Id en el POST a /Auth/reset-password.
- No enviar idGym en el body.

### 4. Ajustar el copy de la pantalla

Archivo objetivo:
ResetPasswordPage.tsx

- Eliminar referencias a selección manual de gimnasio.
- Simplificar el texto para enfocarlo únicamente en el ingreso de la nueva contraseña.

### 5. Mantener compatibilidad con el flujo actual de login y forgot password

Archivos a revisar:
ForgotPasswordPage.tsx
authService.ts

- Verificar que forgot-password siga enviando el gimnasio correcto al backend para generar el email.
- Confirmar que el link de recuperación final llegue con token y gymId.
- No cambiar el contrato del backend salvo lo ya implementado en AuthService.cs.

## Validaciones sugeridas

- Abrir un enlace de recuperación con token y gymId válidos.
- Verificar que la pantalla de reset no muestre selector de gimnasio.
- Confirmar que el submit llama a POST /api/Auth/reset-password con:
  - body: tokenPlano y newPassword
  - header: X-Gym-Id con el gymId de la URL
- Probar casos de error:
  - token ausente
  - gymId ausente
  - gymId inválido
  - token expirado o reutilizado

## Criterio de aceptación

- La UI de reset-password no pide gimnasio manualmente.
- El gimnasio se obtiene exclusivamente desde gymId en la URL.
- El request de reset usa X-Gym-Id correctamente.
- El flujo sigue funcionando con el backend actualizado.
