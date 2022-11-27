using FAZN6M_HFT_2022231.Logic;
using FAZN6M_HFT_2022231.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAZN6M_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordLabelController : ControllerBase
    {
        IRecordLabelLogic logic;

        public RecordLabelController(IRecordLabelLogic logic)
        {
            this.logic = logic;
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
        }

        // PUT api/<RecordLabelController>/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] RecordLabel value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<RecordLabelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
