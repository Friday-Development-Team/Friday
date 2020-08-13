using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
  //  [Authorize]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService service;

        public ConfigurationController(IConfigurationService service)
        {
            this.service = service;
        }
        /// <summary>
        /// Returns the Configuration options for this instance of FRIDAY
        /// </summary>
        /// <returns>Config</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Configuration Get()
        {
            return service.GetConfig();
        }
        /// <summary>
        /// Sets the configuration options
        /// </summary>
        /// <param name="config">Configuration option Object</param>
       // [Authorize(Roles = Role.Admin)]
        [HttpPut]
        public void Put([FromBody]Configuration config)
        {
            service.SetConfig(config);
        }

    }
}
