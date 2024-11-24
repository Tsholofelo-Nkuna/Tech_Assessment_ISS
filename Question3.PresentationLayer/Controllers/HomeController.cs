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
            
            return this.HandleServiceRequestErrors(this._logger, () => {
                var viewModel = new ClientsViewModel();
                viewModel.SearchFormComponentViewModel.FormState = filters;
                viewModel.TableConfig.Data = (_clientService.Get(filters)).Select(x => (dynamic)x).ToList();
                return Task.FromResult<IActionResult>(View(viewModel));
            }, filters);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdate([FromForm] ClientDto client)
        {
            var result = await this.HandleServiceRequestErrors(_logger, async () =>
            {
                var success = await this._clientService.AddOrUpdate(client);
                return Redirect(this.Request.Headers.Referer!);
            }, client);
            return result;
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
          return await this.HandleServiceRequestErrors(_logger, async () =>
           {
               await this._clientService.Delete(new List<Guid> { id });
               return Redirect(Request.Headers.Referer!);
           }, id);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> Details(Guid Id)
        {
            return await this.HandleServiceRequestErrors(_logger, async () =>
            {
                var client = (await this._clientService.Get(x => x.Id == Id)).FirstOrDefault();
                var vModel = new ClientDetailsViewModel(client ?? new ClientDto());
                if (client is ClientDto clientDto)
                {
                    vModel.ClientDto = clientDto;
                }
                return View(vModel);
            }, Id);
        }

        public async Task<IActionResult> Archive([FromRoute] Guid id)
        {
            return await this.HandleServiceRequestErrors(_logger, async () =>
            {
                var success = await this._clientService.Archive(new List<Guid> { id });
                var vModel = new ClientsViewModel();
                vModel.TableConfig.Data = (await _clientService.Get(x => !x.Archived)).Select(x => (dynamic)x).ToList();
                return Redirect(this.Request.Headers.Referer!);
            }, id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
