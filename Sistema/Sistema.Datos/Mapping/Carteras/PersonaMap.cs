using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Carteras;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Carteras
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("persona")
                .HasKey(p => p.idpersona);
        }
    }
}
