document.addEventListener("DOMContentLoaded", () => {
    const audioFileInput = document.getElementById("audioFile");
    const selectAudioButton = document.getElementById("selectAudioButton");
    const audioFileNameDisplay = document.getElementById("audioFileName");

    const imageFileInput = document.getElementById("coverImage");
    const selectImageButton = document.getElementById("selectImageButton");
    const imageFileNameDisplay = document.getElementById("imageFileName");

    const submitButtonSong = document.getElementById("submitButtonSong");

    const playlistSelect = document.getElementById("playlist");
    const addPlaylistButton = document.getElementById("addPlaylistButton");

    selectAudioButton.onclick = () => audioFileInput.click();
    selectImageButton.onclick = () => imageFileInput.click();

    audioFileInput.onchange = () => {
        if (audioFileInput.files.length > 0) {
            const file = audioFileInput.files[0];
            audioFileNameDisplay.textContent = `Archivo de audio: ${file.name}`;
            validateForm();
        }
    };

    imageFileInput.onchange = () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = `Imagen seleccionada: ${file.name}`;
            validateForm();
        }
    };

    document.getElementById("songForm").onsubmit = async (e) => {
        e.preventDefault();
        submitButtonSong.disabled = true;
        submitButtonSong.textContent = "Subiendo...";

        await new Promise(resolve => setTimeout(resolve, 2000)); // Simulación de subida

        submitButtonSong.textContent = "Subir canción";
        resetForm();
        bootstrap.Modal.getInstance(document.getElementById("addSongModal")).hide();
    };

    // Reemplazar por el AJAX
    addPlaylistButton.onclick = () => {
        const newPlaylist = prompt("Ingrese el nombre de la nueva playlist:");
        if (newPlaylist) {
            const newOption = document.createElement("option");
            newOption.value = newPlaylist.toLowerCase().replace(/\s+/g, '-');
            newOption.textContent = newPlaylist;
            playlistSelect.appendChild(newOption);
            playlistSelect.value = newOption.value; // Selecciona la nueva playlist
            validateForm();
        }
    };

    function validateForm() {
        const songName = document.getElementById("songName").value.trim();
        const artist = document.getElementById("artist").value.trim();
        const audioSelected = audioFileInput.files.length > 0;
        const imageSelected = imageFileInput.files.length > 0;
        const playlistSelected = playlistSelect.value;

        submitButton.disabled = !(songName && artist && audioSelected && imageSelected && playlistSelected);
    }

    function resetForm() {
        document.getElementById("songName").value = "";
        document.getElementById("artist").value = "";
        audioFileInput.value = null;
        imageFileInput.value = null;
        playlistSelect.value = "";
        audioFileNameDisplay.textContent = "";
        imageFileNameDisplay.textContent = "";
    }
});