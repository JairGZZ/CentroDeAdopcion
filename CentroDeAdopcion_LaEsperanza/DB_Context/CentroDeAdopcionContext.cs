using CentroDeAdopcion_LaEsperanza.Models;
using Microsoft.EntityFrameworkCore;

namespace CentroDeAdopcion_LaEsperanza.DB_Context;

public partial class CentroDeAdopcionContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CentroDeAdopcionContext(DbContextOptions<CentroDeAdopcionContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            

            var connectionString = _configuration.GetConnectionString("CentroDeAdopcionContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Adopcione> Adopciones { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adopcione>(entity =>
        {
            entity.HasKey(e => e.IdAdopcion).HasName("PK__Adopcion__E4E156E9B264DB76");

            entity.Property(e => e.IdAdopcion).HasColumnName("id_adopcion");
            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_solicitud");
            entity.Property(e => e.IdAdoptante).HasColumnName("id_adoptante");
            entity.Property(e => e.IdMascota).HasColumnName("id_mascota");

            entity.HasOne(d => d.IdAdoptanteNavigation).WithMany(p => p.Adopciones)
                .HasForeignKey(d => d.IdAdoptante)
                .HasConstraintName("FK__Adopcione__id_ad__4316F928");

            entity.HasOne(d => d.IdMascotaNavigation).WithMany(p => p.Adopciones)
                .HasForeignKey(d => d.IdMascota)
                .HasConstraintName("FK__Adopcione__id_ma__4222D4EF");
        });

        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.IdMascota).HasName("PK__Mascotas__6F03735295F2FCE5");

            entity.Property(e => e.IdMascota).HasColumnName("id_mascota");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.EstadoSalud)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("estado_salud");
            entity.Property(e => e.Foto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.IdPropietario).HasColumnName("id_propietario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Raza)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("raza");
            entity.Property(e => e.Sexo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sexo");
            entity.Property(e => e.Tamaño)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tamaño");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdPropietarioNavigation).WithMany(p => p.Mascota)
                .HasForeignKey(d => d.IdPropietario)
                .HasConstraintName("FK__Mascotas__id_pro__3E52440B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04AD862941B4");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__AB6E61641BB4BBD8").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
