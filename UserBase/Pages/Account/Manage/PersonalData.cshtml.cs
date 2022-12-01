// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserBase.DAL.Entities;

namespace UserBase.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public byte[] Image { get; set; }
        public string Name { get; set; }


        public PersonalDataModel(
            UserManager<User> userManager,
            ILogger<PersonalDataModel> logger,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _logger = logger;
            _environment = environment;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Image = user.Image;
            Name = user.UserName;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FileStream fileStream;
            using (var ms = new MemoryStream())
            {
                Upload.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var user = await _userManager.GetUserAsync(User); 
                user.Image = fileBytes;
                IdentityResult result = await _userManager.UpdateAsync(user);
            }
            return RedirectToPage("/Account/Manage/PersonalData");
        }


        private byte[] ToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
