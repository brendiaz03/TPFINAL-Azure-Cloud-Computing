// Variables globales
const submitButtonPlaylist = document.getElementById("submitButtonPlaylist");

document.addEventListener("DOMContentLoaded", () => {
    submitButtonPlaylist.disabled = true;

    const imageFileInput = document.getElementById("coverImage");
    const selectImageButton = document.getElementById("selectImageButton");
    const imageFileNameDisplay = document.getElementById("imageFileName");
    const playlistNameInput = document.getElementById("nombre");

    selectImageButton.onclick = () => imageFileInput.click();

    imageFileInput.onchange = () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = file.name;
        }
    };

    playlistNameInput.addEventListener('input', () => {
        validateForm(); // Validar cada vez que el input cambia
    });

    // Manejo del evento submit
    $('#playlistForm').submit(function (event) {
        event.preventDefault();

        const formData = new FormData(this);

        // Llama a la función para crear la playlist, solo si el form tiene los datos necesarios
        if (validateForm()) {
            crearPlaylist(formData);
            resetearForm();
        }
    });
});

function crearPlaylist(formData) {
    $.ajax({
        url: '/ListaReproduccion/AgregarListaReproduccion',
        type: 'POST',
        data: formData,
        processData: false, // Evita que jQuery procese el data
        contentType: false, // Permite que el navegador gestione el content type
        success: function (data) {
            console.log('Playlist creada con éxito:', data);
        },
        error: function (xhr, status, error) {
            console.error('Error en la generación de la playlist:', error);
        }
    });
}

function validateForm() {
    const playlistName = document.getElementById("nombre").value.trim();

    if (playlistName) {
        submitButtonPlaylist.disabled = false;
    } else {
        submitButtonPlaylist.disabled = true;
    }

    return playlistName !== "";
}

function resetearForm() {
    document.getElementById('nombre').value = "";
    document.getElementById('coverImage').value = null;
    submitButtonPlaylist.disabled = true;
    cerrarModal('addListaReproduccionModal');
}

function cerrarModal(idModal) {
    $('#' + idModal).modal('hide');
}
