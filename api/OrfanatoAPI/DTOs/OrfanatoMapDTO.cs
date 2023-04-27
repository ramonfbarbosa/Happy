using OrfanatoAPI.Models;

namespace OrfanatoAPI.DTOs;

public class OrfanatoMapDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public OrfanatoMapDTO(Orphanage entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Latitude = entity.Latitude;
        Longitude = entity.Longitude;
    }
}
