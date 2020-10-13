using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataStorage _dataStorage;

        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;

            _dataStorage = DataStorage.GetDataStorage();
        }

        /// <summary>
        /// Get values from controller.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the array of values</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetValues()
        {
            _logger.LogInformation("User send request to get values");

            return Ok(_dataStorage.Values.ToArray());
        }

        /// <summary>
        /// Get value from list by index.
        /// </summary>
        /// <param name="index"> Index of element to get.</param>
        /// <returns></returns>
        /// <response code="200">Return values</response>
        /// <response code="400">Nothing to return. Incorrect index.</response>
        /// <response code="401">Non-authorized.</response>
        [Authorize]
        [HttpGet("{index:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int index)
        {
            try
            {
                _logger.LogInformation("User send request to get values[index]");

                return Ok(_dataStorage.Values.ElementAt(index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add some string value to the list of values.
        /// </summary>
        /// <param name="value"> Value for adding to the list.</param>
        /// <returns></returns>
        /// <response code="200">Nothing to return. Operation is successful.</response>
        /// <response code="400">Input value is null or empty.</response>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult CreateValue([FromBody]string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return BadRequest();
            }

            _dataStorage.Values.Add(value);

            return Ok();
        }

        /// <summary>
        /// Update element from the list at index position to value.
        /// </summary>
        /// <param name="index">Index of element to update.</param>
        /// <param name="value">Value to update.</param>
        /// <returns></returns>
        /// <response code="200">Nothing to return. Operation is successful.</response>
        /// <response code="400">Incorrect input.</response>
        [HttpPut("{index:int}/{value}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult UpdateValue(int index, string value)
        {
            try
            {
                _dataStorage.Values[index] = value;

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Delete element from list by index;
        /// </summary>
        /// <param name="index">Index of element to delete.</param>
        /// <returns></returns>
        /// <response code="200">Nothing to return. Operation is successful.</response>
        /// <response code="400">Input is incorrect.</response>
        [HttpDelete("{index:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult RemoveValue(int index)
        {
            try
            {
                _dataStorage.Values.RemoveAt(index);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
