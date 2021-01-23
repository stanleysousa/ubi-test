using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryUI.Presentation.Models;
using InventoryUI.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using InventoryUI.Infrastructure.Entity;
using System.Collections.Generic;
using System.Text.Json;

namespace InventoryUI.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        const string SessionIdUSer = "";
        const string SessionUsername = "";
        const string SessionUserrole = "";

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> IndexAsync(IdentifiedUser identifiedUser)
        {
            if (identifiedUser is null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                HttpContext.Session.SetString(SessionIdUSer, identifiedUser.IdUser);
                HttpContext.Session.SetString(SessionUsername, identifiedUser.UserName);
                HttpContext.Session.SetInt32(SessionUserrole, identifiedUser.IdRole);

                ViewBag.IdUser = identifiedUser.IdUser;
                ViewBag.UserName = identifiedUser.UserName;
                ViewBag.SessionUserrole = identifiedUser.IdRole;

                var items = new List<User>();
                if (identifiedUser.IdRole == 1) //admin
                {
                    var users = await _userService.GetAllUsers();
                    foreach (var user in users)
                    {
                        var item = await _userService.GetUserWithItems(user.IdUser);
                        items.Add(item);
                    }
                }
                else
                {
                    var item = await _userService.GetUserWithItems(int.Parse(identifiedUser.IdUser));
                    items.Add(item);

                }
                var opts = new JsonSerializerOptions();
                ViewBag.Items = JsonSerializer.Serialize(items);
                return View();                
            }
        }

        public async Task<IActionResult> Login(IdentifiedUser identifiedUser)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(identifiedUser?.UserName)) 
            {
                var user = await _userService.GetUserByName(identifiedUser.UserName);
                if (user != null && identifiedUser.Password.Equals(user.Password))
                {
                    identifiedUser.IdRole = user.Role.IdRole;
                    identifiedUser.IdUser = user.IdUser.ToString();

                    return RedirectToAction("Index", identifiedUser);

                }
            }
            return View(identifiedUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
