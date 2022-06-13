using MVC_TDPC13.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_TDPC13.DB
{
    public class Repository
    {
        private DBContext DBContext;
        public Repository(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        public List<Prenotazione> GetPersons()
        {
            //select * from persons
            List<Prenotazione> result = this.DBContext.Prenotazione.ToList();
            return result;
        }
        public Prenotazione GetPersonByID(string id)
        {
            //select * from persons where id = "id"
            Prenotazione result = this.DBContext.Prenotazione.Where(p => p.ID_prenota.ToString() == id).FirstOrDefault();
            return result;
        }
        public List<Prenotazione> GetPersonWithFilter(string filter)
        {
            //select * from persons where nome like "%filter%"
            //or cognome like "%filter%"
            List<Prenotazione> result = this.DBContext.Prenotazione
                .Where(p => p.idVillage.Contains(filter)
                            || p.idUser.Contains(filter)
                            || p.settimana.Contains(filter)
                       ).ToList();
            return result;
        }
        public String InsertPerson(Prenotazione person)
        {
            try
            {
                this.DBContext.Prenotazione.Add(person);
                this.DBContext.SaveChanges();
                return "Record inserito";
            }
            catch (Exception ex) 
            {
                return ex.ToString(); // stampa messaggio errore
            }
        }
    }
}