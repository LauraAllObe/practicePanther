using Summer2022Proj0.library.Models;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Summer2022Proj0.API.EC
{
    public class BillEC
    {
        private EfContext ef = new EfContextFactory().CreateDbContext(new string[0]);
        public BillDTO AddOrEdit(BillDTO dto)
        {
            var bill = new Bill(dto);
            if (dto.Id <= 0)
                ef.Bills.Add(bill);
            else
                ef.Bills.Update(bill);
            ef.SaveChanges();

            return new BillDTO(bill);
        }

        public BillDTO? Get(int id)
        {
            var returnVal = ef.Bills
                    .FirstOrDefault(b => b.Id == id)
                    ?? new Bill();
            return new BillDTO(returnVal);
        }

        public BillDTO? Delete(int id)
        {
            var bill = ef.Bills.FirstOrDefault(b => b.Id == id);
            if (bill != null)
                ef.Bills.Remove(bill);
            ef.SaveChanges();
            return bill != null ? new BillDTO(bill) : null;
        }

        public IEnumerable<BillDTO> Search(string query = "")
        {
            IEnumerable<BillDTO> returnVal = ef.Bills
                    .Where(b => b.DueDate.ToString().ToUpper()
                    .Contains(query.ToUpper()))
                    .Take(1000)
                    .Select(b => new BillDTO(b));
            return returnVal;
        }
    }
}
