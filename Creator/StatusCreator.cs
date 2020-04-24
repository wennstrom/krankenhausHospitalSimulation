using KrankenhausSjukhuset.Models;

namespace KrankenhausSjukhuset.Creator
{
    public class StatusCreator
    {
        public void CreateAll()
        {
            CreateQueue();
            CreateSanotorium();
            CreateIVA();
            CreateRecovered();
            CreateAfterlife();

        }
        internal void CreateQueue()
        {
            using (var Context = new KrankenhausSjukhusetDbContext())
            {
                var queue = new Status() { ID = 1, PatientStatus = "Queue", Patients = null };
                Context.Satuses.Add(queue);
                Context.SaveChanges();
            }
        }
        internal void CreateSanotorium()
        {
            using (var Context = new KrankenhausSjukhusetDbContext())
            {
                var sanotorium = new Status() { ID = 2, PatientStatus = "Sanotorium", Patients = null };
                Context.Satuses.Add(sanotorium);
                Context.SaveChanges();
            }
        }
        internal void CreateIVA()
        {
            using (var Context = new KrankenhausSjukhusetDbContext())
            {
                var iva = new Status() { ID = 3, PatientStatus = "IVA", Patients = null };
                Context.Satuses.Add(iva);
                Context.SaveChanges();
            }
        }
        internal void CreateRecovered()
        {
            using (var Context = new KrankenhausSjukhusetDbContext())
            {
                var recovered = new Status() { ID = 4, PatientStatus = "Recovered", Patients = null };
                Context.Satuses.Add(recovered);
                Context.SaveChanges();
            }
        }
        internal void CreateAfterlife()
        {
            using (var Context = new KrankenhausSjukhusetDbContext())
            {
                var afterlife = new Status() { ID = 5, PatientStatus = "Afterlife", Patients = null };
                Context.Satuses.Add(afterlife);
                Context.SaveChanges();
            }
        }
    }
}
