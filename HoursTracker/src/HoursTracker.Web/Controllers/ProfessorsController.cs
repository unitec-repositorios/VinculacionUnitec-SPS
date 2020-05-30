using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Professors;
using HoursTracker.Domain.Aggregates.Professors;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly IProfessorService _professorService;

        public ProfessorsController(IProfessorService professorService)
        {
            _professorService = professorService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _professorService.All())
                .Select(professor => new ProfessorViewModel
                {
                    Id = professor.Id,
                    Code = professor.Code,
                    FirstName = professor.FirstName,
                    SecondName = professor.SecondName,
                    FirstLastName = professor.FirstLastName,
                    SecondLastName = professor.SecondLastName
                });
            
            return Ok(data);
        }
        
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _professorService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(ProfessorViewModel professorViewModel)
        {
            var professor = new Professor
            {
                Code = professorViewModel.Code,
                FirstName = professorViewModel.FirstName,
                SecondName = professorViewModel.SecondName,
                FirstLastName = professorViewModel.FirstLastName,
                SecondLastName = professorViewModel.SecondLastName
            };
            
            await _professorService.Create(professor);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _professorService.Remove(id);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }
        
        [HttpPut]
        public async Task Edit(int id, ProfessorViewModel professorViewModel)
        {
            var professor = new Professor
            {
                Code = professorViewModel.Code,
                FirstName = professorViewModel.FirstName,
                SecondName = professorViewModel.SecondName,
                FirstLastName = professorViewModel.FirstLastName,
                SecondLastName = professorViewModel.SecondLastName
            };
            await _professorService.Update(id, professor);
        }
    }
}