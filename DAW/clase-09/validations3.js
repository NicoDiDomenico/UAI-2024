function handlerForm(){
    // Selecciono el formulario 
    var form = document.querySelector("form");
    /* 
    - querySelector() - Cómo se usa:
    var form = document.querySelector("form"); // Selecciona la primer etiqueta <form> en el documento.
    var form = document.querySelector(".miClase"); // Selecciona el primer elemento tipo clase. 
    var form = document.querySelector("#miId"); // Selecciona el primer elemento tipo ID."
    */

    // Selecciona el campo de entrada del nombre completo por su ID
    var fullNameInput = document.getElementById("nombre");

    // Añade un evento al formulario para manejar el envío
    form.addEventListener("submit", function(event) {
        event.preventDefault(); // Previene el envío del formulario por defecto
        validateForm(); // Llama a la función que valida el formulario
    });

    // Añade eventos al campo de nombre completo para actualizar el título cuando se escribe o se enfoca en el campo
    fullNameInput.addEventListener("keydown", updateTitle);
    fullNameInput.addEventListener("focus", updateTitle);

    // Obtiene todos los campos de entrada dentro del formulario
    var inputs = form.querySelectorAll("input");

    // Añade eventos a cada campo de entrada para validar y limpiar errores
    inputs.forEach(input => {
        input.addEventListener("blur", validateField); // Valida el campo cuando pierde el foco
        input.addEventListener("focus", clearError); // Limpia el error cuando se enfoca en el campo
    });

    // Función que valida todo el formulario
    function validateForm() {
        var isValid = true; // Variable para rastrear si el formulario es válido
        var errorMessages = []; // Arreglo para almacenar mensajes de error

        // Valida cada campo del formulario
        inputs.forEach(input => {
            var errorMessage = validateField({ target: input }); // Valida el campo
            if (errorMessage) { // Si hay un mensaje de error
                isValid = false; // Marca el formulario como inválido
                errorMessages.push(errorMessage); // Agrega el mensaje de error al arreglo
            }
        });

        // Muestra una alerta con los errores si el formulario no es válido, o un mensaje de éxito si es válido
        if (!isValid) {
            showModal("modalError", "Errores en el formulario:\n" + errorMessages.join("\n"));
        } else {
            showModal("modalSuccess", "Formulario enviado correctamente:\n" + getFormData());
        }
    }

    // Función que valida un campo individual
    function validateField(event) {
        var field = event.target; // El campo que disparó el evento
        var errorElement = document.getElementById(`${field.id}Error`); // Elemento de error asociado al campo
        var errorMessage = ""; // Mensaje de error por defecto vacío

        // Validaciones específicas para cada campo
