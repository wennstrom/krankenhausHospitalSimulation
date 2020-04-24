using System;
using System.Linq;
using System.Threading;
using KrankenhausSjukhuset.Resetes;
using KrankenhausSjukhuset.StatisticsEvent;
using KrankenhausSjukhuset.Database.Creator;

namespace KrankenhausSjukhuset
{
    public class Simulation
    {
        public event EventHandler<RecoveredAndDeathsStatisiticsEventArgs> RecoveredAndDeathEvent;
        public event EventHandler<MoveStatisticsEventargs> MovedEvent;

        public static bool run = true;
        public  void Starter()
        {
            var simulation = new Simulation();
            
            var patientReset = new PatientReset();
            var statusReset = new StatusReset();
            var writer = new KrankenHausStatisticsWriter();
            var logger = new KrankenhausStatisticsFileLogger();

            simulation.RecoveredAndDeathEvent += writer.TotalDeathAndRecoveredOutput;
            simulation.RecoveredAndDeathEvent += logger.RecoveredAndDeathsStatistics;
            simulation.MovedEvent += logger.MovedOutput;
            simulation.MovedEvent += writer.MovedStatisticOutput;

            //trådarna för simulationen
            var t1 = new Thread(simulation.Add30Patients);
            var t2 = new Thread(simulation.MovePatients);
            var t3 = new Thread(() => RandomizePatientsIllnessLevel(new Random()));
            var t4 = new Thread(simulation.UpdatePatientStatus);

            t1.Start();
            t1.Join(); //låter t1 köra klart innan de andra anropas
            

            t2.Start();            
            t3.Start();
            t4.Start();
            t4.Join();
        }

