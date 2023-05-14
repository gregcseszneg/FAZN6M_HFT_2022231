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
    public class AlbumController : ControllerBase
    {
        IAlbumLogic logic;
        IHubContext<SignalRHub> hub;
        public AlbumController(IAlbumLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub= hub;
        }

        [HttpGet]
        public IEnumerable<Album> ReadAll()
        {
            return this.logic.ReadAll();
        }


        [HttpGet("{id}")]
        public Album Read(int id)
        {
            return this.logic.Read(id);
        }


        [HttpPost]
        public void Create([FromBody] Album value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("AlbumCreated", value);
        }


        [HttpPut]
        public void Update([FromBody] Album value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("AlbumUpdated", value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var albumDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("AlbumDeleted", albumDelete);
        }
    }
}
