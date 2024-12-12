using Core.Presentation.Models.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Question3.BusinessLogicLayer.Interfaces;
using Question3.BusinessLogicLayer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientManagement.Presentation.Web.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }



        // GET: api/<ClientController>/Get
        [HttpPost("[action]")]
        public IEnumerable<ClientDto> Get(ClientDto filter)
        {
            return this._clientService.Get(filter);
        }

        // GET api/<ClientController>/5
        [HttpGet("Get/{id}")]
        public async Task<ClientDto?> Get(Guid id)
        {
            return  (await _clientService.Get(x => !x.Archived && x.Id == id)).FirstOrDefault();
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<bool> Post(ClientDto value)
        {
            return await this._clientService.AddOrUpdate(value);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpPost("[action]/{id}")]
        public async Task<bool> Delete(Guid id)
        {
           return await _clientService.Delete(new[] { id });
        }
    }
}
