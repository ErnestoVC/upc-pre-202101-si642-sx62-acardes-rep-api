using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Carteras;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Carteras
{
    public class CarteraMap : IEntityTypeConfiguration<Cartera>
    {
        public void Configure(EntityTypeBuilder<Cartera> builder)
        {
            builder.ToTable("cartera")
                .HasKey(i => i.idcartera);
            builder.HasOne(i => i.personas)
                .WithMany(p => p.carteras)
                .HasForeignKey(i => i.idcliente);
        }
    }
}
