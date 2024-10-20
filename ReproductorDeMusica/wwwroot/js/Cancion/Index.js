// Variables globales
const submitButtonSong = document.getElementById("submitButtonSong");

document.addEventListener("DOMContentLoaded", () => {
    submitButtonSong.disabled = true;

    const audioFileInput = document.getElementById("audioFile");
    const selectAudioButton = document.getElementById("selectAudioButton");
    const audioFileNameDisplay = document.getElementById("audioFileName");

    const imageFileInput = document.getElementById("coverImage");
    const selectImageButton = document.getElementById("selectImageButton");
    const imageFileNameDisplay = document.getElementById("imageFileName");

    selectAudioButton.onclick = () => audioFileInput.click();
    selectImageButton.onclick = () => imageFileInput.click();

    audioFileInput.onchange = () => {
        if (audioFileInput.files.length > 0) {
            const file = audioFileInput.files[0];
            audioFileNameDisplay.textContent = file.name;
        }
    };

    imageFileInput.onchange = () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = file.name;
        }
    };

});

$('#songForm').submit(function (event) {
    event.preventDefault();

    const formData = new FormData(this); // Crea un FormData a partir del formulario

    // Llama a la función para crear la canción, solo si el form tiene los datos necesarios
    if (validateForm()) {
        crearCancion(formData);
        resetearForm();
    }

});

function crearCancion(formData) {
    $.ajax({
        url: '/Cancion/AgregarCancion',
        type: 'POST',
        data: formData,
        processData: false, // Evita que jQuery procese el data
        contentType: false, // Permite que el navegador gestione el content type
        success: function (data) {
            //    return data;
        },
        error: function (xhr, status, error) {
            console.error('Error en la generación de la canción:', error);
        }
    });
}

function validateForm() {
    const songName = document.getElementById("songName").value.trim();
    const artist = document.getElementById("artist").value.trim();
    const audioFileInput = document.getElementById("audioFile").value;
    const audioSelected = audioFileInput.files.length > 0;
    const imageFileInput = document.getElementById("coverImage").value;
    const imageSelected = imageFileInput.files.length > 0;

    var valido = false;

    if (songName && artist && audioSelected && imageSelected) {
        submitButtonSong.disabled = false;
        valido = true;
    }

    return valido;
}

function resetearForm() {
    document.getElementById('songName').value = "";
    document.getElementById('artist').value = "";
    document.getElementById('album').value = "";
    document.getElementById('audioFile').value = null;
    document.getElementById('coverImage').value = null;
    submitButtonSong.disabled = true;
    cerrarModal('addSongModal');
}

function cerrarModal(idModal) {
    $('#' + idModal).modal('hide');
}