using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Projecthours;
using HoursTracker.Domain.Aggregates.Projecthours;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class ProjecthoursController : Controller
    {
        private readonly IProjecthourService _projecthourService;

        public ProjecthoursController(IProjecthourService projecthourService)
        {
            _projecthourService = projecthourService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _projecthourService.All())
                .Select(projecthour => new ProjecthourViewModel
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
        public async Task Create(CreateProjecthoursViewModel projecthourViewModel)
        {
            var projecthour = new Projecthour
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
        public async Task Edit(int id, ProjecthourViewModel projecthourViewModel)
        {
            var updatedProjecthour = new Projecthour
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