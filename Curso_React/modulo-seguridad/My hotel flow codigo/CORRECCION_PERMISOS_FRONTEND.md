# Corrección del Sistema de Permisos - Frontend

## Problema Identificado en el Frontend

Aunque el backend ahora valida correctamente los permisos, **el frontend mostraba opciones que el usuario no debería ver**:

- ❌ Los **clientes veían enlaces** a Usuarios, Grupos, Acciones en el menú
- ❌ Las **rutas no estaban protegidas** por permisos, solo por autenticación
- ❌ Un cliente podía acceder directamente escribiendo la URL (ej: `/users`)
- ✅ Los **botones ya estaban protegidos** con el componente `<Can>`

## Solución Implementada

### 1. Protección de Rutas por Permisos

**Archivo:** `frontend/src/routes/AppRoutes.tsx`

#### Antes ❌
```tsx
<Route element={<ProtectedRoute />}>
  {/* TODAS las rutas solo validaban autenticación */}
  <Route path="/users" element={<UsersListPage />} />
  <Route path="/groups" element={<GroupsListPage />} />
  <Route path="/actions" element={<ActionsListPage />} />
  {/* ... */}
</Route>
```

#### Después ✅
```tsx
{/* Rutas de usuarios - Requiere permisos específicos */}
<Route element={<ProtectedRoute requiredPermissions={['config.usuarios.listar']} />}>
  <Route path="/users" element={<UsersListPage />} />
</Route>
<Route element={<ProtectedRoute requiredPermissions={['config.usuarios.crear']} />}>
  <Route path="/users/create" element={<UserFormPage />} />
</Route>
<Route element={<ProtectedRoute requiredPermissions={['config.usuarios.modificar']} />}>
  <Route path="/users/:id/edit" element={<UserFormPage />} />
</Route>
{/* ... */}
```

### 2. Tabla de Rutas Protegidas

| Ruta | Permiso Requerido |
|------|-------------------|
| `/dashboard` | Solo autenticación |
| `/auth/change-password` | Solo autenticación |
| **Usuarios** | |
| `/users` | `config.usuarios.listar` |
| `/users/create` | `config.usuarios.crear` |
| `/users/:id/edit` | `config.usuarios.modificar` |
| `/users/:id/permissions` | `config.usuarios.asignarGrupos` OR `config.usuarios.asignarAcciones` |
| **Grupos** | |
| `/groups` | `config.grupos.listar` |
| `/groups/create` | `config.grupos.crear` |
| `/groups/:id/edit` | `config.grupos.modificar` |
| `/groups/:id/actions` | `config.grupos.asignarAcciones` |
| `/groups/:id/children` | `config.grupos.asignarHijos` |
| **Acciones** | |
| `/actions` | `config.acciones.listar` |
| `/actions/create` | `config.acciones.crear` |
| `/actions/:id/edit` | `config.acciones.modificar` |

### 3. Sistema de Permisos Existente (Ya Funcionaba)

#### Componente `<Can>`
**Archivo:** `frontend/src/components/auth/Can.tsx`

```tsx
// Oculta contenido si el usuario no tiene el permiso
<Can perform="config.usuarios.crear">
  <button>Crear Usuario</button>
</Can>

// Múltiples permisos con OR
<Can perform={['config.usuarios.listar', 'config.usuarios.modificar']} requireAll={false}>
  <Link to="/users">Ver Usuarios</Link>
</Can>

// Múltiples permisos con AND (por defecto)
<Can perform={['config.usuarios.listar', 'config.usuarios.modificar']}>
  <button>Gestionar Usuarios</button>
</Can>
```

#### Sidebar con Permisos
**Archivo:** `frontend/src/components/layout/Sidebar.tsx`

```tsx
const navSections: NavSection[] = [
  {
    title: 'Configuración',
    items: [
      {
        label: 'Usuarios',
        path: '/users',
        icon: Users,
        permission: 'config.usuarios.listar', // ✅ Se valida
      },
      {
        label: 'Grupos',
        path: '/groups',
        icon: Shield,
        permission: 'config.grupos.listar', // ✅ Se valida
      },
      // ...
    ],
  },
];
```

