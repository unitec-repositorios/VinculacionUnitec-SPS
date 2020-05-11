using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Students;
using HoursTracker.Domain.Aggregates.Students;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _studentService.All())
                .Select(student => new StudentViewModel
                {
                    Id = student.Id,
                    Account = student.Account,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    FirstSurname = student.FirstSurname,
                    SecondSurname = student.SecondSurname,
                    MajorCode = student.MajorCode,
                    CampusCode = student.CampusCode,
                    Settlement = student.Settlement
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _studentService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(StudentViewModel studentViewModel)
        {
            var student = new Student
            {
                Id = studentViewModel.Id,
                Account = studentViewModel.Account,
                FirstName = studentViewModel.FirstName,
                SecondName = studentViewModel.SecondName,
                FirstSurname = studentViewModel.FirstSurname,
                SecondSurname = studentViewModel.SecondSurname,
                MajorCode = studentViewModel.MajorCode,
                CampusCode = studentViewModel.CampusCode,
                Settlement = studentViewModel.Settlement
            };

            await _studentService.Create(student);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _studentService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, StudentViewModel studentViewModel)
        {
            var student = new Student
            {
                Id = studentViewModel.Id,
                Account = studentViewModel.Account,
                FirstName = studentViewModel.FirstName,
                SecondName = studentViewModel.SecondName,
                FirstSurname = studentViewModel.FirstSurname,
                SecondSurname = studentViewModel.SecondSurname,
                MajorCode = studentViewModel.MajorCode,
                CampusCode = studentViewModel.CampusCode,
                Settlement = studentViewModel.Settlement
            };
            await _studentService.Update(id, student);
        }
    }
}