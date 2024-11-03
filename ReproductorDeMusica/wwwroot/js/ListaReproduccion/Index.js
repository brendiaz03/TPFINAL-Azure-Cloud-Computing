document.addEventListener("DOMContentLoaded", () => {
    // Variables globales
    const submitButtonPlaylist = document.getElementById("submitButtonPlaylist");
    const imageFileInput = document.getElementById("coverImage");
    const selectImageButton = document.getElementById("selectImageButton");
    const imageFileNameDisplay = document.getElementById("imageFileName");

    // Desactiva el botón de submit inicialmente
    submitButtonPlaylist.disabled = true;

    // Configuración de eventos
    document.getElementById("nombre").addEventListener("input", validateForm);
    imageFileInput.addEventListener("change", validateForm);
    selectImageButton.onclick = () => imageFileInput.click();

    $('#playlistForm').submit(function (event) {
        event.preventDefault();
        const formData = new FormData(this);

        if (validateForm()) {
            crearPlaylist(formData);
            resetearForm();
        }
    });

    // Actualizar el nombre del archivo de imagen cuando se selecciona una
    imageFileInput.addEventListener("change", () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = file.name;
        } else {
            imageFileNameDisplay.textContent = ""; // Limpia el nombre si no hay imagen seleccionada
        }
    });

    // Muestra el modal al hacer clic en el botón de agregar playlist
    $('#addPlaylistButton').on('click', function () {
        $('#addListaReproduccionModal').modal('show');
    });

    // Función para crear la playlist mediante una llamada AJAX
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

    // Validación del formulario
    function validateForm() {
        const nombre = document.getElementById("nombre").value.trim();
        const imageSelected = imageFileInput.files.length > 0;

        // Habilita o deshabilita el botón según la validación
        submitButtonPlaylist.disabled = !(nombre && imageSelected);
        return !submitButtonPlaylist.disabled;
    }

    // Resetea el formulario y cierra el modal
    function resetearForm() {
        document.getElementById("nombre").value = "";
        imageFileInput.value = "";
        imageFileNameDisplay.textContent = "";
        submitButtonPlaylist.disabled = true;
        cerrarModal('addListaReproduccionModal');
    }

    // Función para cerrar el modal
    function cerrarModal(idModal) {
        $('#' + idModal).modal('hide');
    }
});
