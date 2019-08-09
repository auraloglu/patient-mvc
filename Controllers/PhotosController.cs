using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Final.Models;
using MVC_Final.Controllers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Final.Controllers
{
    public class PhotosController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        //GET: Photos

        [HttpGet]
        public ActionResult Create(int id)
        {
            var photoToCreate = new Photos();
            photoToCreate.InspectionID = id;

            return View(photoToCreate);
        }

        [HttpPost]
        public ActionResult Create(Photos photoToCreate, HttpPostedFileBase file)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Save
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/images/" + ImageName);
                file.SaveAs(physicalPath);
                photoToCreate.Photo = ImageName;
                photoToCreate.Active = true;
                photoToCreate.RegisterDate = DateTime.Now;
                entities.Photos.Add(photoToCreate);

                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
            //var inspectionID = entities.Inspections.Where(x => x.ID == photoToCreate.InspectionID).FirstOrDefault();
            //var patientID = inspectionID.PatientID;
            //return RedirectToAction("Details/" + patientID, "Details");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var photo = entities.Photos.Where(a => a.ID == id).FirstOrDefault();
            if (photo != null)
            {
                return View(photo);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePhoto(int id)
        {
            bool status = false;
            var photo = entities.Photos.Where(x => x.ID == id).FirstOrDefault();
            if (photo != null)
            {
                photo.Active = false;
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class PhotoModel
    {
        public int ID { get; set; }

        public int InspectionID { get; set; }

        public string Photo { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}