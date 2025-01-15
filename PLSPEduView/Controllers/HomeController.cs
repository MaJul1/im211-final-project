using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PLSPEduView.Models;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Services;

namespace PLSPEduView.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HomeViewService _homeService;

    public HomeController(ILogger<HomeController> logger, HomeViewService homeViewService)
    {
        _logger = logger;
        _homeService = homeViewService;
    }

    public IActionResult Index()
    {
        return View(_homeService.GetHomeViewModel());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
