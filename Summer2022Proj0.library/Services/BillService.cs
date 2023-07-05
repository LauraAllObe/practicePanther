using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Summer2022Proj0.library.Models;

namespace Summer2022Proj0.library.Services
{
    public class BillService
    {
        private List<Bill> bills;
        public List<Bill> Bills
        {
            get
            {
                return bills;
            }
        }
        private static BillService? instance;
        private static object _lock = new object();
        public static BillService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new BillService();
                    }
                }
                return instance;
            }
        }

        private BillService()
        {
            bills = new List<Bill>();
            /*projects = new List<Project>()
            {
                new Project{ClientId = 1, Id=1, ClosedDate=DateTime.Now, OpenDate=DateTime.Today, IsActive=true, LongName="longname1", ShortName="shortname1"},
                new Project{ClientId = 1, Id=2, ClosedDate=DateTime.Now, OpenDate=DateTime.Today, IsActive=true, LongName="longname2", ShortName="shortname2"}
            };*/
        }
        public Bill? Get(int id)
        {
            return Bills.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Bill? bill)
        {
            if (bill != null)
            {
                if (bill.Id == 0)
                {
                    bill.Id = FreeId;
                }
                bills.Add(bill);
            }
        }
        public void Delete(int id)
        {
            var enrollmentToRemove = Get(id);
            if (enrollmentToRemove != null)
            {
                bills.Remove(enrollmentToRemove);
            }
        }

        private int FreeId
        {
            get
            {
                bool currentExists;
                for (int i = 1; i > 0; i++)
                {
                    currentExists = false;
                    foreach (var bill in bills)
                    {
                        if (bill.Id == i)
                        {
                            currentExists = true;
                            break;
                        }
                    }
                    if (currentExists == false)
                        return i;
                }
                return 0;
            }
        }

        public IEnumerable<Bill> Search(string query)
        {
            return bills.Where(s => (s.DueDate.ToString()).Contains(query.ToUpper()));
        }
    }
}
