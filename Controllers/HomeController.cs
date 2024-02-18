using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskSched.Configuration;
using TaskSched.Data.Context;
using TaskSched.Data.Models;
using TaskSched.Services.Interfaces;
using TaskSched.ViewModels;

namespace TaskSched.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new UserComplaintsViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Contacts([FromServices] ICompanyDetailsInfoService companyDetailsInfoService,
            [FromServices] IConfiguration configuration,
            [FromServices] IMapper mapper)
        {

			string companyName = configuration["CompanyName"] ?? "";

            var companyDetails = await companyDetailsInfoService.GetInfo(companyName).FirstOrDefaultAsync();

            if (companyDetails == null)
                return NotFound();

            var companyDetailsViewModel = mapper.Map<CompanyDetailsViewModel>(companyDetails);

            return View(companyDetailsViewModel);
        }

        public async Task<IActionResult> AddComplaint([FromServices] IUserComplaintsCrudService complaintsCrudService, UserComplaintsViewModel userComplaintsViewModel)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity?.Name ?? "";

                UserComplaints newUserComplaint = new UserComplaints()
                {
                    Name = userComplaintsViewModel.Name,
                    Description = userComplaintsViewModel.Name
                };

                var result = await complaintsCrudService.AddComplaint(newUserComplaint, userName);

                return Json(new { success = result });
            }

            return View("Index", userComplaintsViewModel);
        }
    }
}
