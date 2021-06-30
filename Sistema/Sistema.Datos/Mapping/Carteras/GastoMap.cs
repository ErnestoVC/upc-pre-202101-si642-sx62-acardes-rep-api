using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Carteras;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Carteras
{
    public class GastoMap : IEntityTypeConfiguration<Gasto>
    {
        public void Configure(EntityTypeBuilder<Gasto> builder)
        {
            builder.ToTable("gasto")
                .HasKey(a => a.idgasto);
        }
    }
}
