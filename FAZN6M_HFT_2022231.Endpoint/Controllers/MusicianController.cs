using FAZN6M_HFT_2022231.Endpoint.Services;
using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAZN6M_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MusicianController : ControllerBase
    {

        IMusicianLogic logic;
        IHubContext<SignalRHub> hub;
        public MusicianController(IMusicianLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        // GET: api/<MusicianController>
        [HttpGet]
        public IEnumerable<Musician> ReadAll()
        {
            return this.logic.ReadAll();
        }
        // GET api/<MusicianController>/5
        [HttpGet("{id}")]
        public Musician Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<MusicianController>
        [HttpPost]
        public void Create([FromBody] Musician value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("MusicianCreated", value);
        }

        // PUT api/<MusicianController>/5
        [HttpPut]
        public void Update([FromBody] Musician value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("MusicianUpdated", value);
        }

        // DELETE api/<MusicianController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var musicianDelete= this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("MusicianDeleted", musicianDelete);
        }
    }
}

