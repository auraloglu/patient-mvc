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
    public class FindingsController : Controller
    {
        PatientsDBEntities entities = new PatientsDBEntities();
        // GET: Findings
        [HttpPost]
        public ActionResult Edit(Findings findingToEdit)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //Edit 
                var null_finding = new Findings();
                var finding = entities.Findings.Where(x => x.InspectionID == findingToEdit.InspectionID).FirstOrDefault();
                if (finding != null)
                {
                    finding.Finding = findingToEdit.Finding;
                    finding.Active = true;
                }
                else
                {
                    null_finding.Finding = findingToEdit.Finding;
                    null_finding.RegisterDate = DateTime.Now;
                    null_finding.Active = true;
                    null_finding.InspectionID = findingToEdit.InspectionID;

                    entities.Findings.Add(null_finding);
                }
                entities.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
    public class FindingModel
    {
        public int ID { get; set; }

        public int InspectionID { get; set; }

        public string Finding { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
    }
}