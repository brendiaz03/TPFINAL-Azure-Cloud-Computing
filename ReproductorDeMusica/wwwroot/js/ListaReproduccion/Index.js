// Variables globales
const submitButtonPlaylist = document.getElementById("submitButtonPlaylist");
const imageFileInput = document.getElementById("coverImage");
const selectImageButton = document.getElementById("selectImageButton");
const imageFileNameDisplay = document.getElementById("imageFileName");

document.addEventListener("DOMContentLoaded", () => {
    crearAlertaDeMensaje();
    submitButtonPlaylist.disabled = true; // Desactiva el botón inicialmente

    // Configuración de eventos
    $('#playlistForm').submit(function (event) {
        event.preventDefault();

        const formData = new FormData(this);

        crearPlaylist(formData);
        resetearForm();
    });

    selectImageButton.onclick = () => imageFileInput.click();

    // Eventos para habilitar el botón si ambos campos están completos
    document.getElementById("nombre").addEventListener("input", toggleSubmitButton);
    imageFileInput.addEventListener("change", toggleSubmitButton);

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
            mostrarAlerta('La playlist fue creada exitosamente', 'success');
        },
        error: function (xhr, status, error) {
            mostrarAlerta('Hubo un error al crear la playlist', 'error');
        }
    });
}

// Función para habilitar el botón de enviar solo si el campo de nombre y la imagen están seleccionados
function toggleSubmitButton() {
    const nombre = document.getElementById("nombre").value.trim();
    const imageSelected = imageFileInput.files.length > 0;

    submitButtonPlaylist.disabled = !(nombre && imageSelected);
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

function crearAlertaDeMensaje() {
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
