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
    public class ExaminationsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Examinations

        [HttpPost]
        public ActionResult Edit(Examinations examinationToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_examination = new Examinations();
                var examination = entities.Examinations.Where(x => x.InspectionID == examinationToEdit.InspectionID).FirstOrDefault();
                if (examination != null)
                {
                    examination.Examination = examinationToEdit.Examination;
                    examination.Active = true;
                }
                else
                {
                    null_examination.Examination = examinationToEdit.Examination;
                    null_examination.RegisterDate = DateTime.Now;
                    null_examination.Active = true;
                    null_examination.InspectionID = examinationToEdit.InspectionID;

                    entities.Examinations.Add(null_examination);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class ExaminationModel
    {
        public int ID { get; set; }

        public int InspectionID { get; set; }

        public string Examination { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}