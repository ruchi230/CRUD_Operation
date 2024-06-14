using CRUD_Operation.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CRUD_Operation.Controllers
{
    public class HomeController : Controller
    {
        private RegDBEntities db = new RegDBEntities();
        public ActionResult Index(int id = 0)
        {
            ViewBag.ServiceList = db.tblusers.ToList();
            ViewBag.hobbies = new List<string> { "Travelling", "Reading", "Dance", "Cricket" };
            ViewBag.detail = db.tblusers;
            if (id > 0)
            {
                return View(db.tblusers.Find(id));
            }
            else
            {
                return View(new tbluser());
            }

        }
        [HttpPost]
        public ActionResult Index(tbluser user, FormCollection fc, HttpPostedFileBase ufile)
        {
            int id = Convert.ToInt32(fc["uid"]);
            ViewBag.ServiceList = db.tblusers.ToList();
            ViewBag.hobbies = new List<string> { "Travelling", "Reading", "Dance", "Cricket" };
            ViewBag.detail = db.tblusers;

            if (id > 0)
            {
                tbluser euser = db.tblusers.Find(id);
                euser.Name = user.Name;
                euser.Email = user.Email;
                euser.Password = user.Password;
                euser.Gender = user.Gender;
                euser.Education = user.Education;
                euser.Hobbies = fc["hobbies"];

                if (ufile != null)
                {
                    euser.Image = "/Image/" + ufile.FileName;
                    ufile.SaveAs(Server.MapPath(euser.Image));
                }
                else
                {
                    euser.Image = fc["imgval"].ToString();
                }
                db.SaveChanges();
                return RedirectToAction("ViewAllUser");

            }
            else
            {
                user.Name = fc["name"];
                user.Email = fc["email"];
                user.Password = fc["password"];
                user.Gender = fc["gender"];
                user.Education = fc["education"];
                user.Hobbies = fc["hobbies"];

                user.Image = "/Image/" + ufile.FileName;
                ufile.SaveAs(Server.MapPath(user.Image));

                db.tblusers.Add(user);
                db.SaveChanges();

                return View("ViewAllUser");

            }

        }

        public ActionResult ViewAllUser()
        {
            
            ViewBag.hobbies = new List<string> { "Travelling", "Reading", "Dance", "Cricket" };

            ViewBag.detail = db.tblusers;
            return View();
        }

        public ActionResult Delete(int id)
        {

            ViewBag.hobbies = new List<string> { "Travelling", "Reading", "Dance", "Cricket" };
            ViewBag.detail = db.tblusers;

            tbluser user = db.tblusers.Find(id);
            db.tblusers.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ViewAllUser");
        }
    }
}