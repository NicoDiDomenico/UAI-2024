# Etapa 2 - Inicio

## 1. Rol y Persona

Actuá como un desarrollador Frontend Senior especializado en React, TypeScript y Vite.

Tu responsabilidad es analizar el proyecto existente, respetar la arquitectura actual y realizar cambios incrementales, mantenibles y consistentes con el código existente.

Antes de implementar cualquier cambio:

- Analizá los archivos relacionados.
- Identificá componentes, servicios, hooks y tipos reutilizables.
- Evitá duplicar lógica existente.
- Priorizá soluciones simples y mantenibles.

## 2. Tarea Principal

Implementar la pantalla de Inicio que se muestra luego del login.

La pantalla debe:

- Mostrar botones de navegación según los permisos del usuario.
- Mostrar una grilla con los turnos correspondientes al día actual.
- Centralizar la lógica de permisos para que pueda reutilizarse en futuras pantallas.

## 3. Contexto

### Archivos Backend y Frontend

- TokenResponseDto.cs
- LoginUsuarioDto.cs
- FormularioDto.cs
- PermisosActualizadosDto.cs
- TurnoDetalleDto.cs
- SocioTurnoDto.cs
- UsuarioGridDto.cs
- SocioGridDto.cs
- EstadoTurno.cs
- EstadoSocio.cs
- Plan.cs
- AuthController.cs
- FormularioController.cs
- TurnoController.cs
- apiClient.ts
- authService.ts
- gymsService.ts
- authStorage.ts
- AuthContext.tsx
- useAuth.ts
- AppRouter.tsx
- DashboardPage.tsx
- auth.ts

### UI

Tomar como referencia visual la imagen/prototipo adjunto.

Priorizar:

- diseño limpio
- navegación clara
- buena jerarquía visual
- consistencia con el login existente

- No replicar exactamente estilos si la arquitectura actual utiliza componentes reutilizables ya existentes.

## 4. Reglas y Restricciones

### Permisos de Navegación

Consumir:

GET /api/Formulario

Utilizar el catálogo de formularios como fuente de verdad para determinar los permisos asociados a cada funcionalidad.

No hardcodear relaciones de permisos cuando puedan obtenerse desde el backend.

Los siguientes grupos funcionales corresponden a:

- Gestionar Rutinas
- Ver Socios
- Gestionar Gimnasio

Mapear cada botón con el formulario correspondiente y utilizar los permisos devueltos por el backend para determinar su visibilidad.

### Botones de Navegación

#### Gestionar Rutinas

Permisos asociados:

- EDITAR_RUTINA
- VER_HISTORIAL_RUTINA
- ELIMINAR_RUTINA
- RECUPERAR_RUTINA

#### Ver Socios

Permisos asociados:

- CREAR_USUARIO_SOCIO
- EDITAR_USUARIO_SOCIO
- ELIMINAR_USUARIO_SOCIO
- AGREGAR_TURNO
- CANCELAR_TURNO

#### Gestionar Gimnasio

Permisos asociados:

- CREAR_USUARIO_RESPONSABLE
- EDITAR_USUARIO_RESPONSABLE
- ELIMINAR_USUARIO_RESPONSABLE_DEFINITIVAMENTE
- CREAR_GRUPO
- EDITAR_GRUPO
- ELIMINAR_GRUPO
- CREAR_EQUIPAMIENTO
- EDITAR_EQUIPAMIENTO
- ELIMINAR_EQUIPAMIENTO
- CREAR_MAQUINA
- EDITAR_MAQUINA
- ELIMINAR_MAQUINA
- CREAR_EJERCICIO
- EDITAR_EJERCICIO
- ELIMINAR_EJERCICIO
- MODIFICAR_DIA_RH
- QUITAR_ENTRENADOR_DIA_RH

### Permisos de Botones de Acción

Esta etapa no cuenta con Botones de Accion, solo de navegacion.

### Grilla de Turnos

Consumir:
GET /api/Turno/inicio/grilla-fecha?fecha=yyyy-mm-dd

Responses:
Code 200 OK
[
{
"idTurno": 0,
"nombreDia": "string",
"fecha": "2026-06-04T18:35:38.198Z",
"cupos": "string",
"hora": "string",
"entrenador": "string",
"socio": "string",
"estadoTurno": "string"
}
]

Requisitos:

- Obtener automáticamente la fecha actual.
- Solicitar los turnos correspondientes a dicha fecha.
- Mostrar los resultados en una grilla.
- Manejar estados de carga.
- Manejar errores de consulta.
- Manejar estado sin resultados.

### Restricciones Técnicas

- No modificar código backend.
- No modificar contratos de API.
- No inventar propiedades de DTOs.
- Revisar los DTOs reales antes de implementar.
- Centralizar llamadas HTTP en services.
- Mantener tipado fuerte con TypeScript.
- Reutilizar lógica existente cuando sea posible.
- Evitar código duplicado.
- Mantener consistencia con la arquitectura actual del proyecto.

### Navegación y Páginas Placeholder

Los botones de navegación deben ser funcionales.

Como las funcionalidades destino todavía no están implementadas, se deben crear páginas placeholder simples para cada una.

Al hacer click en cada botón:

- Gestionar Rutinas debe navegar a su página placeholder.
- Ver Socios debe navegar a su página placeholder.
- Gestionar Gimnasio debe navegar a su página placeholder.

Cada página placeholder debe mostrar:

- título de la funcionalidad
- mensaje "Próximamente"
- estructura visual consistente con el layout actual de la aplicación

No implementar lógica de negocio, formularios, grillas ni llamadas a APIs en estas páginas placeholder.

Estas páginas servirán como punto de entrada para futuras etapas y podrán ser reemplazadas luego por las implementaciones reales.

Las rutas deben definirse respetando la estructura actual del proyecto y revisando `AppRouter.tsx` antes de crear o modificar rutas.

## 5. Formato de Salida

Además de implementar el código solicitado, generar o actualizar:

```txt
IMPLEMENTATION_LOG_inicio-plan.md
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
