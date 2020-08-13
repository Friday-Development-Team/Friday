using System.Collections.Generic;
using Friday.Data.IServices;
using Friday.DTOs.Items;
using Friday.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class ItemController : ControllerBase
    {

        private readonly IItemService service;
        public ItemController(IItemService service)
        {
            this.service = service;
        }
        // GET: api/<controller>
        /// <summary>
        /// Returns a list containing all the Items. Check Schema's for their format. A default set of Items has been provided. These can be modified or deleted if needed.
        /// </summary>
        /// <returns>List of Items</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public ActionResult<IList<Item>> Get()
        {
            return new OkObjectResult(service.GetAll());
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Changes the Amount of an Item, or how many items of a certain Item are still in stock. Use a negative number to subtract. 
        /// </summary>
        /// <param name="id">Id of the Item</param>
        /// <param name="amount">Amount to be added. Negative to subtract</param>
        /// <returns>True if it was successful. You can't get a negative amount, at best the count can be reduced to 0</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [Authorize(Roles = Role.Admin)]
        public ActionResult<bool> Put(int id, int amount)
        {
            var result = service.ChangeCount(id, amount);
            if (result)
                return new OkResult();
            return new BadRequestResult();
        }

        /// <summary>
        /// Adds a new Item to the database.
        /// </summary>
        /// <param name="dto">ItemDTO object containing the data to make the Item</param>
        /// <returns>HTTP Code depending on result.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [Authorize(Roles = "Catering")]
        public ActionResult Post(ItemDTO dto)
        {
            var result = service.AddItem(dto.ToItem(), dto.Details.ToItemDetails());
            if (result)
                return new OkResult();
            return new BadRequestResult();

        }

        /// <summary>
        /// Deletes an Item. Removes the Item itself, its ItemDetails and all Orders where this Item was ordered.
        /// Use this with caution. This action cannot be undone and should only be used during setup.
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <returns>ActionResult depending on the outcome</returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Catering")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            var result = service.DeleteItem(id);
            if (result)
                return new OkResult();
            return NotFound();
        }
    }
}
