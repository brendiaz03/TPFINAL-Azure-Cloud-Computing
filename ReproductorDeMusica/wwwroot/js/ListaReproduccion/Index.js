// Variables globales
const submitButtonPlaylist = document.getElementById("submitButtonPlaylist");

document.addEventListener("DOMContentLoaded", () => {
    submitButtonPlaylist.disabled = true;

    const imageFileInput = document.getElementById("coverImage");
    const selectImageButton = document.getElementById("selectImageButton");
    const imageFileNameDisplay = document.getElementById("imageFileName");

    selectImageButton.onclick = () => imageFileInput.click();

    // Validar en cada cambio de los inputs
    document.getElementById("nombre").addEventListener("input", validateForm);
    imageFileInput.addEventListener("change", validateForm);

    imageFileInput.onchange = () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = file.name;
        }
    };
});

$('#playlistForm').submit(function (event) {
    event.preventDefault();

    const formData = new FormData(this); // Crea un FormData a partir del formulario

    // Llama a la función para crear la playlist solo si el form tiene los datos necesarios
    if (validateForm()) {
        crearPlaylist(formData);
        resetearForm();
    }
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
    const imageFileInput = document.getElementById("coverImage");

    const imageSelected = imageFileInput.files.length > 0;

    // Habilita el botón solo si todos los campos son válidos
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
    document.getElementById('coverImage').value = "";
    document.getElementById('imageFileName').textContent = "";
    submitButtonPlaylist.disabled = true;
    cerrarModal('addListaReproduccionModal');
}

function cerrarModal(idModal) {
    $('#' + idModal).modal('hide');
}