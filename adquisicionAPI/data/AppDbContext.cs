using Microsoft.EntityFrameworkCore;
using adquisicionAPI.Models;

namespace adquisicionAPI.data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Adquisicion> Adquisiciones { get; set; }
        public DbSet<Historial> Historial { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la propiedad Presupuesto
            modelBuilder.Entity<Adquisicion>()
                .Property(a => a.Presupuesto)
                .HasColumnType("decimal(18, 2)"); // Precisión de 18 y escala de 2

            // Configurar otras propiedades decimales si es necesario
            modelBuilder.Entity<Adquisicion>()
                .Property(a => a.ValorUnitario)
                .HasColumnType("decimal(18, 2)"); // Precisión de 18 y escala de 2

            modelBuilder.Entity<Adquisicion>()
                .Property(a => a.ValorTotal)
                .HasColumnType("decimal(18, 2)"); // Precisión de 18 y escala de 2

            modelBuilder.Entity<Historial>()
                .Property(p => p.FechaCambio)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Historial>()
                .Property(p => p.Usuario)
                .HasDefaultValueSql("SUSER_NAME()");



            string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "SqlScripts", "TriggerAdquisiciones.sql");

            string triggerSql = File.ReadAllText(scriptPath);
            modelBuilder.HasAnnotation("SqlServer:PostDeploymentScript", triggerSql);


            modelBuilder.Entity<Adquisicion>()
                            .ToTable(tb => tb.HasTrigger("trg_Adquisiciones_Auditoria")); // Indica que la tabla tiene un trigger

            base.OnModelCreating(modelBuilder);
        }

public void AplicarTrigger()
{
    string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "SqlScripts", "TriggerAdquisiciones.sql");

    if (!File.Exists(scriptPath))
    {
        throw new FileNotFoundException($"El archivo de script '{scriptPath}' no se encontró.");
    }

    string triggerSql = File.ReadAllText(scriptPath);

            using var connection = Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                          command.CommandText = @"
                IF NOT EXISTS (SELECT 1 FROM sys.triggers WHERE name = 'trg_Adquisiciones_Auditoria')
                BEGIN
                    EXEC sp_executesql N'" + triggerSql.Replace("'", "''") + @"'
                END";
                command.ExecuteNonQuery();
            }
        }
    }


}