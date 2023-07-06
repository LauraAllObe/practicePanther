using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Summer2022Proj0.library.Models
{
    public class Bill
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                }
            }
        }

        private decimal totalAmount;
        public decimal TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                if (totalAmount != value)
                {
                    totalAmount = value;
                }
            }
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                if (dueDate != value)
                {
                    dueDate = value;
                }
            }
        }

        private int clientId;
        public int ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                if (clientId != value)
                {
                    clientId = value;
                }
            }
        }

        private int projectId;
        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                if (projectId != value)
                {
                    projectId = value;
                }
            }
        }

        public Bill()
        {
            id = 0;
            DueDate = DateTime.MaxValue;
            clientId = 0;
            projectId= 0;
            totalAmount = 0;
        }

        public override string ToString()
        {
            string projects = $"project {projectId}";
            if (projectId == 0)
                projects = $"one or more projects";
            return $"{id}. Client {clientId}'s bill for {projects} of {totalAmount}$ is due on {dueDate}.";
        }
    }
}
