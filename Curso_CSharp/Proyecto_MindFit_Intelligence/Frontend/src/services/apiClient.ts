import axios from 'axios'
import { getStoredSession } from '../utils/authStorage'

declare module 'axios' {
  export interface AxiosRequestConfig {
    skipGymId?: boolean
  }

  export interface InternalAxiosRequestConfig {
    skipGymId?: boolean
  }
}

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL || '/api'

export const apiClient = axios.create({
  baseURL: apiBaseUrl,
})

apiClient.interceptors.request.use((config) => {
  const session = getStoredSession()

  if (session?.accessToken) {
    config.headers.Authorization = `Bearer ${session.accessToken}`
  }

  if (session?.idGym && !config.skipGymId) {
    config.headers['X-Gym-Id'] = String(session.idGym)
  }

  return config
})
