using System;
using System.Collections.Generic;
using KrankenhausSjukhuset.Models;

namespace KrankenhausSjukhuset.Database.Creator
{
    public class PatientCreator
    {
        internal static Random rdm = new Random();
        public Patient CreatePatient(int count)
        {
            DateTime birthDate = GenerateBirthdate();
            int age = DateTime.Now.Year - birthDate.Year; 
            var patient = new Patient()
            {
                SSN = GenerateSSN(birthDate),
                Name = $"Patient #{count}",
                Age = age,
                IllnessLevel = GenerateIllnessLevel(),
                StatusID = 1
            };

            return patient;
        }


        public ICollection<Patient> GetMultiplePatients(int num, int count)
        {
            List<Patient> result = new List<Patient>();

            for (int i = 0; i < num; i++)
            {
                var newPatient = CreatePatient(count + i);
                result.Add(newPatient);
            }

            return result;
        }


        internal static DateTime GenerateBirthdate()
        {
            var oldest = DateTime.Parse("1903-01-02"); //äldsta människoan
            var now = DateTime.Now; //idag

            int daysRange = (int)(now - oldest).TotalDays; //antal dagar som skiljer mellan oldest och idag
            int value = rdm.Next(0, daysRange); //slumpar ett tel mellan 0 till skillnaden

            return oldest.AddDays(value); //lägger till resultatet till oldest
        }
        internal static string GenerateSSN(DateTime dateOfBirth)
        {
            string lastFour = rdm.Next(0, 9999).ToString("D4");


            return dateOfBirth.ToString("yyMMdd") + "-" + lastFour; //ssnformatet = yymmdd-xxxx
        }
        internal static int GenerateIllnessLevel()
        {
            int value = rdm.Next(1, 10); //mindre än noll = frisk / större än 10 = död

            return value;
        }
        internal static string GenerateName(int count)
        {
            return $"Patient #{count + 1}";
        }
    }
}

