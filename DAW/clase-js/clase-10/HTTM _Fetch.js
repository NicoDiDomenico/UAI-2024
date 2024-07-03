/* HTTP y Persistencia de Datos */
/* Solicitudes HTTP
HTTP (Hypertext Transfer Protocol) define un conjunto de métodos de petición para interactuar con recursos de un servidor. Estos métodos son conocidos como "HTTP verbs" y se utilizan para realizar diferentes acciones sobre los recursos.
*/

/* 
GET

- Solicita una representación de un recurso específico.
- Solo debe recuperar datos y no modificar el estado del recurso.

Ejemplo en JavaScript utilizando fetch:
*/
fetch('https://api.example.com/datos') 
// - Inicia una solicitud HTTP GET a la URL https://api.example.com/data.
// - fetch devuelve inmediatamente una promesa.
  .then(response => response.json()) 
  // - La promesa devuelta por fetch se resuelve con un objeto Response cuando el servidor responde.
  // - response es el objeto Response que contiene varios métodos y propiedades para inspeccionar la respuesta HTTP. 
  // - response.json() es un método que devuelve una promesa que se resuelve con los datos de la respuesta convertidos a JSON. Cuando un servidor envía una respuesta, esa respuesta suele estar en formato JSON. El método .json() es necesario para convertir esa respuesta JSON a un objeto JavaScript, de modo que puedas trabajar con los datos en tu aplicación.
  .then(data => console.log(data)) //Aquí manejamos la promesa devuelta por response.json(), data ahora contiene los datos de la respuesta como un objeto JavaScript, permitiendo que trabajemos directamente con ellos.
  .catch(error => console.error('Error:', error)); // catch captura cualquier error que ocurra durante el proceso de la solicitud o la conversión de datos.

/* 
¿Qué es fetch?
fetch es una función nativa de JavaScript que se utiliza para hacer solicitudes HTTP asíncronas. Se introdujo en ECMAScript 6 (ES6) como una mejora sobre XMLHttpRequest, proporcionando una API más simple y más flexible para realizar solicitudes y manejar respuestas de servidores web.
La Fetch API proporciona una interfaz para realizar solicitudes HTTP y manejar sus respuestas. fetch devuelve una promesa que se resuelve en la respuesta a la solicitud.

Sintaxis básica: fetch(url, options);
- url: La URL a la que se va a realizar la solicitud.
- options: Un objeto opcional que contiene configuraciones adicionales como método, encabezados, cuerpo de la solicitud, etc.
*/

/* 
POST

- Envía una entidad al recurso especificado, generalmente causando un cambio en el estado del servidor.

Ejemplo en JavaScript utilizando fetch:  
*/
fetch('https://api.example.com/crear', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({ nombre: 'Juan', edad: 30 })
})
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.error('Error:', error));

/*
PUT

- Reemplaza todas las representaciones actuales del recurso de destino con la carga útil de la petición.

Ejemplo en JavaScript utilizando fetch:
*/
fetch('https://api.example.com/actualizar/1', {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({ nombre: 'Juan', edad: 31 })
})
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.error('Error:', error));

/* 
DELETE

- Borra un recurso específico.

Ejemplo en JavaScript utilizando fetch:
*/
fetch('https://api.example.com/eliminar/1', {
    method: 'DELETE'
})
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.error('Error:', error));
  
/* 
Persistencia de Datos en el Navegador:
El almacenamiento de datos en el navegador permite mantener información entre sesiones o dentro de una misma sesión de usuario. Las principales herramientas de persistencia de datos en el navegador son LocalStorage, SessionStorage y Cookies.
*/
/*  
1. LocalStorage:
- Almacena datos sin fecha de expiración.
- La información perdura hasta que se limpia la caché del navegador.
- Puede almacenar una gran cantidad de información.

Ejemplo en JavaScript:
*/
// Guardar datos
localStorage.setItem('nombre', 'Juan');

// Obtener datos
let nombre = localStorage.getItem('nombre');
console.log(nombre);

// Eliminar datos
localStorage.removeItem('nombre');

// Limpiar todo
localStorage.clear();

/* 
2. SessionStorage:

- Almacena datos solo durante la sesión de la página (pestaña del navegador).
- La información se elimina cuando se cierra la pestaña del navegador.

Ejemplo en JavaScript
*/
// Guardar datos
sessionStorage.setItem('nombre', 'Juan');

// Obtener datos
let nombre = sessionStorage.getItem('nombre');
console.log(nombre);

// Eliminar datos
sessionStorage.removeItem('nombre');

// Limpiar todo
sessionStorage.clear();

/*
Cookies

- Almacenan una pequeña cantidad de datos con una fecha de expiración.
- Se pueden acceder tanto desde el front-end como desde el back-end.
- La seguridad es limitada a menos que se configure como HttpOnly.

Ejemplo en JavaScript:
*/
// Crear cookie
document.cookie = "nombre=Juan; expires=Fri, 31 Dec 2024 12:00:00 UTC; path=/";

// Leer cookies
let cookies = document.cookie.split(';');
cookies.forEach(cookie => {
  let [key, value] = cookie.split('=');
  console.log(key.trim(), value);
});

