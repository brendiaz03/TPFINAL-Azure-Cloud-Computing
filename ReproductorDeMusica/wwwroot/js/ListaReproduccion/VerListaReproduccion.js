// Variables globales
let audio = new Audio(); 
let cancionReproduciendo = null; 
let estaPausado = false; 

document.addEventListener("DOMContentLoaded", () => {
    configurarInteraccionesPlaylist();
});

function configurarInteraccionesPlaylist() {

    // Cada vez que paso el mouse por encima de la fila, cambia el icono
    $('tr').on('mouseenter', function () {
        $(this).find('.playlist-play-btn').show();
    }).on('mouseleave', function () {
        $(this).find('.playlist-play-btn').hide();
    });

    document.querySelector('.table').addEventListener('click', function (event) {
        if (event.target.closest('button.playlist-play-btn')) {
            event.stopPropagation();
            const songButton = event.target.closest('button.playlist-play-btn'); // Obtiene el botón
            const row = songButton.closest('tr'); // Encuentra la fila
            const cancionId = row.getAttribute('data-cancion-id'); // Obtiene el ID de la canción

            if (cancionReproduciendo && cancionReproduciendo.id === cancionId) {
                // Si la canción ya se está reproduciendo, verifica si está pausada
                if (estaPausado) {
                    audio.play(); // Reanuda la reproducción
                    estaPausado = false; // Marca que la canción está en reproducción
                    songButton.querySelector('i').classList.remove('fa-play'); // Cambia ícono a "stop"
                    songButton.querySelector('i').classList.add('fa-stop');
                } else {
                    // Si no está pausada, pausa la canción
                    audio.pause();
                    estaPausado = true; // Marca que la canción está pausada
                    songButton.querySelector('i').classList.remove('fa-stop'); // Cambia ícono a "play"
                    songButton.querySelector('i').classList.add('fa-play');
                }
            } else {
                // Detener el audio si hay uno reproduciéndose
                if (cancionReproduciendo) {
                    audio.pause();
                    estaPausado = true; // Marca que la canción anterior está pausada
                    const previousButton = document.querySelector(`button[data-cancion-id="${cancionReproduciendo.id}"]`);
                    if (previousButton) {
                        previousButton.querySelector('i').classList.remove('fa-stop');
                        previousButton.querySelector('i').classList.add('fa-play');
                    }
                }

                // Cambiar el estado de la nueva canción que se va a reproducir
                cancionReproduciendo = {
                    id: cancionId,
                    ruta: songButton.dataset.rutaAudio // Guarda la ruta de la canción que se está reproduciendo
                };
                sessionStorage.setItem('cancionReproduciendo', cancionReproduciendo);
                audio.src = cancionReproduciendo.ruta; // Asigna la nueva ruta al audio
                audio.play(); // Reproduce la canción
                estaPausado = false; // Marca que la canción está en reproducción
                songButton.querySelector('i').classList.remove('fa-play'); // Cambia ícono a "stop"
                songButton.querySelector('i').classList.add('fa-stop');
                
            }
        }
    });
}