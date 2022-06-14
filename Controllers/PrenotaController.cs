using Microsoft.AspNetCore.Http;
using MVC_TDPC13.DB;

using Microsoft.AspNetCore.Mvc;
using MVC_TDPC13.DB.Entities;
using MVC_TDPC13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_TDPC13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrenotaController : ControllerBase
    {
        
        private readonly Repository repository;
        public PrenotaController(Repository repository)
        {
            this.repository = repository;
        }


        // GET: api/<Prenota1Controller>
        [HttpGet]
        public IEnumerable<string> Get() // legge tutti
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<Prenota1Controller>
        [HttpGet("GetPrenotazioni")]
        public IEnumerable<Prenotazione> GetPrenotazioni([FromQuery] string id) // legge tutti // cambiare con guid
        {
            if (id != null)
            {
                //Prenotazione prenota = new Prenotazione();
                //renota.idUser = id;

                List<Prenotazione> myPrenota = this.repository.GetPersons(id);
                //String res = this.repository.InsertPerson(prenota);
                //var result = new { result = res };

                return myPrenota;
            }

            // return new string[] { "value1", "value2" };
            return new List<Prenotazione>();
        }

        // GET: api/<Prenota1Controller>
        [HttpGet("GetAllPrenotazioni")]
        public IEnumerable<Prenotazione> GetAllPrenotazioni() // legge tutti // cambiare con guid
        {
                List<Prenotazione> myPrenota = this.repository.GetPersons();
                return myPrenota;
        }

        // POST api/<Prenota1Controller>
        [HttpPost]
        //public void Post([FromBody] string value) // aggiunge uno (ha i dati nel body della request)
        public async Task<IActionResult> Post([FromBody] PrenotazioneModel model) // aggiunge uno (ha i dati nel body della request)
        {
            if (model != null)
            {
                Prenotazione prenota = new Prenotazione();
                prenota.userName = model.userName;
                prenota.idUser = model.idUser;
                prenota.idVillage = model.idVillage;
                prenota.posti = Convert.ToInt32(model.posti);
                prenota.settimana = model.settimana;

                String res = this.repository.InsertPerson(prenota);
                var result = new { result = res};

                return Ok(result); // riceve messaggio o errore
            }

            return Ok("Prenotazione non andata a buon fine " + model);
        }

        // PUT api/<Prenota1Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) // modifica singolo con id
        {
        }

        // DELETE api/<Prenota1Controller>/5
        [HttpDelete("{id}")] 
        public void Delete(int id) // elimina uno con id
        {
        }
    }
}
