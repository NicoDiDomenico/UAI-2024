// scriptGestionarGimnasio.js

// Espera a que el DOM esté completamente cargado
document.addEventListener('DOMContentLoaded', function() {
    const gestionarGimnasioButton = document.querySelector('.button-group button:nth-child(3)');
    const loginModal = document.getElementById('loginModal');
    const iniciarSesionButton = document.getElementById('iniciarSesionButton');
    const usuarioInput = document.getElementById('usuarioInput');
    const claveLoginInput = document.getElementById('claveLoginInput');

    // Muestra el modal de inicio de sesión al hacer clic en "Gestionar Gimnasio"
    gestionarGimnasioButton.addEventListener('click', function() {
        loginModal.classList.remove('hidden');
    });

    // Valida el inicio de sesión al hacer clic en "Iniciar Sesión"
    iniciarSesionButton.addEventListener('click', function() {
        const usuario = usuarioInput.value;
        const clave = claveLoginInput.value;
        
        // Aquí deberías añadir la lógica de validación de usuario y clave
        if (usuario === "admin" && clave === "1234") {  // Ejemplo de validación simple
            loginModal.classList.add('hidden');
            // Lógica para redirigir o habilitar funcionalidades
        } else {
            claveError.classList.remove("hidden");
            setTimeout(function(){
                claveError.classList.add("hidden");
            }, 1500);
        }
    });

    // Añadir evento para que al presionar Enter, también se valide la clave
    [usuarioInput, claveLoginInput].forEach(input => {
        input.addEventListener('keydown', function(event) {
            if (event.key === 'Enter') {
                iniciarSesionButton.click();
            }
        });
    });
});
