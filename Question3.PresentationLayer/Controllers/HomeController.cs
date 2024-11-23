using Microsoft.AspNetCore.Mvc;
using Question3.BusinessLogicLayer;
using Question3.BusinessLogicLayer.Interfaces;
using Question3.Models.DataTransferObjects;
using Question3.PresentationLayer.Models;
using System.Diagnostics;

namespace Question3.PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientService _clientService;
        public HomeController(ILogger<HomeController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        public Task<IActionResult> Index([FromQuery] ClientDto filters)
        {
            try
            {
                var viewModel = new ClientsViewModel();
                viewModel.SearchFormComponentViewModel.FormState = filters;
                viewModel.TableConfig.Data = (_clientService.Get(filters)).Select(x => (dynamic)x).ToList();
                return Task.FromResult<IActionResult>(View(viewModel));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Input\n: {filters.ToJsonString()}");
                throw;
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromForm] ClientDto client)
        {
            try
            {
                var success = await this._clientService.AddOrUpdate(client);
                return Redirect(this.Request.Headers.Referer!);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Input\n: {client.ToJsonString()}");
                throw;
            }
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            try
            {
                await this._clientService.Delete(new List<Guid> { id });
                return Redirect(Request.Headers.Referer!);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Input\n: {id.ToJsonString()}");
                throw;
            }
        
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> Details(Guid Id)
        {
            try
            {
               var client = (await this._clientService.Get(x => x.Id == Id)).FirstOrDefault();
               var vModel = new ClientDetailsViewModel(client ?? new ClientDto());
               if(client is ClientDto clientDto)
                {
                    vModel.ClientDto = clientDto;
                }
               return View(vModel);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"input:\n {Id.ToJsonString()}");
                throw;
            }
        }

        public async Task<IActionResult> Archive([FromRoute] Guid id)
        {
            try
            {
                var success = await this._clientService.Archive(new List<Guid> { id });
                var vModel = new ClientsViewModel();
                vModel.TableConfig.Data = (await _clientService.Get(x => !x.Archived)).Select(x => (dynamic)x).ToList();
                return Redirect(this.Request.Headers.Referer!);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Input\n: {id.ToJsonString()}");
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
