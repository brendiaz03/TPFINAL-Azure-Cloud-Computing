document.addEventListener("DOMContentLoaded", () => {
    const addPlaylistButton = document.getElementById("addPlaylistButton");
    const closePlayListButton = document.getElementById("closePlayListButton");
    const addPlaylistModal = new bootstrap.Modal(document.getElementById("addListaReproduccionModal"));

    addPlaylistButton.addEventListener("click", () => {
        addPlaylistModal.show();
    });
    closePlayListButton.addEventListener("click", () => {
        addPlaylistModal.hide();
    })
    // Configuración de otros eventos, por ejemplo, para habilitar el botón de enviar
    const submitButtonPlaylist = document.getElementById("submitButtonPlaylist");
    const imageFileInput = document.getElementById("coverImage");
    const selectImageButton = document.getElementById("selectImageButton");
    const imageFileNameDisplay = document.getElementById("imageFileName");
    const nombreInput = document.getElementById("nombre");

    submitButtonPlaylist.disabled = true; // Desactiva el botón inicialmente

    // Evento para seleccionar la imagen
    selectImageButton.addEventListener("click", () => imageFileInput.click());

    // Eventos para habilitar el botón de envío cuando ambos campos están completos
    nombreInput.addEventListener("input", toggleSubmitButton);
    imageFileInput.addEventListener("change", toggleSubmitButton);

    imageFileInput.addEventListener("change", () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = file.name;
        }
    });

    function toggleSubmitButton() {
        const nombre = nombreInput.value.trim();
        const imageSelected = imageFileInput.files.length > 0;
        submitButtonPlaylist.disabled = !(nombre && imageSelected);
    }
});
