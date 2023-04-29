using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace webApiPractica.Models
{
    public class estados_Reserva
    {
        [Key]
        public int estado_res_id { get; set; }
        public string estado { get; set; }
    }
}
