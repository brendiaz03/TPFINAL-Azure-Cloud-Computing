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

document.addEventListener('DOMContentLoaded', function () {
    fetchPlaylists();
});

function fetchPlaylists() {
    fetch('/api/playlists') // Reemplaza con tu endpoint
        .then(response => response.json())
        .then(data => {
            const container = document.querySelector('.spotify-playlists');
            data.forEach(playlist => {
                const item = document.createElement('div');
                item.classList.add('item');
                item.innerHTML = `
                    <img src="${playlist.image}" />
                    <div class="play">
                        <span class="fa fa-play"></span>
                    </div>
                    <h4 class="fw-bold">${playlist.name}</h4>
                `;
                container.appendChild(item);
            });
        })
        .catch(error => console.error('Error al cargar las playlists:', error));
}