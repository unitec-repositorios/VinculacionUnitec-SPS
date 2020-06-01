using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.ProjectHours;
using HoursTracker.Domain.Aggregates.ProjectHours;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class ProjectHoursController : Controller
    {
        private readonly IProjectHourService _projectHourService;

        public ProjectHoursController(IProjectHourService projectHourService)
        {
            _projectHourService = projectHourService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _projectHourService.All())
                .Select(projecthour => new ProjectHourViewModel
                {
                    Id = projecthour.Id,
                    Hours = projecthour.Hours,
                    StudentAccount = projecthour.StudentAccount
                    //SectionCode = projecthour.SectionCode
                    //ProjectName = projecthour.ProjectName
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _projectHourService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(ProjectHourViewModel projectviewViewModel)
        {
            var projecthour = new ProjectHour
            {
                Hours = projectviewViewModel.Hours
            };

            await _projectHourService.Create(projecthour);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _projectHourService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, ProjectHourViewModel projectviewViewModel)
        {
            var career = new ProjectHour
            {
                Hours = projectviewViewModel.Hours
                //StudentId = projectviewViewModel.StudentId
                //StudentName = projectviewViewModel.StudentName
            };
            await _projectHourService.Update(id, career);
        }

    }
}