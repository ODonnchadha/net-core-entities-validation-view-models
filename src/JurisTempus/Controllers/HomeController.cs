using AutoMapper;
using JurisTempus.Data;
using JurisTempus.Data.Entities;
using JurisTempus.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JurisTempus.Controllers
{
  public class HomeController : Controller
  {
    private readonly BillingContext context;
    private readonly ILogger<HomeController> logger;
    private readonly IMapper mapper;

  public HomeController(BillingContext context, ILogger<HomeController> logger, IMapper mapper)
  {
    this.context = context;
    this.logger = logger;
    this.mapper = mapper;
  }

    public IActionResult Index()
    {
        var clients = context.Clients.Include(c => c.Address).Include(c => c.Cases).ToArray();
        var result = mapper.Map<ClientViewModel[]>(clients);
        return View(result);
    }

    [HttpGet("editor/{id:int}")]
    public async Task<IActionResult> ClientEditor(int id)
    {
        var clients = await context.Clients.Include(c => c.Address).Where(c => c.Id == id).FirstOrDefaultAsync();
        var result = mapper.Map<ClientViewModel>(clients);
        return View(result);
    }

    [HttpPost("editor/{id:int}")]
    public async Task<IActionResult> ClientEditor(int id, ClientViewModel viewModel)
    {
      if (ModelState.IsValid)
      {
        var currentClient = await context.Clients.Include(c => c.Address).Where(c => c.Id == id).FirstOrDefaultAsync();

        if (null != currentClient)
        {
          mapper.Map(viewModel, currentClient);

          if (await context.SaveChangesAsync() > 0)
          {
            return RedirectToAction("Index");
          }
        }
        else
        {
          var newClient = mapper.Map<Client>(viewModel);
          context.Add(newClient);

          if (await context.SaveChangesAsync() > 0)
          {
            return RedirectToAction("Index");
          }
        }
      }

      return View();
    }

    [HttpGet("timesheet")]
    public IActionResult Timesheet()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
