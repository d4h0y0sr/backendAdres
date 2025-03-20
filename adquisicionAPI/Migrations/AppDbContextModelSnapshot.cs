﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using adquisicionAPI.data;

#nullable disable

namespace adquisicionAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:PostDeploymentScript", "CREATE TRIGGER trg_Adquisiciones_Auditoria\r\nON dbo.Adquisiciones\r\nAFTER INSERT, UPDATE\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\r\n    -- Manejar INSERT (Registra todos los campos)\r\n    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)\r\n    BEGIN\r\n        INSERT INTO Historial (IdAdquisicion, Operacion, FechaCambio, Usuario, Cambios)\r\n        SELECT \r\n            i.Id, \r\n            'INSERT', \r\n            GETDATE(), \r\n            SUSER_NAME(),\r\n            JSON_QUERY('[' + STRING_AGG(\r\n                '{\"campo\": \"' + c.campo + '\", \"antiguo\": null, \"nuevo\": \"' + ISNULL(c.nuevo, 'NULL') + '\"}', ', ')\r\n            + ']')\r\n        FROM inserted i\r\n        CROSS APPLY (VALUES\r\n            ('Presupuesto', NULL, CAST(i.Presupuesto AS NVARCHAR(50))),\r\n            ('Unidad', NULL, i.Unidad),\r\n            ('TipoBienServicio', NULL, i.TipoDeBien),\r\n            ('Cantidad', NULL, CAST(i.Cantidad AS NVARCHAR(50))),\r\n            ('ValorUnitario', NULL, CAST(i.ValorUnitario AS NVARCHAR(50))),\r\n            ('ValorTotal', NULL, CAST(i.ValorTotal AS NVARCHAR(50))),\r\n            ('FechaAdquisicion', NULL, CONVERT(NVARCHAR, i.FechaAdquisicion, 120)), -- Formato ISO\r\n            ('Proveedor', NULL, i.Proveedor),\r\n            ('Documentacion', NULL, i.Documentacion),\r\n            ('Activo', NULL, CAST(i.Activo AS NVARCHAR(5))) -- Convertir bit a string ('0' o '1')\r\n        ) AS c(campo, antiguo, nuevo)\r\n        GROUP BY i.Id;\r\n    END\r\n\r\n    -- Manejar UPDATE (Solo registrar campos que cambiaron)\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO Historial (IdAdquisicion, Operacion, FechaCambio, Usuario, Cambios)\r\n        SELECT \r\n            i.Id, \r\n            'UPDATE', \r\n            GETDATE(), \r\n            SUSER_NAME(),\r\n            JSON_QUERY('[' + STRING_AGG(\r\n                '{\"campo\": \"' + c.campo + '\", \"antiguo\": \"' + ISNULL(c.antiguo, 'NULL') + '\", \"nuevo\": \"' + ISNULL(c.nuevo, 'NULL') + '\"}', ', ')\r\n            + ']')\r\n        FROM inserted i\r\n        JOIN deleted d ON i.Id = d.Id\r\n        CROSS APPLY (VALUES\r\n            ('Presupuesto', CAST(d.Presupuesto AS NVARCHAR(50)), CAST(i.Presupuesto AS NVARCHAR(50))),\r\n            ('Unidad', d.Unidad, i.Unidad),\r\n            ('TipoBienServicio', d.TipoDeBien, i.TipoDeBien),\r\n            ('Cantidad', CAST(d.Cantidad AS NVARCHAR(50)), CAST(i.Cantidad AS NVARCHAR(50))),\r\n            ('ValorUnitario', CAST(d.ValorUnitario AS NVARCHAR(50)), CAST(i.ValorUnitario AS NVARCHAR(50))),\r\n            ('ValorTotal', CAST(d.ValorTotal AS NVARCHAR(50)), CAST(i.ValorTotal AS NVARCHAR(50))),\r\n            ('FechaAdquisicion', CONVERT(NVARCHAR, d.FechaAdquisicion, 120), CONVERT(NVARCHAR, i.FechaAdquisicion, 120)), \r\n            ('Proveedor', d.Proveedor, i.Proveedor),\r\n            ('Documentacion', d.Documentacion, i.Documentacion),\r\n            ('Activo', CAST(d.Activo AS NVARCHAR(5)), CAST(i.Activo AS NVARCHAR(5))) -- Convertir bit a string ('0' o '1')\r\n        ) AS c(campo, antiguo, nuevo)\r\n        WHERE c.antiguo <> c.nuevo -- solo guardar si hubo un cambio\r\n        GROUP BY i.Id;\r\n    END\r\nEND;\r\nGO");

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("adquisicionAPI.Models.Adquisicion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Documentacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaAdquisicion")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Presupuesto")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Proveedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDeBien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Adquisiciones", t =>
                        {
                            t.HasTrigger("trg_Adquisiciones_Auditoria");
                        });

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);
                });

            modelBuilder.Entity("adquisicionAPI.Models.Historial", b =>
                {
                    b.Property<int>("IdHistorial")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHistorial"));

                    b.Property<string>("Cambios")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCambio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("IdAdquisicion")
                        .HasColumnType("int");

                    b.Property<string>("Operacion")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("SUSER_NAME()");

                    b.HasKey("IdHistorial");

                    b.ToTable("Historial");
                });
#pragma warning restore 612, 618
        }
    }
}
