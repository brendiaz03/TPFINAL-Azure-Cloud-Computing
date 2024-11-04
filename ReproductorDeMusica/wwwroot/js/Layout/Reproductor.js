window.addEventListener('scroll', function () {
    const footer = document.querySelector('.footer');
    const reproductor = document.querySelector('.reproductor');
    const footerRect = footer.getBoundingClientRect();
    const reproductorRect = reproductor.getBoundingClientRect();

    if (footerRect.top < reproductorRect.height) {
        reproductor.classList.add('stuck');
    } else {
        reproductor.classList.remove('stuck');
    }
});
