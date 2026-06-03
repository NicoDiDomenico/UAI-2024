Especificación de Requerimiento
Módulo: Autenticación
Feature: Recuperación de contraseña multi-tenant sin re-selección de gimnasio
Versión: 1.0

---

1. Objetivo
   Permitir que un usuario recupere y restablezca su contraseña sin tener que volver a elegir gimnasio al abrir el enlace del correo.

---

2. Problema actual
   • El backend envía link de reset solo con token.
   • El endpoint público POST api/Auth/reset-password requiere resolver tenant por X-Gym-Id.
   • Al abrir el link desde email, el frontend pierde el contexto de gimnasio y obliga a seleccionar gym nuevamente.

---

3. Solución requerida
   Incluir el gymId en el link de recuperación enviado por email, para que el frontend lo lea y lo envíe automáticamente en X-Gym-Id al hacer reset.

---

4. Requerimientos funcionales
   RF-01: Forgot Password (público)
   • Endpoint: POST api/Auth/forgot-password
   • Request body: ForgotPasswordRequestDto { email }
   • Header obligatorio: X-Gym-Id
   • Respuesta: siempre 200 OK con mensaje genérico (sin revelar existencia del email).
   RF-02: Generación de link de recuperación
   • Backend debe generar link con:
   • token (query param)
   • gymId (query param)
   • Formato esperado:
   /reset-password?token={tokenPlano}&gymId={idGym}
   RF-03: Reset Password (público)
   • Endpoint: POST api/Auth/reset-password
   • Request body: ResetPasswordRequestDto { tokenPlano, newPassword }
   • Header obligatorio: X-Gym-Id (tomado del gymId de la URL)
   • Si token válido: 200 OK
   • Si token inválido/expirado: 400 BadRequest con mensaje claro.
   RF-04: Frontend reset sin selector de gym
   • La pantalla de reset no debe pedir gimnasio si gymId viene en URL.
   • Debe mapear:
   • token (URL) -> TokenPlano (DTO)
   • gymId (URL) -> header X-Gym-Id
   RF-05: Manejo de URL incompleta
   • Si falta token o gymId en URL:
   • no enviar request,
   • mostrar error funcional claro.

---

5. Requerimientos no funcionales
   • Mantener arquitectura existente frontend (src/services, tipos TS, axios configurado).
   • No hardcodear base URL.
   • No cambiar nombres de endpoints ni DTOs.
   • Mantener comportamiento anti-enumeración de usuarios en forgot-password.

---

6. Criterios de aceptación (QA)

1) Dado un email válido + gym correcto, se envía correo con link que incluye token y gymId.
2) Al abrir el link, la pantalla reset se carga sin pedir selección de gym.
3) Al confirmar nueva contraseña, el frontend envía X-Gym-Id con valor de URL.
4) Si token expira, se muestra mensaje de enlace inválido/expirado.
5) Forgot-password siempre responde mensaje genérico, exista o no el email.

---

7. Fuera de alcance
   • Cambios en política de unicidad de email.
   • Cambios de UX del login fuera del link “Olvidé mi contraseña”.
   • Reescritura del mecanismo de tenant resolution.
