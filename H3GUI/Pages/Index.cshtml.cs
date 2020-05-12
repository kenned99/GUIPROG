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

        [BindProperty]
        public decimal lat { get; set; }
        [BindProperty]
        public decimal lng { get; set; }



        public IActionResult OnGet()
        {
            

            if (HttpContext.Session.GetInt32("sessionUser") == null)
                
                return RedirectToPage("/login");

            return Page();
        }

        public void OnPostUpdateLocation()
        {
            var member = serverSideAccess.GetMember(1);
            var gps = serverSideAccess.GetGpsLocation(3);
            gps.Latitude = lat;
            gps.Longtitude = lng;
            serverSideAccess.UpdateLocation(gps, member);
            serverSideAccess.Commit();
        }

    }
}
