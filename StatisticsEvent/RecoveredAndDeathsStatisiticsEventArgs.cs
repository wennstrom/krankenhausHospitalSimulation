using System;

namespace KrankenhausSjukhuset.StatisticsEvent
{
    public class RecoveredAndDeathsStatisiticsEventArgs : EventArgs
    {
        public RecoveredAndDeathsStatisiticsEventArgs(int recovered, int deadPatients)
        {
            RecorevedPatients = recovered;
            DeadPatients = deadPatients;
        }
        public int RecorevedPatients { get; set; }
        public int DeadPatients { get; set; }
        public override string ToString()
        {
            return $"Recovered: {RecorevedPatients} Deaths: {RecorevedPatients}";
        }

    }
}
