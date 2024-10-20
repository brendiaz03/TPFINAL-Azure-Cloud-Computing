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

    // Validar en cada cambio de los inputs
    document.getElementById("songName").addEventListener("input", validateForm);
    document.getElementById("artist").addEventListener("input", validateForm);
    audioFileInput.addEventListener("change", validateForm);
    imageFileInput.addEventListener("change", validateForm);

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

    // Llama a la función para crear la canción solo si el form tiene los datos necesarios
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
            console.log("Canción creada");
        },
        error: function (xhr, status, error) {
            console.error('Error en la generación de la canción:', error);
        }
    });
}

function validateForm() {
    const songName = document.getElementById("songName").value.trim();
    const artist = document.getElementById("artist").value.trim();
    const audioFileInput = document.getElementById("audioFile");
    const imageFileInput = document.getElementById("coverImage");

    const audioSelected = audioFileInput.files.length > 0;
    const imageSelected = imageFileInput.files.length > 0;

    // Habilita el botón solo si todos los campos son válidos
    if (songName && artist && audioSelected && imageSelected) {
        submitButtonSong.disabled = false;
        return true;
    } else {
        submitButtonSong.disabled = true;
        return false;
    }
}

function resetearForm() {
    document.getElementById('songName').value = "";
    document.getElementById('artist').value = "";
    document.getElementById('album').value = "";
    document.getElementById('audioFile').value = "";
    document.getElementById('coverImage').value = "";
    document.getElementById('audioFileName').textContent = "";
    document.getElementById('imageFileName').textContent = "";
    submitButtonSong.disabled = true;
    cerrarModal('addSongModal');
}

function cerrarModal(idModal) {
    $('#' + idModal).modal('hide');
}
