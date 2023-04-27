using OrfanatoAPI.Models;
using System.Security;

namespace OrfanatoAPI.DTOs;

public class OrfanatoByIdDTO
{
    public string Name { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Instructions { get; set; }
    public string About { get; set; }
    public string Whatsapp { get; set; }
    public string AbreAs { get; set; }
    public string FechaAs { get; set; }
    public bool OpenOnWeekends { get; set; }
    public List<ImageDTO> Images { get; set; }
}
