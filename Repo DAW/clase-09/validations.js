document.addEventListener("DOMContentLoaded", function() {
    var form = document.getElementById("formulario");
    var inputs = form.querySelectorAll("input");
    console.log(inputs);
    console.log(inputs[0]);

    form.addEventListener("submit", function(event) {
        event.preventDefault(); // Previene que el formulario se envie vacio
        validateForm(); // Llama a la función que valida el formulario
    });

    inputs.forEach(function(input){
        input.addEventListener("blur", function() {
            validateField(input); // Valida el campo cuando pierde el foco
        });
        input.addEventListener("focus", function() {
            clearError(input); // Limpia el error cuando se enfoca en el campo
        });
    });

    function validateForm() {
        var isValid = true; // Variable para rastrear si el formulario es válido
        var errorMessages = []; // Arreglo para almacenar mensajes de error

        inputs.forEach(function(input) {
            var errorMessage = validateField(input); // Valida el campo
            if (errorMessage) { // Si hay un mensaje de error
                isValid = false; // Marca el formulario como inválido
                errorMessages.push(errorMessage); // Agrega el mensaje de error al arreglo
            }
        });

        if (!isValid) {
            showModal("modalError", "Errores en el formulario:\n" + errorMessages.join("\n"));
        } else {
            showModal("modalSuccess", "Formulario enviado correctamente:\n" + getFormData());
        }
    }

    function validateField(field) {
        var errorElement = document.getElementById(`${field.id}Error`); // Elemento de error asociado al campo
        var errorMessage = ""; // Mensaje de error por defecto vacío

        switch (field.id) {
            case "nombre":
                if (!/^.+\s.+$/.test(field.value)) { // ! --> voy por el true cuando en realidad iria por el else
                    errorMessage = "Debe tener más de 6 letras y al menos un espacio entre medio.";
                }
               /*  Seria como tener esto:
                if (/^.+\s.+$/.test(field.value))
                }else {
                    errorMessage = "Debe tener más de 6 letras y al menos un espacio entre medio.";
                } */
                break;
            case "email":
                if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(field.value)) {
                    errorMessage = "Debe tener un formato de email válido.";
                }
                break;
            case "contraseña":
                if (!/^(?=.*[a-zA-Z])(?=.*\d).{8,}$/.test(field.value)) {
                    errorMessage = "Debe tener al menos 8 caracteres, formados por letras y números.";
                }
                break;
            case "repContraseña":
                if (field.value !== document.getElementById("contraseña").value) {
                    errorMessage = "Las contraseñas no coinciden.";
                }
                break;
            case "edad":
                if (!/^\d+$/.test(field.value) || parseInt(field.value) < 18) {
                    errorMessage = "Debe ser un número entero mayor o igual a 18.";
                }
                break;
            case "telefono":
                if (field.value && !/^\d{7,}$/.test(field.value)) {
                    errorMessage = "Debe ser un número de al menos 7 dígitos, sin espacios, guiones ni paréntesis.";
                }
                break;
            case "direccion":
                if (!/.+\s.+/.test(field.value) || field.value.length < 5) {
                    errorMessage = "Debe tener al menos 5 caracteres, con letras, números y un espacio en el medio.";
                }
                break;
            case "ciudad":
                if (field.value.length < 3) {
                    errorMessage = "Debe tener al menos 3 caracteres.";
                }
                break;
            case "codigoPostal":
                if (field.value.length < 3) {
                    errorMessage = "Debe tener al menos 3 caracteres.";
                }
                break;
            case "dni":
                if (!/^\d{7,8}$/.test(field.value)) {
                    errorMessage = "Debe ser un número de 7 u 8 dígitos.";
                }
                break;
            default:
                break;
        }

        if (errorMessage) {
            if (!errorElement) {
                var newErrorElement = document.createElement("div");
                newErrorElement.id = `${field.id}Error`;
                newErrorElement.classList.add("error-message");
                field.parentNode.appendChild(newErrorElement);
            }
            document.getElementById(`${field.id}Error`).textContent = errorMessage; // Establece el mensaje de error
            document.getElementById(`${field.id}Error`).style.display = "block"; // Muestra el elemento de error
            return errorMessage; // Devuelve el mensaje de error
        } else {
            if (errorElement) {
                errorElement.textContent = ""; // Limpia el mensaje de error
                errorElement.style.display = "none"; // Oculta el elemento de error
            }
            return ""; // Devuelve una cadena vacía (sin errores)
        }
    }

    function clearError(field) {
        var errorElement = document.getElementById(`${field.id}Error`); // Elemento de error asociado al campo
        if (errorElement) {
            errorElement.textContent = ""; // Limpia el mensaje de error
            errorElement.style.display = "none"; // Oculta el elemento de error
        }
    }

    function getFormData() {
        return Array.from(inputs).map(input => `${input.name}: ${input.value}`).join("\n"); // Recorre todos los inputs y crea una cadena con sus nombres y valores
    }

    function showModal(modalId, message) {
        var modal = document.getElementById(modalId);
        var modalMessage = modal.querySelector("p");
        modalMessage.textContent = message;
        modal.style.display = "block";

        var closeModal = modal.querySelector(".close");
        closeModal.onclick = function() {
            modal.style.display = "none";
        };

        window.onclick = function(event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        };
    }
});
