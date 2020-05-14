using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerSide.Model;

namespace H3GUI.Pages
{
    public class PrivacyModel : PageModel
    {


        private readonly ILogger<PrivacyModel> _logger;



        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetInt32("sessionUser") == null)
                return RedirectToPage("/login");

            return Page();
        }
    }
}
