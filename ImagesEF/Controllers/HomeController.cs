using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImagesEF.Web.Models;
using ImagesEF.Data;
using Microsoft.Extensions.Configuration;

namespace ImagesEF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string _connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new ImageRepository(_connectionString);
            var vm = new IndexViewModel {
                Images = (List<Image>)repo.GetImages()
            };
            return View(vm);
        }

        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddImage(Image image)
        {
            var repo = new ImageRepository(_connectionString);
            repo.AddImage(image);
            return Redirect("/");
        }

        public IActionResult ImageView(int id)
        {
            var repo = new ImageRepository(_connectionString);
            var vm = new ImageViewModel();
            var image = repo.GetImageById(id);
            if (image != null)
            {
                vm.Image = image;
            }
            return View(vm);
        }

        [HttpPost]
        public void UpdateLikeCount(LikeViewModel vm)
        {
            var repo = new ImageRepository(_connectionString);
            repo.UpdateLikeCount(vm.Id);
            Response.Cookies.Append($"{vm.Id}", "liked");
        }

        public bool GetStatus(StatusViewModel vm)
        {
            return (!String.IsNullOrEmpty(Request.Cookies[$"{vm.Id}"]));
        }

        public int GetLikeCount(int id)
        {
            var repo = new ImageRepository(_connectionString);
            return repo.GetLikeCount(id);
        }
    }
}
