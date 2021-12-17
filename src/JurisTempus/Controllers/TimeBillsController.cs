using AutoMapper;
using JurisTempus.Data;
using JurisTempus.Data.Entities;
using JurisTempus.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace JurisTempus.Controllers
{
  // NOTE: We expect this to be a controller that only returns APIs, so we're going to perform the ModelState check for you. ApiController equals less boilerplate.
  [ApiController, Route("api/timebills")]
  public class TimeBillsController : ControllerBase
  {
    private readonly BillingContext context;
    private readonly ILogger<TimeBillsController> logger;
    private readonly IMapper mapper;

    public TimeBillsController(BillingContext context, ILogger<TimeBillsController> logger, IMapper mapper)
    {
      this.context = context;
      this.logger = logger;
      this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<TimeBillViewModel[]>> Get()
    {
      var entities = await context.TimeBills
        .Include(t => t.Case)
        .Include(t => t.Employee)
        .ToArrayAsync();
      var viewModels = mapper.Map<TimeBillViewModel[]>(entities);
      return Ok(viewModels);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TimeBillViewModel>> Get(int id)
    {
      var entity = await context.TimeBills
        .Include(t => t.Case)
        .Include(t => t.Employee)
        .Where(t => t.Id == id)
        .FirstOrDefaultAsync();
      var viewModel = mapper.Map<TimeBillViewModel>(entity);
      return Ok(viewModel);
    }

    [HttpPost()]
    public async Task<ActionResult<TimeBillViewModel>> Post([FromBody] TimeBillViewModel viewModel)
    {
      //NOTE: See class-level attribute.
      //if (!ModelState.IsValid)
      //{
      //  return BadRequest(ModelState);
      //}

      var entity = mapper.Map<TimeBill>(viewModel);

      var c = await context.Cases
        .Where(c => c.Id == viewModel.CaseId)
        .FirstOrDefaultAsync();

      var emp = await context.Employees
        .Where(e => e.Id == viewModel.EmployeeId)
        .FirstOrDefaultAsync();

      if (c == null || emp == null)
      {
        return BadRequest("Couldn't find case or employee");
      }

      entity.Case = c;
      entity.Employee = emp;
      context.Add(entity);

      if (await context.SaveChangesAsync() > 0)
      {
        var result = mapper.Map<TimeBillViewModel>(entity);
        return CreatedAtAction("Get", new { id = result.Id }, result);
      }

      return BadRequest("Failed to save new timebill");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TimeBillViewModel>> Put(int id, [FromBody] TimeBillViewModel viewModel)
    {
      var entity = await context.TimeBills
        .Where(b => b.Id == id)
        .FirstOrDefaultAsync();

      if (entity == null) return BadRequest("Invalid Id");

      mapper.Map(viewModel, entity);

      var c = await context.Cases
        .Where(c => c.Id == viewModel.CaseId)
        .FirstOrDefaultAsync();

      var emp = await context.Employees
        .Where(e => e.Id == viewModel.EmployeeId)
        .FirstOrDefaultAsync();

      entity.Case = c;
      entity.Employee = emp;

      if (await context.SaveChangesAsync() > 0)
      {
        var result = mapper.Map<TimeBillViewModel>(entity);
        return Ok(result);
      }

      return BadRequest("Failed to save new timebill");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
      var entity = await context.TimeBills
        .Where(b => b.Id == id)
        .FirstOrDefaultAsync();

      if (entity == null) return NotFound();

      context.Remove(entity);

      if (await context.SaveChangesAsync() > 0)
      {
        return Ok();
      }

      return BadRequest("Failed to save new timebill");
    }
  }
}
