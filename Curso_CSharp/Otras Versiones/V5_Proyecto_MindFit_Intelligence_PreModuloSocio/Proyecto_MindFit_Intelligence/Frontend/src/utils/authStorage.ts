import type { AuthSession } from '../types/auth'

const STORAGE_KEYS = {
  idGym: 'mindfit.idGym',
  accessToken: 'mindfit.accessToken',
  refreshToken: 'mindfit.refreshToken',
  permisos: 'mindfit.permisos',
} as const

function isBrowser() {
  return typeof window !== 'undefined'
}

export function getStoredSession(): AuthSession | null {
  if (!isBrowser()) {
    return null
  }

  const idGymValue = window.localStorage.getItem(STORAGE_KEYS.idGym)
  const accessToken = window.localStorage.getItem(STORAGE_KEYS.accessToken)
  const refreshToken = window.localStorage.getItem(STORAGE_KEYS.refreshToken)
  const permisosValue = window.localStorage.getItem(STORAGE_KEYS.permisos)

  if (!idGymValue || !accessToken || !refreshToken) {
    return null
  }

  const idGym = Number(idGymValue)

  if (Number.isNaN(idGym)) {
    clearStoredSession()
    return null
  }

  let permisos: string[] = []

  if (permisosValue) {
    try {
      const parsed = JSON.parse(permisosValue)
      if (Array.isArray(parsed)) {
        permisos = parsed.filter((value): value is string => typeof value === 'string')
      }
    } catch {
      clearStoredSession()
      return null
    }
  }

  return {
    idGym,
    accessToken,
    refreshToken,
    permisos,
  }
}

export function setStoredSession(session: AuthSession) {
  if (!isBrowser()) {
    return
  }

  window.localStorage.setItem(STORAGE_KEYS.idGym, String(session.idGym))
  window.localStorage.setItem(STORAGE_KEYS.accessToken, session.accessToken)
  window.localStorage.setItem(STORAGE_KEYS.refreshToken, session.refreshToken)
  window.localStorage.setItem(STORAGE_KEYS.permisos, JSON.stringify(session.permisos))
}

export function clearStoredSession() {
  if (!isBrowser()) {
    return
  }

  window.localStorage.removeItem(STORAGE_KEYS.idGym)
  window.localStorage.removeItem(STORAGE_KEYS.accessToken)
  window.localStorage.removeItem(STORAGE_KEYS.refreshToken)
  window.localStorage.removeItem(STORAGE_KEYS.permisos)
}
