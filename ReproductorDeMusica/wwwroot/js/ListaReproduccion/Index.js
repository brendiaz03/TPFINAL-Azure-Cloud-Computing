// Variables globales
const submitButtonPlaylist = document.getElementById("submitButtonPlaylist");
const imageFileInput = document.getElementById("coverImagePlaylist");
const selectImageButton = document.getElementById("selectImagePlaylistButton");
const imageFileNameDisplay = document.getElementById("imageFileNamePlaylist");

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
        dataType: "json",
        success: function (data) {
            $('.modal-backdrop').hide();
            mostrarAlerta('La playlist fue creada exitosamente', 'success');
            agregarPlaylistAlHtml(data);
        },
        error: function (xhr, status, error) {
            $('.modal-backdrop').hide();
        }
    });
}

// Agregar la nueva playlist al HTML sin recargar
function agregarPlaylistAlHtml(playlist) {
    const playlistContainer = document.querySelector(".row.mt-4");

    const newPlaylistHtml = `
        <div class="col-md-4 mb-4">
            <a href="/ListaReproduccion/VerListaReproduccion/${playlist.id}" class="text-decoration-none">
                <div class="card h-100">
                    <div class="card-img-top" style="height: 200px; background-color: #f0f0f0; display: flex; align-items: center; justify-content: center;">
                        ${playlist.urlPortada ? `<img src="${playlist.urlPortada}" alt="Portada de ${playlist.nombre}" class="img-fluid" style="height: 100%; object-fit: cover;">` : `<span class="text-muted" style="font-size: 48px;">+</span>`}
                    </div>
                    <div class="card-body text-center">
                        <h5 class="card-title text-dark">${playlist.nombre}</h5>
                    </div>
                </div>
            </a>
        </div>
    `;

    // Insertar la nueva playlist en el contenedor
    playlistContainer.insertAdjacentHTML('beforeend', newPlaylistHtml);
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