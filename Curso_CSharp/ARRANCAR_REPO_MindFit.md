# Cómo arrancar este repo

Este proyecto tiene esta estructura:

```txt
Proyecto_MindFit_Intelligence/
├─ Backend/
└─ Frontend/
```

## 1. Abrir el proyecto

Abrir VS Code en la carpeta raíz:

```txt
Proyecto_MindFit_Intelligence
```

No abrir solo `Frontend` ni solo `Backend`.

## 2. Arrancar el frontend

Abrir una terminal en VS Code y ejecutar:

```powershell
cd Frontend
npm run dev
```

Si todo está bien, Vite va a mostrar una URL parecida a:

```txt
http://localhost:5173/
```

Abrir esa URL en el navegador.

## 3. Usar Codex

Abrir una segunda terminal en VS Code.

Asegurarse de estar en la raíz del proyecto:

```powershell
cd ..
```

La terminal debería quedar en algo parecido a:

```txt
C:\Facu\UAI\V2\UAI-2024\Curso_CSharp\Proyecto_MindFit_Intelligence
```

Ejecutar:

```powershell
codex
```

## 4. Flujo recomendado

Usar dos terminales:

```txt
Terminal 1: Frontend corriendo con npm run dev
Terminal 2: Codex abierto para trabajar con IA
```

## 5. Primeros prompts útiles para Codex

```txt
Analizá la carpeta Backend y explicame la arquitectura actual del proyecto .NET.
```

```txt
Detectá los endpoints disponibles del backend y proponé la estructura ideal del frontend React.
```

```txt
Creá una arquitectura frontend moderna en React + TypeScript para consumir este backend.
```

```txt
Generá servicios API en el frontend usando axios para consumir los endpoints del backend.
```

## 6. Recordatorio importante

No hace falta reinstalar Node, npm, Vite ni Codex cada vez que se abre el proyecto.

Solo hay que:

```powershell
cd Frontend
npm run dev
```

y en otra terminal:

```powershell
codex
```
