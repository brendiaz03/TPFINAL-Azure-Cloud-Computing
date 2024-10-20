// Inicializar comportamiento de carrusel
const carousels = document.querySelectorAll('.carousel');

carousels.forEach(carousel => {
    let isDown = false;
    let startX;
    let scrollLeft;

    carousel.addEventListener('mousedown', (e) => {
        isDown = true;
        startX = e.pageX - carousel.offsetLeft;
        scrollLeft = carousel.scrollLeft;
    });

    carousel.addEventListener('mouseleave', () => {
        isDown = false;
    });

    carousel.addEventListener('mouseup', () => {
        isDown = false;
    });

    carousel.addEventListener('mousemove', (e) => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - carousel.offsetLeft;
        const walk = (x - startX) * 3; // Ajustar la velocidad de desplazamiento
        carousel.scrollLeft = scrollLeft - walk;
    });
});

// Agregar eventos a los botones de autenticación
document.querySelector('.login-btn').addEventListener('click', () => {
    alert('Iniciar sesión clickeado');
});

document.querySelector('.register-btn').addEventListener('click', () => {
    alert('Registrarse clickeado');
});
