using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLS.Application.Level.Queries.GetAllLevelsQuery;

namespace WebUI.Controllers
{
    public class LevelController : ApiController
    {
        [HttpGet]
        public async Task<IList<LevelDto>> Get()
        {
            return await Mediator.Send(new GetAllLevelsQuery());
        }
    }
}