#### Hook `usePermissions`
**Archivo:** `frontend/src/contexts/PermissionsContext.tsx`

```tsx
const { 
  hasPermission,        // Verificar UN permiso
  hasAllPermissions,    // Verificar TODOS los permisos (AND)
  hasAnyPermission,     // Verificar CUALQUIER permiso (OR)
  permissions,          // Set de permisos del usuario
  isLoading,            // Estado de carga
  refetchPermissions    // Recargar permisos
} = usePermissions();

// Ejemplo de uso directo
if (hasPermission('config.usuarios.crear')) {
  // Mostrar botón de crear
}
```

## Cómo Funciona Ahora

### Flujo Completo de Validación

1. **Usuario intenta acceder** → `/users`
2. **React Router** → Valida la ruta con `ProtectedRoute`
3. **ProtectedRoute** → Verifica:
   - ✅ ¿Está autenticado? (token JWT válido)
   - ✅ ¿Tiene el permiso `config.usuarios.listar`?
4. Si **NO tiene permiso** → Redirige a `/forbidden`
5. Si **SÍ tiene permiso** → Renderiza `UsersListPage`
6. **UsersListPage** → Los botones se renderizan con `<Can>`:
   ```tsx
   <Can perform="config.usuarios.crear">
     <button>Crear Usuario</button> {/* Solo se muestra si tiene el permiso */}
   </Can>
   ```

### Capas de Seguridad

```
┌─────────────────────────────────────────┐
│ 1. SIDEBAR                              │
│    Oculta enlaces sin permiso (<Can>)  │ ✅ Ya funcionaba
└─────────────────────────────────────────┘
              ↓
┌─────────────────────────────────────────┐
│ 2. RUTAS (React Router)                 │
│    Redirige a /forbidden sin permiso    │ ✅ AGREGADO HOY
└─────────────────────────────────────────┘
              ↓
┌─────────────────────────────────────────┐
│ 3. BOTONES Y ACCIONES                   │
│    Oculta botones sin permiso (<Can>)  │ ✅ Ya funcionaba
└─────────────────────────────────────────┘
              ↓
┌─────────────────────────────────────────┐
│ 4. BACKEND (API)                        │
│    Valida permisos en cada endpoint     │ ✅ CORREGIDO HOY
└─────────────────────────────────────────┘
```

## Pruebas de Funcionamiento

### 1. Verificar Sidebar
- ✅ **Admin:** Ve todos los enlaces (Usuarios, Grupos, Acciones, etc.)
- ✅ **Cliente:** Solo ve Dashboard y enlaces para los que tiene permisos
- ✅ Los enlaces ocultos no aparecen en el DOM

### 2. Verificar Rutas Protegidas
```
Cliente intenta acceder a /users directamente
↓
No tiene permiso 'config.usuarios.listar'
↓
Redirigido a /forbidden (403 Forbidden)
```

### 3. Verificar Botones
```tsx
// Cliente sin permisos ve la lista vacía de usuarios
// pero NO ve el botón "Crear Usuario"
<Can perform="config.usuarios.crear">
  <button>Crear Usuario</button> {/* No se renderiza */}
</Can>
```

### 4. Verificar Backend
```
Cliente intenta POST /users vía API
↓
Backend valida con ActionsGuard
↓
403 Forbidden: "Insufficient permissions"
```

## Ejemplos de Uso en Componentes

### Ocultar Botón de Crear
```tsx
export const UsersListPage = () => {
  return (
    <div>
      <h1>Usuarios</h1>
      
      {/* Solo se muestra si tiene el permiso */}
      <Can perform="config.usuarios.crear">
        <Link to="/users/create" className="btn-primary">
          Crear Usuario
        </Link>
      </Can>
      
      {/* Lista de usuarios */}
    </div>
  );
};
```

