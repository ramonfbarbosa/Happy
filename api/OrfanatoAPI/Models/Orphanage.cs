namespace OrfanatoAPI.Models;

public class Orphanage
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Whatsapp { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string About { get; set; }
    public string Instructions { get; set; }
    public string AbreAs { get; set; }
    public string FechaAs { get; set; }
    public bool AbertoFimDeSemana { get; set; }
    public bool Ativo { get; set; }
    public virtual List<Image> Imagens { get; set; }

    public Orphanage() { }
}
