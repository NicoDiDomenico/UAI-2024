'use strict'; /* introducido en ECMAScript 5 (ES5) y tiene el objetivo de ayudar a los desarrolladores a escribir código más seguro y eficiente, al eliminar algunas de las características más propensas a errores de JavaScript */

var d = document;

var contactForm = d.getElementById('contactForm');

var nameInput = document.getElementById('name');
var emailInput = document.getElementById('email');
var messageInput = document.getElementById('message');

var nameError = document.getElementById('nameError');
var emailError = document.getElementById('emailError');
var messageError = document.getElementById('messageError');
/* Me parece que al id lo uso para el JS y a la clase para el CSS */

/* funcion anónima */
var validateAndSendForm = function (e) { /* esta forma permite manipular la variable para agregarle otra funcion o pasarla como argumento, la otra forma es: function validateAndSendForme(e){...} */
  e.preventDefault(); /* Esta línea previene el comportamiento por defecto del formulario, que sería recargar la página al enviarse. */

  var valido = true;

  // Valido el nombre
  if (nameInput.value.trim() === '') { /* .trim() elimina los espacios en blanco al inicio y al final del valor. */
    nameError.textContent = 'El nombre es obligatorio';
    valido = false;
  } else if (!/^[a-zA-Z0-9 ]+$/.test(nameInput.value)) { /* El método .test() en JavaScript se utiliza para comprobar si una cadena de texto cumple con un patrón específico definido por una expresión regular */
    nameError.textContent = 'El nombre solo puede contener letras, números y espacios';
    valido = false;
  } else {
    nameError.textContent = ''; /* para mi esto está al pedo */
  }

  // Valido el correo electrónico
  if (emailInput.value.trim() === '') {
    emailError.textContent = 'El correo electrónico es obligatorio';
    valido = false;
  } else if (!/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(emailInput.value)) {
    emailError.textContent = 'El correo electrónico no es válido';
    valido = false;
  } else {
    emailError.textContent = '';
  }

  // Valido el mensaje
  if (messageInput.value.trim().length < 5) {
    messageError.textContent = 'El mensaje debe tener al menos 5 caracteres';
    valido = false;
  } else {
    messageError.textContent = '';
  }

  // Abro el email del sist. operativo con los datos del form
  if (valido) {
    var email = "mailto:agusalbo2024@gmail.com?subject=Boggle-Contacto&body=" + messageInput.value; /* estos backticks se hacen con Alt Gr + Tecla */
    /* 
    mailto: es un esquema de URI (Uniform Resource Identifier) que se usa para crear un enlace que abre el cliente de correo electrónico predeterminado del usuario con un nuevo mensaje prellenado. La estructura básica es mailto:email@example.com.
    Parámetros de la URL: Después de la dirección de correo, puedes agregar parámetros como subject (asunto) y body (cuerpo del mensaje). Estos se añaden después de un signo de interrogación (?) y se separan por el signo &.
    */

    window.location.href = email;
    /* 
    Al asignar esta URL a window.location.href, el navegador abrirá el cliente de correo electrónico del usuario con un nuevo mensaje dirigido a agusalbo2024@gmail.com, con el asunto "Boggle-Contacto" y el cuerpo "Hola, este es mi mensaje".
    */
  }
};

contactForm.addEventListener('submit', validateAndSendForm);

