using ChatSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace ChatSystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                return View(db.Rooms.ToList());
            }

        }
        [Authorize]
        public ActionResult GetRoomMessages(string roomName)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = db.Messages.Include(a => a.Owner)
                    .Where(a => a.RoomName == roomName).OrderByDescending(a => a.SendDateTime).Select(a =>
                   new
                   {
                       message = this.HttpContext.User.Identity.Name == a.Owner.UserName ? "شما:  " + a.Content :
                       a.Owner.UserName + ":" + a.Content

                   }).ToList();

                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
          
            return View();
        }
    }
}