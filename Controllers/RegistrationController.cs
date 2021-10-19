using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication32_DropDown.Models;
namespace WebApplication32_DropDown.Controllers
{
    public class RegistrationController : Controller
    {
        public readonly DatabaseContext db;

        public RegistrationController(DatabaseContext context)
        {
            db = context;
        }
        public IActionResult Create(int Id=0)
        {
            ViewBag.Bt = "Save";
            TblRegistration tbl = new TblRegistration();
            if (Id > 0) {
                var data = (from a in db.TblRegistrations where a.Id == Id select a).ToList();
                tbl.Id = data[0].Id;
                tbl.Name = data[0].Name;
                tbl.Email = data[0].Email;
                tbl.Phone = data[0].Phone;
                tbl.Country = data[0].Country;
                ViewBag.Bt = "Update";
            }
            ViewBag.ctr =(from a in db.TblCountries select a).ToList();
            return View(tbl);
        }
        [HttpPost]
        public IActionResult Create(TblRegistration tblRegistration)
        {
            if (tblRegistration.Id > 0) {
                db.Entry(tblRegistration).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            } else {
                db.Add(tblRegistration);
            }
            db.SaveChanges();
            return RedirectToAction("Show");
        }
        public IActionResult Show() {
            var data = (from a in db.TblRegistrations
                        join b in db.TblCountries on a.Country equals b.Cid
                        select new TblShow{
                           Id= a.Id,
                           Name = a.Name,
                           Email = a.Email,
                           Phone = a.Phone,
                           CountryName = b.Cname }).ToList();
            return View(data);
        }
        public IActionResult Delete(int Id) {
            var deleteId = db.TblRegistrations.Find(Id);
            db.Remove(deleteId);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
    }
}
