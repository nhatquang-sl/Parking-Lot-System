using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLS.Application.Parking.Commands.Leave;
using PLS.Application.Parking.Commands.Park;

namespace WebUI.Controllers
{
    public class ParkingController : ApiController
    {
        [HttpPost]
        public async Task<IList<SpotDto>> Park(ParkCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task Leave(LeaveCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