### Ocultar Columna de Acciones
```tsx
<table>
  <thead>
    <tr>
      <th>Usuario</th>
      <th>Email</th>
      {/* Solo muestra columna si puede editar o eliminar */}
      <Can perform={['config.usuarios.modificar', 'config.usuarios.eliminar']} requireAll={false}>
        <th>Acciones</th>
      </Can>
    </tr>
  </thead>
  <tbody>
    {users.map(user => (
      <tr key={user.id}>
        <td>{user.username}</td>
        <td>{user.email}</td>
        <Can perform={['config.usuarios.modificar', 'config.usuarios.eliminar']} requireAll={false}>
          <td>
            <Can perform="config.usuarios.modificar">
              <button>Editar</button>
            </Can>
            <Can perform="config.usuarios.eliminar">
              <button>Eliminar</button>
            </Can>
          </td>
        </Can>
      </tr>
    ))}
  </tbody>
</table>
```

### Validación Programática
```tsx
export const MyComponent = () => {
  const { hasPermission, hasAllPermissions } = usePermissions();
  
  const handleAction = () => {
    if (!hasPermission('config.usuarios.modificar')) {
      toast.error('No tienes permisos para realizar esta acción');
      return;
    }
    
    // Continuar con la acción
  };
  
  return (
    <button 
      onClick={handleAction}
      disabled={!hasPermission('config.usuarios.modificar')}
    >
      Modificar Usuario
    </button>
  );
};
```

## Página de Error 403 Forbidden

**Archivo:** `frontend/src/pages/errors/ForbiddenPage.tsx`

```tsx
// Ya existe y se muestra cuando el usuario no tiene permisos
// para acceder a una ruta
```

Cuando un usuario intenta acceder a una ruta sin permisos:
1. Es redirigido a `/forbidden`
2. Ve un mensaje: "No tienes permisos para acceder a esta página"
3. Puede volver al Dashboard

## Caché de Permisos

Los permisos se cachean en:
- **Memoria (Context):** Set de permisos en `PermissionsContext`
- **LocalStorage:** Clave `permissions_cache` para persistencia
- **React Query:** Query con key `['permissions']` (TTL del servidor)

### Refrescar Permisos
```tsx
const { refetchPermissions } = usePermissions();

// Después de asignar nuevos permisos a un usuario
await refetchPermissions();
```

## Archivos Modificados

1. ✅ `frontend/src/routes/AppRoutes.tsx` - Agregadas validaciones de permisos a las rutas

## Archivos que YA Funcionaban Correctamente

- ✅ `frontend/src/components/auth/Can.tsx` - Renderizado condicional
- ✅ `frontend/src/components/layout/Sidebar.tsx` - Menú con permisos
- ✅ `frontend/src/contexts/PermissionsContext.tsx` - Hook de permisos
- ✅ `frontend/src/routes/ProtectedRoute.tsx` - Componente de rutas protegidas
- ✅ `frontend/src/pages/errors/ForbiddenPage.tsx` - Página 403

## Resumen de Cambios

### Antes ❌
```
Frontend:
- Sidebar: ✅ Oculta enlaces (ya funcionaba)
- Rutas: ❌ Solo valida autenticación
- Botones: ✅ Oculta botones (ya funcionaba)

Backend:
- API: ❌ No validaba permisos
```

### Después ✅
```
Frontend:
- Sidebar: ✅ Oculta enlaces
- Rutas: ✅ Valida permisos específicos
- Botones: ✅ Oculta botones

Backend:
- API: ✅ Valida permisos con ActionsGuard
```

## Verificación Visual

### Cliente SIN permisos de configuración:

**Sidebar visible:**
```
🏠 Dashboard
❌ Usuarios (oculto)
❌ Grupos (oculto)
❌ Acciones (oculto)
```

**Si intenta acceder a `/users` directamente:**
```
→ Redirigido a /forbidden
→ Mensaje: "No tienes permisos para acceder a esta página"
```

### Admin CON todos los permisos:

**Sidebar visible:**
```
🏠 Dashboard
👥 Usuarios
🛡️ Grupos
🔑 Acciones
📅 Reservas
🏨 Habitaciones
...
```

**Puede acceder a cualquier ruta sin restricciones**

---

**El sistema de permisos del frontend ahora está completamente funcional y sincronizado con el backend.** 🎨🔒
