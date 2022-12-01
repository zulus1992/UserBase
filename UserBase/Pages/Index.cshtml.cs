using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserBase.DAL;

namespace UserBase.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UsersContext _context;
        public Dictionary<string, byte[]?> Users { get; set; }

        public IndexModel(ILogger<IndexModel> logger, UsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Users = _context.Users
               .ToDictionary(x =>x.UserName,x=>x.Image);
        }
    }
}