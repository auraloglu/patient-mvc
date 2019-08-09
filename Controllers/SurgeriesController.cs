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
    public class SurgeriesController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        [HttpPost]
        public ActionResult Edit(Surgeries surgeryToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_surgery = new Surgeries();
                var surgery = entities.Surgeries.Where(x => x.PatientID == surgeryToEdit.PatientID).FirstOrDefault();
                if (surgery != null)
                {
                    surgery.Surgery = surgeryToEdit.Surgery;
                    surgery.Active = true;
                }
                else
                {
                    null_surgery.Surgery = surgeryToEdit.Surgery;
                    null_surgery.RegisterDate = DateTime.Now;
                    null_surgery.Active = true;
                    null_surgery.PatientID = surgeryToEdit.PatientID;

                    entities.Surgeries.Add(null_surgery);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class SurgeryModel
    {
        public int ID { get; set; }

        public int PatientID { get; set; }

        public string Surgery { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}