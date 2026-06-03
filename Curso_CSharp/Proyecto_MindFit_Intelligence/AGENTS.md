# AGENTS.md

## Project Context

This project is **MindFit Intelligence**.

It has two main folders:

```txt
/Backend   - .NET backend API
/Frontend  - React + TypeScript + Vite frontend
```

The frontend must consume the backend API and should be built as a modern, maintainable web application.

## Base URL

```txt
https://localhost:7199
```

## Main Stack

Frontend:

- React
- TypeScript
- Vite
- React Router
- Axios

Backend:

- .NET API

## Agent Behavior

Before making large changes:

- Analyze the current structure.
- Explain the plan briefly.
- Prefer small incremental changes.
- Do not rewrite the whole project unless explicitly asked.
- Do not delete files unless explicitly approved.
- When adding dependencies, explain why they are needed.

## Frontend Architecture

Use this frontend structure when possible:

```txt
src/
├─ components/
├─ pages/
├─ layouts/
├─ services/
├─ hooks/
├─ types/
├─ routes/
├─ contexts/
└─ utils/
```

## API Integration

- Backend code is located in `/Backend`.
- Frontend code is located in `/Frontend`.
- Read the backend controllers/models before creating API calls.
- Centralize API calls in `/Frontend/src/services`.
- Use TypeScript types for request and response models.
- Use environment variables for backend base URLs.
- Do not hardcode production URLs.

## Auth / API Conventions

### Tenant Header

The backend uses multitenancy based on the request header:

```
X-Gym-Id
```

The client must send `X-Gym-Id` with the selected `idGym` in all relevant requests, including authentication-related public endpoints when required by backend middleware/services.

`idGym` must NOT be sent in request bodies unless explicitly required by the backend DTO.

Always verify tenant requirements by reading:

- controllers
- middleware
- services
- DTO documentation

before implementing frontend integrations.

---

### LocalStorage Conventions

Use namespaced localStorage keys:

```
mindfit.idGym
mindfit.accessToken
mindfit.refreshToken
mindfit.permisos
```

Rules:

- store `idGym` as string
- convert to `Number` when hydrating if necessary
- store `permisos` using `JSON.stringify`
- persist permissions exactly as returned by backend
- never store username or password

---

### Axios / API Client

Use a centralized Axios instance.

The Axios/API client must:

- use `VITE_API_BASE_URL`
- centralize Authorization handling
- centralize `X-Gym-Id` handling
- avoid duplicated header logic across pages/components

Request interceptor conventions:

- add `Authorization: Bearer {accessToken}` when available
- add `X-Gym-Id: {idGym}` when available

For login requests:

```
POST /api/Auth/login
```

send `X-Gym-Id` explicitly even before authenticated session persistence exists.

---

### DTO / Serialization Rules

Assume camelCase JSON serialization unless backend documentation indicates otherwise.

Do not:

- invent DTO fields
- rename DTO properties
- infer undocumented payload structures

Backend DTOs are the source of truth.

---

### Permissions

Permissions are received as:

```
string[]
```

Store permissions exactly as returned by backend.

Future frontend authorization logic will depend on exact string matching.

## Backend Protection Rules

The backend is considered stable and should be treated as the source of truth.

The AI agent may:

- read backend code
- analyze controllers and models
- infer API contracts
- generate frontend integrations

The AI agent must NOT:

- modify backend business logic
- refactor backend architecture
- rename backend endpoints
- change backend models
- alter backend authentication flows
- modify backend code unless explicitly requested

Focus primarily on frontend development and frontend/backend integration.

The user has significantly more backend knowledge than frontend knowledge.

When making frontend architectural decisions:

- explain the reasoning briefly
- prefer simple and maintainable approaches
- avoid unnecessary complexity
- avoid overengineering

## Frontend Development Workflow

The frontend will be built progressively by stages/modules.

The user has Word documents with documented backend endpoints grouped by module, such as authentication, users, routines, shifts, permissions, equipment, machines, exercises, schedules, and gym acquisition.

For each stage, the user will define which endpoints must be implemented.

Before implementing a frontend stage, the AI agent must:

- read the relevant endpoint documentation provided by the user
- identify the required endpoints for that stage
- ask for or use the request DTO and response DTO from Swagger
- map each endpoint to the required frontend screen, form, grid, modal, button, or workflow
- respect backend authorization rules and permission codes
- avoid guessing DTO fields when they are not provided

The backend documentation and Swagger DTOs are the source of truth for frontend integration.

If an endpoint, DTO, permission, or workflow is unclear, the AI agent should ask before implementing instead of inventing behavior.

## UI Direction

When working on UI, pages, layouts, dashboards, forms, modals, visual hierarchy, or motion, follow:

```txt
frontend-skill.md
```

## Code Style

- Prefer functional components.
- Use hooks.
- Keep components small and focused.
- Avoid duplicated logic.
- Prefer readable code over clever code.
- Use clear names for files, functions, components, and types.

## Quality Rules

Before finishing a task:

- Check that the app still runs.
- Fix TypeScript errors when possible.
- Keep the code consistent with the existing project.
- Explain important architectural decisions.
