using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Final.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Final.Controllers
{
    public class MedicinesController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Medicines
        [HttpPost]
        public ActionResult Edit(Medicines medicineToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_medicine = new Medicines();
                var medicine = entities.Medicines.Where(x => x.PatientID == medicineToEdit.PatientID).FirstOrDefault();
                if (medicine != null)
                {
                    medicine.Medicine = medicineToEdit.Medicine;
                    medicine.Active = true;
                }
                else
                {
                    null_medicine.Medicine = medicineToEdit.Medicine;
                    null_medicine.RegisterDate = DateTime.Now;
                    null_medicine.Active = true;
                    null_medicine.PatientID = medicineToEdit.PatientID;

                    entities.Medicines.Add(null_medicine);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class MedicineModel
    {
        public int ID { get; set; }

        public int PatientID { get; set; }

        public string Medicine { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}