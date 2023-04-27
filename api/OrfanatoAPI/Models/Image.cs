using OrfanatoAPI.DTOs;

namespace OrfanatoAPI.Models;

public class Image
{
    public int Id { get; set; }
    public string ImagemUrl { get; set; }
    public int OrfanatoId { get; set; }

    public Image() { }

    public Image(string caminho, int orfanatoId)
    {
        ImagemUrl = caminho;
        OrfanatoId = orfanatoId;
    }


    public Image(BlobImagesDTO dto)
    {
        ImagemUrl = dto.Url;
    }
}
