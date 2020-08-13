using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Friday.Controllers {
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase {

        private readonly IOrderService service;

        public OrderController(IOrderService service) {
            this.service = service;
        }


        // GET api/<controller>/5
        /// <summary>
        /// Returns the history of all the completed Orders placed by a user.
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <returns>Order history. Check schema for format</returns>
        [HttpGet("history")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderHistory> Get() {
            var name = User.Identity.Name;
            var result = service.GetHistory(name);
            if (result == null)
                return new NotFoundObjectResult(null);
            return new OkObjectResult(result);
        }



        // POST api/<controller>
        /// <summary>
        /// Places an Order.
        /// </summary>
        /// <param name="order">JSON containing the needed information. Requires a username and a List of objects containing ItemId and Amount.</param>
        /// <returns>ID of the order with a 200 if successfully placed. 400 otherwise.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<int> Post([FromBody]OrderDTO order) {
            var result = service.PlaceOrder(User.Identity.Name, order);
            if (result != 0)
                return new OkObjectResult(result);
            return new BadRequestResult();
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Sets the Accepted flag of an Order.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="isKitchen">If it should be sent to the kitchen instead of accepted (if catering and kitchen are not combined, see config options)</param>
        /// <param name="value">True if the Order needs to be Accepted. False if it should return to Pending</param>
        /// <returns>True if the change was successful and was not already set to that value</returns>
        [HttpPut("accept/{id}/{isKitchen}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Catering,Kitchen")]
        public ActionResult<bool> Accept(int id, bool isKitchen, [FromBody]bool value) {
            var result = service.SetAccepted(id, value, isKitchen);
            if (result)
                return new OkResult();
            return new NotFoundResult();
        }
        /// <summary>
        /// Cancels an Order. Sets the Status flag to Cancelled. This cannot be undone. A new Order needs to be placed instead.
        /// An Order can only be cancelled if its Status is Pending.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the change was successful.</returns>
        [HttpPut("cancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
      //  [Authorize(Roles = "Catering")]
        public ActionResult<bool> Cancel(int id) {
            var result = service.Cancel(id);
            if (result)
                return new OkResult();
            return new NotFoundResult();
        }

        /// <summary>
        /// Completed an Order. Sets the Status flag to Completed. This cannot be undone.
        /// An Order can only be Completed if its Status is Accepted.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if the change was successful.</returns>
        [HttpPut("complete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       // [Authorize(Roles = "Catering")]
        public ActionResult<bool> Complete(int id, [FromBody] string type) {
            if (type.ToLower() != "food" && type.ToLower() != "beverage" && type.ToLower() != "both")
                return new NotFoundResult();

            var result = type.ToLower() == "both"
                 ? service.SetCompleted(id, true) && service.SetCompleted(id, false)//If both need to set to complete
                 : service.SetCompleted(id, type.ToLower() == "beverage");//Else either Beverage or Food

            if (result)
                return new OkResult();
            return new NotFoundResult();
        }

        /// <summary>
        /// Returns the Status of the specified Order as a string
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>String form of the Status</returns>
        [HttpGet("status/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<string> GetStatus(int id) {
            var result = service.GetStatus(id);
            if (result == null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Returns a List of all the ongoing Orders
        /// </summary>
        /// <returns>List of all ongoing Orders</returns>
        [HttpGet("catering")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = Role.Admin + "," + Role.Catering + "," + Role.Kitchen)]
        public ActionResult<IList<CateringOrder>> GetAll(bool isKitchen) {
            var result = service.GetAll(isKitchen) ?? new List<CateringOrder>();
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Returns a list of all the running orders of a user, sorted by Accepted first, then by date
        /// </summary>
        /// <returns>List of running orders of user</returns>
        [HttpGet("running")]
        [ProducesResponseType(StatusCodes.Status200OK)]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IList<CateringOrder>> GetRunningOrders() {
            var user = User.Identity.Name;
            var result = service.GetAll(false).Where(s => s.User == user) ?? new List<CateringOrder>();
            return new OkObjectResult(result);
        }

    }
}
