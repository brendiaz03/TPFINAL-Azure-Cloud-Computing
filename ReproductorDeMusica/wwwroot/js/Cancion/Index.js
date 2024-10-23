// Variables globales
const submitButtonSong = document.getElementById("submitButtonSong");

document.addEventListener("DOMContentLoaded", () => {

    crearModalParaCrearPlaylist();

    const createPlaylistButton = document.getElementById('createPlaylistButton');
    if (createPlaylistButton) {
        createPlaylistButton.addEventListener("click", function () {
            addSongModal.show();
        });
    }

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
        submitButtonSong.disabled = false;
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

    const isValid = songName && artist && audioSelected && imageSelected;

    // Habilitar o deshabilitar el botón de submit
    submitButtonSong.disabled = !isValid;

    return isValid;
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

function crearModalParaCrearPlaylist() {
    // Crear el modal
    const modalHTML = `
        <div class="modal fade" id="addSongModal" tabindex="-1" aria-labelledby="addSongModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title fw-bold text-center" id="addSongModalLabel">Añadir nueva canción</h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p id="subtitle">Sube tu canción y añade los detalles aquí.</p>
                        <form id="songForm" action="/Cancion/AgregarCancion" method="POST" enctype="multipart/form-data">
                            <input type="hidden" readonly id="id" />
                            <div class="mb-3">
                                <label for="songName" class="form-label">Nombre de la canción</label>
                                <input type="text" class="form-control" id="songName" name="Titulo" placeholder="Ingrese el nombre de la canción" required>
                            </div>
                            <div class="mb-3">
                                <label for="artist" class="form-label">Artista</label>
                                <input type="text" class="form-control" id="artist" name="Artista" placeholder="Ingrese el nombre del artista" required>
                            </div>
                            <div class="mb-3">
                                <label for="album" class="form-label"> Álbum </label>
                                <input type="text" class="form-control" id="album" name="Album" placeholder="Ingrese el nombre del álbum" />
                            </div>
                            <div class="d-flex gap-3">
                                <div class="w-50">
                                    <label for="audioFile" class="form-label">Archivo de audio</label>
                                    <input type="file" class="form-control d-none" id="audioFile" name="Audio" accept="audio/*">
                                    <button type="button" id="selectAudioButton" class="btn btn-primary w-100">
                                        <i class="fa-solid fa-arrow-up-from-bracket"></i>  Seleccionar audio
                                    </button>
                                    <p id="audioFileName" class="mt-2 text-info"></p>
                                </div>
                                <div class="w-50">
                                    <label for="coverImage" class="form-label">Imagen de portada</label>
                                    <input type="file" class="form-control d-none" id="coverImage" name="Imagen" accept="image/*">
                                    <button type="button" id="selectImageButton" class="btn btn-secondary w-100">
                                        <i class="fa-solid fa-file-image"></i> Seleccionar portada
                                    </button>
                                    <p id="imageFileName" class="mt-2 text-info"></p>
                                </div>
                            </div>
                            <div class="d-flex justify-content-end mt-3">
                                <button type="button" class="btn btn-secondary me-2" data-bs-dismiss="modal">Cancelar</button>
                                <button type="submit" class="btn btn-success" id="submitButtonSong" disabled>Subir canción</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    `;

    // Agregar el modal al cuerpo del documento
    document.body.insertAdjacentHTML('beforeend', modalHTML);

    // Inicializar el modal de Bootstrap
    addSongModal = new bootstrap.Modal(document.getElementById('addSongModal'));
}


function cerrarModal(idModal) {
    $('#' + idModal).modal('hide');
}
