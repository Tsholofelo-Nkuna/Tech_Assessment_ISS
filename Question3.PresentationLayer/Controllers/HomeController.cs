using Microsoft.AspNetCore.Mvc;
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
            var viewModel = new ClientsViewModel();
            viewModel.SearchFormComponentViewModel.FormState = filters;
            viewModel.TableConfig.data =  (_clientService.Get(filters)).Select(x => (dynamic)x).ToList(); 
            return Task.FromResult<IActionResult>(View(viewModel));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrCreate([FromForm] ClientDto client)
        {
            if(client.Id != Guid.Empty)
            {
                await this._clientService.Update(new () { client });
            }
            else
            {
                await this._clientService.Insert(new() { client });
            }
            var results =  (await _clientService.Get(x => !x.Archived)).Select(x => (dynamic)x).ToList();
            var vModel = new ClientsViewModel();
            vModel.TableConfig.data = results;
            return View(nameof(Index),vModel);
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            await this._clientService.Delete(new List<Guid>{ id});
            var vModel = new ClientsViewModel();
            vModel.TableConfig.data = (await _clientService.Get(x => !x.Archived)).Select(x => (dynamic)x).ToList();
            return View(nameof(Index),vModel);
        }

        public async Task<IActionResult> Archive([FromRoute] Guid id)
        {
            var toBeArchived = (await this._clientService.Get(x => !x.Archived && x.Id == id)).Select(x =>
            {
                x.Archived = true;
                return x;
            }).ToList();
            await this._clientService.Update(toBeArchived);
            var vModel = new ClientsViewModel();
            vModel.TableConfig.data = (await _clientService.Get(x => !x.Archived)).Select(x => (dynamic)x).ToList();
            return View(nameof(Index),vModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
