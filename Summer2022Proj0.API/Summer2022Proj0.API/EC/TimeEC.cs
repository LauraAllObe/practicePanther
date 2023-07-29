using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Summer2022Proj0.API.EC
{
    public class TimeEC
    {
        private EfContext ef = new EfContextFactory().CreateDbContext(new string[0]);
        public TimeDTO AddOrEdit(TimeDTO dto)
        {
            var time = new Time(dto);
            if (dto.Id <= 0)
                ef.Times.Add(time);
            else
                ef.Times.Update(time);
            ef.SaveChanges();

            return new TimeDTO(time);
        }

        public TimeDTO? Get(int id)
        {
            var returnVal = ef.Times
                    .FirstOrDefault(t => t.Id == id)
                    ?? new Time();
            return new TimeDTO(returnVal);
        }

        public TimeDTO? Delete(int id)
        {
            var time = ef.Times.FirstOrDefault(t => t.Id == id);
            if (time != null)
                ef.Times.Remove(time);
            ef.SaveChanges();
            return time != null ? new TimeDTO(time) : null;
        }

        public IEnumerable<TimeDTO> Search(string query = "")
        {
            IEnumerable<TimeDTO> returnVal = ef.Times
                    .Where(t => t.Narrative.ToUpper()
                    .Contains(query.ToUpper()))
                    .Take(1000)
                    .Select(t => new TimeDTO(t));
            return returnVal;
        }
    }
}
