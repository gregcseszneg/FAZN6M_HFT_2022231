using FAZN6M_HFT_2022231.Endpoint.Services;
using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAZN6M_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        ITrackLogic logic;
        IHubContext<SignalRHub> hub;
        public TrackController(ITrackLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        // GET: api/<TrackController>
        [HttpGet]
        public IEnumerable<Track> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<TrackController>/5
        [HttpGet("{id}")]
        public Track Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<TrackController>
        [HttpPost]
        public void Create([FromBody] Track value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("TrackCreated", value);
        }

        // PUT api/<TrackController>/5
        [HttpPut]
        public void Update([FromBody] Track value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("TrackUpdated", value);
        }

        // DELETE api/<TrackController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var trackDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("TrackDeleted", trackDelete);
        }
    }
}
