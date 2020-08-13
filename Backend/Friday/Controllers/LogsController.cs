using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Logs;
using Friday.Models.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Friday.DTOs;

namespace Friday.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {

        private readonly ILogsService service;
        private readonly IItemService itemService;

        public LogsController(ILogsService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Returns a List containing all the logs involving currency.
        /// </summary>
        /// <returns>List of currency logs</returns>
        [HttpGet("currency/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //  [Authorize(Roles = Role.Admin)]
        public ActionResult<IList<LogDTO>> GetAllCurrencyLogs()
        {
            var result = service.GetAllCurrencyLogs();
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }

        [HttpGet("item/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [Authorize(Roles = Role.Admin)]
        public ActionResult<IList<LogDTO>> GetAllItemLogs()
        {
            var result = service.GetAllItemLogs();
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }

        [HttpGet("currency/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = Role.Admin)]
        public ActionResult<IList<LogDTO>> GetByUser([FromQuery] string param)
        {
            var result = service.GetByUser(param);
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }
        /// <summary>
        /// Returns all available logs linked to a given item.
        /// </summary>
        /// <param name="param">Parameter. ID of the item.</param>
        /// <returns></returns>
        [HttpGet("item/id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [Authorize(Roles = Role.Admin)]
        public ActionResult<IList<LogDTO>> GetPerItem([FromQuery] int param)
        {
            var result = service.GetPerItem(param);
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }

        [HttpGet("stock/remaining")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [Authorize(Roles = Role.Admin)]
        public ActionResult<IList<ItemAmountDTO>> GetRemainingStock()
        {
            var result = service.GetRemainingStock();
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }

        [HttpGet("stock/sold")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [Authorize(Roles = Role.Admin)]
        public ActionResult<IList<ItemAmountDTO>> GetTotalStockSold()
        {
            var result = service.GetTotalStockSold();
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }


        [HttpGet("total")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [Authorize(Roles = Role.Admin)]
        public ActionResult<double> GetTotalIncome()
        {
            var result = service.GetTotalIncome();
            if (result != null)
                return new OkObjectResult(result);
            return NotFound();
        }


    }
}