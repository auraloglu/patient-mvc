using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Final.Models
{
    public class UserViewModel
    {
        public List<Complains> Complain_List { get; set; }
        public List<Diagnosis> Diagnosis_List { get; set; }
        public List<Findings> Finding_List { get; set; }
        public List<Treatment_Plans> Treatment_Plan_List { get; set; }
        public List<Examinations> Examination_List { get; set; }
        public List<Photos> Photos_List { get; set; }
        public List<Inspections> Inspections_List { get; set; }
        public Patients Patients { get; set; }
        public Diseases Disease { get; set; }
        public Surgeries Surgery { get; set; }
        public Medicines Medicine { get; set; }
        public Informations Information { get; set; }
    }
}