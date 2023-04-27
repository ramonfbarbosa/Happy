using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrfanatoAPI.Models;

namespace OrfanatoAPI.Mapping;

public class ImagensMap : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("TAB_IMAGENS");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID_IMAGENS").HasColumnType("INT");
        builder.Property(x => x.ImagemUrl).HasColumnName("IMAGEM_URL").HasColumnType("VARCHAR(MAX)");
        builder.Property(x => x.OrfanatoId).HasColumnName("ORFANATO_ID").HasColumnType("INT");
    }
}
