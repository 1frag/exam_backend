using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class ExamController : Controller
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IStorageGenres _storageGenres;

        public ExamController(ILogger<ExamController> logger,
            IStorageGenres storageGenres)
        {
            _logger = logger;
            _storageGenres = storageGenres;
        }

        [HttpGet]
        [Route("/New")]
        [ActionName("CreateNew")]
        public IActionResult GetCreateNewGenre(int parent)
        {
            var model = new GenreModel();
            var forView = new GenreViewModel();
            forView.Parent = parent;
            forView.GenreModelItem = model;
            return View("CreateGenre", forView);
        }

        [HttpPost]
        [Route("/New")]
        [ActionName("CreateNew")]
        public IActionResult PostCreateNewGenre(GenreViewModel viewModel)
        {
            var model = _storageGenres.Add(viewModel.GenreModelItem, viewModel.Parent);
            return Redirect($"/Get?id={model.Id}");
        }

        [HttpGet]
        [Route("/Get")]
        [ActionName("GetGenre")]
        public IActionResult GetGenre(int id)
        {
            var subgs = new List<GenreModel>();
            _logger.LogInformation($"id={id}");
            var model = _storageGenres.Get(id);
            if (model == null)
            {
                return NotFound();
            }

            if (model.Subgenres != null)
            {
                foreach (var subg in model.Subgenres)
                {
                    subgs.Add(_storageGenres.Get(subg));
                }
            }

            ViewBag.Subgs = subgs;
            return View("GetGenre", model);
        }
    }
}