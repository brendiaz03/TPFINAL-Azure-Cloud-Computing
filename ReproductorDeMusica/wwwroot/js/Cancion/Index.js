document.addEventListener("DOMContentLoaded", () => {

    crearModalParaCrearCancion();
    crearAlertaDeMensaje();

    const createSongButton = document.getElementById('createSongButton');
    if (createSongButton) {
        createSongButton.addEventListener("click", function () {
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

    document.getElementById("songForm").addEventListener("submit", function (event) {
        event.preventDefault(); // Previene el envío completo del formulario

        const formData = new FormData(this); // Recupera los datos del formulario

        fetch('/Cancion/AgregarCancion', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                // Cierra el modal y resetea el formulario 
                addSongModal.hide();
                this.reset(); // Resetea el formulario después de enviarlo
                audioFileNameDisplay.textContent = '';
                imageFileNameDisplay.textContent = '';
                mostrarAlerta('Canción creada exitosamente', 'success');;
            })
            .catch(error => {
                mostrarAlerta('Hubo un error al crear la canción', 'error');;
                console.error("Error:", error);
            });
    });

});

function crearCancion(formData) {
    $.ajax({
        url: '/Cancion/AgregarCancion',
        type: 'POST',
        data: formData,
        processData: false, // Evita que jQuery procese el data
        contentType: false, // Permite que el navegador gestione el content type
        success: function (data) {
            console.log('Canción creada con éxito');
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

function crearModalParaCrearCancion() {
    // Crear el modal
    const modalHTML = `
        <div class="modal fade" id="addSongModal" tabindex="-1" aria-labelledby="addSongModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content modal-song-content">
                    <div class="modal-header modal-song-header">
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

function crearAlertaDeMensaje() {
    // Crear el contenedor de la alerta y agregarlo al DOM
    const alertaHTML = `
        <div id="mensajeAlerta" class="alert text-center position-fixed d-none" role="alert" style="top: 50%; left: 50%; transform: translate(-50%, -50%); width: 50%; z-index: 1050;">
            <span id="mensajeAlertaIcono"></span>
            <span id="mensajeAlertaTexto"></span>
        </div>
    `;
    document.body.insertAdjacentHTML('beforeend', alertaHTML);
}

function mostrarAlerta(mensaje, tipo) {
    const mensajeAlerta = document.getElementById("mensajeAlerta");
    const mensajeAlertaIcono = document.getElementById("mensajeAlertaIcono");
    const mensajeAlertaTexto = document.getElementById("mensajeAlertaTexto");

    // Limpiar clases e íconos anteriores
    mensajeAlerta.className = "alert text-center position-fixed"; // Restablecer clases base
    mensajeAlertaIcono.className = ""; // Limpiar icono previo
    mensajeAlertaTexto.textContent = mensaje;

    // Añadir clases e íconos según el tipo de mensaje (success o error)
    if (tipo === 'success') {
        mensajeAlerta.classList.add("alert-success");
        mensajeAlertaIcono.classList.add("fa-solid", "fa-circle-check", "text-success", "me-2");
    } else {
        mensajeAlerta.classList.add("alert-danger");
        mensajeAlertaIcono.classList.add("fa-solid", "fa-circle-xmark", "text-danger", "me-2");
    }

    // Mostrar la alerta
    mensajeAlerta.classList.remove("d-none");

    // Cerrar automáticamente después de 3 segundos
    setTimeout(() => {
        mensajeAlerta.classList.add("d-none");
    }, 3000);
}

function cerrarModal(idModal) {
    $('#' + idModal).modal('hide');
}
