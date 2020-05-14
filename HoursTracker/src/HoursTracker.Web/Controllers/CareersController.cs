using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Careers;
using HoursTracker.Domain.Aggregates.Careers;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class CareersController : Controller
    {
        private readonly ICareerService _careerService;

        public CareersController(ICareerService careerService)
        {
            _careerService = careerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _careerService.All())
                .Select(career => new CareerViewModel
                {
                    Id = career.Id,
                    Name = career.Name,
                    Code = career.Code,
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _careerService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(CareerViewModel careerViewModel)
        {
            var career = new Career
            {
                Name = careerViewModel.Name,
                Code = careerViewModel.Code
            };

            await _careerService.Create(career);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _careerService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, CareerViewModel careerViewModel)
        {
            var career = new Career
            {
                Name = careerViewModel.Name,
                Code = careerViewModel.Code
            };
            await _careerService.Update(id, career);
        }
    }
}