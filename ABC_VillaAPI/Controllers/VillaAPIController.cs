using ABC_VillaAPI.Data;
using ABC_VillaAPI.Models;
using ABC_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ABC_VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO>GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa=_db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null) {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<IList<VillaDTO>> CreateVilla (VillaDTO villa)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villa.Name.ToLower())!=null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists");
                return BadRequest(ModelState);
            }

            if(villa == null)
            {
                return BadRequest();
            }

            if(villa.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
                    }

            Villa model = new()
            {
                Name = villa.Name,
                Amenity = villa.Amenity,
                
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
               
            };
            _db.Villas.Add(model);
            _db.SaveChanges();
            return Ok(villa);
        }

        [HttpDelete("id")]
        public IActionResult DeleteVilla (int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
           _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();

        }

        [HttpPut("id")]

        public IActionResult UpdateVilla(int id, VillaDTO villa)
        {
            if(villa == null || id! == villa.Id)
            {
                return BadRequest();
            }
            //var thisvilla= _db.Villas.FirstOrDefault(v => v.Id == id);
            //thisvilla.Name= villa.Name;
            //thisvilla.Sqft= villa.Sqft;
            //thisvilla.Occupancy= villa.Occupancy;
            Villa model = new()
            {
                Name = villa.Name,
                Amenity = villa.Amenity,

                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,

            };
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();
        }

    } 
}
 