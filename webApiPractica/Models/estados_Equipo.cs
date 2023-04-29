using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace webApiPractica.Models
{
    public class estados_Equipo
    {
        [Key]

        public int id_estados_equipo { get; set; }

        public string descripcion { get; set; }
        public string estado { get; set; }
    }
}
