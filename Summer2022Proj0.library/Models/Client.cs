using Summer2022Proj0.library.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer2022Proj0.library.Models
{
    public class Client
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
                if(id != value)
                {
                    id = value;
                }
            }
        }

        private DateTime openDate;
        public DateTime OpenDate
        {
            get
            {
                return openDate;
            }
            set
            {
                if(openDate != value)
                {
                    openDate = value;
                }
            }
        }

        private DateTime closedDate;
        public DateTime ClosedDate
        {
            get
            {
                return closedDate;
            }
            set
            {
                if(closedDate != value)
                {
                    closedDate = value;
                }
            }
        }

        private Boolean isActive;
        public Boolean IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                if(isActive != value)
                {
                    isActive = value;
                }
            }
        }

        private String name;
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                if(name != value)
                {
                    name = value;
                }
            }
        }

        private String notes;
        public String Notes
        {
            get
            {
                return notes;
            }
            set
            {
                if(notes != value)
                {
                    notes = value;
                }
            }
        }

        public Client()
        {
            notes = "N/A";
            name = "John/Jane Doe";
            id = 0;
            openDate = DateTime.MinValue;
            closedDate = DateTime.MinValue;
            isActive = true;
        }
        public Client(ClientDTO dto)
        {
            this.Notes = dto.Notes;
            this.Name = dto.Name;
            this.Id = dto.Id;
            this.OpenDate = dto.OpenDate;
            this.ClosedDate = dto.ClosedDate;
            this.IsActive = dto.IsActive;
        }

        public override string ToString()
        {
            string isActiveString = "not an Active";
            if (isActive == true)
                isActiveString = "an Active";

            return $"{id}. {name} is {isActiveString} client open from {openDate} up until {closedDate}. Notes: {notes}";
        }
    }
}
