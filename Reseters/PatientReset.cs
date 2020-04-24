using System.Linq;

namespace KrankenhausSjukhuset.Resetes
{
    public class PatientReset
    {
        public void Reset()
        {
            using (var context = new KrankenhausSjukhusetDbContext())
            {

                var allPatients = context.Patients.ToList();

                context.Patients.RemoveRange(allPatients);

                context.SaveChanges();
            }
        }
    }
}
