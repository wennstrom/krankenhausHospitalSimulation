using System.IO;

namespace KrankenhausSjukhuset.StatisticsEvent
{
    public class KrankenhausStatisticsFileLogger
    {
        string path = "./filelogger.txt";
        public void MovedOutput(object source, MoveStatisticsEventargs args)
        {

            lock (path) // för att undvika konflikter
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(args.ToString());
                }
            }



        }
        public void RecoveredAndDeathsStatistics(object source, RecoveredAndDeathsStatisiticsEventArgs args)
        {

            lock (path)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(args.ToString());
                }
            }

        }
        public void ClearFile()
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Flush();
            }
        }

    }
}
