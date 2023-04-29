using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using static webApiPractica.Models.equipo;

namespace webApiPractica.Models
{
    public class equiposContext: DbContext
    {
        public equiposContext(DbContextOptions<equiposContext> options) : base(options)
        { }

       
        public DbSet<equipos> equipos { get; set; }

        public DbSet<marcas> marcas { get; set; }

        public DbSet<tipo_Equipo> tipo_equipo { get; set; }

        public DbSet<estados_Equipo> estados_equipo { get; set; }

        public DbSet<estados_Reserva> estados_reserva { get; set; }

        public DbSet<carreras> carreras { get; set; }

        public DbSet<facultad> facultades { get; set; }

        public DbSet<reservas> reservas { get; set; }

        public DbSet<usuarios> usuarios { get; set; }

    }
}
   

