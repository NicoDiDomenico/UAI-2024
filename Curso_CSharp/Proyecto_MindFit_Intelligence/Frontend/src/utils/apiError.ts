import axios from 'axios'

const DEFAULT_LOGIN_ERROR =
  'No pudimos iniciar sesion. Verifica tus credenciales e intenta nuevamente.'
const DEFAULT_GYMS_ERROR =
  'No pudimos cargar los gimnasios activos. Revisa la conexion con el backend.'
const DEFAULT_FORGOT_PASSWORD_ERROR =
  'No pudimos procesar la recuperacion en este momento. Intenta nuevamente en unos minutos.'
const DEFAULT_RESET_PASSWORD_ERROR =
  'No pudimos restablecer tu contrasena. Revisa el enlace o intenta solicitar uno nuevo.'

function getServerStringMessage(error: unknown) {
  if (!axios.isAxiosError(error)) {
    return null
  }

  const serverMessage = error.response?.data

  if (typeof serverMessage === 'string' && serverMessage.trim()) {
    return serverMessage
  }

  return null
}

export function getLoginErrorMessage(error: unknown) {
  if (axios.isAxiosError(error)) {
    if (!error.response) {
      return DEFAULT_LOGIN_ERROR
    }

    if (error.response.status >= 400) {
      return DEFAULT_LOGIN_ERROR
    }
  }

  return DEFAULT_LOGIN_ERROR
}

export function getGymsErrorMessage(error: unknown) {
  const serverMessage = getServerStringMessage(error)

  if (serverMessage) {
    return serverMessage
  }

  return DEFAULT_GYMS_ERROR
}

export function getForgotPasswordErrorMessage(error: unknown) {
  const serverMessage = getServerStringMessage(error)

  if (serverMessage) {
    return serverMessage
  }

  return DEFAULT_FORGOT_PASSWORD_ERROR
}

export function getResetPasswordErrorMessage(error: unknown) {
  const serverMessage = getServerStringMessage(error)

  if (serverMessage) {
    return serverMessage
  }

  return DEFAULT_RESET_PASSWORD_ERROR
}
