using System;
using System.Linq;
using System.Threading.Tasks;
using HoursTracker.Core.VinculationTypes;
using HoursTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoursTracker.Web.Controllers
{
    public class VinculationTypesController : Controller
    {
        private readonly IVinculationTypeService _vinculationService;

        public VinculationTypesController(IVinculationTypeService studentService)
        {
            _vinculationService = studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var data = (await _vinculationService.All())
                .Select(vinc => new VinculationTypeViewModel
                {
                    Id = vinc.Id,
                    Code = vinc.Code,
                    Type = vinc.Type
                }); ;

            return Ok(data);
        }

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _vinculationService.FindById(id));
        }

       
        

        
    }
}
