using OrfanatoAPI.Models;

namespace OrfanatoAPI.DTOs;

public class OrfanatoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Whatsapp { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Sobre { get; set; }
    public string Instrucoes { get; set; }
    public string AbreAs { get; set; }
    public string FechaAs { get; set; }
    public bool AbertoFimDeSemana { get; set; }
    public bool Ativo { get; set; }
    public List<Image> Imagens { get; set; }

    public OrfanatoDTO() { }

    public OrfanatoDTO(Orphanage orfanato)
    {
        Id = orfanato.Id;
        Nome = orfanato.Name;
        Whatsapp = orfanato.Whatsapp;
        Latitude = orfanato.Latitude;
        Longitude = orfanato.Longitude;
        Sobre = orfanato.About;
        Instrucoes = orfanato.Instructions;
        AbreAs = orfanato.AbreAs;
        FechaAs = orfanato.FechaAs;
        AbertoFimDeSemana = orfanato.AbertoFimDeSemana;
        Ativo = orfanato.Ativo;
        Imagens = orfanato.Imagens;
    }
}