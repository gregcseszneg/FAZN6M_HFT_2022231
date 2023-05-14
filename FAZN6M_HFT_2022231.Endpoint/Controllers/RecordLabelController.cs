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
    public class RecordLabelController : ControllerBase
    {
        IRecordLabelLogic logic;
        IHubContext<SignalRHub> hub;
        public RecordLabelController(IRecordLabelLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        // GET: api/<RecordLabelController>
        [HttpGet]
        public IEnumerable<RecordLabel> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<RecordLabelController>/5
        [HttpGet("{id}")]
        public RecordLabel Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<RecordLabelController>
        [HttpPost]
        public void Create([FromBody] RecordLabel value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("RecordLabelCreated", value);
        }

        // PUT api/<RecordLabelController>/5
        [HttpPut]
        public void Update([FromBody] RecordLabel value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RecordLabelUpdated", value);
        }

        // DELETE api/<RecordLabelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var recordLabelDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RecordLabelDeleted", recordLabelDelete);
        }
    }
}
