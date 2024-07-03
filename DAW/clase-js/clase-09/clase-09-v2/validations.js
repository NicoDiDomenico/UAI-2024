document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('formulario');
    const fields = {
        nombre: {
            element: document.getElementById('nombre'),
            validate: value => value.length > 6 && /\s/.test(value),
            errorMessage: 'El nombre debe tener más de 6 letras y al menos un espacio entre medio.'
        },
        email: {
            element: document.getElementById('email'),
            validate: value => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value),
            errorMessage: 'Debe ser un email válido.'
        },
        contraseña: {
            element: document.getElementById('contraseña'),
            validate: value => /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/.test(value),
            errorMessage: 'La contraseña debe tener al menos 8 caracteres, formados por letras y números.'
        },
        repContraseña: {
            element: document.getElementById('repContraseña'),
            validate: value => value === document.getElementById('contraseña').value,
            errorMessage: 'Las contraseñas no coinciden.'
        },
        edad: {
            element: document.getElementById('edad'),
            validate: value => /^\d+$/.test(value) && parseInt(value) >= 18,
            errorMessage: 'Debe ser un número entero mayor o igual a 18.'
        },
        telefono: {
            element: document.getElementById('telefono'),
            validate: value => /^\d{7,}$/.test(value),
            errorMessage: 'Debe ser un número de al menos 7 dígitos, sin espacios, guiones ni paréntesis.'
        },
        direccion: {
            element: document.getElementById('direccion'),
            validate: value => value.length >= 5 && /\s/.test(value),
            errorMessage: 'La dirección debe tener al menos 5 caracteres, con letras, números y un espacio en el medio.'
        },
        ciudad: {
            element: document.getElementById('ciudad'),
            validate: value => value.length >= 3,
            errorMessage: 'Debe tener al menos 3 caracteres.'
        },
        codigoPostal: {
            element: document.getElementById('codigoPostal'),
            validate: value => value.length >= 3,
            errorMessage: 'Debe tener al menos 3 caracteres.'
        },
