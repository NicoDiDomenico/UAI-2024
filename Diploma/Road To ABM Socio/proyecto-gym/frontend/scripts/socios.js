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
  
      // Crear elementos en la lista para cada socio
      socios.forEach(socio => {
        const socioElemento = document.createElement('div');
        socioElemento.classList.add('socio');
  
        socioElemento.innerHTML = `
          <span class="status active"></span>
          <p>Nombre y Apellido: <strong>${socio.nombre}</strong></p>
          <p>Estado: <strong>${socio.estado}</strong></p>
          <p>Fecha Vencimiento Cuota: <strong>${socio.fechaVencimiento}</strong></p>
        `;
  
        listaSocios.appendChild(socioElemento);
      });
    } catch (error) {
      console.error("Error al obtener los socios:", error);
      const listaSocios = document.querySelector('.socios-list');
      listaSocios.innerHTML = '<p class="no-socios-message">Error al cargar los socios. Intenta de nuevo más tarde.</p>';
    }
  }
  
  // Llamar a la función cuando la página cargue
  document.addEventListener('DOMContentLoaded', obtenerSocios);
  