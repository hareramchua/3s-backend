using _5s.Model;
using _5s.Services;
using Microsoft.AspNetCore.Mvc;

namespace _5s.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;
        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        /// <summary>
        /// Creates a new building
        /// </summary>
        /// <param name="building">Building details</param>
        /// <returns>Returns the newly created building</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /buildings
        ///     {
        ///         "buildingName: "GLE"
        ///         "BuildingCode: "1"
        ///         "Image[]: "images"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfully created a building</response>
        /// <response code="400">Building details invalid</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(Name = "CreateBuilding")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBuilding([FromBody] Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newBuilding = await _buildingService.CreateBuilding(building);
                return CreatedAtRoute("CreateBuilding", new { id = building.Id }, newBuilding);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get all building
        /// </summary>
        /// <returns>Returns all biulding</returns>
        /// <response code="200">Buildings found</response>
        /// <reponse code="204">No Buildings found</reponse>
        /// <reponse code="500">Internal server error</reponse>
        [HttpGet(Name = "GetAllBuilding")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBuilding()
        {
            try
            {
                var building = await _buildingService.GetAllBuilding();
                if (building == null || !building.Any())
                {
                    return NoContent();
                }
                return Ok(building);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Get Building by Id
        /// </summary>
        /// <param name="id">Building Id</param>
        /// <returns>Returns the details of building with id <paramref name="id"/></returns>
        /// <response code="200">Building found</response>
        /// <reponse code="404">Building not found</reponse>
        /// <reponse code="500">Internal server error</reponse>
        [HttpGet("{id}/building", Name = "GetBuildingById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBuilding(string id)
        {
            try
            {
                var building = await _buildingService.GetBuildingById(id);
                if (building == null)
                {
                    return NotFound();
                }
                return Ok(building);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update a Building by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="building"></param>
        /// <returns>Returns the Id of the Updated Building</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /buildings
        ///     {
        ///         "buildingName: "GLE"
        ///         "BuildingCode: "1"
        ///         "Image[]: "images"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Building found</response>
        /// <reponse code="404">Building not found</reponse>
        /// <reponse code="500">Internal server error</reponse>
        [HttpPut("{id}", Name = "UpdateBuilding")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBuilding(string id, [FromBody] Building building)
        {
            try
            {
                var dbbuilding = await _buildingService.GetBuildingById(id);
                if (dbbuilding == null)
                    return NotFound();
                var updatedBuilding = await _buildingService.UpdateBuilding(id, building);
                return Ok(updatedBuilding);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Delete an Existing Building
        /// </summary>
        /// <param name="name">The Name of the Building that will be deleted</param>
        /// <response code="200">Building found</response>
        /// <reponse code="404">Building not found</reponse>
        /// <reponse code="500">Internal server error</reponse>
        [HttpDelete("{name}", Name = "DeleteBuilding")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBuilding(string name)
        {
            try
            {
                var dbRatings = await _buildingService.GetBuildingByName(name);
                if (dbRatings == null)
                    return NotFound();
                await _buildingService.DeleteBuilding(dbRatings.Id);
                return Ok("Building successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
