using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DTOs;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly PE_PRN_Fall22B1Context _context = new PE_PRN_Fall22B1Context();

        [HttpGet("[action]/{nationality}/{gender}")]
        public IActionResult GetDirectors(string nationality, string gender)
        {
            var directors = _context.Directors.Where(x => x.Nationality.ToLower().Trim() == nationality.ToLower().Trim() && x.Male == (gender.ToLower() == "male")).ToList();

            if (directors == null)
            {
                return NotFound();
            }

            var result = directors.Select(x => new DirectorDTO
            {
                Id = x.Id,
                FullName = x.FullName,
                Gender = x.Male ? "Male" : "Female",
                Dob = x.Dob,
                dobString = x.Dob.ToString("M/d/yyyy"),
                Nationality = x.Nationality,
                Description = x.Description
            });

            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetDirector(int id)
        {
            var director = _context.Directors.Include(x => x.Movies).ThenInclude(x => x.Producer).FirstOrDefault(x => x.Id == id);

            if (director == null)
            {
                return NotFound();
            }

            var result = new DirectorDTO
            {
                Id = director.Id,
                FullName = director.FullName,
                Gender = director.Male ? "Male" : "Female",
                Dob = director.Dob,
                dobString = director.Dob.ToString("M/d/yyyy"),
                Nationality = director.Nationality,
                Description = director.Description,
                Movies = director.Movies.Select(x => new MovieDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    ReleaseDate = x.ReleaseDate,
                    ReleaseYear = x.ReleaseDate?.Year.ToString(),
                    Description = x.Description,
                    Language = x.Language,
                    ProducerId = x.ProducerId,
                    DirectorId = x.DirectorId,
                    ProducerName = x.Producer.Name,
                    DirectorName = x.Director.FullName,
                    Genres = x.Genres,
                    Stars = x.Stars
                }).ToList()
            };

            return Ok(result);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] DirectorDTORequest request)
        {
            try
            {
                var newDirector = new Director()
                {
                    Description = request.Description,
                    FullName = request.FullName,
                    Dob = Convert.ToDateTime(request.Dob),
                    Nationality = request.Nationality,
                    Male = request.Male
                };

                _context.Directors.Add(newDirector);
                var addedRecord = _context.SaveChanges();

                return Ok(addedRecord);
            }
            catch (Exception)
            {
                return Conflict("There is an error while adding.");
            }
        }
    }
}
