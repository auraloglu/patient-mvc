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
    public class DiseasesController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Diseases

        [HttpPost]
        public ActionResult Edit(Diseases diseaseToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_disease = new Diseases();
                var disease = entities.Diseases.Where(x => x.PatientID == diseaseToEdit.PatientID).FirstOrDefault();
                if (disease != null)
                {
                    disease.Disease = diseaseToEdit.Disease;
                    disease.Active = true;
                }
                else
                {
                    null_disease.Disease = diseaseToEdit.Disease;
                    null_disease.RegisterDate = DateTime.Now;
                    null_disease.Active = true;
                    null_disease.PatientID = diseaseToEdit.PatientID;

                    entities.Diseases.Add(null_disease);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class DiseaseModel
    {
        public int ID { get; set; }

        public int PatientID { get; set; }

        public string Disease { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}