using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrankenhausSjukhuset.Models
{
    public class Status
    {
        public Status() { }
        public Status(int id, string patientStatus)
        {
            this.ID = id;
            this.PatientStatus = patientStatus;
        }
        public int ID { get; set; }
        [Required]
        public string PatientStatus { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}
