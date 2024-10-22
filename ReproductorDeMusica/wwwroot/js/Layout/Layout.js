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

    $(document).on('click', '.btn-reproducir', function (e) {
        e.preventDefault();

    const audioUrl = $(this).data('ruta-audio');

    $('#audioSource').attr('src', audioUrl);

    const player = $('#audioPlayer')[0];
    player.load();

    player.play();
            });
        });