using System;
using KrankenhausSjukhuset.Creator;
using KrankenhausSjukhuset.Resetes;
using KrankenhausSjukhuset.StatisticsEvent;

namespace KrankenhausSjukhuset
{
    class Program
    {
        static void Main(string[] args)
        {
            var simulation = new Simulation();
            var patientReset = new PatientReset();
            var statusReset = new StatusReset();
            var logger = new KrankenhausStatisticsFileLogger();
            var statusCreator = new StatusCreator();

            logger.ClearFile();
            statusReset.ResetAll();
            patientReset.Reset();
            statusCreator.CreateAll();

            DateTime startTime = DateTime.Now;
            Console.WriteLine("simulating ... \n");        

            simulation.Starter();
            var runTime = CalculateRuntime(startTime);


            Console.WriteLine($"\napplication runtime: {runTime}");
            Console.ReadLine();
        }
        static TimeSpan CalculateRuntime(DateTime startTime)
        {
            return DateTime.Now - startTime;
        }
    }
}
