// Evento de clic en toda la fila de la tabla
$(document).on('click', '.playlist-item', function (e) {
    e.preventDefault();

    const audioUrl = $(this).data('ruta-audio');

    $('#audioSource').attr('src', audioUrl);
    const player = $('#audioPlayer')[0];
    player.load();
    player.play();
});

// Si quieres mantener la funcionalidad del botón, puedes dejar este código también
$(document).on('click', '.btn-reproducir', function (e) {
    e.stopPropagation(); // Evita que el clic en el botón active también el evento de la fila
    const audioUrl = $(this).closest('tr').data('ruta-audio');

    $('#audioSource').attr('src', audioUrl);
    const player = $('#audioPlayer')[0];
    player.load();
    player.play();
});
});