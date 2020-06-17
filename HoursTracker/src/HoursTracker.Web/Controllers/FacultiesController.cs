using HoursTracker.Core.Faculties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Careers;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using HoursTracker.Domain.Aggregates.Faculties;

namespace HoursTracker.Web.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultyService _facultyService;

        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _facultyService.All())
                .Select(campus => new CampusViewModel
                {
                    Id = campus.Id,
                    Name = campus.Name,
                    Code = campus.Code,
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _facultyService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CampusViewModel campusViewModel)
        {
            var existingCampus = await _facultyService.FindByCode(campusViewModel.Code);


            if (existingCampus == null)
            {
                var campus = new Faculty
                {
                    Name = campusViewModel.Name,
                    Code = campusViewModel.Code
                };
                await _facultyService.Create(campus);
                return Ok();
            }
            else
            {
                return BadRequest("Ya existe un campus con este codigo");
            }

        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _facultyService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(int id, CampusViewModel campusViewModel)
        {
            var existinCampus = await _facultyService.FindByCode(campusViewModel.Code);
            if (existinCampus == null)
            {
                var campus = new Faculty
                {
                    Name = campusViewModel.Name,
                    Code = campusViewModel.Code
                };
                await _facultyService.Update(id, campus);
                return Ok();
            }
            else
            {
                return BadRequest("Ya existe un campus con este codigo");
            }

        }
    }
}
