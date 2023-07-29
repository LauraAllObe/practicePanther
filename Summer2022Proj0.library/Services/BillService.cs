using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Summer2022Proj0.library.DTO;
using Summer2022Proj0.library.Models;
using Summer2022Proj0.Library.Utilities;

namespace Summer2022Proj0.library.Services
{
    public class BillService
    {
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
            var response = new WebRequestHandler()
                    .Get("/Bill")
                    .Result;
            bills = JsonConvert
                .DeserializeObject<List<BillDTO>>(response)
                ?? new List<BillDTO>();

        }
        private List<BillDTO> bills;
        public List<BillDTO> Bills
        {
            get
            {
                return bills ?? new List<BillDTO>();
            }
        }
        public BillDTO? Get(int id)
        {
            return Bills.FirstOrDefault(b => b.Id == id);
        }
        public void AddOrEdit(BillDTO b)
        {
            var response
                = new WebRequestHandler().Post("/Bill", b).Result;
            //MISSING CODE
            var myEditedBill = JsonConvert.DeserializeObject<BillDTO>(response);
            if (myEditedBill != null)
            {
                var existingBill = bills.FirstOrDefault(b => b.Id == myEditedBill.Id);
                if (existingBill == null)
                {
                    bills.Add(myEditedBill);
                }
                else
                {
                    var index = bills.IndexOf(existingBill);
                    bills.Remove(existingBill);
                    bills.Insert(index, myEditedBill);
                }
            }
        }
        public void Delete(int id)
        {
            var billToDelete = Bills.FirstOrDefault(b => b.Id == id);
            if (billToDelete != null)
            {
                var response
                = new WebRequestHandler().Delete($"/Bill/Delete/{id}").Result;
                Bills.Remove(billToDelete);
            }
        }
        public IEnumerable<BillDTO> Search(string query)
        {
            return Bills
                .Where(b => b.DueDate.ToString().ToUpper()
                .Contains(query.ToUpper()));
        }
    }
}
