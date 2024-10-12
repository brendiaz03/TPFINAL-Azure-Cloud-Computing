namespace ReproductorDeMusica.Entidades;
using ReproductorDeMusica.Entidades.Enumeradores;
public class Plan
{

    public int Id { get; set; }
    public TipoPlan TipoPlan { get; set; }
    public double Precio { get; set; }
    public int Duracion { get; set; }
}
