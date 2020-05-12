using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.Classes;
using HoursTracker.Domain.Aggregates.Classes;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace HoursTracker.Web.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _classService.All())
                .Select(@class => new ClassViewModel
                {
                    Id = @class.Id,
                    ClassName = @class.ClassName
                });

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _classService.FindById(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(ClassViewModel classViewModel)
        {
            var @class = new Class
            {
                ClassName = classViewModel.ClassName
            };

            await _classService.Create(@class);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _classService.Remove(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(id);
        }

        [HttpPut]
        public async Task Edit(int id, ClassViewModel classViewModel)
        {
            var classs = new Class
            {
                ClassName = classViewModel.ClassName
            };
            await _classService.Update(id, classs);
        }
    }
}
