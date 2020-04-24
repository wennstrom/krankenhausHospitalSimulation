using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KrankenhausSjukhuset.Models;

namespace KrankenhausSjukhuset.Models
{
    public class Patient
    {
        public Patient() { }
        public Patient(string ssn, string name, int age, int illnesslevel, int statusid)
        {
            this.SSN = ssn;
            this.Name = name;
            this.Age = age;
            this.IllnessLevel = illnesslevel;
            this.StatusID = statusid;

        }
 

        //Poperties
        public string SSN { get; set; }  //Personnummer + fyra sista = unikt, därav primary key

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int IllnessLevel { get; set; }

        [Required]
        public int StatusID { get; set; }
        public Status Status { get; set; }

      
    }
}
