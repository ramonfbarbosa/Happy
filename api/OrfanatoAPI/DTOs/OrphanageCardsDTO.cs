using OrfanatoAPI.Models;

namespace OrfanatoAPI.DTOs;

public class OrphanageCardsDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Whatsapp { get; set; }
    public string AbreAs { get; set; }
    public string FechaAs { get; set; }
    public bool Status { get; set; }

    public OrphanageCardsDTO(Orphanage entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Whatsapp = entity.Whatsapp;
        AbreAs = entity.AbreAs;
        FechaAs = entity.FechaAs;
        Status = entity.Ativo;
    }
}
