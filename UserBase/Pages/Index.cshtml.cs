using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UserBase.DAL;
using UserBase.DAL.Entities;

namespace UserBase.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<User> _userManager;
        public List<User> Users { get; set; }

        public IndexModel(ILogger<IndexModel> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public void OnGet()
        {
            Users = _userManager.Users.ToList();               
        }
    }
}