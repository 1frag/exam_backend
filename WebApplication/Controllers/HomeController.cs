using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegistrationService _registrationService;

        public HomeController(ILogger<HomeController> logger,
            IRegistrationService registrationService)
        {
            _logger = logger;
            _registrationService = registrationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private bool CheckModelState(BusinessModel businessModel)
        {
            if (businessModel != null && ModelState.IsValid)
            {
                return false;
            }

            _logger.Log(LogLevel.Information, "Model state isn't valid");
            foreach (var problem in ModelState.Keys)
            {
                var value = ModelState.GetValueOrDefault(problem);
                foreach (var err in value.Errors)
                {
                    _logger.Log(LogLevel.Information, $"error: {err.ErrorMessage}");
                    ModelState.AddModelError("", err.ErrorMessage);
                }
            }

            return true;
        }

        [HttpGet]
        [Route("/New/Business")]
        [ActionName("NewBusiness")]
        public IActionResult GetCreateNewBusinessActionResult()
        {
            return View("CreateNewBusiness");
        }

        [Route("/New/Business")]
        [HttpPost]
        public IActionResult PostCreateNewBusiness(BusinessModel businessModel)
        {
            if (CheckModelState(businessModel))
            {
                return View("CreateNewBusiness");
            }

            _logger.Log(LogLevel.Information, businessModel.GetType().ToString());
            _logger.Log(LogLevel.Information, $"PostCreateNewBusiness {businessModel}");
            return RedirectToAction("FillStaff", businessModel);
        }

        [HttpGet]
        [Route("/Fill/Staff")]
        [ActionName("FillStaff")]
        public IActionResult GetAddYourStaff(BusinessModel businessModel, int code)
        {
            if (CheckModelState(businessModel))
            {
                return RedirectToAction("NewBusiness");
            }

            businessModel.StaffCollection = new List<StaffModel>();
            for (var i = 0; i < businessModel.CountStaff; i++)
            {
                businessModel.StaffCollection.Add(new StaffModel());
            }

            _logger.Log(LogLevel.Information, $"GetAddYourStaff {businessModel}");
            return View("AddYourStaff", businessModel);
        }

        [HttpPost]
        [Route("/Fill/Staff")]
        [ActionName("FillStaff")]
        public IActionResult PostAddYourStaff(BusinessModel businessModel, int code)
        {
            _logger.Log(LogLevel.Information, $"code = {code}");
            if (!(2600 < code && code < 4986))
            {
                _logger.Log(LogLevel.Information, "incorrect code");
                return StatusCode(401);
            }

            if (businessModel.CountStaff != businessModel.StaffCollection.Count)
            {
                _logger.LogInformation("count not equal.");
                return StatusCode(400);
            }

            if (CheckModelState(businessModel))
            {
                return RedirectToAction("FillStaff", businessModel);
            }

            _logger.Log(LogLevel.Information, $"GetAddYourStaff {businessModel}");

            return RedirectToAction("FillHobbies", businessModel);
        }

        [HttpGet]
        [Route("/Fill/Hobbies")]
        [ActionName("FillHobbies")]
        public IActionResult GetFillHobbies(BusinessModel businessModel)
        {
            if (CheckModelState(businessModel))
            {
                return RedirectToAction("NewBusiness");
            }

            if (businessModel.CountStaff != businessModel.StaffCollection.Count)
            {
                _logger.Log(LogLevel.Information, "not eq");
                ModelState.AddModelError("", "count must be equal.");
                return RedirectToAction("FillStaff");
            }
            
            for (var i = 0; i < businessModel.StaffCollection.Count; i++)
            {
                _logger.Log(LogLevel.Information, $"add for {i} staff");
                businessModel.StaffCollection[i].Hobbies = new List<string>();
                for (var j = 0; j < 3; j++)
                {
                    _logger.Log(LogLevel.Information, $"add for {j} hobby");
                    businessModel.StaffCollection[i].Hobbies.Add(null);
                }
            }

            return View("FillHobbies");
        }

        [HttpPost]
        [Route("/Fill/Hobbies")]
        [ActionName("FillHobbies")]
        public IActionResult PostFillHobbies(BusinessModel businessModel)
        {
            if (CheckModelState(businessModel))
            {
                return RedirectToAction("FillHobbies");
            }

            if (businessModel.CountStaff != businessModel.StaffCollection.Count)
            {
                ModelState.AddModelError("", "count must be equal.");
                return RedirectToAction("FillHobbies");
            }

            _logger.Log(LogLevel.Information, $"GetAddYourStaff {businessModel}");

            return RedirectToPage($"/businesses?name={businessModel.Name}");
        }

        [HttpGet]
        [Route("/businesses")]
        [ActionName("GetBusinesses")]
        public IActionResult GetBusiness(string owner, string name)
        {
            var businesses = _registrationService.GetBusiness(owner, name);
            return View("ShowBusiness", new ManyBusinessModel(businesses));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}