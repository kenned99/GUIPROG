using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public string filter { get; set; }

        public IEnumerable<Member> Members => serverSideAccess.GetMembersByName(filter).OrderBy(x => x.Id);

        public void OnGet()
        {
            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(Member);
            
        }
    }
}
