using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAZN6M_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        ITrackLogic logic;
        public TrackController(ITrackLogic logic)
        {
            this.logic = logic;
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
        }

        // PUT api/<TrackController>/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] Track value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<TrackController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
