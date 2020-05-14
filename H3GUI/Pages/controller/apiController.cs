using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerSide;
using ServerSide.DTOObject;
using ServerSide.Model;

namespace H3GUI.Pages.api
{
    [Produces("application/json")]
    [Route("controller/[controller]")]
    [ApiController]
    public class apiController : Controller
    {
        private readonly IServerSideAccess serverSideAccess;

        public apiController(IServerSideAccess serverSideAccess)
        {
            this.serverSideAccess = serverSideAccess;
        }

        // GET: api
        [HttpGet]
        public IEnumerable<Member> GetLocation()
        {
            var data = serverSideAccess.GetMembersByName();
            foreach (var item in data)
                if(item.LastKnownLocationId.HasValue)
                    item.LastKnownLocation = serverSideAccess.GetGpsLocation(item.LastKnownLocationId.Value);

            return data;
        }


        // GET: api/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: api/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: api/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: api/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: api/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: api/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: api/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Poster GPS lokalition på siden
        [HttpPost]
        [Route("geoloc")]
        public DTOGps PostGpsLocation([FromBody] DTOGps gps)
        {
            

            return serverSideAccess.AddGpsLoc(gps);
          
            //Member.LastKnownLocation = gps;
            //serverSideAccess.UpdateMember(Member);
            //serverSideAccess.Commit();
           
        }

        [HttpGet]
        [Route("Message")]
        public IEnumerable<Message> RecieveMessage(RecieveMessageInfo DTO)
        {
            return serverSideAccess.RecieveMessage(DTO).OrderBy(x => x.TimeSent);
        }
    }
}