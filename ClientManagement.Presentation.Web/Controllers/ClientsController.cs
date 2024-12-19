using Core.Presentation.Models.DataTransferObjects;

using Microsoft.AspNetCore.Mvc;
using ClientManagement.BusinessLogicLayer.Interfaces;
using ClientManagement.BusinessLogicLayer;
using Core.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientManagement.Presentation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientsController> _logger;
        private readonly ControllerRequestHandler<ClientsController> _requestHandler;
        public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
        {
            _clientService = clientService;
            _logger = logger;
            _requestHandler = new ControllerRequestHandler<ClientsController>(_logger);
        }



        // GET: api/<ClientController>/Get
        [HttpPost("[action]")]
        public async Task<IEnumerable<ClientDto>?> Get(ClientDto filter)
        {
            var response = (await _requestHandler.HandleRequest(async () =>
            {
                return await this._clientService.Get(filter);
            }, nameof(Get)));
            return response;
          
        }

        // GET api/<ClientController>/5
        [HttpGet("Get/{id}")]
        public async Task<ClientDto?> Get(Guid id)
        {
            return await this._requestHandler.HandleRequest(async () => (await _clientService.Get(x => !x.Archived && x.Id == id)).FirstOrDefault(),
                 nameof(Get),
                 id
                );
           
        }

        // GET api/<ClientController>/5
        [HttpGet("GetByArchive/{id}")]
        public async Task<ClientDto?> Get(Guid id,[FromQuery] bool archived)
        {
            return  (await _clientService.Get( new ClientDto { Id = id, Archived= archived})).FirstOrDefault();
        }

        // POST api/<ClientController>
        [HttpPost]
        public async Task<bool> Post(ClientDto value)
        {
            return await this._clientService.AddOrUpdate(value);
        }

        // GET api/Archive/5
        [HttpGet("[action]/{id}")]
        public async Task<bool> Archive(Guid id)
        {
          return  await this._clientService.Archive(new[] { id });
        }

        // DELETE api/Delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<bool> Delete(Guid id)
        {
           return await _clientService.Delete(new[] { id });
        }
    }
}
