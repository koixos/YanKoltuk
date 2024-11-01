using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YanKoltukBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private static List<dynamic> services = new List<dynamic>
        {
            new {
                id = 1,
                plate = "34ABC34",
                driver = new {
                    name = "Ali Akça",
                    phone = "+905527841212"
                },
                stewardess = new {
                    name = "Ayşe Kaya",
                    phone = "+905527841213"
                },
                capacity = 20,
                departureTime = "08:30",
                departureLocation = "İlçe/mahalle",
                students = new List<dynamic> {
                    new { name = "Öğrenci A", schoolNumber = 12345 },
                    new { name = "Öğrenci B", schoolNumber = 67890 },
                    new { name = "Öğrenci A", schoolNumber = 12345 },
                }
            }
        };

        [HttpGet]
        public IActionResult GetServices()
        {
            return Ok(services);
        }

        [HttpGet("{id}")]
        public IActionResult GetServiceById(int id)
        {
            var service = services.FirstOrDefault(s => s.id == id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public IActionResult AddService([FromBody] dynamic newService)
        {
            services.Add(newService);
            return CreatedAtAction(nameof(GetServiceById), new { id = newService.Id }, newService);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, [FromBody] dynamic updatedService)
        {
            var serviceIndex = services.FindIndex(s => s.id == id);

            if (serviceIndex == -1)
            {
                return NotFound();
            }

            services[serviceIndex] = updatedService;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = services.FirstOrDefault(s => s.id == id);

            if (service == null)
            {
                return NotFound();
            }

            services.Remove(service);
            return NoContent();
        }

        [HttpGet("{id}/students")]
        public IActionResult GetStudents(int id)
        {
            var service = services.FirstOrDefault(s => s.id == id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service.students);
        }

        [HttpPost("{id}/students")]
        public IActionResult AddStudent(int id, [FromBody] dynamic newStudent)
        {
            var service = services.FirstOrDefault(s => s.id == id);

            if (service == null)
            {
                return NotFound();
            }

            service.students.Add(newStudent);
            return Ok(newStudent);
        }
    }
}
