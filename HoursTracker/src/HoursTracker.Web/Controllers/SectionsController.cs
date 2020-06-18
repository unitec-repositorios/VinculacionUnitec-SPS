using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Sections;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _sectionService.All())
                .Select(section => new SectionsViewModel
                {
                    Id = section.Id,
                    Class = section.Class,
                    Code = section.Code,
                    Period = section.Period,
                    Professor = section.Professor
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _sectionService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(CreateSectionViewModel sectionViewModel)
        {
            var section = new CreateSectionDto
            {
                Code = sectionViewModel.Code,
                Class = sectionViewModel.Class,
                Period = sectionViewModel.Period,
                Professor = sectionViewModel.Professor
            };

            await _sectionService.Create(section);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _sectionService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, CreateSectionViewModel sectionViewModel)
        {
            var section = new UpdateSectionDto()
            {
                Code = sectionViewModel.Code,
                Class = sectionViewModel.Class,
                Period = sectionViewModel.Period,
                Professor = sectionViewModel.Professor
            };
            await _sectionService.Update(id, section);
        }
    }
}