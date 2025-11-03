# Corrección: Permisos no se Cargan Después del Login

## Problema Identificado

Después de hacer login, los permisos **no se cargaban automáticamente**. El usuario tenía que:
1. Hacer login ✅
2. **Recargar la página manualmente** 🔄
3. Entonces los permisos se cargaban ✅

### Causa Raíz

El `PermissionsContext` usaba `enabled: hasToken` para determinar si ejecutar el query:

```tsx
// ❌ ANTES - Problema
const hasToken = !!getToken(TOKEN_KEY);

const { data } = useQuery({
  queryKey: ['permissions'],
  queryFn: permissionsApi.getPermissions,
  enabled: hasToken,  // ❌ hasToken se evalúa una sola vez al montar
});
```

**Problema:** `hasToken` se evaluaba cuando el componente se montaba (antes del login). Aunque el token se guardaba después del login, el query no se reactivaba porque `hasToken` ya había sido evaluado como `false`.

## Solución Implementada

Cambiar la condición de `enabled` para usar `isAuthenticated` del `AuthContext` en lugar de verificar el token directamente:

```tsx
// ✅ DESPUÉS - Solución
const { isAuthenticated } = useAuth();

const { data } = useQuery({
  queryKey: ['permissions'],
  queryFn: permissionsApi.getPermissions,
  enabled: isAuthenticated,  // ✅ Se reactiva cuando isAuthenticated cambia
  staleTime: 5 * 60 * 1000,
  refetchOnWindowFocus: false,
});
```

### Por qué Funciona

1. **Usuario hace login** → `AuthContext.login()` se ejecuta
2. **Token se guarda** → `setToken(TOKEN_KEY, accessToken)`
3. **Usuario se actualiza** → `setUser(userProfile)`
4. **isAuthenticated cambia** → De `false` a `true`
5. **Query se activa** → `enabled: isAuthenticated` detecta el cambio
6. **Permisos se cargan** → Automáticamente sin recargar

## Mejoras Adicionales

### 1. Limpieza de Permisos al Logout

```tsx
// Limpiar permisos cuando el usuario cierra sesión
useEffect(() => {
  if (!isAuthenticated) {
    setPermissions(new Set());
    removeToken(PERMISSIONS_CACHE_KEY);
  }
}, [isAuthenticated]);
```

### 2. Configuración de Cache

```tsx
const { data } = useQuery({
  // ...
  staleTime: 5 * 60 * 1000,      // Los datos son válidos por 5 minutos
  refetchOnWindowFocus: false,   // No recargar al cambiar de pestaña
});
```

## Flujo Completo

### Antes ❌

```
Login → Token guardado → isAuthenticated = true
  ↓
PermissionsContext monta
  ↓
hasToken = false (evaluado al montar, antes del login)
  ↓
Query NO se ejecuta
  ↓
Usuario recarga página manualmente
  ↓
PermissionsContext monta nuevamente
  ↓
hasToken = true (ahora sí encuentra el token)
  ↓
Query se ejecuta → Permisos cargados ✅
```

### Después ✅

```
Login → Token guardado → setUser() → isAuthenticated = true
  ↓
PermissionsContext detecta cambio en isAuthenticated
  ↓
Query se activa automáticamente (enabled: isAuthenticated)
  ↓
Permisos cargados → Sidebar y rutas se actualizan
  ↓
Usuario ve su interfaz correcta inmediatamente ✅
```

## Archivo Modificado

- ✅ `frontend/src/contexts/PermissionsContext.tsx`

### Cambios Específicos

1. **Import agregado:**
   ```tsx
   import { useAuth } from './AuthContext';
   ```

2. **Cambio en la condición de enabled:**
   ```tsx
   // Antes
   const hasToken = !!getToken(TOKEN_KEY);
   enabled: hasToken,
   
   // Después
   const { isAuthenticated } = useAuth();
   enabled: isAuthenticated,
   ```

3. **Limpieza al logout:**
   ```tsx
   useEffect(() => {
     if (!isAuthenticated) {
       setPermissions(new Set());
       removeToken(PERMISSIONS_CACHE_KEY);
     }
   }, [isAuthenticated]);
   ```

## Cómo Probar

### 1. Limpia el Storage
```javascript
// En DevTools Console
localStorage.clear();
```

### 2. Recarga la Aplicación
```
Ctrl + Shift + R (hard reload)
```

### 3. Haz Login
```
Usuario: admin
Password: Admin123!
```

### 4. Verifica en DevTools

**Console:**
```
✅ Login success, user: {id: 1, username: "admin", ...}
✅ Permissions loaded: 50 permissions
```

**Network Tab:**
```
✅ POST /auth/login → 200 OK
✅ GET /auth/me → 200 OK
✅ GET /auth/permissions → 200 OK (sin recargar!)
```

**Application Tab → Local Storage:**
```
✅ access_token: "eyJ..."
✅ refresh_token: "eyJ..."
✅ user_profile: {id: 1, ...}
✅ permissions_cache: ["config.usuarios.listar", ...]
```

### 5. Verifica la UI

**Inmediatamente después del login (sin recargar):**
- ✅ Redirige al dashboard
- ✅ Sidebar muestra los enlaces correctos (Usuarios, Grupos, Acciones)
- ✅ Las rutas protegidas funcionan
- ✅ Los botones se muestran/ocultan según permisos

## Beneficios

1. ✅ **Mejor UX:** No hay que recargar la página después del login
2. ✅ **Más rápido:** Los permisos se cargan en el flujo del login
3. ✅ **Más limpio:** Usa el estado reactivo de React correctamente
4. ✅ **Más mantenible:** Depende del estado de autenticación, no del storage directamente

## Comparación de Experiencia

### Antes ❌
```
1. Usuario escribe credenciales
2. Click en "Iniciar Sesión"
3. Redirige a /dashboard
4. Sidebar está vacío (no ve enlaces)
5. Usuario confundido 😕
6. Recarga la página (F5)
7. Ahora sí ve sus opciones ✅
```

### Después ✅
```
1. Usuario escribe credenciales
2. Click en "Iniciar Sesión"
3. Redirige a /dashboard
4. Sidebar muestra todos sus enlaces inmediatamente ✅
5. Usuario feliz 😊
```

---

**Problema resuelto. Los permisos ahora se cargan automáticamente después del login sin necesidad de recargar la página.** ✅
