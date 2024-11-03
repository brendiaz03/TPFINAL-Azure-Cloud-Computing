$(document).ready(function () {

    $('input[name="titulo"]').on('input', function () {
        const titulo = $(this).val();

        if (titulo.length > 2) { // Iniciar búsqueda cuando hay más de 2 caracteres
            $.get('/Cancion/Buscar', { titulo: titulo }, function (data) {
                $('#resultadosBusqueda').html(data).show(); // Muestra los resultados
            });
        } else {
            $('#resultadosBusqueda').hide(); // Oculta los resultados si la consulta es muy corta
        }
    });

    // Cerrar la lista de resultados al hacer clic fuera de ella
    $(document).on('click', function (e) {
        if (!$(e.target).closest('.search-bar').length) {
            $('#resultadosBusqueda').hide();
        }
    });

    document.getElementById('usuarioMenu').addEventListener('click', function () {
        const dropdownMenu = document.getElementById('dropdownMenu');
        dropdownMenu.style.display = dropdownMenu.style.display === 'none' ? 'block' : 'none';
    });

    // Cierra el menú si se hace clic fuera de él
    window.addEventListener('click', function (event) {
        const dropdownMenu = document.getElementById('dropdownMenu');
        if (!document.getElementById('usuarioMenu').contains(event.target)) {
            dropdownMenu.style.display = 'none';
        }
    });

});

$(document).ready(function () {
    $('#search-input').on('input', function () {
        const query = $(this).val();

        if (query.length > 2) {
            $.get('/Cancion/Buscar', { query: query }, function (data) {
                $('#resultadosBusqueda').html(data).show();
            });
        } else {
            $('#resultadosBusqueda').hide();
        }
    });

    // Variable para almacenar el botón que está en reproducción actual
    let currentButton = null;

    $(document).on('click', '.btn-reproducir', function (e) {
        e.preventDefault();

        const audioUrl = $(this).data('ruta-audio');
        const player = $('#audioPlayer')[0];

        // Verifica si el mismo botón fue presionado
        if (currentButton && currentButton[0] === this) {
            // Si ya está reproduciendo, pausamos y cambiamos el ícono
            if (!player.paused) {
                player.pause();
                $(this).html('▶️'); // Cambiar a icono de play
            } else {
                player.play();
                $(this).html('⏸️'); // Cambiar a icono de pausa
            }
        } else {
            // Si es otro botón, actualizamos la fuente del audio y reproducimos
            $('#audioSource').attr('src', audioUrl);
            player.load();
            player.play();

            // Cambiar el ícono del botón actual a pausa
            $(this).html('⏸️');

            // Restaurar el ícono de play en el botón anterior, si hay uno
            if (currentButton) {
                currentButton.html('▶️');
            }

            // Actualizar el botón en reproducción actual
            currentButton = $(this);
        }

        // Evento para restaurar el ícono al finalizar la canción
        player.onended = function () {
            currentButton.html('▶️');
            currentButton = null;
        };
    });
});
