// Variables globales
let audio = new Audio(); 
let cancionReproduciendo = null; 
let estaPausado = false; 

document.addEventListener("DOMContentLoaded", () => {
    configurarInteraccionesPlaylist();
    crearAlertaDeMensaje();
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

function manejarEliminarCancion() {
    $(document).on('click', '.delete-song-button', function (e) {
        e.stopPropagation();

        const idCancion = e.target.closest('.delete-song-button').getAttribute('data-cancion-id');
        const idListaCancion = e.target.cloest('.delete-song-button').getAttribute('data-lista-id');
        const fila = e.target.closest('tr');

        if (idCancion && idListaCancion) {
            eliminarCancionDeLaLista(idCancion, idLista, fila);

        }

    });
}

function eliminarCancionDeLaLista(idCancion, idLista, fila) {
    $.ajax({
        url: '/CancionListaReproduccionController/EliminarCancionListaReproduccion', 
        type: 'POST',
        data: { idCancion: idCancion, idLista: idLista},
        success: function (response) {
            mostrarAlerta('La canción se ha eliminado correctamente', 'success');
            $(fila).remove();
        },
        error: function (xhr, status, error) {
            mostrarAlerta('No hemos podido eliminar la canción de la playlist', 'error');
        }
    });
}

function crearAlertaDeMensaje() {
    // Crear el contenedor de la alerta y agregarlo al DOM
    const alertaHTML = `
        <div id="mensajeAlerta" class="alert text-center position-fixed d-none" role="alert" style="top: 50%; left: 50%; transform: translate(-50%, -50%); width: 50%; z-index: 1050;">
            <span id="mensajeAlertaIcono"></span>
            <span id="mensajeAlertaTexto"></span>
        </div>
    `;
    document.body.insertAdjacentHTML('beforeend', alertaHTML);
}

function mostrarAlerta(mensaje, tipo) {
    const mensajeAlerta = document.getElementById("mensajeAlerta");
    const mensajeAlertaIcono = document.getElementById("mensajeAlertaIcono");
    const mensajeAlertaTexto = document.getElementById("mensajeAlertaTexto");

    // Limpiar clases e íconos anteriores
    mensajeAlerta.className = "alert text-center position-fixed"; // Restablecer clases base
    mensajeAlertaIcono.className = ""; // Limpiar icono previo
    mensajeAlertaTexto.textContent = mensaje;

    // Añadir clases e íconos según el tipo de mensaje (success o error)
    if (tipo === 'success') {
        mensajeAlerta.classList.add("alert-success");
        mensajeAlertaIcono.classList.add("fa-solid", "fa-circle-check", "text-success", "me-2");
    } else {
        mensajeAlerta.classList.add("alert-danger");
        mensajeAlertaIcono.classList.add("fa-solid", "fa-circle-xmark", "text-danger", "me-2");
    }

    // Mostrar la alerta
    mensajeAlerta.classList.remove("d-none");

    // Cerrar automáticamente después de 3 segundos
    setTimeout(() => {
        mensajeAlerta.classList.add("d-none");
    }, 3000);
}