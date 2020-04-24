using System;

namespace KrankenhausSjukhuset.StatisticsEvent
{
    public class MoveStatisticsEventargs : EventArgs
    {

        public MoveStatisticsEventargs(int movedToIva, int movedToSanotorium)
        {
            this.MovedToIva = movedToIva;
            this.MovedToSanotorium = movedToSanotorium;
        }
        public int MovedToIva { get; set; }
        public int MovedToSanotorium { get; set; }

        public override string ToString()
        {
            return $"{MovedToIva} patients moved in to IVA. {MovedToSanotorium} moved into Sanotorium";
        }
    }
}
