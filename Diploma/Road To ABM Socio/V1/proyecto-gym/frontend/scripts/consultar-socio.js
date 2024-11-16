// Obtener el parámetro `id` de la URL
const urlParams = new URLSearchParams(window.location.search);
const socioId = urlParams.get('id');
console.log("Socio ID:", socioId);  // Debe mostrar el id correcto --> Si lo hace

if (socioId) {
    obtenerDatosSocio(socioId);
} else {
    alert('No se ha seleccionado ningún socio.');
}

async function obtenerDatosSocio(id) {
    try {
        const response = await fetch(`http://localhost:3001/api/socios/${id}`);
        if (response.ok) {
            const socio = await response.json();

            // Formatear la fecha para que sea compatible con el input[type="date"]
            if (socio.fechaNacimiento) {
                const fecha = new Date(socio.fechaNacimiento);
                const fechaFormateada = fecha.toISOString().split('T')[0]; // Extrae solo la parte de la fecha (YYYY-MM-DD)
                document.querySelector('[name="fechaNacimiento"]').value = fechaFormateada;
            } else {
                console.warn("Fecha de nacimiento no válida:", socio.fechaNacimiento);
            }

            // Otros campos
            document.querySelector('[name="id"]').value = socio.id;
            document.querySelector('[name="nombre"]').value = socio.nombre;
            document.querySelector(`[name="genero"][value="${socio.genero}"]`).checked = true;
            document.querySelector('[name="dni"]').value = socio.dni;
            document.querySelector('[name="direccion"]').value = socio.direccion;
            document.querySelector('[name="telefono"]').value = socio.telefono;
            document.querySelector('[name="email"]').value = socio.email;
            document.querySelector('[name="obraSocial"]').value = socio.obraSocial;
            document.querySelector('[name="nombreUsuario"]').value = socio.nombreUsuario;
            document.querySelector('[name="contrasena"]').value = socio.contrasena;
            document.querySelector('[name="preguntaSeguridad"]').value = socio.preguntaSeguridad;
            document.querySelector('[name="respuestaSeguridad"]').value = socio.respuestaSeguridad;
            document.querySelector(`[name="plan"][value="${socio.plan}"]`).checked = true;

            // Marcar los checkboxes de días de asistencia
            const diasAsistencia = socio.diasAsistencia.split(', ');
            diasAsistencia.forEach(dia => {
                const checkbox = document.querySelector(`[name="diasAsistencia"][value="${dia}"]`);
                if (checkbox) checkbox.checked = true;
            });
        } else {
            console.error("Error al obtener los datos del socio:", response.statusText);
        }
    } catch (error) {
        console.error("Error al obtener los datos del socio:", error);
    }
}

// Función para habilitar la edición de un campo específico
function habilitarEdicion(campo) {
    const input = document.querySelector(`[name="${campo}"]`);
    if (input) {
        input.removeAttribute('readonly'); // Habilitar edición
        input.focus(); // Enfocar el campo para edición
        
        // Añadir un listener para capturar la tecla Enter
        input.addEventListener('keydown', function confirmarEdicion(event) {
            if (event.key === 'Enter') { // Si la tecla presionada es Enter
                event.preventDefault(); // Evita que se envíe el formulario

                input.setAttribute('readonly', 'true'); // Bloquear nuevamente el campo
                input.removeEventListener('keydown', confirmarEdicion); // Remover el listener
                console.log(`El campo "${campo}" ahora tiene el valor: ${input.value}`);
            }
        });
    }
}

async function actualizarSocio() {
    // Capturar todos los campos del formulario
    const nombre = document.querySelector('[name="nombre"]').value;
    const fechaNacimiento = document.querySelector('[name="fechaNacimiento"]').value;
    const genero = document.querySelector('[name="genero"]:checked').value;
    const dni = document.querySelector('[name="dni"]').value;
    const direccion = document.querySelector('[name="direccion"]').value;
    const telefono = document.querySelector('[name="telefono"]').value;
    const email = document.querySelector('[name="email"]').value;
    const obraSocial = document.querySelector('[name="obraSocial"]').value;
    const diasAsistencia = Array.from(document.querySelectorAll('[name="diasAsistencia"]:checked')).map(checkbox => checkbox.value).join(', ');
    const nombreUsuario = document.querySelector('[name="nombreUsuario"]').value;
    const contrasena = document.querySelector('[name="contrasena"]').value;
    const preguntaSeguridad = document.querySelector('[name="preguntaSeguridad"]').value;
    const respuestaSeguridad = document.querySelector('[name="respuestaSeguridad"]').value;
    const plan = document.querySelector('[name="plan"]:checked').value;

    // Enviar datos al servidor
    try {
        const response = await fetch(`http://localhost:3001/api/socios/${socioId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                nombre, fechaNacimiento, genero, dni, direccion, telefono, email, obraSocial,
                diasAsistencia, nombreUsuario, contrasena, preguntaSeguridad, respuestaSeguridad, plan
            })
        });

        if (response.ok) {
            alert('Socio actualizado correctamente');
            window.location.href = './socios.html'; // Redirige a la lista de socios
        } else {
            const data = await response.json();
            alert(`Error al actualizar el socio: ${data.message}`);
        }
    } catch (error) {
        console.error('Error al actualizar el socio:', error);
    }
}

// Asocia la función `actualizarSocio` al botón de confirmar
document.getElementById("confirmar").addEventListener("click", (event) => {
    event.preventDefault();
    actualizarSocio();
});

