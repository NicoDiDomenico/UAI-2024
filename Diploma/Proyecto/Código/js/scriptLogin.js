// Espera a que el DOM esté completamente cargado antes de intentar agregar los event listener
document.addEventListener('DOMContentLoaded', function() {
    const ingresarButton = document.getElementById('ingresarButton');
    const claveInput = document.getElementById('claveInput');

    // Agrega un event listener para el evento 'click'
    ingresarButton.addEventListener('click', function() {
        validarClave();
    });

    // Agrega un event listener para el evento 'keydown' en el campo de entrada
    claveInput.addEventListener('keydown', function(event) {
        if (event.key === 'Enter') {
            validarClave();
        }
    });
});

// Función de validación
function validarClave() {
    const clave = document.getElementById("claveInput").value;
    const claveModal = document.getElementById("claveModal");
    const menuContent = document.getElementById("menuContent");
    const claveError = document.querySelector('.claveError');
    
    if (clave === "1234") {  // Acá voy a tener que hacer que la clave venga desde el modeo
        claveModal.classList.add("hidden");
        menuContent.classList.remove("hidden");
    } else {
        claveError.classList.remove("hidden");
        setTimeout(function(){
            claveError.classList.add("hidden");
        }, 1500);
    }
}