// Eliminar cookie (estableciendo una fecha pasada)
document.cookie = "nombre=Juan; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";

/* 
Resumen:
- HTTP: Métodos GET, POST, PUT, DELETE para interactuar con recursos del servidor.
- LocalStorage: Almacenamiento persistente en el navegador sin fecha de expiración.
- SessionStorage: Almacenamiento temporal en el navegador durante la sesión de la página.
- Cookies: Almacenamiento de pequeñas cantidades de datos con fecha de expiración, accesibles desde front-end y back-end.
Estos conceptos son fundamentales para construir aplicaciones web modernas y eficientes, permitiendo manejar datos de manera flexible y segura tanto en el servidor como en el cliente.
*/

/* Diferencias Clave entre el form del HTML (action y method) y uso de Fetch en JavaScript */

/* 
Voy a simplificar los conceptos y destacar las diferencias clave entre el uso de formularios HTML tradicionales y el uso de la función `fetch` en JavaScript para enviar datos.

### Formularios HTML

**Concepto:**
- Los formularios HTML se utilizan para recopilar y enviar datos del usuario a un servidor.
- Se configuran usando las etiquetas `<form>`, `<input>`, `<select>`, etc.
- Usan los atributos `action` y `method` para definir cómo y dónde se envían los datos.

**Ejemplo:**

```html
<form action="/submit" method="POST">
  <label for="name">Nombre:</label>
  <input type="text" id="name" name="name">
  <input type="submit" value="Enviar">
</form>
```

**Funcionamiento:**
- **action**: La URL a la que se enviarán los datos del formulario.
- **method**: El método HTTP utilizado para enviar los datos (`GET` o `POST`).

**Métodos HTTP:**
- **GET**: Envía los datos como parte de la URL (cadena de consulta).
- **POST**: Envía los datos en el cuerpo de la solicitud HTTP.

**Características:**
- **Recarga la página**: El navegador envía los datos y luego recarga la página para mostrar la respuesta del servidor.
- **Simplicidad**: No requiere JavaScript.
- **Uso común**: Bueno para formularios simples donde la recarga de la página no es un problema.

### Fetch en JavaScript

**Concepto:**
- `fetch` es una función de JavaScript que permite realizar solicitudes HTTP de manera asíncrona.
- Se utiliza para enviar y recibir datos sin recargar la página.

**Ejemplo:**

html
<form id="myForm">
  <label for="name">Nombre:</label>
  <input type="text" id="name" name="name">
  <input type="submit" value="Enviar">
</form>

<script>
document.getElementById('myForm').addEventListener('submit', function(event) {
  event.preventDefault(); // Evita el comportamiento predeterminado del formulario.

  let formData = new FormData(this);

  fetch('/submit', {
    method: 'POST', // Especifica el método HTTP.
    body: formData // Envía los datos del formulario.
  })
  .then(response => response.json()) // Convierte la respuesta a JSON.
  .then(data => console.log(data)) // Maneja los datos recibidos.
  .catch(error => console.error('Error:', error)); // Maneja cualquier error.
});
</script>

/*
**Funcionamiento:**
- **event.preventDefault()**: Evita que el formulario se envíe de la manera tradicional.
- **fetch()**: Realiza la solicitud HTTP de manera asíncrona.
- **method**: El método HTTP utilizado (`GET`, `POST`, etc.).
- **body**: Los datos que se envían al servidor (puede ser JSON, `FormData`, etc.).

**Características:**
- **No recarga la página**: Permite enviar y recibir datos sin recargar la página.
- **Asíncrono**: Realiza las solicitudes de manera no bloqueante, mejorando la experiencia del usuario.
- **Flexible**: Ofrece mayor control sobre las solicitudes y respuestas HTTP.

### Diferencias Clave

1. **Recarga de Página**:
   - **Formularios HTML**: Recargan la página después de enviar los datos.
   - **Fetch en JavaScript**: No recarga la página, proporcionando una experiencia más fluida.

2. **Facilidad de Uso**:
   - **Formularios HTML**: Muy fáciles de usar y configurar, adecuados para formularios simples.
   - **Fetch en JavaScript**: Requiere conocimientos de JavaScript, pero ofrece mayor flexibilidad y control.

3. **Asincronía**: --> Lo mas importante!
   - **Formularios HTML**: Sincronía; la página espera a que se complete la solicitud antes de continuar.
   - **Fetch en JavaScript**: Asincronía; la solicitud se realiza en segundo plano, y el usuario puede seguir interactuando con la página.

4. **Control y Flexibilidad**:
   - **Formularios HTML**: Menos control sobre la solicitud y la respuesta.
   - **Fetch en JavaScript**: Más control sobre los detalles de la solicitud HTTP (encabezados, métodos, cuerpos, manejo de respuestas y errores).

### Resumen

- **Formularios HTML** son simples y adecuados para enviar datos en aplicaciones web básicas, pero recargan la página.
- **Fetch en JavaScript** es ideal para aplicaciones web dinámicas que necesitan enviar y recibir datos sin recargar la página, ofreciendo mayor control y mejor experiencia de usuario.

Espero que esta explicación haya aclarado las diferencias y relaciones entre ambos métodos.
*/