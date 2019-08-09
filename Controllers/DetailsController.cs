using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Final.Models;

namespace MVC_Final.Controllers
{
    public class DetailsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Details
        public ActionResult Details(int id)
        {
            var patient = entities.Patients.Where(x => x.ID == id).FirstOrDefault();
            ViewBag.patientID = patient.ID;
            ViewBag.first_primary = true;
            ViewBag.first_secondary = true;

            var uvm = new UserViewModel();

            uvm.Inspections_List = entities.Inspections.Where(x => x.PatientID == id && x.Active == true).ToList();
            uvm.Disease = entities.Diseases.Where(x => x.PatientID == id && x.Active == true).FirstOrDefault();
            uvm.Information = entities.Informations.Where(x => x.PatientID == id && x.Active == true).FirstOrDefault();
            uvm.Surgery = entities.Surgeries.Where(x => x.PatientID == id && x.Active == true).FirstOrDefault();
            uvm.Medicine = entities.Medicines.Where(x => x.PatientID == id && x.Active == true).FirstOrDefault();

            uvm.Complain_List = entities.Complains.ToList();
            uvm.Finding_List = entities.Findings.ToList();
            uvm.Diagnosis_List = entities.Diagnosis.ToList();
            uvm.Examination_List = entities.Examinations.ToList();
            uvm.Treatment_Plan_List = entities.Treatment_Plans.ToList();

            return View(uvm);
        }

        public ActionResult GetPatientDetails(int id)
        {
            var patientDetail = entities.Patients.Where(x => x.ID == id).ToList().Select(x => new PatientModel()
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate.Value.ToString("yyyy-MM-dd"),
                Age = -(x.BirthDate.Value.Year - DateTime.Now.Year),
                RegisterDate = x.RegisterDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                Summary = x.Summary
            });

            return Json(new { data = patientDetail }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInspections(int id)
        {
            var patientInspection = entities.Inspections.Where(x => x.PatientID == id && x.Active == true).ToList().Select(x => new InspectionModel()
            {
                ID = x.ID,
                PatientID = x.PatientID,
                RegisterDate = x.RegisterDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
            });

            return Json(new { data = patientInspection }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPatientPhoto(int id)
        {
            var patientPhoto = entities.Photos.Where(x => x.InspectionID == id && x.Active == true).ToList().Select(x => new PhotoModel()
            {
                ID = x.ID,
                InspectionID = x.InspectionID,
                Photo = x.Photo,
                RegisterDate = x.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"),
            });

            return Json(new { data = patientPhoto }, JsonRequestBehavior.AllowGet);
        }
    }
}