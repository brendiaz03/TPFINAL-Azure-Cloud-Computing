document.addEventListener("DOMContentLoaded", () => {

    $('.add-song-button').on('click', function () {
        var cancionId = $(this).closest('.add-song-button').attr('data-cancion-id');
        agregarCancionAPlaylist(cancionId);
    });

});

function agregarCancionAPlaylist(cancionId) {
    $.ajax({
        url: '/CancionListaReproduccion/AgregarCancionListaReproduccion?idCancion=' + cancionId, 
        type: 'GET',
        success: function (idLista) {
            showToast('#F2C84B', 'La canción se agregó con éxito');

            setTimeout(function () {
                window.location.href = '/ListaReproduccion/VerListaReproduccion?id=' + idLista;
            }, 4000);

        },
        error: function (xhr, status, error) {
            console.log('Error al agregar la canción a la playlist');
        }
    });
}

function showToast(backgroundColor, message) {
    Toastify({
        text: message,
        duration: 3000, // Duración en milisegundos
        close: true, // Mostrar botón de cerrar
        gravity: 'bottom', // Posición (top o bottom)
        position: 'right', // Posición (left, center o right)
        backgroundColor: backgroundColor, // Color de fondo
        stopOnFocus: true, // Detener la animación si el mouse está sobre la notificación
    }).showToast();
}