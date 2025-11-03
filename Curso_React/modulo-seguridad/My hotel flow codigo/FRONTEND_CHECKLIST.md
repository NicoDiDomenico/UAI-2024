# CHECKLIST DE IMPLEMENTACIÓN FRONTEND — MyHotelFlow

**Sistema de Reservas Hoteleras**
**Stack:** React 18+ + TypeScript 5+ + Vite 5+ + Tailwind CSS 3+ + TanStack Query v5
**Fecha:** Octubre 2025

---

## 📋 TABLA DE CONTENIDOS

1. [Setup Inicial del Proyecto](#1-setup-inicial-del-proyecto)
2. [Configuración Base](#2-configuración-base)
3. [Sistema de Autenticación](#3-sistema-de-autenticación)
4. [Sistema de Permisos](#4-sistema-de-permisos)
5. [Gestión de Usuarios](#5-gestión-de-usuarios)
6. [Gestión de Grupos](#6-gestión-de-grupos)
7. [Gestión de Acciones](#7-gestión-de-acciones)
8. [Layout y Navegación](#8-layout-y-navegación)
9. [Componentes Reutilizables](#9-componentes-reutilizables)
10. [Testing](#10-testing)
11. [Documentación](#11-documentación)

---

## 1. SETUP INICIAL DEL PROYECTO

### 1.1 Crear Proyecto con Vite

```bash
npm create vite@latest frontend -- --template react-ts
cd frontend
npm install
```

**Tareas:**
- [ ] Crear proyecto con plantilla React + TypeScript
- [ ] Verificar que package.json tiene las versiones correctas
- [ ] Ejecutar `npm run dev` y verificar que funciona

### 1.2 Instalar Dependencias Core

```bash
# Routing
npm install react-router-dom
npm install -D @types/react-router-dom

# Data Fetching & State
npm install @tanstack/react-query
npm install @tanstack/react-query-devtools

# HTTP Client
npm install axios

# Forms
npm install react-hook-form
npm install @hookform/resolvers

# Validation
npm install zod

# UI Components
npm install @headlessui/react
npm install lucide-react

# Styles
npm install -D tailwindcss postcss autoprefixer
npm install clsx tailwind-merge

# Utils
npm install date-fns
```

**Tareas:**
- [ ] Instalar todas las dependencias de producción
- [ ] Instalar todas las dependencias de desarrollo
- [ ] Verificar que package.json tiene todas las dependencias listadas
- [ ] Ejecutar `npm install` sin errores

### 1.3 Configurar Tailwind CSS

```bash
npx tailwindcss init -p
```

**Archivo:** `tailwind.config.js`

```js
/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        primary: {
          50: '#eff6ff',
          100: '#dbeafe',
          200: '#bfdbfe',
          300: '#93c5fd',
          400: '#60a5fa',
          500: '#3b82f6',
          600: '#2563eb',
          700: '#1d4ed8',
          800: '#1e40af',
          900: '#1e3a8a',
        },
        accent: {
          50: '#fefce8',
          100: '#fef9c3',
          200: '#fef08a',
          300: '#fde047',
          400: '#facc15',
          500: '#eab308',
          600: '#ca8a04',
          700: '#a16207',
          800: '#854d0e',
          900: '#713f12',
        },
        success: {
          50: '#f0fdf4',
          100: '#dcfce7',
          500: '#10b981',
          600: '#059669',
          700: '#047857',
        },
        warning: {
          50: '#fffbeb',
          100: '#fef3c7',
          500: '#f59e0b',
          600: '#d97706',
          700: '#b45309',
        },
        error: {
          50: '#fef2f2',
          100: '#fee2e2',
          500: '#ef4444',
          600: '#dc2626',
          700: '#b91c1c',
        },
      },
      fontFamily: {
        sans: ['Inter', 'ui-sans-serif', 'system-ui', 'sans-serif'],
      },
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
  ],
}
```

**Archivo:** `src/index.css`

```css
@tailwind base;
@tailwind components;
@tailwind utilities;

@layer components {
  .btn-primary {
    @apply px-6 py-3 bg-primary-600 hover:bg-primary-700 text-white font-medium rounded-lg shadow-sm hover:shadow-md transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed;
  }

  .btn-secondary {
    @apply px-6 py-3 bg-white hover:bg-gray-50 text-primary-600 font-medium border-2 border-primary-600 rounded-lg transition-colors duration-200;
  }

  .input {
    @apply w-full px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent placeholder:text-gray-400 disabled:bg-gray-100 disabled:cursor-not-allowed;
  }

  .card {
    @apply bg-white rounded-lg shadow-md hover:shadow-xl transition-shadow duration-300;
  }
}
```

**Tareas:**
- [ ] Crear configuración de Tailwind con paleta de colores del diseño
- [ ] Instalar plugin @tailwindcss/forms
- [ ] Configurar clases utilitarias personalizadas
- [ ] Importar Tailwind en index.css
- [ ] Verificar que los estilos se aplican correctamente

### 1.4 Estructura de Carpetas

```
frontend/
├── public/
├── src/
│   ├── api/                    # Configuración de axios y endpoints
│   │   ├── axios.config.ts
│   │   ├── auth.api.ts
│   │   ├── users.api.ts
│   │   ├── groups.api.ts
│   │   └── actions.api.ts
│   ├── components/             # Componentes reutilizables
│   │   ├── ui/                # Componentes base (Button, Input, Modal)
│   │   ├── layout/            # Layout components (Navbar, Sidebar)
│   │   └── features/          # Componentes de negocio
│   ├── contexts/              # Context providers
│   │   ├── AuthContext.tsx
│   │   └── PermissionsContext.tsx
│   ├── hooks/                 # Custom hooks
│   │   ├── useAuth.ts
│   │   ├── usePermissions.ts
│   │   ├── useUsers.ts
│   │   ├── useGroups.ts
│   │   └── useActions.ts
│   ├── pages/                 # Páginas/Rutas
│   │   ├── auth/
│   │   ├── dashboard/
│   │   ├── users/
│   │   ├── groups/
│   │   └── actions/
│   ├── routes/                # Configuración de rutas
│   │   ├── AppRoutes.tsx
│   │   └── ProtectedRoute.tsx
│   ├── schemas/               # Zod schemas
│   │   ├── auth.schema.ts
│   │   ├── user.schema.ts
│   │   ├── group.schema.ts
│   │   └── action.schema.ts
│   ├── types/                 # TypeScript types
│   │   ├── auth.types.ts
│   │   ├── user.types.ts
│   │   ├── group.types.ts
│   │   └── action.types.ts
│   ├── utils/                 # Utilidades
│   │   ├── cn.ts             # clsx + tailwind-merge
│   │   ├── storage.ts        # localStorage helpers
│   │   └── formatters.ts     # Formateadores
│   ├── config/                # Configuración
│   │   └── constants.ts
│   ├── App.tsx
│   ├── main.tsx
│   └── index.css
├── .env.example
├── .env
├── vite.config.ts
├── tailwind.config.js
├── tsconfig.json
└── package.json
```

**Tareas:**
- [ ] Crear todas las carpetas de la estructura
- [ ] Crear archivos .gitkeep en carpetas vacías
- [ ] Verificar que la estructura sigue el patrón recomendado

---

## 2. CONFIGURACIÓN BASE

### 2.1 Variables de Entorno

**Archivo:** `.env.example`

```env
VITE_API_URL=http://localhost:3000/api
VITE_APP_NAME=MyHotelFlow
VITE_JWT_TOKEN_KEY=myhotelflow_access_token
VITE_JWT_REFRESH_TOKEN_KEY=myhotelflow_refresh_token
```

**Archivo:** `.env`

```env
VITE_API_URL=http://localhost:3000/api
VITE_APP_NAME=MyHotelFlow
VITE_JWT_TOKEN_KEY=myhotelflow_access_token
VITE_JWT_REFRESH_TOKEN_KEY=myhotelflow_refresh_token
```

**Archivo:** `src/config/constants.ts`

```typescript
export const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:3000/api';
export const APP_NAME = import.meta.env.VITE_APP_NAME || 'MyHotelFlow';
export const TOKEN_KEY = import.meta.env.VITE_JWT_TOKEN_KEY || 'access_token';
export const REFRESH_TOKEN_KEY = import.meta.env.VITE_JWT_REFRESH_TOKEN_KEY || 'refresh_token';

export const PERMISSIONS_CACHE_KEY = 'user_permissions';
export const USER_PROFILE_KEY = 'user_profile';
```

**Tareas:**
- [ ] Crear archivo .env.example
- [ ] Crear archivo .env (gitignored)
- [ ] Crear constants.ts con variables de entorno
- [ ] Verificar que las variables son accesibles con import.meta.env

### 2.2 Configuración de Axios

**Archivo:** `src/api/axios.config.ts`

```typescript
import axios, { AxiosError, InternalAxiosRequestConfig } from 'axios';
import { API_URL, TOKEN_KEY, REFRESH_TOKEN_KEY } from '@/config/constants';
import { getToken, setToken, removeToken } from '@/utils/storage';

// Crear instancia de axios
export const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor - agregar token
api.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    const token = getToken(TOKEN_KEY);
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor - refresh token en 401
let isRefreshing = false;
let failedQueue: Array<{
  resolve: (value?: unknown) => void;
  reject: (reason?: unknown) => void;
}> = [];

const processQueue = (error: Error | null, token: string | null = null) => {
  failedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });

  failedQueue = [];
};

api.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config as InternalAxiosRequestConfig & {
      _retry?: boolean;
    };

    // Si es 401 y no es el endpoint de refresh
    if (error.response?.status === 401 && !originalRequest._retry) {
      if (isRefreshing) {
        // Agregar a la cola
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        })
          .then((token) => {
            if (originalRequest.headers) {
              originalRequest.headers.Authorization = `Bearer ${token}`;
            }
            return api(originalRequest);
          })
          .catch((err) => Promise.reject(err));
      }

      originalRequest._retry = true;
      isRefreshing = true;

      const refreshToken = getToken(REFRESH_TOKEN_KEY);

      if (!refreshToken) {
        // No hay refresh token, cerrar sesión
        removeToken(TOKEN_KEY);
        removeToken(REFRESH_TOKEN_KEY);
        window.location.href = '/login';
        return Promise.reject(error);
      }

      try {
        // Intentar refrescar el token
        const response = await axios.post(`${API_URL}/auth/refresh`, {
          refreshToken,
        });

        const { accessToken, refreshToken: newRefreshToken } = response.data;

        setToken(TOKEN_KEY, accessToken);
        setToken(REFRESH_TOKEN_KEY, newRefreshToken);

        if (originalRequest.headers) {
          originalRequest.headers.Authorization = `Bearer ${accessToken}`;
        }

        processQueue(null, accessToken);
        isRefreshing = false;

        return api(originalRequest);
      } catch (refreshError) {
        processQueue(refreshError as Error, null);
        isRefreshing = false;

        // Refresh falló, cerrar sesión
        removeToken(TOKEN_KEY);
        removeToken(REFRESH_TOKEN_KEY);
        window.location.href = '/login';

        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export default api;
```

**Tareas:**
- [ ] Crear configuración de axios con baseURL
- [ ] Implementar request interceptor para agregar token
- [ ] Implementar response interceptor para refresh automático
- [ ] Implementar cola de requests durante refresh
- [ ] Agregar redirección a /login cuando falla el refresh
- [ ] Verificar que funciona correctamente

### 2.3 Utilidades de Storage

**Archivo:** `src/utils/storage.ts`

```typescript
/**
 * Guardar en localStorage
 */
export const setToken = (key: string, value: string): void => {
  try {
    localStorage.setItem(key, value);
  } catch (error) {
    console.error('Error saving to localStorage:', error);
  }
};

/**
 * Obtener de localStorage
 */
export const getToken = (key: string): string | null => {
  try {
    return localStorage.getItem(key);
  } catch (error) {
    console.error('Error reading from localStorage:', error);
    return null;
  }
};

/**
 * Eliminar de localStorage
 */
export const removeToken = (key: string): void => {
  try {
    localStorage.removeItem(key);
  } catch (error) {
    console.error('Error removing from localStorage:', error);
  }
};

/**
 * Limpiar todo localStorage
 */
export const clearStorage = (): void => {
  try {
    localStorage.clear();
  } catch (error) {
    console.error('Error clearing localStorage:', error);
  }
};

/**
 * Guardar objeto JSON
 */
export const setItem = <T>(key: string, value: T): void => {
  try {
    localStorage.setItem(key, JSON.stringify(value));
  } catch (error) {
    console.error('Error saving object to localStorage:', error);
  }
};

/**
 * Obtener objeto JSON
 */
export const getItem = <T>(key: string): T | null => {
  try {
    const item = localStorage.getItem(key);
    return item ? JSON.parse(item) : null;
  } catch (error) {
    console.error('Error reading object from localStorage:', error);
    return null;
  }
};
```

**Tareas:**
- [ ] Crear utilidades de localStorage
- [ ] Implementar getToken, setToken, removeToken
- [ ] Implementar setItem<T> y getItem<T> para objetos
- [ ] Agregar manejo de errores try/catch
- [ ] Verificar que funciona correctamente

### 2.4 Utilidad cn() para clases

**Archivo:** `src/utils/cn.ts`

```typescript
import { type ClassValue, clsx } from 'clsx';
import { twMerge } from 'tailwind-merge';

/**
 * Combina clases de Tailwind sin conflictos
 * Usa clsx para condicionales y twMerge para merge
 */
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}
```

**Tareas:**
- [ ] Crear utilidad cn()
- [ ] Instalar clsx y tailwind-merge
- [ ] Verificar que funciona con clases condicionales

### 2.5 Configuración de TanStack Query

**Archivo:** `src/main.tsx`

```typescript
import React from 'react';
import ReactDOM from 'react-dom/client';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import App from './App.tsx';
import './index.css';

// Configurar QueryClient
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
      staleTime: 5 * 60 * 1000, // 5 minutos
    },
  },
});

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <App />
      <ReactQueryDevtools initialIsOpen={false} />
    </QueryClientProvider>
  </React.StrictMode>
);
```

**Tareas:**
- [ ] Configurar QueryClient con opciones por defecto
- [ ] Envolver App con QueryClientProvider
- [ ] Agregar ReactQueryDevtools para desarrollo
- [ ] Verificar que funciona correctamente

---

## 3. SISTEMA DE AUTENTICACIÓN

### 3.1 Types de Autenticación

**Archivo:** `src/types/auth.types.ts`

```typescript
export interface User {
  id: number;
  username: string;
  email: string;
  fullName?: string;
  isActive: boolean;
  lastLoginAt?: string;
  createdAt: string;
  updatedAt: string;
}

export interface LoginCredentials {
  identity: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  user: User;
  effectiveActions: string[];
}

export interface ChangePasswordPayload {
  currentPassword: string;
  newPassword: string;
}

export interface RecoverPasswordRequest {
  email: string;
}

export interface RecoverPasswordConfirm {
  token: string;
  newPassword: string;
}

export interface AuthContextValue {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (credentials: LoginCredentials) => Promise<void>;
  logout: () => Promise<void>;
  changePassword: (payload: ChangePasswordPayload) => Promise<void>;
}
```

**Tareas:**
- [ ] Crear interfaces de autenticación
- [ ] Definir User, LoginCredentials, LoginResponse
- [ ] Definir ChangePasswordPayload
- [ ] Definir RecoverPasswordRequest y RecoverPasswordConfirm
- [ ] Definir AuthContextValue
- [ ] Verificar que los tipos coinciden con el backend

### 3.2 Schemas de Validación

**Archivo:** `src/schemas/auth.schema.ts`

```typescript
import { z } from 'zod';

export const loginSchema = z.object({
  identity: z
    .string()
    .min(1, 'Usuario o email es requerido')
    .max(255, 'Máximo 255 caracteres'),
  password: z
    .string()
    .min(8, 'La contraseña debe tener al menos 8 caracteres')
    .max(255, 'Máximo 255 caracteres'),
});

export const changePasswordSchema = z
  .object({
    currentPassword: z
      .string()
      .min(1, 'Contraseña actual es requerida'),
    newPassword: z
      .string()
      .min(8, 'La nueva contraseña debe tener al menos 8 caracteres')
      .regex(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]/,
        'La contraseña debe contener mayúsculas, minúsculas, números y símbolos'
      ),
    confirmPassword: z.string(),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: 'Las contraseñas no coinciden',
    path: ['confirmPassword'],
  })
  .refine((data) => data.currentPassword !== data.newPassword, {
    message: 'La nueva contraseña debe ser diferente a la actual',
    path: ['newPassword'],
  });

export const recoverRequestSchema = z.object({
  email: z.string().email('Email inválido'),
});

export const recoverConfirmSchema = z
  .object({
    token: z.string().min(1, 'Token es requerido'),
    newPassword: z
      .string()
      .min(8, 'La contraseña debe tener al menos 8 caracteres')
      .regex(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]/,
        'La contraseña debe contener mayúsculas, minúsculas, números y símbolos'
      ),
    confirmPassword: z.string(),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: 'Las contraseñas no coinciden',
    path: ['confirmPassword'],
  });

export type LoginFormData = z.infer<typeof loginSchema>;
export type ChangePasswordFormData = z.infer<typeof changePasswordSchema>;
export type RecoverRequestFormData = z.infer<typeof recoverRequestSchema>;
export type RecoverConfirmFormData = z.infer<typeof recoverConfirmSchema>;
```

**Tareas:**
- [ ] Crear schemas con Zod
- [ ] Validar login (identity + password)
- [ ] Validar cambio de contraseña con requisitos de complejidad
- [ ] Validar recuperación de contraseña
- [ ] Exportar tipos inferidos
- [ ] Verificar que las validaciones funcionan

### 3.3 API de Autenticación

**Archivo:** `src/api/auth.api.ts`

```typescript
import api from './axios.config';
import {
  LoginCredentials,
  LoginResponse,
  ChangePasswordPayload,
  RecoverPasswordRequest,
  RecoverPasswordConfirm,
  User,
} from '@/types/auth.types';

export const authApi = {
  /**
   * Login con credenciales
   */
  login: async (credentials: LoginCredentials): Promise<LoginResponse> => {
    const response = await api.post<LoginResponse>('/auth/login', credentials);
    return response.data;
  },

  /**
   * Refresh access token
   */
  refresh: async (refreshToken: string): Promise<LoginResponse> => {
    const response = await api.post<LoginResponse>('/auth/refresh', {
      refreshToken,
    });
    return response.data;
  },

  /**
   * Logout (revocar tokens)
   */
  logout: async (): Promise<void> => {
    await api.post('/auth/logout');
  },

  /**
   * Obtener perfil del usuario autenticado
   */
  getProfile: async (): Promise<User> => {
    const response = await api.get<User>('/auth/me');
    return response.data;
  },

  /**
   * Cambiar contraseña
   */
  changePassword: async (payload: ChangePasswordPayload): Promise<void> => {
    await api.patch('/auth/password', payload);
  },

  /**
   * Solicitar recuperación de contraseña
   */
  recoverRequest: async (payload: RecoverPasswordRequest): Promise<void> => {
    await api.post('/auth/recover/request', payload);
  },

  /**
   * Confirmar recuperación de contraseña
   */
  recoverConfirm: async (payload: RecoverPasswordConfirm): Promise<void> => {
    await api.post('/auth/recover/confirm', payload);
  },
};
```

**Tareas:**
- [ ] Crear authApi con todos los métodos
- [ ] Implementar login, refresh, logout
- [ ] Implementar getProfile
- [ ] Implementar changePassword
- [ ] Implementar recoverRequest y recoverConfirm
- [ ] Verificar que funciona con el backend

### 3.4 Auth Context

**Archivo:** `src/contexts/AuthContext.tsx`

```typescript
import React, { createContext, useContext, useState, useEffect } from 'react';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { authApi } from '@/api/auth.api';
import {
  User,
  LoginCredentials,
  ChangePasswordPayload,
  AuthContextValue,
} from '@/types/auth.types';
import {
  getToken,
  setToken,
  removeToken,
  setItem,
  getItem,
  clearStorage,
} from '@/utils/storage';
import { TOKEN_KEY, REFRESH_TOKEN_KEY, USER_PROFILE_KEY } from '@/config/constants';

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const queryClient = useQueryClient();

  // Cargar usuario del storage al montar
  useEffect(() => {
    const token = getToken(TOKEN_KEY);
    const savedUser = getItem<User>(USER_PROFILE_KEY);

    if (token && savedUser) {
      setUser(savedUser);
    }
    setIsLoading(false);
  }, []);

  // Query para obtener perfil (solo si hay token)
  const { refetch: refetchProfile } = useQuery({
    queryKey: ['auth', 'profile'],
    queryFn: authApi.getProfile,
    enabled: false,
  });

  // Mutation: Login
  const loginMutation = useMutation({
    mutationFn: authApi.login,
    onSuccess: (data) => {
      setToken(TOKEN_KEY, data.accessToken);
      setToken(REFRESH_TOKEN_KEY, data.refreshToken);
      setItem(USER_PROFILE_KEY, data.user);
      setUser(data.user);

      // Guardar permisos en PermissionsContext si existe
      queryClient.setQueryData(['permissions'], data.effectiveActions);
    },
  });

  // Mutation: Logout
  const logoutMutation = useMutation({
    mutationFn: authApi.logout,
    onSettled: () => {
      removeToken(TOKEN_KEY);
      removeToken(REFRESH_TOKEN_KEY);
      clearStorage();
      setUser(null);
      queryClient.clear();
    },
  });

  // Mutation: Change Password
  const changePasswordMutation = useMutation({
    mutationFn: authApi.changePassword,
  });

  const login = async (credentials: LoginCredentials): Promise<void> => {
    await loginMutation.mutateAsync(credentials);
  };

  const logout = async (): Promise<void> => {
    await logoutMutation.mutateAsync();
  };

  const changePassword = async (payload: ChangePasswordPayload): Promise<void> => {
    await changePasswordMutation.mutateAsync(payload);
  };

  const value: AuthContextValue = {
    user,
    isAuthenticated: !!user,
    isLoading,
    login,
    logout,
    changePassword,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = (): AuthContextValue => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
```

**Tareas:**
- [ ] Crear AuthContext con createContext
- [ ] Crear AuthProvider component
- [ ] Implementar estado de user con useState
- [ ] Implementar mutations de login, logout, changePassword
- [ ] Guardar/recuperar tokens y usuario en localStorage
- [ ] Limpiar caché de TanStack Query al logout
- [ ] Crear custom hook useAuth()
- [ ] Verificar que funciona correctamente

### 3.5 Páginas de Autenticación

#### **Página de Login**

**Archivo:** `src/pages/auth/LoginPage.tsx`

```typescript
import React from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '@/contexts/AuthContext';
import { loginSchema, LoginFormData } from '@/schemas/auth.schema';
import { AlertTriangle } from 'lucide-react';

export const LoginPage: React.FC = () => {
  const navigate = useNavigate();
  const { login } = useAuth();
  const [error, setError] = React.useState<string | null>(null);

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: LoginFormData) => {
    try {
      setError(null);
      await login(data);
      navigate('/dashboard');
    } catch (err: any) {
      setError(err.response?.data?.message || 'Error al iniciar sesión');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 px-4">
      <div className="max-w-md w-full">
        <div className="text-center mb-8">
          <h1 className="text-4xl font-bold text-primary-600 mb-2">
            MyHotelFlow
          </h1>
          <p className="text-gray-600">Sistema de Reservas Hoteleras</p>
        </div>

        <div className="bg-white rounded-lg shadow-md p-8">
          <h2 className="text-2xl font-semibold mb-6">Iniciar Sesión</h2>

          {error && (
            <div className="bg-error-50 border-l-4 border-error-500 p-4 rounded-r-md mb-6">
              <div className="flex items-start">
                <AlertTriangle className="text-error-500 mt-0.5 mr-3" size={20} />
                <p className="text-sm text-error-700">{error}</p>
              </div>
            </div>
          )}

          <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <div>
              <label htmlFor="identity" className="block text-sm font-medium text-gray-700 mb-1">
                Usuario o Email
              </label>
              <input
                id="identity"
                type="text"
                className="input"
                placeholder="usuario@hotel.com"
                {...register('identity')}
              />
              {errors.identity && (
                <p className="text-error-600 text-sm mt-1">{errors.identity.message}</p>
              )}
            </div>

            <div>
              <label htmlFor="password" className="block text-sm font-medium text-gray-700 mb-1">
                Contraseña
              </label>
              <input
                id="password"
                type="password"
                className="input"
                placeholder="••••••••"
                {...register('password')}
              />
              {errors.password && (
                <p className="text-error-600 text-sm mt-1">{errors.password.message}</p>
              )}
            </div>

            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <input
                  id="remember"
                  type="checkbox"
                  className="w-4 h-4 text-primary-600 border-gray-300 rounded focus:ring-primary-500"
                />
                <label htmlFor="remember" className="ml-2 text-sm text-gray-700">
                  Recordarme
                </label>
              </div>

              <Link
                to="/auth/recover"
                className="text-sm text-primary-600 hover:text-primary-700"
              >
                ¿Olvidaste tu contraseña?
              </Link>
            </div>

            <button
              type="submit"
              disabled={isSubmitting}
              className="btn-primary w-full"
            >
              {isSubmitting ? 'Iniciando sesión...' : 'Iniciar Sesión'}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};
```

**Tareas:**
- [ ] Crear página de login con react-hook-form
- [ ] Usar zodResolver con loginSchema
- [ ] Mostrar errores de validación
- [ ] Mostrar errores de servidor
- [ ] Deshabilitar botón mientras se envía
- [ ] Redirigir a /dashboard después de login
- [ ] Agregar link a recuperar contraseña
- [ ] Aplicar estilos con Tailwind

#### **Página de Cambiar Contraseña**

**Archivo:** `src/pages/auth/ChangePasswordPage.tsx`

```typescript
import React from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '@/contexts/AuthContext';
import { changePasswordSchema, ChangePasswordFormData } from '@/schemas/auth.schema';
import { CheckCircle, AlertTriangle } from 'lucide-react';

export const ChangePasswordPage: React.FC = () => {
  const navigate = useNavigate();
  const { changePassword } = useAuth();
  const [error, setError] = React.useState<string | null>(null);
  const [success, setSuccess] = React.useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm<ChangePasswordFormData>({
    resolver: zodResolver(changePasswordSchema),
  });

  const onSubmit = async (data: ChangePasswordFormData) => {
    try {
      setError(null);
      setSuccess(false);
      await changePassword({
        currentPassword: data.currentPassword,
        newPassword: data.newPassword,
      });
      setSuccess(true);
      reset();

      setTimeout(() => {
        navigate('/dashboard');
      }, 2000);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Error al cambiar contraseña');
    }
  };

  return (
    <div className="max-w-2xl mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6">Cambiar Contraseña</h1>

      {success && (
        <div className="bg-success-50 border-l-4 border-success-500 p-4 rounded-r-md mb-6">
          <div className="flex items-start">
            <CheckCircle className="text-success-500 mt-0.5 mr-3" size={20} />
            <p className="text-sm text-success-700">
              Contraseña cambiada exitosamente. Redirigiendo...
            </p>
          </div>
        </div>
      )}

      {error && (
        <div className="bg-error-50 border-l-4 border-error-500 p-4 rounded-r-md mb-6">
          <div className="flex items-start">
            <AlertTriangle className="text-error-500 mt-0.5 mr-3" size={20} />
            <p className="text-sm text-error-700">{error}</p>
          </div>
        </div>
      )}

      <div className="card p-6">
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div>
            <label htmlFor="currentPassword" className="block text-sm font-medium text-gray-700 mb-1">
              Contraseña Actual
            </label>
            <input
              id="currentPassword"
              type="password"
              className="input"
              {...register('currentPassword')}
            />
            {errors.currentPassword && (
              <p className="text-error-600 text-sm mt-1">{errors.currentPassword.message}</p>
            )}
          </div>

          <div>
            <label htmlFor="newPassword" className="block text-sm font-medium text-gray-700 mb-1">
              Nueva Contraseña
            </label>
            <input
              id="newPassword"
              type="password"
              className="input"
              {...register('newPassword')}
            />
            {errors.newPassword && (
              <p className="text-error-600 text-sm mt-1">{errors.newPassword.message}</p>
            )}
            <p className="text-sm text-gray-500 mt-1">
              Mínimo 8 caracteres con mayúsculas, minúsculas, números y símbolos
            </p>
          </div>

          <div>
            <label htmlFor="confirmPassword" className="block text-sm font-medium text-gray-700 mb-1">
              Confirmar Nueva Contraseña
            </label>
            <input
              id="confirmPassword"
              type="password"
              className="input"
              {...register('confirmPassword')}
            />
            {errors.confirmPassword && (
              <p className="text-error-600 text-sm mt-1">{errors.confirmPassword.message}</p>
            )}
          </div>

          <div className="flex gap-3">
            <button
              type="button"
              onClick={() => navigate(-1)}
              className="btn-secondary flex-1"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isSubmitting}
              className="btn-primary flex-1"
            >
              {isSubmitting ? 'Cambiando...' : 'Cambiar Contraseña'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
```

**Tareas:**
- [ ] Crear página de cambiar contraseña
- [ ] Validar con changePasswordSchema
- [ ] Mostrar mensaje de éxito
- [ ] Mostrar errores de validación y servidor
- [ ] Redirigir después de cambiar contraseña
- [ ] Aplicar estilos

#### **Página de Recuperar Contraseña**

**Archivo:** `src/pages/auth/RecoverPasswordPage.tsx`

*(Similar estructura, usar recoverRequestSchema)*

**Tareas:**
- [ ] Crear página de solicitar recuperación
- [ ] Validar email con recoverRequestSchema
- [ ] Mostrar mensaje de éxito (email enviado)
- [ ] Aplicar estilos

**Archivo:** `src/pages/auth/RecoverConfirmPage.tsx`

*(Similar estructura, usar recoverConfirmSchema)*

**Tareas:**
- [ ] Crear página de confirmar recuperación
- [ ] Leer token de URL query params
- [ ] Validar con recoverConfirmSchema
- [ ] Redirigir a login después de confirmar
- [ ] Aplicar estilos

---

## 4. SISTEMA DE PERMISOS

### 4.1 Types de Permisos

**Archivo:** `src/types/permissions.types.ts`

```typescript
export interface PermissionsContextValue {
  permissions: Set<string>;
  isLoading: boolean;
  hasPermission: (action: string) => boolean;
  hasAllPermissions: (actions: string[]) => boolean;
  hasAnyPermission: (actions: string[]) => boolean;
  refetchPermissions: () => Promise<void>;
}
```

**Tareas:**
- [ ] Definir PermissionsContextValue
- [ ] Exportar interface

### 4.2 Permissions Context

**Archivo:** `src/contexts/PermissionsContext.tsx`

```typescript
import React, { createContext, useContext, useState, useEffect } from 'react';
import { useQuery } from '@tanstack/react-query';
import { authApi } from '@/api/auth.api';
import { PermissionsContextValue } from '@/types/permissions.types';
import { getToken, setItem, getItem } from '@/utils/storage';
import { TOKEN_KEY, PERMISSIONS_CACHE_KEY } from '@/config/constants';

const PermissionsContext = createContext<PermissionsContextValue | undefined>(
  undefined
);

export const PermissionsProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [permissions, setPermissions] = useState<Set<string>>(new Set());
  const hasToken = !!getToken(TOKEN_KEY);

  // Query para obtener permisos del usuario
  const { data, isLoading, refetch } = useQuery({
    queryKey: ['permissions'],
    queryFn: async () => {
      const profile = await authApi.getProfile();
      // Asumiendo que el backend devuelve effectiveActions en el perfil
      // Si no, necesitaremos un endpoint específico
      return (profile as any).effectiveActions || [];
    },
    enabled: hasToken,
  });

  useEffect(() => {
    if (data) {
      const permsSet = new Set(data);
      setPermissions(permsSet);
      setItem(PERMISSIONS_CACHE_KEY, data);
    } else {
      // Cargar del cache
      const cached = getItem<string[]>(PERMISSIONS_CACHE_KEY);
      if (cached) {
        setPermissions(new Set(cached));
      }
    }
  }, [data]);

  const hasPermission = (action: string): boolean => {
    return permissions.has(action);
  };

  const hasAllPermissions = (actions: string[]): boolean => {
    return actions.every((action) => permissions.has(action));
  };

  const hasAnyPermission = (actions: string[]): boolean => {
    return actions.some((action) => permissions.has(action));
  };

  const refetchPermissions = async (): Promise<void> => {
    await refetch();
  };

  const value: PermissionsContextValue = {
    permissions,
    isLoading,
    hasPermission,
    hasAllPermissions,
    hasAnyPermission,
    refetchPermissions,
  };

  return (
    <PermissionsContext.Provider value={value}>
      {children}
    </PermissionsContext.Provider>
  );
};

export const usePermissions = (): PermissionsContextValue => {
  const context = useContext(PermissionsContext);
  if (context === undefined) {
    throw new Error('usePermissions must be used within a PermissionsProvider');
  }
  return context;
};
```

**Tareas:**
- [ ] Crear PermissionsContext
- [ ] Crear PermissionsProvider
- [ ] Implementar query para obtener permisos
- [ ] Guardar permisos en localStorage como cache
- [ ] Implementar hasPermission, hasAllPermissions, hasAnyPermission
- [ ] Crear custom hook usePermissions()
- [ ] Verificar que funciona correctamente

### 4.3 ProtectedRoute Component

**Archivo:** `src/routes/ProtectedRoute.tsx`

```typescript
import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '@/contexts/AuthContext';
import { usePermissions } from '@/contexts/PermissionsContext';

interface ProtectedRouteProps {
  requiredPermissions?: string[];
  requireAll?: boolean; // true = AND, false = OR
}

export const ProtectedRoute: React.FC<ProtectedRouteProps> = ({
  requiredPermissions = [],
  requireAll = true,
}) => {
  const { isAuthenticated, isLoading: authLoading } = useAuth();
  const { hasAllPermissions, hasAnyPermission, isLoading: permsLoading } = usePermissions();

  if (authLoading || permsLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-4 border-gray-200 border-t-primary-600"></div>
      </div>
    );
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (requiredPermissions.length > 0) {
    const hasAccess = requireAll
      ? hasAllPermissions(requiredPermissions)
      : hasAnyPermission(requiredPermissions);

    if (!hasAccess) {
      return <Navigate to="/forbidden" replace />;
    }
  }

  return <Outlet />;
};
```

**Tareas:**
- [ ] Crear ProtectedRoute component
- [ ] Verificar autenticación
- [ ] Verificar permisos requeridos
- [ ] Redirigir a /login si no autenticado
- [ ] Redirigir a /forbidden si no tiene permisos
- [ ] Mostrar spinner mientras carga
- [ ] Soportar requireAll (AND) y requireAny (OR)

### 4.4 Can Component (Renderizado Condicional)

**Archivo:** `src/components/auth/Can.tsx`

```typescript
import React from 'react';
import { usePermissions } from '@/contexts/PermissionsContext';

interface CanProps {
  perform: string | string[];
  requireAll?: boolean; // true = AND, false = OR
  children: React.ReactNode;
  fallback?: React.ReactNode;
}

/**
 * Componente para renderizado condicional basado en permisos
 *
 * Ejemplo:
 * <Can perform="users.create">
 *   <button>Crear Usuario</button>
 * </Can>
 *
 * <Can perform={['users.create', 'users.edit']} requireAll={false}>
 *   <button>Gestionar Usuarios</button>
 * </Can>
 */
export const Can: React.FC<CanProps> = ({
  perform,
  requireAll = true,
  children,
  fallback = null,
}) => {
  const { hasPermission, hasAllPermissions, hasAnyPermission } = usePermissions();

  const permissions = Array.isArray(perform) ? perform : [perform];

  let hasAccess: boolean;

  if (permissions.length === 1) {
    hasAccess = hasPermission(permissions[0]);
  } else {
    hasAccess = requireAll
      ? hasAllPermissions(permissions)
      : hasAnyPermission(permissions);
  }

  return hasAccess ? <>{children}</> : <>{fallback}</>;
};
```

**Tareas:**
- [ ] Crear componente Can
- [ ] Soportar string o array de permisos
- [ ] Soportar requireAll y requireAny
- [ ] Soportar fallback
- [ ] Documentar con ejemplos
- [ ] Verificar que funciona

---

## 5. GESTIÓN DE USUARIOS

### 5.1 Types de Usuarios

**Archivo:** `src/types/user.types.ts`

```typescript
export interface User {
  id: number;
  username: string;
  email: string;
  fullName?: string;
  isActive: boolean;
  lastLoginAt?: string;
  groups: Group[];
  actions: Action[];
  createdAt: string;
  updatedAt: string;
}

export interface CreateUserPayload {
  username: string;
  email: string;
  password?: string;
  fullName?: string;
  isActive?: boolean;
}

export interface UpdateUserPayload {
  username?: string;
  email?: string;
  fullName?: string;
  isActive?: boolean;
}

export interface SetUserGroupsPayload {
  groupKeys: string[];
}

export interface SetUserActionsPayload {
  actionKeys: string[];
}

export interface ResetPasswordPayload {
  newPassword: string;
}
```

**Tareas:**
- [ ] Definir interfaces de usuario
- [ ] Exportar tipos

### 5.2 Schemas de Usuario

**Archivo:** `src/schemas/user.schema.ts`

```typescript
import { z } from 'zod';

export const createUserSchema = z.object({
  username: z
    .string()
    .min(3, 'Mínimo 3 caracteres')
    .max(50, 'Máximo 50 caracteres')
    .regex(/^[a-zA-Z0-9_-]+$/, 'Solo letras, números, guiones y guiones bajos'),
  email: z.string().email('Email inválido'),
  password: z
    .string()
    .min(8, 'Mínimo 8 caracteres')
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]/,
      'Debe contener mayúsculas, minúsculas, números y símbolos'
    )
    .optional(),
  fullName: z.string().max(255, 'Máximo 255 caracteres').optional(),
  isActive: z.boolean().optional(),
});

export const updateUserSchema = createUserSchema.partial().omit({ password: true });

export const setUserGroupsSchema = z.object({
  groupKeys: z.array(z.string()).min(1, 'Debe seleccionar al menos un grupo'),
});

export const setUserActionsSchema = z.object({
  actionKeys: z.array(z.string()),
});

export const resetPasswordSchema = z.object({
  newPassword: z
    .string()
    .min(8, 'Mínimo 8 caracteres')
    .regex(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]/,
      'Debe contener mayúsculas, minúsculas, números y símbolos'
    ),
});

export type CreateUserFormData = z.infer<typeof createUserSchema>;
export type UpdateUserFormData = z.infer<typeof updateUserSchema>;
export type SetUserGroupsFormData = z.infer<typeof setUserGroupsSchema>;
export type SetUserActionsFormData = z.infer<typeof setUserActionsSchema>;
export type ResetPasswordFormData = z.infer<typeof resetPasswordSchema>;
```

**Tareas:**
- [ ] Crear schemas de validación
- [ ] Exportar tipos inferidos

### 5.3 API de Usuarios

**Archivo:** `src/api/users.api.ts`

```typescript
import api from './axios.config';
import {
  User,
  CreateUserPayload,
  UpdateUserPayload,
  SetUserGroupsPayload,
  SetUserActionsPayload,
  ResetPasswordPayload,
} from '@/types/user.types';

export const usersApi = {
  /**
   * Listar todos los usuarios
   */
  getAll: async (): Promise<User[]> => {
    const response = await api.get<User[]>('/users');
    return response.data;
  },

  /**
   * Obtener un usuario por ID
   */
  getById: async (id: number): Promise<User> => {
    const response = await api.get<User>(`/users/${id}`);
    return response.data;
  },

  /**
   * Crear un nuevo usuario
   */
  create: async (payload: CreateUserPayload): Promise<User> => {
    const response = await api.post<User>('/users', payload);
    return response.data;
  },

  /**
   * Actualizar un usuario
   */
  update: async (id: number, payload: UpdateUserPayload): Promise<User> => {
    const response = await api.patch<User>(`/users/${id}`, payload);
    return response.data;
  },

  /**
   * Eliminar un usuario
   */
  delete: async (id: number): Promise<void> => {
    await api.delete(`/users/${id}`);
  },

  /**
   * Asignar grupos a un usuario
   */
  setGroups: async (id: number, payload: SetUserGroupsPayload): Promise<User> => {
    const response = await api.patch<User>(`/users/${id}/groups`, payload);
    return response.data;
  },

  /**
   * Asignar acciones a un usuario
   */
  setActions: async (id: number, payload: SetUserActionsPayload): Promise<User> => {
    const response = await api.patch<User>(`/users/${id}/actions`, payload);
    return response.data;
  },

  /**
   * Resetear contraseña de un usuario (admin)
   */
  resetPassword: async (id: number, payload: ResetPasswordPayload): Promise<void> => {
    await api.post(`/users/${id}/reset-password`, payload);
  },
};
```

**Tareas:**
- [ ] Crear usersApi con todos los métodos
- [ ] Verificar que funciona con el backend

### 5.4 Custom Hooks de Usuarios

**Archivo:** `src/hooks/useUsers.ts`

```typescript
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { usersApi } from '@/api/users.api';
import {
  CreateUserPayload,
  UpdateUserPayload,
  SetUserGroupsPayload,
  SetUserActionsPayload,
  ResetPasswordPayload,
} from '@/types/user.types';

export const useUsers = () => {
  const queryClient = useQueryClient();

  // Query: Listar usuarios
  const { data: users, isLoading, error } = useQuery({
    queryKey: ['users'],
    queryFn: usersApi.getAll,
  });

  // Query: Obtener usuario por ID
  const useUser = (id: number) =>
    useQuery({
      queryKey: ['users', id],
      queryFn: () => usersApi.getById(id),
      enabled: !!id,
    });

  // Mutation: Crear usuario
  const createMutation = useMutation({
    mutationFn: usersApi.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // Mutation: Actualizar usuario
  const updateMutation = useMutation({
    mutationFn: ({ id, payload }: { id: number; payload: UpdateUserPayload }) =>
      usersApi.update(id, payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // Mutation: Eliminar usuario
  const deleteMutation = useMutation({
    mutationFn: usersApi.delete,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // Mutation: Asignar grupos
  const setGroupsMutation = useMutation({
    mutationFn: ({ id, payload }: { id: number; payload: SetUserGroupsPayload }) =>
      usersApi.setGroups(id, payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // Mutation: Asignar acciones
  const setActionsMutation = useMutation({
    mutationFn: ({ id, payload }: { id: number; payload: SetUserActionsPayload }) =>
      usersApi.setActions(id, payload),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });

  // Mutation: Resetear contraseña
  const resetPasswordMutation = useMutation({
    mutationFn: ({ id, payload }: { id: number; payload: ResetPasswordPayload }) =>
      usersApi.resetPassword(id, payload),
  });

  return {
    users,
    isLoading,
    error,
    useUser,
    createUser: createMutation.mutateAsync,
    updateUser: updateMutation.mutateAsync,
    deleteUser: deleteMutation.mutateAsync,
    setGroups: setGroupsMutation.mutateAsync,
    setActions: setActionsMutation.mutateAsync,
    resetPassword: resetPasswordMutation.mutateAsync,
    isCreating: createMutation.isPending,
    isUpdating: updateMutation.isPending,
    isDeleting: deleteMutation.isPending,
  };
};
```

**Tareas:**
- [ ] Crear custom hook useUsers
- [ ] Implementar queries y mutations
- [ ] Invalidar caché después de mutaciones
- [ ] Exportar estados de loading
- [ ] Verificar que funciona

### 5.5 Páginas de Usuarios

#### **Lista de Usuarios**

**Archivo:** `src/pages/users/UsersListPage.tsx`

```typescript
import React from 'react';
import { Link } from 'react-router-dom';
import { useUsers } from '@/hooks/useUsers';
import { Can } from '@/components/auth/Can';
import { Plus, Edit, Trash2, Shield } from 'lucide-react';

export const UsersListPage: React.FC = () => {
  const { users, isLoading, deleteUser } = useUsers();

  const handleDelete = async (id: number) => {
    if (window.confirm('¿Está seguro de eliminar este usuario?')) {
      await deleteUser(id);
    }
  };

  if (isLoading) {
    return <div>Cargando...</div>;
  }

  return (
    <div className="px-4 py-8">
      <div className="flex items-center justify-between mb-6">
        <h1 className="text-3xl font-bold">Usuarios</h1>

        <Can perform="config.usuarios.crear">
          <Link to="/users/create" className="btn-primary flex items-center gap-2">
            <Plus size={20} />
            Crear Usuario
          </Link>
        </Can>
      </div>

      <div className="card overflow-hidden">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Usuario
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Email
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Estado
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Grupos
              </th>
              <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                Acciones
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {users?.map((user) => (
              <tr key={user.id} className="hover:bg-gray-50">
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="text-sm font-medium text-gray-900">{user.username}</div>
                  <div className="text-sm text-gray-500">{user.fullName}</div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="text-sm text-gray-900">{user.email}</div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <span
                    className={`inline-flex items-center px-3 py-1 text-sm font-medium rounded-full ${
                      user.isActive
                        ? 'bg-success-100 text-success-700'
                        : 'bg-error-100 text-error-700'
                    }`}
                  >
                    {user.isActive ? 'Activo' : 'Inactivo'}
                  </span>
                </td>
                <td className="px-6 py-4">
                  <div className="flex flex-wrap gap-1">
                    {user.groups?.map((group) => (
                      <span
                        key={group.id}
                        className="inline-flex items-center px-2 py-1 text-xs font-medium bg-gray-100 text-gray-700 rounded"
                      >
                        {group.name}
                      </span>
                    ))}
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                  <div className="flex items-center justify-end gap-2">
                    <Can perform="config.usuarios.modificar">
                      <Link
                        to={`/users/${user.id}/edit`}
                        className="text-primary-600 hover:text-primary-900"
                      >
                        <Edit size={18} />
                      </Link>
                    </Can>

                    <Can perform="config.usuarios.asignarGrupos">
                      <Link
                        to={`/users/${user.id}/permissions`}
                        className="text-accent-600 hover:text-accent-900"
                      >
                        <Shield size={18} />
                      </Link>
                    </Can>

                    <Can perform="config.usuarios.eliminar">
                      <button
                        onClick={() => handleDelete(user.id)}
                        className="text-error-600 hover:text-error-900"
                      >
                        <Trash2 size={18} />
                      </button>
                    </Can>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
```

**Tareas:**
- [ ] Crear página de lista de usuarios
- [ ] Mostrar tabla con usuarios
- [ ] Mostrar badges de grupos
- [ ] Proteger acciones con componente Can
- [ ] Implementar confirmación de eliminación
- [ ] Aplicar estilos

#### **Crear/Editar Usuario**

**Archivo:** `src/pages/users/UserFormPage.tsx`

*(Implementar con react-hook-form + zodResolver)*

**Tareas:**
- [ ] Crear página de formulario (crear + editar)
- [ ] Usar react-hook-form con validación Zod
- [ ] Mostrar errores de validación
- [ ] Redirigir después de crear/actualizar
- [ ] Aplicar estilos

#### **Gestionar Permisos del Usuario**

**Archivo:** `src/pages/users/UserPermissionsPage.tsx`

*(Selección múltiple de grupos y acciones)*

**Tareas:**
- [ ] Crear página de permisos
- [ ] Mostrar grupos disponibles con checkboxes
- [ ] Mostrar acciones disponibles con checkboxes
- [ ] Implementar setGroups y setActions
- [ ] Aplicar estilos

---

## 6. GESTIÓN DE GRUPOS

### 6.1 Types de Grupos

**Archivo:** `src/types/group.types.ts`

```typescript
export interface Group {
  id: number;
  key: string;
  name: string;
  description?: string;
  actions: Action[];
  children: Group[];
  createdAt: string;
  updatedAt: string;
}

export interface CreateGroupPayload {
  key: string;
  name: string;
  description?: string;
}

export interface UpdateGroupPayload {
  key?: string;
  name?: string;
  description?: string;
}

export interface SetGroupActionsPayload {
  actionKeys: string[];
}

export interface SetGroupChildrenPayload {
  childGroupKeys: string[];
}
```

**Tareas:**
- [ ] Definir interfaces de grupos
- [ ] Exportar tipos

### 6.2 Schemas de Grupos

**Archivo:** `src/schemas/group.schema.ts`

```typescript
import { z } from 'zod';

export const createGroupSchema = z.object({
  key: z
    .string()
    .min(1, 'Clave es requerida')
    .max(100, 'Máximo 100 caracteres')
    .regex(/^[a-z0-9._-]+$/, 'Solo minúsculas, números, puntos, guiones y guiones bajos'),
  name: z.string().min(1, 'Nombre es requerido').max(255, 'Máximo 255 caracteres'),
  description: z.string().max(1000, 'Máximo 1000 caracteres').optional(),
});

export const updateGroupSchema = createGroupSchema.partial();

export const setGroupActionsSchema = z.object({
  actionKeys: z.array(z.string()),
});

export const setGroupChildrenSchema = z.object({
  childGroupKeys: z.array(z.string()),
});

export type CreateGroupFormData = z.infer<typeof createGroupSchema>;
export type UpdateGroupFormData = z.infer<typeof updateGroupSchema>;
export type SetGroupActionsFormData = z.infer<typeof setGroupActionsSchema>;
export type SetGroupChildrenFormData = z.infer<typeof setGroupChildrenSchema>;
```

**Tareas:**
- [ ] Crear schemas de validación
- [ ] Exportar tipos inferidos

### 6.3 API de Grupos

**Archivo:** `src/api/groups.api.ts`

*(Similar estructura a users.api.ts)*

**Tareas:**
- [ ] Crear groupsApi con getAll, getById, create, update, delete
- [ ] Implementar setActions y setChildren
- [ ] Implementar getEffectiveActions
- [ ] Verificar que funciona

### 6.4 Custom Hooks de Grupos

**Archivo:** `src/hooks/useGroups.ts`

*(Similar estructura a useUsers.ts)*

**Tareas:**
- [ ] Crear custom hook useGroups
- [ ] Implementar queries y mutations
- [ ] Invalidar caché
- [ ] Exportar estados

### 6.5 Páginas de Grupos

#### **Lista de Grupos**

**Archivo:** `src/pages/groups/GroupsListPage.tsx`

*(Similar a UsersListPage)*

**Tareas:**
- [ ] Crear página de lista de grupos
- [ ] Mostrar tabla con grupos
- [ ] Mostrar acciones con Can
- [ ] Aplicar estilos

#### **Crear/Editar Grupo**

**Archivo:** `src/pages/groups/GroupFormPage.tsx`

**Tareas:**
- [ ] Crear formulario de grupo
- [ ] Validar con Zod
- [ ] Aplicar estilos

#### **Gestionar Permisos del Grupo**

**Archivo:** `src/pages/groups/GroupPermissionsPage.tsx`

**Tareas:**
- [ ] Crear página de permisos del grupo
- [ ] Selector de acciones (checkboxes)
- [ ] Selector de grupos hijos (checkboxes)
- [ ] Implementar setActions y setChildren
- [ ] Validar anti-ciclos en el frontend (opcional)
- [ ] Aplicar estilos

---

## 7. GESTIÓN DE ACCIONES

### 7.1 Types de Acciones

**Archivo:** `src/types/action.types.ts`

```typescript
export interface Action {
  id: number;
  key: string;
  name: string;
  description?: string;
  area: string;
  createdAt: string;
  updatedAt: string;
}

export interface CreateActionPayload {
  key: string;
  name: string;
  description?: string;
  area?: string;
}

export interface UpdateActionPayload {
  key?: string;
  name?: string;
  description?: string;
  area?: string;
}
```

**Tareas:**
- [ ] Definir interfaces de acciones
- [ ] Exportar tipos

### 7.2 Schemas de Acciones

**Archivo:** `src/schemas/action.schema.ts`

```typescript
import { z } from 'zod';

export const createActionSchema = z.object({
  key: z
    .string()
    .min(1, 'Clave es requerida')
    .max(100, 'Máximo 100 caracteres')
    .regex(/^[a-z0-9._-]+$/, 'Solo minúsculas, números, puntos, guiones y guiones bajos'),
  name: z.string().min(1, 'Nombre es requerido').max(255, 'Máximo 255 caracteres'),
  description: z.string().max(1000, 'Máximo 1000 caracteres').optional(),
  area: z.string().max(50, 'Máximo 50 caracteres').optional(),
});

export const updateActionSchema = createActionSchema.partial();

export type CreateActionFormData = z.infer<typeof createActionSchema>;
export type UpdateActionFormData = z.infer<typeof updateActionSchema>;
```

**Tareas:**
- [ ] Crear schemas de validación
- [ ] Exportar tipos

### 7.3 API de Acciones

**Archivo:** `src/api/actions.api.ts`

*(Similar estructura a users.api.ts, sin setGroups/setActions)*

**Tareas:**
- [ ] Crear actionsApi
- [ ] Implementar CRUD básico
- [ ] Implementar getByArea
- [ ] Verificar que funciona

### 7.4 Custom Hooks de Acciones

**Archivo:** `src/hooks/useActions.ts`

**Tareas:**
- [ ] Crear custom hook useActions
- [ ] Implementar queries y mutations
- [ ] Exportar estados

### 7.5 Páginas de Acciones

#### **Lista de Acciones**

**Archivo:** `src/pages/actions/ActionsListPage.tsx`

**Tareas:**
- [ ] Crear página de lista
- [ ] Agrupar por área
- [ ] Mostrar tabla
- [ ] Proteger acciones
- [ ] Aplicar estilos

#### **Crear/Editar Acción**

**Archivo:** `src/pages/actions/ActionFormPage.tsx`

**Tareas:**
- [ ] Crear formulario
- [ ] Validar con Zod
- [ ] Aplicar estilos

---

## 8. LAYOUT Y NAVEGACIÓN

### 8.1 Layout Principal

**Archivo:** `src/components/layout/MainLayout.tsx`

```typescript
import React from 'react';
import { Outlet } from 'react-router-dom';
import { Navbar } from './Navbar';
import { Sidebar } from './Sidebar';

export const MainLayout: React.FC = () => {
  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <div className="flex">
        <Sidebar />
        <main className="flex-1 p-8">
          <Outlet />
        </main>
      </div>
    </div>
  );
};
```

**Tareas:**
- [ ] Crear MainLayout con Navbar + Sidebar + Outlet
- [ ] Aplicar estilos

### 8.2 Navbar

**Archivo:** `src/components/layout/Navbar.tsx`

```typescript
import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '@/contexts/AuthContext';
import { LogOut, User, Settings } from 'lucide-react';

export const Navbar: React.FC = () => {
  const { user, logout } = useAuth();

  const handleLogout = async () => {
    await logout();
  };

  return (
    <nav className="bg-white shadow-sm border-b border-gray-200">
      <div className="mx-auto px-4">
        <div className="flex items-center justify-between h-16">
          <Link to="/dashboard" className="text-2xl font-bold text-primary-600">
            MyHotelFlow
          </Link>

          <div className="flex items-center gap-4">
            <span className="text-sm text-gray-700">{user?.fullName || user?.username}</span>

            <Link to="/profile" className="text-gray-500 hover:text-primary-600">
              <User size={20} />
            </Link>

            <Link to="/settings" className="text-gray-500 hover:text-primary-600">
              <Settings size={20} />
            </Link>

            <button
              onClick={handleLogout}
              className="text-gray-500 hover:text-error-600"
            >
              <LogOut size={20} />
            </button>
          </div>
        </div>
      </div>
    </nav>
  );
};
```

**Tareas:**
- [ ] Crear Navbar con logo y usuario
- [ ] Mostrar botón de logout
- [ ] Aplicar estilos

### 8.3 Sidebar

**Archivo:** `src/components/layout/Sidebar.tsx`

```typescript
import React from 'react';
import { NavLink } from 'react-router-dom';
import { Can } from '@/components/auth/Can';
import {
  LayoutDashboard,
  Users,
  Shield,
  Key,
  Calendar,
  BedDouble,
  FileText,
  CreditCard,
} from 'lucide-react';

export const Sidebar: React.FC = () => {
  const linkClass = ({ isActive }: { isActive: boolean }) =>
    `flex items-center gap-3 px-4 py-3 rounded-lg transition-colors ${
      isActive
        ? 'bg-primary-100 text-primary-700 font-medium'
        : 'text-gray-700 hover:bg-gray-100'
    }`;

  return (
    <aside className="w-64 bg-white border-r border-gray-200 min-h-[calc(100vh-4rem)]">
      <nav className="p-4 space-y-2">
        <NavLink to="/dashboard" className={linkClass}>
          <LayoutDashboard size={20} />
          Dashboard
        </NavLink>

        <div className="pt-4">
          <p className="px-4 text-xs font-semibold text-gray-400 uppercase mb-2">
            Configuración
          </p>

          <Can perform="config.usuarios.listar">
            <NavLink to="/users" className={linkClass}>
              <Users size={20} />
              Usuarios
            </NavLink>
          </Can>

          <Can perform="config.grupos.listar">
            <NavLink to="/groups" className={linkClass}>
              <Shield size={20} />
              Grupos
            </NavLink>
          </Can>

          <Can perform="config.acciones.listar">
            <NavLink to="/actions" className={linkClass}>
              <Key size={20} />
              Acciones
            </NavLink>
          </Can>
        </div>

        {/* Otros módulos */}
        <div className="pt-4">
          <p className="px-4 text-xs font-semibold text-gray-400 uppercase mb-2">
            Operaciones
          </p>

          <Can perform="reservas.listar">
            <NavLink to="/reservations" className={linkClass}>
              <Calendar size={20} />
              Reservas
            </NavLink>
          </Can>

          <Can perform="habitaciones.listar">
            <NavLink to="/rooms" className={linkClass}>
              <BedDouble size={20} />
              Habitaciones
            </NavLink>
          </Can>

          <Can perform="comprobantes.ver">
            <NavLink to="/invoices" className={linkClass}>
              <FileText size={20} />
              Comprobantes
            </NavLink>
          </Can>

          <Can perform="checkout.registrarPago">
            <NavLink to="/payments" className={linkClass}>
              <CreditCard size={20} />
              Pagos
            </NavLink>
          </Can>
        </div>
      </nav>
    </aside>
  );
};
```

**Tareas:**
- [ ] Crear Sidebar con navegación
- [ ] Proteger links con componente Can
- [ ] Aplicar estilos activos con NavLink
- [ ] Agrupar por secciones

### 8.4 Rutas de la Aplicación

**Archivo:** `src/routes/AppRoutes.tsx`

```typescript
import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { ProtectedRoute } from './ProtectedRoute';
import { MainLayout } from '@/components/layout/MainLayout';

// Auth pages
import { LoginPage } from '@/pages/auth/LoginPage';
import { ChangePasswordPage } from '@/pages/auth/ChangePasswordPage';
import { RecoverPasswordPage } from '@/pages/auth/RecoverPasswordPage';
import { RecoverConfirmPage } from '@/pages/auth/RecoverConfirmPage';

// Dashboard
import { DashboardPage } from '@/pages/dashboard/DashboardPage';

// Users
import { UsersListPage } from '@/pages/users/UsersListPage';
import { UserFormPage } from '@/pages/users/UserFormPage';
import { UserPermissionsPage } from '@/pages/users/UserPermissionsPage';

// Groups
import { GroupsListPage } from '@/pages/groups/GroupsListPage';
import { GroupFormPage } from '@/pages/groups/GroupFormPage';
import { GroupPermissionsPage } from '@/pages/groups/GroupPermissionsPage';

// Actions
import { ActionsListPage } from '@/pages/actions/ActionsListPage';
import { ActionFormPage } from '@/pages/actions/ActionFormPage';

// Error pages
import { NotFoundPage } from '@/pages/errors/NotFoundPage';
import { ForbiddenPage } from '@/pages/errors/ForbiddenPage';

export const AppRoutes: React.FC = () => {
  return (
    <BrowserRouter>
      <Routes>
        {/* Public routes */}
        <Route path="/login" element={<LoginPage />} />
        <Route path="/auth/recover" element={<RecoverPasswordPage />} />
        <Route path="/auth/recover/confirm" element={<RecoverConfirmPage />} />

        {/* Protected routes */}
        <Route element={<ProtectedRoute />}>
          <Route element={<MainLayout />}>
            <Route path="/dashboard" element={<DashboardPage />} />
            <Route path="/profile" element={<div>Profile Page (TODO)</div>} />
            <Route path="/settings" element={<div>Settings Page (TODO)</div>} />
            <Route path="/change-password" element={<ChangePasswordPage />} />

            {/* Users */}
            <Route path="/users" element={<UsersListPage />} />
            <Route path="/users/create" element={<UserFormPage />} />
            <Route path="/users/:id/edit" element={<UserFormPage />} />
            <Route path="/users/:id/permissions" element={<UserPermissionsPage />} />

            {/* Groups */}
            <Route path="/groups" element={<GroupsListPage />} />
            <Route path="/groups/create" element={<GroupFormPage />} />
            <Route path="/groups/:id/edit" element={<GroupFormPage />} />
            <Route path="/groups/:id/permissions" element={<GroupPermissionsPage />} />

            {/* Actions */}
            <Route path="/actions" element={<ActionsListPage />} />
            <Route path="/actions/create" element={<ActionFormPage />} />
            <Route path="/actions/:id/edit" element={<ActionFormPage />} />

            {/* TODO: Other modules */}
            <Route path="/reservations" element={<div>Reservations (TODO)</div>} />
            <Route path="/rooms" element={<div>Rooms (TODO)</div>} />
            <Route path="/invoices" element={<div>Invoices (TODO)</div>} />
            <Route path="/payments" element={<div>Payments (TODO)</div>} />
          </Route>
        </Route>

        {/* Error routes */}
        <Route path="/forbidden" element={<ForbiddenPage />} />
        <Route path="/404" element={<NotFoundPage />} />

        {/* Redirects */}
        <Route path="/" element={<Navigate to="/dashboard" replace />} />
        <Route path="*" element={<Navigate to="/404" replace />} />
      </Routes>
    </BrowserRouter>
  );
};
```

**Tareas:**
- [ ] Crear AppRoutes con BrowserRouter
- [ ] Configurar rutas públicas y protegidas
- [ ] Usar ProtectedRoute para rutas autenticadas
- [ ] Configurar rutas anidadas con MainLayout
- [ ] Agregar redirects
- [ ] Verificar que funciona

### 8.5 App.tsx

**Archivo:** `src/App.tsx`

```typescript
import React from 'react';
import { AppRoutes } from './routes/AppRoutes';
import { AuthProvider } from './contexts/AuthContext';
import { PermissionsProvider } from './contexts/PermissionsContext';

function App() {
  return (
    <AuthProvider>
      <PermissionsProvider>
        <AppRoutes />
      </PermissionsProvider>
    </AuthProvider>
  );
}

export default App;
```

**Tareas:**
- [ ] Envolver AppRoutes con providers
- [ ] Verificar que funciona

---

## 9. COMPONENTES REUTILIZABLES

### 9.1 Button Component

**Archivo:** `src/components/ui/Button.tsx`

```typescript
import React from 'react';
import { cn } from '@/utils/cn';

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: 'primary' | 'secondary' | 'ghost' | 'danger';
  size?: 'sm' | 'md' | 'lg';
  isLoading?: boolean;
}

export const Button: React.FC<ButtonProps> = ({
  children,
  variant = 'primary',
  size = 'md',
  isLoading = false,
  className,
  disabled,
  ...props
}) => {
  const baseStyles = 'font-medium rounded-lg transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed';

  const variants = {
    primary: 'bg-primary-600 hover:bg-primary-700 text-white focus:ring-primary-500',
    secondary: 'bg-white hover:bg-gray-50 text-primary-600 border-2 border-primary-600',
    ghost: 'text-gray-700 hover:text-primary-600 hover:bg-gray-100',
    danger: 'bg-error-600 hover:bg-error-700 text-white focus:ring-error-500',
  };

  const sizes = {
    sm: 'px-4 py-2 text-sm',
    md: 'px-6 py-3',
    lg: 'px-8 py-4 text-lg',
  };

  return (
    <button
      className={cn(baseStyles, variants[variant], sizes[size], className)}
      disabled={disabled || isLoading}
      {...props}
    >
      {isLoading ? (
        <div className="flex items-center gap-2">
          <div className="animate-spin rounded-full h-4 w-4 border-2 border-white border-t-transparent"></div>
          <span>Cargando...</span>
        </div>
      ) : (
        children
      )}
    </button>
  );
};
```

**Tareas:**
- [ ] Crear Button component
- [ ] Soportar variantes (primary, secondary, ghost, danger)
- [ ] Soportar tamaños (sm, md, lg)
- [ ] Mostrar spinner cuando isLoading
- [ ] Usar cn() para merge de clases

### 9.2 Input Component

**Archivo:** `src/components/ui/Input.tsx`

```typescript
import React from 'react';
import { cn } from '@/utils/cn';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  error?: string;
}

export const Input = React.forwardRef<HTMLInputElement, InputProps>(
  ({ label, error, className, ...props }, ref) => {
    return (
      <div className="w-full">
        {label && (
          <label htmlFor={props.id} className="block text-sm font-medium text-gray-700 mb-1">
            {label}
          </label>
        )}
        <input
          ref={ref}
          className={cn(
            'input',
            error && 'border-error-500 focus:ring-error-500',
            className
          )}
          {...props}
        />
        {error && <p className="text-error-600 text-sm mt-1">{error}</p>}
      </div>
    );
  }
);

Input.displayName = 'Input';
```

**Tareas:**
- [ ] Crear Input component con forwardRef
- [ ] Soportar label y error
- [ ] Cambiar estilos cuando hay error
- [ ] Exportar

### 9.3 Modal Component

**Archivo:** `src/components/ui/Modal.tsx`

```typescript
import React from 'react';
import { X } from 'lucide-react';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  children: React.ReactNode;
}

export const Modal: React.FC<ModalProps> = ({ isOpen, onClose, title, children }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4">
      {/* Overlay */}
      <div
        className="fixed inset-0 bg-black bg-opacity-50 backdrop-blur-sm"
        onClick={onClose}
      ></div>

      {/* Modal */}
      <div className="bg-white rounded-lg shadow-2xl max-w-2xl w-full p-6 relative z-10">
        <div className="flex items-center justify-between mb-4">
          <h2 className="text-2xl font-bold">{title}</h2>
          <button
            onClick={onClose}
            className="text-gray-500 hover:text-gray-700"
          >
            <X size={24} />
          </button>
        </div>
        {children}
      </div>
    </div>
  );
};
```

**Tareas:**
- [ ] Crear Modal component
- [ ] Agregar overlay con blur
- [ ] Cerrar al hacer click en overlay
- [ ] Exportar

### 9.4 Spinner Component

**Archivo:** `src/components/ui/Spinner.tsx`

```typescript
import React from 'react';
import { cn } from '@/utils/cn';

interface SpinnerProps {
  size?: 'sm' | 'md' | 'lg';
  className?: string;
}

export const Spinner: React.FC<SpinnerProps> = ({ size = 'md', className }) => {
  const sizes = {
    sm: 'h-4 w-4 border-2',
    md: 'h-8 w-8 border-4',
    lg: 'h-12 w-12 border-4',
  };

  return (
    <div
      className={cn(
        'animate-spin rounded-full border-gray-200 border-t-primary-600',
        sizes[size],
        className
      )}
    ></div>
  );
};
```

**Tareas:**
- [ ] Crear Spinner component
- [ ] Soportar tamaños
- [ ] Exportar

### 9.5 Alert Component

**Archivo:** `src/components/ui/Alert.tsx`

```typescript
import React from 'react';
import { AlertTriangle, CheckCircle, Info, XCircle } from 'lucide-react';
import { cn } from '@/utils/cn';

interface AlertProps {
  type: 'success' | 'error' | 'warning' | 'info';
  title?: string;
  message: string;
  className?: string;
}

export const Alert: React.FC<AlertProps> = ({ type, title, message, className }) => {
  const icons = {
    success: <CheckCircle className="text-success-500" size={20} />,
    error: <XCircle className="text-error-500" size={20} />,
    warning: <AlertTriangle className="text-warning-500" size={20} />,
    info: <Info className="text-info-500" size={20} />,
  };

  const styles = {
    success: 'bg-success-50 border-success-500',
    error: 'bg-error-50 border-error-500',
    warning: 'bg-warning-50 border-warning-500',
    info: 'bg-info-50 border-info-500',
  };

  const textStyles = {
    success: 'text-success-800',
    error: 'text-error-800',
    warning: 'text-warning-800',
    info: 'text-info-800',
  };

  return (
    <div className={cn('border-l-4 p-4 rounded-r-md', styles[type], className)}>
      <div className="flex items-start">
        <div className="mt-0.5">{icons[type]}</div>
        <div className="ml-3">
          {title && (
            <h3 className={cn('text-sm font-medium', textStyles[type])}>
              {title}
            </h3>
          )}
          <p className={cn('text-sm', textStyles[type], title && 'mt-1')}>
            {message}
          </p>
        </div>
      </div>
    </div>
  );
};
```

**Tareas:**
- [ ] Crear Alert component
- [ ] Soportar tipos (success, error, warning, info)
- [ ] Mostrar icono según tipo
- [ ] Exportar

---

## 10. TESTING

### 10.1 Configurar Vitest

```bash
npm install -D vitest @testing-library/react @testing-library/jest-dom @testing-library/user-event
npm install -D jsdom @vitejs/plugin-react
```

**Archivo:** `vite.config.ts`

```typescript
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  test: {
    globals: true,
    environment: 'jsdom',
    setupFiles: './src/test/setup.ts',
  },
  resolve: {
    alias: {
      '@': '/src',
    },
  },
});
```

**Archivo:** `src/test/setup.ts`

```typescript
import '@testing-library/jest-dom';
```

**Tareas:**
- [ ] Instalar Vitest y testing-library
- [ ] Configurar vite.config.ts
- [ ] Crear setup.ts
- [ ] Verificar que funciona

### 10.2 Test Ejemplos

**Archivo:** `src/components/ui/Button.test.tsx`

```typescript
import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import { Button } from './Button';

describe('Button', () => {
  it('renders correctly', () => {
    render(<Button>Click me</Button>);
    expect(screen.getByText('Click me')).toBeInTheDocument();
  });

  it('shows loading state', () => {
    render(<Button isLoading>Click me</Button>);
    expect(screen.getByText('Cargando...')).toBeInTheDocument();
  });
});
```

**Tareas:**
- [ ] Crear tests para Button
- [ ] Crear tests para Input
- [ ] Crear tests para Modal
- [ ] Crear tests para Can component
- [ ] Crear tests para useAuth hook
- [ ] Ejecutar: `npm run test`

---

## 11. DOCUMENTACIÓN

### 11.1 README.md

**Archivo:** `README.md`

```markdown
# MyHotelFlow - Frontend

Sistema de Reservas Hoteleras - Interfaz de Usuario

## Stack Tecnológico

- React 18+
- TypeScript 5+
- Vite 5+
- Tailwind CSS 3+
- TanStack Query v5
- React Hook Form
- Zod
- Axios
- React Router v6

## Instalación

\`\`\`bash
npm install
\`\`\`

## Configuración

Copiar `.env.example` a `.env` y configurar:

\`\`\`env
VITE_API_URL=http://localhost:3000/api
\`\`\`

## Desarrollo

\`\`\`bash
npm run dev
\`\`\`

## Build

\`\`\`bash
npm run build
npm run preview
\`\`\`

## Testing

\`\`\`bash
npm run test
npm run test:ui
npm run coverage
\`\`\`

## Estructura

- `src/api/` - Configuración de axios y endpoints
- `src/components/` - Componentes reutilizables
- `src/contexts/` - Context providers
- `src/hooks/` - Custom hooks
- `src/pages/` - Páginas/Rutas
- `src/schemas/` - Validación Zod
- `src/types/` - TypeScript types
- `src/utils/` - Utilidades

## Autenticación

El sistema usa JWT con refresh tokens. Los tokens se guardan en localStorage y se renuevan automáticamente.

## Permisos

El sistema de permisos está basado en acciones granulares. Usa el componente `<Can>` para renderizado condicional:

\`\`\`tsx
<Can perform="users.create">
  <button>Crear Usuario</button>
</Can>
\`\`\`

## Rutas Protegidas

Las rutas protegidas usan `<ProtectedRoute>` con verificación de permisos:

\`\`\`tsx
<Route element={<ProtectedRoute requiredPermissions={['users.create']} />}>
  <Route path="/users/create" element={<UserFormPage />} />
</Route>
\`\`\`
```

**Tareas:**
- [ ] Crear README.md completo
- [ ] Documentar stack tecnológico
- [ ] Documentar instalación y configuración
- [ ] Documentar comandos
- [ ] Documentar estructura
- [ ] Documentar autenticación y permisos

### 11.2 CONTRIBUTING.md

**Tareas:**
- [ ] Crear guía de contribución (opcional)

### 11.3 JSDoc en Componentes

**Tareas:**
- [ ] Documentar componentes principales con JSDoc
- [ ] Documentar hooks con JSDoc
- [ ] Documentar utils con JSDoc

---

## 12. CHECKLIST FINAL

### 12.1 Funcionalidad

- [ ] Login funciona correctamente
- [ ] Refresh token funciona automáticamente
- [ ] Logout funciona y limpia storage
- [ ] Cambiar contraseña funciona
- [ ] Recuperar contraseña funciona
- [ ] Lista de usuarios funciona
- [ ] Crear usuario funciona
- [ ] Editar usuario funciona
- [ ] Eliminar usuario funciona
- [ ] Asignar grupos a usuario funciona
- [ ] Asignar acciones a usuario funciona
- [ ] Lista de grupos funciona
- [ ] Crear grupo funciona
- [ ] Editar grupo funciona
- [ ] Eliminar grupo funciona
- [ ] Asignar acciones a grupo funciona
- [ ] Asignar grupos hijos funciona
- [ ] Lista de acciones funciona
- [ ] Crear acción funciona
- [ ] Editar acción funciona
- [ ] Eliminar acción funciona

### 12.2 Permisos

- [ ] Componente Can funciona correctamente
- [ ] ProtectedRoute verifica autenticación
- [ ] ProtectedRoute verifica permisos
- [ ] Los botones/links se ocultan según permisos
- [ ] Redirige a /forbidden cuando no tiene permisos
- [ ] Los permisos se actualizan al cambiar grupos del usuario

### 12.3 UX/UI

- [ ] Los formularios muestran errores de validación
- [ ] Los formularios muestran errores del servidor
- [ ] Los botones se deshabilitan mientras se envían
- [ ] Se muestra spinner en carga de datos
- [ ] Se muestran mensajes de éxito
- [ ] Se muestran mensajes de error
- [ ] Los estilos están aplicados correctamente
- [ ] La navegación funciona correctamente
- [ ] El sidebar muestra la ruta activa
- [ ] Responsive design funciona

### 12.4 Performance

- [ ] TanStack Query caché funciona
- [ ] No hay re-renders innecesarios
- [ ] Las imágenes están optimizadas
- [ ] El bundle size es razonable

### 12.5 Seguridad

- [ ] Los tokens no se exponen en console.log
- [ ] Las contraseñas se validan con requisitos de complejidad
- [ ] Los inputs se sanitizan
- [ ] XSS está preveni do con React
- [ ] CORS está configurado correctamente

### 12.6 Testing

- [ ] Tests unitarios de componentes pasan
- [ ] Tests de hooks pasan
- [ ] Coverage > 70%

### 12.7 Documentación

- [ ] README.md está completo
- [ ] Componentes principales están documentados
- [ ] Hooks están documentados

---

## 13. PRÓXIMOS PASOS (MÓDULOS ADICIONALES)

Una vez completado el módulo de seguridad, implementar:

### 13.1 Módulo de Reservas

- [ ] Lista de reservas
- [ ] Crear reserva
- [ ] Editar reserva
- [ ] Cancelar reserva
- [ ] Búsqueda de disponibilidad

### 13.2 Módulo de Habitaciones

- [ ] Lista de habitaciones
- [ ] Crear habitación
- [ ] Editar habitación
- [ ] Cambiar estado de habitación
- [ ] Vista de ocupación

### 13.3 Módulo de Check-in/Check-out

- [ ] Registrar check-in
- [ ] Registrar check-out
- [ ] Calcular cargos
- [ ] Emitir comprobantes

### 13.4 Módulo de Comprobantes

- [ ] Lista de comprobantes
- [ ] Emitir comprobante
- [ ] Anular comprobante
- [ ] Imprimir comprobante
- [ ] Exportar PDF

### 13.5 Módulo de Pagos

- [ ] Registrar pago
- [ ] Métodos de pago
- [ ] Historial de pagos
- [ ] Devoluciones

---

## 📊 RESUMEN EJECUTIVO

### Componentes a Crear

- **Páginas:** ~25 páginas
- **Componentes UI:** ~15 componentes reutilizables
- **Hooks:** ~10 custom hooks
- **Contexts:** 2 contexts (Auth, Permissions)
- **APIs:** 4 archivos de API
- **Schemas:** 4 archivos de schemas
- **Types:** 4 archivos de tipos

### Tiempo Estimado

- **Setup inicial:** 4-6 horas
- **Autenticación:** 8-10 horas
- **Permisos:** 4-6 horas
- **Gestión de Usuarios:** 8-10 horas
- **Gestión de Grupos:** 6-8 horas
- **Gestión de Acciones:** 4-6 horas
- **Layout y Navegación:** 4-6 horas
- **Componentes UI:** 6-8 horas
- **Testing:** 8-10 horas
- **Documentación:** 2-4 horas

**TOTAL:** 54-74 horas (1.5-2 semanas para 1 desarrollador)

### Stack Final

```
Frontend:
- React 18+ + TypeScript 5+
- Vite 5+
- Tailwind CSS 3+
- TanStack Query v5
- React Hook Form + Zod
- Axios
- React Router v6
- Headless UI
- Lucide React (icons)

Backend (ya implementado):
- NestJS 11 + TypeORM
- PostgreSQL 15
- Redis 7 (cache)
- JWT + Argon2id
```

---

**¡Éxito con la implementación del frontend!** 🚀

**Versión:** 1.0
**Última actualización:** Octubre 2025
**Autor:** Equipo MyHotelFlow
