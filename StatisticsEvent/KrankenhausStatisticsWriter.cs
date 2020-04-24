using System;

namespace KrankenhausSjukhuset.StatisticsEvent
{
    public class KrankenHausStatisticsWriter
    {
        public void MovedStatisticOutput(object source, MoveStatisticsEventargs args)
        {

            Console.WriteLine(args.ToString());

        }
        
        public void TotalDeathAndRecoveredOutput(object source, RecoveredAndDeathsStatisiticsEventArgs args)
        {

            Console.WriteLine(args.ToString());

        }
    }
}
