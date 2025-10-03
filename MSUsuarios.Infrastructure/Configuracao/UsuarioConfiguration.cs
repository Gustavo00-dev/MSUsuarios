using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MSUsuarios.Domain.Entities;

namespace MSUsuarios.Infrastructure.Configuracao
{
  public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.IdUsuario);
            builder.Property(u => u.IdUsuario).HasColumnType("INT").ValueGeneratedOnAdd();
            builder.Property(u => u.Nome).HasColumnName("Nome").IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).HasColumnName("Email").IsRequired().HasMaxLength(100);
            builder.Property(u => u.Senha).HasColumnName("Senha").IsRequired().HasMaxLength(100);
            builder.Property(u => u.Lvl).HasColumnName("Lvl").IsRequired().HasDefaultValue(0);
        }
    }
}