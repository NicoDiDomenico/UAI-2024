document.getElementById('formularioSocio').addEventListener('submit', async function (e) {
    e.preventDefault(); // Prevenir recarga de la página
  
    const formData = new FormData(this);
    const socioData = {};
    formData.forEach((value, key) => socioData[key] = value);
  
    try {
      const response = await fetch('http://localhost:3001/api/socios', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(socioData)
      });
  
      if (response.ok) {
        alert('Socio agregado correctamente');
        window.location.href = './socios.html'; // Redirige a la página de socios
      } else {
        const data = await response.json();
        alert(`Error: ${data.message}`);
      }
    } catch (error) {
      console.error("Error al agregar el socio:", error);
      alert('Error al agregar el socio. Intenta de nuevo más tarde.');
    }
  });
  