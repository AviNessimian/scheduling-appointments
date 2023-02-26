using Microsoft.AspNetCore.Mvc;
using SchedulingAppointments.Domain.Commands;
using SchedulingAppointments.Domain.Querys;
using SchedulingAppointments.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulingAppointments.Api
{
    [Route("appointments")]
    [ApiController]
    [TypeFilter(typeof(ServiceExceptionInterceptorAsync))]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> logger;
        public AppointmentsController(ILogger<AppointmentsController> logger)
        {
            this.logger=logger;
        }

        // GET /appointments?specialty=<SPECIALTY>&date=<DATE>&minScore=<SCORE>
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromServices] IProviderSearch providerSearch,
            [FromQuery] ProviderSearchQuery query)
        {
            var providers = await providerSearch.Handle(query);
            var dto = providers.Select(_ => _.Name);
            return Ok(dto);
        }

        // POST /appointments BODY: { “name”: string, “date”: date }
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] ICreateAppointment createAppointment,
            [FromBody] CreateAppointmentCommand command)
        {
            await createAppointment.Handle(command);
            return Ok();
        }
    }
}


