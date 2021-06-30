using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Carteras;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Carteras
{
    public class DetalleCarteraMap : IEntityTypeConfiguration<DetalleCartera>
    {
        public void Configure(EntityTypeBuilder<DetalleCartera> builder)
        {
            builder.ToTable("detalle_cartera")
                .HasKey(d => d.iddetalle_cartera);
        }
    }
}
