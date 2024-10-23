
document.addEventListener("DOMContentLoaded", () => {
    const imageFileInput = document.getElementById("ImagenUsuario");
    const selectImageButton = document.getElementById("selectImageButton");
    const imageFileNameDisplay = document.getElementById("imageFileName");

    selectImageButton.onclick = () => imageFileInput.click();

    imageFileInput.onchange = () => {
        if (imageFileInput.files.length > 0) {
            const file = imageFileInput.files[0];
            imageFileNameDisplay.textContent = file.name;
        }
    };

});