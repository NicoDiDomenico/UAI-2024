// Función para obtener socios del backend
async function obtenerSocios() {
    try {
        const response = await fetch('http://localhost:3001/api/socios');
        const socios = await response.json();
  
        const listaSocios = document.querySelector('.socios-list');
        listaSocios.innerHTML = ''; // Limpiar mensaje inicial
  
        if (socios.length === 0) {
            listaSocios.innerHTML = '<p class="no-socios-message">No hay socios cargados actualmente.</p>';
            return;
        }
  
        socios.forEach(socio => {
            const socioElemento = document.createElement('div');
            socioElemento.classList.add('socio');
  
            const fecha = new Date(socio.fechaFinActividades);
            const fechaFormateada = `${fecha.getDate().toString().padStart(2, '0')}/${(fecha.getMonth() + 1).toString().padStart(2, '0')}/${fecha.getFullYear()}`;
  
            socioElemento.innerHTML = `
                <input type="radio" name="socioSeleccionado" value="${socio.id}" class="select-radio">
                <span class="status active"></span>
                <p>Nombre y Apellido: <strong>${socio.nombreApellido}</strong></p>
                <p>Estado: <strong>${socio.estadoSocio}</strong></p>
                <p>Fecha Vencimiento Cuota: <strong>${fechaFormateada}</strong></p>
            `;
  
            listaSocios.appendChild(socioElemento);
        });

        habilitarBotones();
    } catch (error) {
        console.error("Error al obtener los socios:", error);
        const listaSocios = document.querySelector('.socios-list');
        listaSocios.innerHTML = '<p class="no-socios-message">Error al cargar los socios. Intenta de nuevo más tarde.</p>';
    }
}

// Habilitar botones cuando un socio está seleccionado
function habilitarBotones() {
    const radios = document.querySelectorAll('input[name="socioSeleccionado"]');
    const btnConsultar = document.getElementById('btn-consultar');
    const btnEliminar = document.getElementById('btn-eliminar');

    radios.forEach(radio => {
        radio.addEventListener('change', () => {
            const seleccionado = document.querySelector('input[name="socioSeleccionado"]:checked');
            btnConsultar.disabled = !seleccionado;
            btnEliminar.disabled = !seleccionado;
        });
    });
}

// Función para consultar el socio seleccionado
function consultarSocio() {
    const socioSeleccionado = document.querySelector('input[name="socioSeleccionado"]:checked');
    
    if (!socioSeleccionado) {
        alert("Por favor, selecciona un socio para consultar.");
        return;
    }

    const id = socioSeleccionado.value;

    window.location.href = `./consultar-socio.html?id=${id}`;
}
  
// Función para eliminar el socio seleccionado
async function eliminarSocioSeleccionado() {
    const socioSeleccionado = document.querySelector('input[name="socioSeleccionado"]:checked');
    if (!socioSeleccionado) {
        alert("Por favor, selecciona un socio para eliminar.");
        return;
    }
  
    const id = socioSeleccionado.value;
  
    const confirmacion = confirm('¿Estás seguro de que deseas eliminar este socio?');
    if (!confirmacion) return;
  
    try {
        const response = await fetch(`http://localhost:3001/api/socios/${id}`, {
            method: 'DELETE',
        });
  
        if (response.ok) {
            alert('Socio eliminado correctamente');
            obtenerSocios(); // Refresca la lista de socios después de eliminar
        } else {
            const data = await response.json();
            alert(`Error: ${data.message}`);
        }
    } catch (error) {
        console.error('Error al eliminar el socio:', error);
        alert('Error al eliminar el socio. Intenta de nuevo más tarde.');
    }
}

// Llamar a la función cuando la página cargue
document.addEventListener('DOMContentLoaded', obtenerSocios);
document.querySelector('.delete-button').addEventListener('click', eliminarSocioSeleccionado);
document.querySelector('.actions button:nth-child(2)').addEventListener('click', consultarSocio);



