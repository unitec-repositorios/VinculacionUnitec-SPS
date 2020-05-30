using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.ProjectHours;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(CreateProjectHourViewModel projecthourViewModel)
        {
            var projecthour = new CreateProjectHourDto
            {
                Student = projecthourViewModel.Student,
                HoursWork = projecthourViewModel.HoursWork
                //Section = projecthourViewModel.Section,
                //Project = projecthourViewModel.Project
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
    }
}