document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("songForm");
    const submitButton = document.getElementById("submitButtonSong");
    const songName = document.getElementById("songName");
    const artist = document.getElementById("artist");
    const audioFile = document.getElementById("audioFile");
    const coverImage = document.getElementById("coverImage");
    const audioFileName = document.getElementById("audioFileName");
    const imageFileName = document.getElementById("imageFileName");
    const selectAudioButton = document.getElementById("selectAudioButton");
    const selectImageButton = document.getElementById("selectImageButton");

    // Función para verificar que todos los campos estén completos
    function validateForm() {
        const isValid = songName.value && artist.value && audioFile.files.length > 0 && coverImage.files.length > 0;
        submitButton.disabled = !isValid;
    }

    // Escucha para habilitar el botón solo cuando todos los campos estén completos
    songName.addEventListener("input", validateForm);
    artist.addEventListener("input", validateForm);
    audioFile.addEventListener("change", function () {
        audioFileName.textContent = audioFile.files[0] ? audioFile.files[0].name : "";
        validateForm();
    });
    coverImage.addEventListener("change", function () {
        imageFileName.textContent = coverImage.files[0] ? coverImage.files[0].name : "";
        validateForm();
    });

    // Manejadores de botón para seleccionar archivos
    selectAudioButton.addEventListener("click", () => audioFile.click());
    selectImageButton.addEventListener("click", () => coverImage.click());

    // Desactiva el botón de envío inicialmente
    submitButton.disabled = true;

    // Maneja el envío del formulario usando AJAX
    //form.addEventListener("submit", function (event) {
    //    event.preventDefault(); // Evita el envío normal del formulario

    //    const formData = new FormData(form);

    //    // Envío del formulario con AJAX
    //    fetch(form.action, {
    //        method: form.method,
    //        body: formData
    //    })
    //        .then(response => {
    //            if (response.ok) {
    //                // Cierra el modal después de 3 segundos
    //                setTimeout(() => {
    //                    const modal = bootstrap.Modal.getInstance(document.getElementById("addSongModal"));
    //                    modal.hide();
    //                }, 3000);
    //            } else {
    //                console.error("Error al enviar el formulario");
    //            }
    //        })
    //        .catch(error => console.error("Error en la solicitud:", error));
    //});
});
