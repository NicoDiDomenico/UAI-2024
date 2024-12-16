// Importamos Axios, una biblioteca para realizar solicitudes HTTP
import axios from "axios";
// Importamos la URL base desde un archivo de constantes, esta URL se usará como base para todas las solicitudes
import { BASE_URL } from "./constants";

// Creamos una instancia personalizada de Axios con configuraciones predeterminadas
const axiosInstance = axios.create({
  // Establece la URL base para todas las solicitudes
  baseURL: BASE_URL,
  // Define un tiempo límite de espera para las solicitudes (10 segundos en este caso)
  timeout: 10000,
  // Especifica que las solicitudes enviarán datos en formato JSON
  headers: {
    "Content-Type": "application/json", // Define el tipo de contenido como JSON
  },
});

// Agregamos un interceptor de solicitudes para realizar acciones antes de enviar una solicitud
axiosInstance.interceptors.request.use(
  // Esta función se ejecuta antes de enviar cada solicitud
  (config) => {
    // Obtenemos el token de acceso del almacenamiento local del navegador (si existe)
    const accessToken = localStorage.getItem("token");
    // Si hay un token, lo agregamos como un encabezado de autorización en la solicitud
    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
    // Devolvemos la configuración de la solicitud actualizada
    return config;
  },
  // Esta función maneja errores si ocurre algo antes de enviar la solicitud
  (error) => {
    // Rechaza la promesa con el error para que pueda manejarse más adelante
    return Promise.reject(error);
  }
);

// Exportamos la instancia personalizada para que pueda ser usada en toda la aplicación
export default axiosInstance;
