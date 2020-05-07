using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ServerSide;
using ServerSide.Model;

namespace H3GUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IServerSideAccess serverSideAccess;

        public IndexModel(IServerSideAccess serverSideAccess)
        {
            this.serverSideAccess = serverSideAccess;
        }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        public IEnumerable<Member> Members => serverSideAccess.GetMembersByName(Filter).OrderBy(x => x.Id);

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("sessionUser") == null)
                return RedirectToPage("/login");

            return Page();
        }
    }
}
