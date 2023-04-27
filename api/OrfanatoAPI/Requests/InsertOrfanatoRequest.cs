using OrfanatoAPI.DTOs;
using OrfanatoAPI.Models;
using System.Globalization;

namespace OrfanatoAPI.Requests;

public class InsertOrfanatoRequest
{
    public string Name { get; set; }
    public string About { get; set; }
    public string Whatsapp { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Instructions { get; set; }
    public string AbreAs { get; set; }
    public string FechaAs { get; set; }
    public string OpenOnWeekends { get; set; }
    //public List<BlobImagesDTO> Images { get; set; }

    public InsertOrfanatoRequest() { }

    public Orphanage GetEntity() =>
        new()
        {
            Name = Name,
            Whatsapp = Whatsapp,
            Latitude = decimal.Parse(Latitude, CultureInfo.InvariantCulture),
            Longitude = decimal.Parse(Longitude, CultureInfo.InvariantCulture),
            About = About,
            Instructions = Instructions,
            AbreAs = AbreAs,
            FechaAs = FechaAs,
            AbertoFimDeSemana = bool.Parse(OpenOnWeekends)
        };
}
