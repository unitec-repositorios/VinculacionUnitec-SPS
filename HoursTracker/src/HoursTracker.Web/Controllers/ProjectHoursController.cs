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
        private readonly IProjectHourService _projecthourService;

        public ProjectHoursController(IProjectHourService projecthourService)
        {
            _projecthourService = projecthourService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> All()
        {
            var data = (await _projecthourService.All())
                .Select(projecthour => new ProjectHourViewModel
                {
                    Id = projecthour.Id,
                    Account = projecthour.Account,
                    StudentFirstName = projecthour.StudentFirstName,
                    StudentLastName = projecthour.StudentLastName,
                    SeccionCode = projecthour.SeccionCode,
                    SeccionName = projecthour.SeccionName,
                    ProjectName = projecthour.ProjectName,
                    TableState = projecthour.TableState,
                    Hours = projecthour.Hours
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _projecthourService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(ProjectHourViewModel projecthourViewModel)
        {
            var projecthour = new ProjectHour
            {
                Account = projecthourViewModel.Account,
                SeccionCode = projecthourViewModel.SeccionCode,
                ProjectName = projecthourViewModel.ProjectName,
                TableState = projecthourViewModel.TableState,
                Hours = projecthourViewModel.Hours
            };

            await _projecthourService.Create(projecthour);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _projecthourService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, ProjectHourViewModel projecthourViewModel)
        {
            var updatedProjecthour = new ProjectHour
            {
                Account = projecthourViewModel.Account,
                StudentFirstName = projecthourViewModel.StudentFirstName,
                StudentLastName = projecthourViewModel.StudentLastName,
                SeccionCode = projecthourViewModel.SeccionCode,
                SeccionName = projecthourViewModel.SeccionName,
                ProjectName = projecthourViewModel.ProjectName,
                TableState = projecthourViewModel.TableState,
                Hours = projecthourViewModel.Hours
            };
            await _projecthourService.Update(id, updatedProjecthour);
        }
    }
}