public class ModalViewModel
{
    public string Title { get; set; }
    public string Message { get; set; }
    public string ButtonText { get; set; } = "Aceptar"; // Texto del bot�n por defecto
    public bool IsVisible { get; set; } = false; // Controla si el modal debe ser visible
    public string ModalId { get; set; } // ID del modal para manejar m�ltiples modales si es necesario
}