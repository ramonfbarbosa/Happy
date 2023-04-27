using OrfanatoAPI.Models;

namespace OrfanatoAPI.Requests;

public class InsertImageRequest
{
    public string ImageUrl { get; set; }
    public int OrfanatoFK { get; set; }

    public InsertImageRequest() { }

    public InsertImageRequest(string imageUrl, int orfanatoFk)
    {
        ImageUrl = imageUrl;
        OrfanatoFK = orfanatoFk;
    }

    public InsertImageRequest(Image imagem)
    {
        ImageUrl = imagem.ImagemUrl;
        OrfanatoFK = imagem.OrfanatoId;
    }

    public Image GetEntity() =>
        new()
        {
            ImagemUrl = ImageUrl,
            OrfanatoId = OrfanatoFK
        };
}
