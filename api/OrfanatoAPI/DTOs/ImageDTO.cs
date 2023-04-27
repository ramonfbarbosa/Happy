
using OrfanatoAPI.Models;

namespace OrfanatoAPI.DTOs;

public class ImageDTO
{
    public int Id { get; set; }
    public string Url { get; set; }

    public ImageDTO(Image image)
    {
        Id = image.Id;
        Url = image.ImagemUrl;
    }
}
