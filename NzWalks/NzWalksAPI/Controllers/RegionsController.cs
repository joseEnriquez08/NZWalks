
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalksAPI.Models.Domain;
using NzWalksAPI.Repositories;

namespace NzWalksAPI.Controllers
{
    [ApiController]
    [Route("Regions")] // the [controller] automatically sets "Regions" to be the route
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {

            var regions = await regionRepository.GetAllAsync();

            //DTO = data transfer object
            //return DTO regions
            /* var regionsDTO = new List<Models.DTO.Region>();
             regions.ToList().ForEach(region =>
             {
                 var regionDTO = new Models.DTO.Region()
                 {
                     Id = region.Id,
                     Name = region.Name,
                     Code = region.Code,
                     Area = region.Area,
                     Lat = region.Lat,
                     Long = region.Long,
                     Population = region.Population,

                 };
                 regionsDTO.Add(regionDTO);
             });*/

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        //route is set to: /regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionsAsync")]
        public async Task<IActionResult> GetRegionsAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        //we are using the AddRegionRequest model instead of the DTO and domain in order to not
        //expose GUID id property
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request to Domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //pass details to repository
            region = await regionRepository.AddAsync(region);

            //convert back to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return CreatedAtAction(nameof(GetRegionsAsync), new {id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var region = await regionRepository.DeleteAsync(id);

            //If null Notfound
            if (region == null)
            {
                return NotFound();
            }

            //convert to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            

            //return Ok response 
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,
        [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //Convert DTO to domain model
            var region = new Region
            {
                Id = id,
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };

            //update Region using repository
            var result = await regionRepository.UpdateAsync(id, region);

            //if null then notFount()
            if(result == null)
            {
                return NotFound();
            }

            //Convert domain to DTO 
            var regionDTO = mapper.Map<Models.DTO.Region>(result);

            //return ok response
            return Ok(regionDTO);
        }
    }
}
