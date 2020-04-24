using System.Linq;

namespace KrankenhausSjukhuset.Resetes
{
   public class StatusReset
    {
        public void ResetAll()
        {
            ResetQueue();
            ResetSanotorium();
            ResetIVA();
            ResetRecovered();
            ResetAfterlife();
        }

        private void ResetQueue()
        {
            using (var context = new KrankenhausSjukhusetDbContext())
            {
                var queue = (from q in context.Satuses
                             where q.ID == 1
                             select q).ToList();
                context.Satuses.RemoveRange(queue);
                context.SaveChanges();
            }
        }
        private void ResetSanotorium()
        {
            using (var context = new KrankenhausSjukhusetDbContext())
            {
                var sanotorium = (from q in context.Satuses
                                  where q.ID == 2
                                  select q).ToList();
                context.Satuses.RemoveRange(sanotorium);
                context.SaveChanges();
            }
        }
        private void ResetIVA()
        {
            using (var context = new KrankenhausSjukhusetDbContext())
            {
                var iva = (from q in context.Satuses
                           where q.ID == 3
                           select q).ToList();
                context.Satuses.RemoveRange(iva);
                context.SaveChanges();
            }
        }
        private void ResetRecovered()
        {
            using (var context = new KrankenhausSjukhusetDbContext())
            {
                var recovered = (from q in context.Satuses
                                 where q.ID == 4
                                 select q).ToList();
                context.Satuses.RemoveRange(recovered);
                context.SaveChanges();
            }
        }
        private void ResetAfterlife()
        {
            using (var context = new KrankenhausSjukhusetDbContext())
            {
                var afterlife = (from q in context.Satuses
                                 where q.ID == 5
                                 select q).ToList();
                context.Satuses.RemoveRange(afterlife);
                context.SaveChanges();
            }
        }
    }
}
