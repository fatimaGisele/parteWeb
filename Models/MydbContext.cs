using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteCarrito> ClienteCarritos { get; set; }

    public virtual DbSet<ClienteMediodepago> ClienteMediodepagos { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<IndumentariaHasVenta> IndumentariaHasVentas { get; set; }

    public virtual DbSet<Indumentarium> Indumentaria { get; set; }

    public virtual DbSet<MedioDePago> MedioDePagos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=mydb;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.20-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PRIMARY");

            entity
                .ToTable("carrito")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdIndumentaria, "fk_carrito_indumentaria1_idx");

            entity.Property(e => e.IdCarrito)
                .HasColumnType("int(11)")
                .HasColumnName("idCarrito");
            entity.Property(e => e.IdIndumentaria)
                .HasColumnType("int(11)")
                .HasColumnName("idIndumentaria");
            entity.Property(e => e.MontoTotal)
                .HasPrecision(10)
                .HasColumnName("montoTotal");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("categoria")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CategoriaNombre)
                .HasMaxLength(45)
                .HasColumnName("categoria_nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("cliente")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(45)
                .HasColumnName("contraseña");
            entity.Property(e => e.Mail)
                .HasMaxLength(45)
                .HasColumnName("mail");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasColumnType("int(11)")
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasColumnType("int(11)")
                .HasColumnName("telefono");
            entity.Property(e => e.Usuario)
                .HasMaxLength(45)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<ClienteCarrito>(entity =>
        {
            entity.HasKey(e => new { e.IdCliente, e.IdCarrito })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("cliente_carrito")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdCarrito, "fk_clienteData_has_carrito_carrito1_idx");

            entity.HasIndex(e => e.IdCliente, "fk_clienteData_has_carrito_clienteData1_idx");

            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("idCliente");
            entity.Property(e => e.IdCarrito)
                .HasColumnType("int(11)")
                .HasColumnName("idCarrito");
            entity.Property(e => e.Cantidad)
                .HasColumnType("int(11)")
                .HasColumnName("cantidad");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.ClienteCarritos)
                .HasForeignKey(d => d.IdCarrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clienteData_has_carrito_carrito1");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClienteCarritos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clienteData_has_carrito_clienteData1");
        });

        modelBuilder.Entity<ClienteMediodepago>(entity =>
        {
            entity.HasKey(e => new { e.IdCliente, e.IdmedioDePago })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("cliente_mediodepago")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdCliente, "fk_clienteData_has_medio_de_pago_clienteData_idx");

            entity.HasIndex(e => e.IdmedioDePago, "fk_clienteData_has_medio_de_pago_medio_de_pago1_idx");

            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("idCliente");
            entity.Property(e => e.IdmedioDePago)
                .HasColumnType("int(11)")
                .HasColumnName("idmedio_de_pago");
            entity.Property(e => e.Tipo)
                .HasMaxLength(45)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ClienteMediodepagos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clienteData_has_medio_de_pago_clienteData");

            entity.HasOne(d => d.IdmedioDePagoNavigation).WithMany(p => p.ClienteMediodepagos)
                .HasForeignKey(d => d.IdmedioDePago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_clienteData_has_medio_de_pago_medio_de_pago1");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Idgenero).HasName("PRIMARY");

            entity.ToTable("genero");

            entity.Property(e => e.Idgenero)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idgenero");
            entity.Property(e => e.Genero1)
                .HasMaxLength(15)
                .HasColumnName("genero");
        });

        modelBuilder.Entity<IndumentariaHasVenta>(entity =>
        {
            entity.HasKey(e => new { e.IndumentariaId, e.VentasId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("indumentaria_has_ventas")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IndumentariaId, "fk_indumentaria_has_ventas_indumentaria1_idx");

            entity.HasIndex(e => e.VentasId, "fk_indumentaria_has_ventas_ventas1_idx");

            entity.Property(e => e.IndumentariaId)
                .HasColumnType("int(11)")
                .HasColumnName("indumentaria_id");
            entity.Property(e => e.VentasId)
                .HasColumnType("int(11)")
                .HasColumnName("ventas_id");
            entity.Property(e => e.Cantidad)
                .HasColumnType("int(11)")
                .HasColumnName("cantidad");
        });

        modelBuilder.Entity<Indumentarium>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.CategoriaId, e.GeneroId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity
                .ToTable("indumentaria")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.CategoriaId, "fk_indumentaria_categoria1_idx");

            entity.HasIndex(e => e.GeneroId, "fk_indumentaria_genero1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("categoria_id");
            entity.Property(e => e.GeneroId)
                .HasColumnType("int(11)")
                .HasColumnName("genero_id");
            entity.Property(e => e.Detalle)
                .HasMaxLength(500)
                .HasColumnName("detalle");
            entity.Property(e => e.Img)
                .HasMaxLength(100)
                .HasColumnName("img");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Stock)
                .HasColumnType("int(11)")
                .HasColumnName("stock");
            entity.Property(e => e.Talle)
                .HasColumnType("int(11)")
                .HasColumnName("talle");
            entity.Property(e => e.Tipo)
                .HasMaxLength(45)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Indumentaria)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_indumentaria_categoria1");

            entity.HasOne(d => d.Genero).WithMany(p => p.Indumentaria)
                .HasForeignKey(d => d.GeneroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_indumentaria_genero1");
        });

        modelBuilder.Entity<MedioDePago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("medio_de_pago")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Numero)
                .HasColumnType("int(11)")
                .HasColumnName("numero");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => new { e.Idventas, e.ClienteId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ventas");

            entity.HasIndex(e => e.ClienteId, "fk_ventas_cliente1_idx");

            entity.Property(e => e.Idventas)
                .HasColumnType("int(11)")
                .HasColumnName("idventas");
            entity.Property(e => e.ClienteId)
                .HasColumnType("int(11)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ventas_cliente1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
