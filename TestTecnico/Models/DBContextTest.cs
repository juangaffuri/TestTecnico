using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestTecnico.Models
{
    public partial class DBContextTest : DbContext
    {
        public DBContextTest()
        {
        }

        public DBContextTest(DbContextOptions<DBContextTest> options)
            : base(options)
        {
        }

        public virtual DbSet<Adelanto> Adelantos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<TipoEmpleado> TipoEmpleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SIS040\\SQLEXPRESS01;Initial Catalog=testTecnico;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Adelanto>(entity =>
            {
                entity.HasKey(e => e.IdAdelanto);

                entity.Property(e => e.IdAdelanto)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idAdelanto");

                entity.Property(e => e.Cancelado).HasColumnName("cancelado");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCancelacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCancelacion");

                entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

                entity.Property(e => e.Monto).HasColumnName("monto");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Adelantos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_Adelantos_Empleados");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Legajo);

                entity.Property(e => e.Legajo)
                    .ValueGeneratedNever()
                    .HasColumnName("legajo");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Dni)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.IdTipoEmpleado).HasColumnName("idTipoEmpleado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sueldo).HasColumnName("sueldo");

                entity.HasOne(d => d.IdTipoEmpleadoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdTipoEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleados_TipoEmpleados");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago);

                entity.Property(e => e.IdPago).HasColumnName("idPago");

                entity.Property(e => e.FechaPago)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaPago")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdAdelanto)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idAdelanto");

                entity.Property(e => e.MontoPago).HasColumnName("montoPago");

                entity.HasOne(d => d.IdAdelantoNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdAdelanto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagos_Adelantos");
            });

            modelBuilder.Entity<TipoEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdTipoEmpleado);

                entity.Property(e => e.IdTipoEmpleado).HasColumnName("idTipoEmpleado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.MaximoAdelanto).HasColumnName("maximoAdelanto");

                entity.Property(e => e.PorcentajeAdelanto).HasColumnName("porcentajeAdelanto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
