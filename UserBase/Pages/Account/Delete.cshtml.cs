using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserBase.DAL.Entities;

namespace UserBase.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LogoutModel> _logger;

        public DeleteModel(UserManager<User> userManager, ILogger<LogoutModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public string UserId { get; set; }

        public void OnGetAsync(string userId)
        {
            UserId = userId;
        }

        public async Task<IActionResult> OnPost(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
            return Redirect("/Index");
        }
    }
}
