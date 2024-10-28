// Variables globales
const submitButtonPlaylist = document.getElementById("submitButtonPlaylist");
const imageFileInput = document.getElementById("coverImage");
const selectImageButton = document.getElementById("selectImageButton");
const imageFileNameDisplay = document.getElementById("imageFileName");

document.addEventListener("DOMContentLoaded", () => {
    submitButtonPlaylist.disabled = true; // Desactiva el botón inicialmente

    // Configuración de eventos
    $('#playlistForm').submit(function (event) {
        event.preventDefault();

        const formData = new FormData(this);

        if (validateForm()) {
            crearPlaylist(formData);
            resetearForm();
        }
    });

    selectImageButton.onclick = () => imageFileInput.click();

    document.getElementById("nombre").addEventListener("input", validateForm);
    imageFileInput.addEventListener("change", validateForm);

    imageFileInput.onchange = () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = file.name;
        }
    };

    $('#addPlaylistButton').on('click', function () {
        $('#addListaReproduccionModal').show();
    });
});

function crearPlaylist(formData) {
    $.ajax({
        url: '/ListaReproduccion/AgregarListaReproduccion',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            console.log("Playlist creada");
        },
        error: function (xhr, status, error) {
            console.error('Error en la generación de la playlist:', error);
        }
    });
}

function validateForm() {
    const nombre = document.getElementById("nombre").value.trim();
    const imageSelected = imageFileInput.files.length > 0;

    if (nombre && imageSelected) {
        submitButtonPlaylist.disabled = false;
        return true;
    } else {
        submitButtonPlaylist.disabled = true;
        return false;
    }
}

function resetearForm() {
    document.getElementById('nombre').value = "";
    imageFileInput.value = "";
    imageFileNameDisplay.textContent = "";
    submitButtonPlaylist.disabled = true;
    cerrarModal('addListaReproduccionModal');
}

function cerrarModal(idModal) {
    $('#' + idModal).modal('hide');
}
