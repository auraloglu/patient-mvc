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
    public class TreatmentsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Treatments
        [HttpPost]
        public ActionResult Edit(Treatment_Plans treatmentToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_treatment = new Treatment_Plans();
                var treatment = entities.Treatment_Plans.Where(x => x.InspectionID == treatmentToEdit.InspectionID).FirstOrDefault();
                if (treatment != null)
                {
                    treatment.Treatment_Plan = treatmentToEdit.Treatment_Plan;
                    treatment.Active = true;
                }
                else
                {
                    null_treatment.Treatment_Plan = treatmentToEdit.Treatment_Plan;
                    null_treatment.RegisterDate = DateTime.Now;
                    null_treatment.Active = true;
                    null_treatment.InspectionID = treatmentToEdit.InspectionID;

                    entities.Treatment_Plans.Add(null_treatment);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class TreatmentModel
    {
        public int ID { get; set; }

        public int InspectionID { get; set; }

        public string Treatment { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}