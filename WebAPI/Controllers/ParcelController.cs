using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    // [Route("api/[controller]")] ifadesinde [controller] yazarsan controller'ın ismi http'ye otomatik olarak yazılır
    [ApiController]
    public class ParcelController:ControllerBase
    {
        private IParcelService _parcelService;

        public ParcelController(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var result = _parcelService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int parcelId)
        {
            var result = _parcelService.GetById(parcelId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Parcel parcel)
        {
            var result = _parcelService.Add(parcel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Parcel parcel)
        {
            var result = _parcelService.Update(parcel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Parcel parcel)
        {
            var result = _parcelService.Delete(parcel);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
