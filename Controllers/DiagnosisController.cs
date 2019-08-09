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
    public class DiagnosisController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Diagnosis
        [HttpPost]
        public ActionResult Edit(Diagnosis diagnosisToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_diagnosis = new Diagnosis();
                var diagnosis = entities.Diagnosis.Where(x => x.InspectionID == diagnosisToEdit.InspectionID).FirstOrDefault();
                if (diagnosis != null)
                {
                    diagnosis.Description = diagnosisToEdit.Description;
                    diagnosis.Active = true;
                }
                else
                {
                    null_diagnosis.Description = diagnosisToEdit.Description;
                    null_diagnosis.RegisterDate = DateTime.Now;
                    null_diagnosis.Active = true;
                    null_diagnosis.InspectionID = diagnosisToEdit.InspectionID;

                    entities.Diagnosis.Add(null_diagnosis);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class DiagnosisModel
    {
        public int ID { get; set; }

        public int InspectionID { get; set; }

        public string Description { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}