        internal void Add30Patients()
        {
            using (var context = new KrankenhausSjukhusetDbContext())
            {

                var patientCreator = new PatientCreator();
                var newPatients = patientCreator.GetMultiplePatients(30, context.Patients.Count());

                context.Patients.AddRange(newPatients);
                context.SaveChanges();
            }


        }
        internal void MovePatients() //flyttar från kön till iva framtills iva är fullt (5)
        {
            bool movePatients = true;

            while (movePatients)
            {

                Thread.Sleep(500);

                //håller räkningen på antalet som flyttat till iva och/eller sanotorium
                int movedToIva = 0;
                int movedToSanotorium = 0;

                using (var context = new KrankenhausSjukhusetDbContext())
                {                                           
                    var mostILL = (from m in context.Patients
                                   where m.StatusID.Equals(1) || m.StatusID.Equals(2)
                                   orderby m.IllnessLevel descending, m.Age descending
                                   select m).ToList(); //most ill = sjukast i kön och sanotorium

                    int ivaCount = context.Patients.Where(p => p.StatusID == 3).Count(); //antal personer i iva
                    int ivaSpotsLeft = 5 - ivaCount;

                    //flyttar till iva om det finns platser kvar i iva och patienter i kön eller sanotorium 
                    for (int i = 0; i < ivaSpotsLeft && i < mostILL.Count(); i++)
                    {
                        mostILL[i].StatusID = 3;
                        movedToIva++;
                    }

                    var queue = (from p in context.Patients
                                 where p.StatusID.Equals(1)
                                 orderby p.IllnessLevel descending, p.Age
                                 select p).ToList();

                    int sanCount = context.Patients.Where(p => p.StatusID == 2).Count();

                    //10 är maximalkapaciteten för sanotoriumet
                    int sanSpotsLeft = 10 - sanCount;


                   

                    for (int i = 0; i < sanSpotsLeft && queue.Count > i; i++)
                    {
                        queue[i].StatusID = 2;
                        movedToSanotorium++;
                    }

                    
                    //skickar ut statistik till eventinvokermetoden
                    var stats = new MoveStatisticsEventargs(movedToIva, movedToSanotorium);
                    MovedEventSender(stats);

                    //kollar hur många patienter som finns kvar i sanotoriumet och kön
                    int queueAndSanCount = context.Patients.Where(p => p.StatusID == 1 || p.StatusID == 2).Count();
                    if (queueAndSanCount == 0) 
                        movePatients = false;   //kör inte loopen igen om det inte finns några kvar att flytta

                    context.SaveChanges();
                }

            }
        }
        internal  void RandomizePatientsIllnessLevel(Random rdm)
        {

            while (run)
            {
                
                Thread.Sleep(300);

                using (var context = new KrankenhausSjukhusetDbContext())
                {
                    var queue = (from p in context.Patients
                                 where p.StatusID == 1
                                 select p).ToList();

                    var sanotorium = (from p in context.Patients
                                      where p.StatusID == 2
                                      select p).ToList();

                    var iva = (from p in context.Patients
                               where p.StatusID == 3
                               select p).ToList();

                    int percentage;

                    foreach (var p in queue)
                    {
                        
                        percentage = rdm.Next(1, 101); //randomerar procenten

                        if (percentage <= 50)
                            p.IllnessLevel += 0;                        
                        else if (percentage <= 60)
                            p.IllnessLevel -= 1;
                        else if (percentage <= 90)
                            p.IllnessLevel += 1;
                        else if (percentage <= 100)
                            p.IllnessLevel += 3;
                    }

                    foreach (var p in sanotorium)
                    {
                        percentage = rdm.Next(1, 101);

                        if (percentage <= 65)
                            p.IllnessLevel += 0;
                        else if (percentage <= 85)
                            p.IllnessLevel -= 1;
                        else if (percentage <= 95)
                            p.IllnessLevel += 1;
                        else if (percentage <= 100)
                            p.IllnessLevel += 3;
                    }

                    foreach (var p in iva)
                    {
                        percentage = rdm.Next(1, 101);

                        if (percentage <= 20)
                            p.IllnessLevel += 0;
                        else if (percentage <= 80)
                            p.IllnessLevel -= 3;
                        else if (percentage <= 90)
                            p.IllnessLevel += 1;
                        else if (percentage <= 100)
                            p.IllnessLevel += 3;
                    }

                    context.SaveChanges();

                }
            }
        }
        internal void UpdatePatientStatus()
        {
            while (run)
            {
                

                Thread.Sleep(500);

                int recoveredPatientsCount = 0, deadPatientsCount = 0;

                using (var context = new KrankenhausSjukhusetDbContext())
                {
                    int numberOfIllPatients = context.Patients.Where(p => p.IllnessLevel > 0 && p.IllnessLevel < 10).Count(); //antalet sjuka personer

                    //kollar hur många patienter som ligger inne eller befinner sig i kön
                    if (numberOfIllPatients == 0)
                    {
                        run = false;
                        return; //tråden och programmet avlutas, corona is no more
                    }

                    var allPatients = (from p in context.Patients
                                       where p.StatusID == 1 || p.StatusID == 2 || p.StatusID == 3
                                       select p).ToList();

                    foreach (var p in allPatients)
                    {
                        if (p.IllnessLevel <= 0)
                        {
                            p.StatusID = 4; //tillfrisknad :)
                            recoveredPatientsCount++;
                        }
                        else if (p.IllnessLevel >= 10)
                        {
                            p.StatusID = 5; //rip 
                            deadPatientsCount++;
                        }
                    }
                                      
                    context.SaveChanges();

                }

                var stats = new RecoveredAndDeathsStatisiticsEventArgs(recoveredPatientsCount, deadPatientsCount);
                RecoveredAndDeathsEventSender(stats);
            }
        }

        internal virtual void MovedEventSender(MoveStatisticsEventargs args)
        {
           MovedEvent?.Invoke(this, args);
        }
        internal virtual void RecoveredAndDeathsEventSender(RecoveredAndDeathsStatisiticsEventArgs args)
        {
            RecoveredAndDeathEvent?.Invoke(this, args);
        }

    }
}
