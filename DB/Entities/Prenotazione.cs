using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MVC_TDPC13.DB.Entities
{
    public class Prenotazione
    {
        [Key]
        public Guid? ID_prenota { get; set; }
        public string userName { get; set; }
        public string idUser { get; set; }
        public string idVillage { get; set; }
        public int? posti { get; set; }
        public string settimana { get; set; }
    }
}
