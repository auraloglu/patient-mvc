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
    public class ComplainsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();

        [HttpPost]
        public ActionResult Edit(Complains complainToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_complain = new Complains();
                var complain = entities.Complains.Where(x => x.InspectionID == complainToEdit.InspectionID).FirstOrDefault();
                if (complain != null)
                {
                    complain.Complain = complainToEdit.Complain;
                    complain.Active = true;
                }
                else
                {
                    null_complain.Complain = complainToEdit.Complain;
                    null_complain.RegisterDate = DateTime.Now;
                    null_complain.Active = true;
                    null_complain.InspectionID = complainToEdit.InspectionID;

                    entities.Complains.Add(null_complain);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
    public class ComplainModel
    {
        public int ID { get; set; }

        public int InspectionID { get; set; }

        public string Complain { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}