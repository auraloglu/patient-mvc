using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Final.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Final.Controllers
{
    public class PatientsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();

        // GET: Patients
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPatients()
        {
            var employees = entities.Patients.Where(x => x.Active == true).ToList().Select(x => new PatientModel()
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate.Value.ToString("yyyy-MM-dd"),
                Age = -(x.BirthDate.Value.Year - DateTime.Now.Year),
                RegisterDate = x.RegisterDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                Summary = x.Summary
            });

            return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var patientToCreate = new Patients();
            return View(patientToCreate);  
        }

        [HttpPost]
        public ActionResult Create(Patients patientToCreate)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Save
                patientToCreate.Active = true;
                patientToCreate.RegisterDate = DateTime.Now;
                entities.Patients.Add(patientToCreate);

                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var patientToEdit = entities.Patients.Where(x => x.ID == id).FirstOrDefault();
            return View(patientToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Patients patientToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var patient = entities.Patients.Where(x => x.ID == patientToEdit.ID).FirstOrDefault();
                if (patient != null)
                {
                    patient.FirstName = patientToEdit.FirstName;
                    patient.LastName = patientToEdit.LastName;
                    patient.BirthDate = patientToEdit.BirthDate;
                    patient.Active = true;
                    patient.Summary = patientToEdit.Summary;
                }
                entities.SaveChanges();
                status = true; 
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var patient = entities.Patients.Where(x => x.ID == id).FirstOrDefault();
            if (patient != null)
            {
                return View(patient);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePatient(int id)
        {
            bool status = false;
            var patient = entities.Patients.Where(x => x.ID == id).FirstOrDefault();
            if (patient != null)
            {
                patient.Active = false;
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult DeletedRecords()
        {
            return View();
        }

        public ActionResult GetDeletedPatients()
        {
            var employees = entities.Patients.Where(x => x.Active == false).ToList().Select(x => new PatientModel()
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate.Value.ToString("yyyy-MM-dd"),
                Age = -(x.BirthDate.Value.Year - DateTime.Now.Year),
                RegisterDate = x.RegisterDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                Summary = x.Summary
            });

            return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBack(int id)
        {
            var patient = entities.Patients.Where(x => x.ID == id).FirstOrDefault();
            if (patient != null)
            {
                return View(patient);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("GetBack")]
        public ActionResult GetBackPatient(int id)
        {
            bool status = false;
            var patient = entities.Patients.Where(x => x.ID == id).FirstOrDefault();
            if (patient != null)
            {
                patient.Active = true;
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class PatientModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Hasta adı boş bırakılamaz!")]
        [DisplayName("Adı")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Hasta soyadı boş bırakılamaz!")]
        [DisplayName("Soyadı")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Hasta doğum tarihi boş bırakılamaz!")]
        [DisplayName("Doğum Tarihi")]
        public string BirthDate { get; set; }

        public int Age { get; set; }

        [DisplayName("Kayıt Tarihi")]
        public string RegisterDate { get; set; }

        [DisplayName("Özet")]
        public string Summary { get; set; }
    }
}