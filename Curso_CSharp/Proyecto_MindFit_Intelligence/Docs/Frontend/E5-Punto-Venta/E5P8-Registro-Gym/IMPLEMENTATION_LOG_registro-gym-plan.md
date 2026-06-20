# Implementation Log - Registro de gimnasio

- **Archivos creados:** `Frontend/src/pages/GymOnboardingPage.tsx` y `Frontend/src/types/gymOnboarding.ts`.
- **Archivos modificados:** `Frontend/src/services/apiClient.ts`, `Frontend/src/services/gymsService.ts`, `Frontend/src/routes/AppRouter.tsx`, `Frontend/src/pages/LandingPage.tsx`, `Frontend/src/pages/PreciosPage.tsx` y `Frontend/src/App.css`.
- **Endpoint integrado:** `POST /api/Gyms/onboarding`, consumido mediante la ruta relativa `/Gyms/onboarding` porque `apiClient` ya incorpora el prefijo `/api`.
- **DTO implementado:** `GymOnboardingRequest` replica la estructura `nombreGym`, `usuarioMaster` y `personaResponsable`. El género se restringe a `Masculino`, `Femenino`, `Otro` o `NoEspecifica`. También se tipó la respuesta con `mensaje` e `idGym`.
- **Header de tenant:** se agregó la opción puntual `skipGymId` al cliente Axios. El onboarding la utiliza para impedir el envío de `X-Gym-Id` sin cambiar el comportamiento de login ni de las solicitudes autenticadas.
- **Formulario:** incluye datos del gimnasio, credenciales del usuario master y datos personales de la persona responsable. La fecha se transforma a formato ISO antes del envío y `confirmarPassword` no forma parte del payload.
- **Validaciones:** se controlan los catorce campos requeridos, formato de email y coincidencia de contraseñas. El request no se ejecuta mientras existan errores.
- **Estados:** el botón se deshabilita durante el envío, los errores usan el mensaje real del backend mediante `getApiErrorMessage` y el éxito limpia el formulario, informa que la activación está pendiente y ofrece acceso a `/login`.
- **Rutas:** `/registro-gym` se agregó como ruta pública fuera de `ProtectedRoute`. Los botones `Solicitar Demo` de inicio y funcionalidades, junto con `¡Lo quiero!` de precios, navegan a esta ruta; `Acceso Clientes` continúa apuntando a `/login`.
- **Diseño:** estilos encapsulados bajo `.gym-onboarding-page`, con una única superficie de formulario, secciones numeradas, foco visible y adaptación a una columna en mobile.
- **Build:** `npm run build` finalizó correctamente. Vite mantiene la advertencia informativa existente por un chunk superior a 500 kB.
- **Lint:** ESLint finalizó correctamente para todos los archivos TypeScript modificados en esta implementación.
- **Pruebas funcionales:** se comprobaron los dos CTA, catorce errores al enviar el formulario vacío, payload anidado, género y fecha serializados, estado de éxito simulado y ausencia de `X-Gym-Id` incluso con una sesión previa almacenada.
- **Pruebas responsive:** verificadas en 1365 x 900 y 390 x 844 sin desbordamiento horizontal.
- **Limitación:** no se realizó un alta real para evitar crear datos de prueba. La respuesta exitosa se simuló en el navegador conservando el contrato del backend.
