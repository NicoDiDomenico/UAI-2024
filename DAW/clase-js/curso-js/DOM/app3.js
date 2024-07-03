/* Aprendiendo a manipular el DOM */
/* 
Con Acceso al DOM JS tiene la capacidad de:
 - Cambiar, remover o agregar todos los elementos o atributos de la página.
 - Cambiar todos los estilos CSS.
 - Reaccionar a todos los eventos HTML existentes en la página o crear nuevos eventos.
Por lo tantos podemos hacer que nuestra página web se comporte de forma dinámica.

Seleccionando eleementos:
- Por el ID.
- Por el nombre de la clase.
- Por el nombre de la etiqueta.
 */

// Seleccionando Elemenos
// Por ID:
console.log('getElementById()');
var elementoPorId = document.getElementById("parrafo1");
elementoPorId.textContent = 'modifico solo el texto';
elementoPorId.innerHTML = '<a href="https://www.google.com.ar/">Además puedo agregar etiquetas</a>';
console.log(elementoPorId.textContent);

// Por nombre de la clase:
console.log('');
console.log('getElementsByClassName()');
var elementosPorClase = document.getElementsByClassName("parrafos");
console.log(typeof elementoPorId, typeof elementosPorClase);
console.log(elementosPorClase.length);
elementosPorClase[0].textContent = "es una clase";
elementosPorClase[1].textContent = "es otra clase";
elementosPorClase[2].textContent = "es una 3era clase";

// Por etiqueta:
console.log('');
console.log('getElementsByTagName()');
var elementosPorEtiquetas = document.getElementsByTagName("p");
console.log(elementosPorEtiquetas.length);
elementosPorEtiquetas[0].textContent = "es una clase";
elementosPorEtiquetas[1].textContent = "es otra clase";
elementosPorEtiquetas[2].textContent = "es una 3era clase";
