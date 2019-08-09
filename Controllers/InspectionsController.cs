using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Final.Models;

namespace MVC_Final.Controllers
{
    public class InspectionsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Inspections
        [HttpPost]
        public ActionResult Edit(Inspections inspectionToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var inspection = entities.Inspections.Where(x => x.ID == inspectionToEdit.ID).FirstOrDefault();
                if (inspection != null)
                {
                    inspection.Name = inspectionToEdit.Name;
                    inspection.Active = true;
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class InspectionModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int PatientID { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}