// Datos de ejemplo
const libros = [
    { id: 1, titulo: "Libro A", autor: "Autor A", precio: 10 },
    { id: 2, titulo: "Libro B", autor: "Autor B", precio: 15 },
    { id: 3, titulo: "Libro C", autor: "Autor C", precio: 20 }
];

// Cargar libros en el catálogo
function cargarLibros() {
    const listaLibros = document.getElementById('listaLibros');
    libros.forEach(libro => {
        const libroDiv = document.createElement('div');
        libroDiv.classList.add('libro');
        libroDiv.innerHTML = `
            <h3>${libro.titulo}</h3>
            <p>Autor: ${libro.autor}</p>
            <p>Precio: $${libro.precio}</p>
            <button onclick="guardarEnFavoritos(${libro.id})">Guardar en Favoritos</button>
            <button onclick="agregarAlCarrito(${libro.id})">Agregar al Carrito</button>
        `;
        listaLibros.appendChild(libroDiv);
    });
}

// Guardar libro en favoritos
function guardarEnFavoritos(id) {
    let favoritos = JSON.parse(localStorage.getItem('favoritos')) || [];
    const libro = libros.find(libro => libro.id === id);
    if (!favoritos.some(fav => fav.id === libro.id)) {
        favoritos.push(libro);
        localStorage.setItem('favoritos', JSON.stringify(favoritos));
        alert(`${libro.titulo} ha sido guardado en favoritos.`);
        mostrarFavoritos();
    }
}

// Mostrar lista de favoritos
function mostrarFavoritos() {
    const listaFavoritos = document.getElementById('listaFavoritos');
    listaFavoritos.innerHTML = '';
    let favoritos = JSON.parse(localStorage.getItem('favoritos')) || [];
    favoritos.forEach(libro => {
        const libroDiv = document.createElement('div');
        libroDiv.classList.add('libro');
        libroDiv.innerHTML = `
            <h3>${libro.titulo}</h3>
            <p>Autor: ${libro.autor}</p>
            <p>Precio: $${libro.precio}</p>
            <button onclick="agregarAlCarrito(${libro.id})">Agregar al Carrito</button>
        `;
        listaFavoritos.appendChild(libroDiv);
    });
}

// Agregar libro al carrito
function agregarAlCarrito(id) {
    let carrito = JSON.parse(localStorage.getItem('carrito')) || [];
    const libro = libros.find(libro => libro.id === id);
    carrito.push(libro);
    localStorage.setItem('carrito', JSON.stringify(carrito));
    alert(`${libro.titulo} ha sido añadido al carrito.`);
    mostrarCarrito();
}

// Mostrar carrito de compras
function mostrarCarrito() {
    const listaCarrito = document.getElementById('listaCarrito');
    listaCarrito.innerHTML = '';
    let carrito = JSON.parse(localStorage.getItem('carrito')) || [];
    let total = 0;
    carrito.forEach(libro => {
        total += libro.precio;
        const libroDiv = document.createElement('div');
        libroDiv.classList.add('libro');
        libroDiv.innerHTML = `
            <h3>${libro.titulo}</h3>
            <p>Autor: ${libro.autor}</p>
            <p>Precio: $${libro.precio}</p>
        `;
        listaCarrito.appendChild(libroDiv);
    });
    listaCarrito.innerHTML += `<p>Total: $${total}</p>`;
}

// Proceder al pago (simple)
document.getElementById('comprarBtn').addEventListener('click', () => {
    alert('Compra realizada con éxito.');
    localStorage.removeItem('carrito');
    mostrarCarrito();
});

// Inicializar la página
cargarLibros();
mostrarFavoritos();
mostrarCarrito();
