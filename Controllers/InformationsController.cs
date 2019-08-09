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
    public class InformationsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Informations
        [HttpPost]
        public ActionResult Edit(Informations informationToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_information = new Informations();
                var information = entities.Informations.Where(x => x.PatientID == informationToEdit.PatientID).FirstOrDefault();
                if (information != null)
                {
                    information.Information = informationToEdit.Information;
                    information.Active = true;
                }
                else
                {
                    null_information.Information = informationToEdit.Information;
                    null_information.RegisterDate = DateTime.Now;
                    null_information.Active = true;
                    null_information.PatientID = informationToEdit.PatientID;

                    entities.Informations.Add(null_information);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class InformationModel
    {
        public int ID { get; set; }

        public int PatientID { get; set; }

        public string Information { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}