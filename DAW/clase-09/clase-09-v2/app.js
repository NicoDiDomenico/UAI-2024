var title 

title = "caca"
/* document.title = "caca" */ // Si hago esto lo camibio al title que muestro en la web
console.log(title) // acá solo lo muestro por consola pero no cambio nada

/* title = document.title
console.log(title)
 */
console.log(document.title)
// cómo title es un elemento html único tiene su propio método para acceder directamente al contenido.
// Otros:
// document.body
// document.head

// Cuando trabajo con una coleccion de elementos HTML uso:
var clase = document.getElementsByClassName("extra"); // Obtiene el elemento con el id "title"
console.log(clase) // Muestro que es una coleccion HTML con X elementos
console.log(clase[0]) // Al ser una coleccion lo trabajo como un arreglo, eligo cual elemento de la coleccion quiero mostrar
console.log(clase[0].textContent); // Al seleccionar el elemento de la coleccion, puedo mostrar su contenido --> '<> contenido </>'
clase[0].textContent = "Hola" // no puedo agregar etiquetas, se escribirian como string
clase[0].innerHTML = "<a href='https://www.youtube.com/watch?v=i6a7vkDCMQg&ab_channel=CristianBallesteros'>Hola</a>" // puedo agregar etiquetas