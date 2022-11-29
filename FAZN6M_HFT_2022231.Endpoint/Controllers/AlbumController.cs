using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAZN6M_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        IAlbumLogic logic;

        public AlbumController(IAlbumLogic logic)
        {
            this.logic = logic;
        }
        // GET: api/<AlbumController>
        [HttpGet]
        public IEnumerable<Album> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public Album Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<AlbumController>
        [HttpPost]
        public void Create([FromBody] Album value)
        {
            this.logic.Create(value);
        }

        // PUT api/<AlbumController>/5
        [HttpPut]
        public void Update(int id, [FromBody] Album value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
