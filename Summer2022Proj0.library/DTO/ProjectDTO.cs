using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Summer2022Proj0.library.Models;

namespace Summer2022Proj0.library.DTO
{
    public class ProjectDTO
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

        private DateTime openDate;
        public DateTime OpenDate
        {
            get
            {
                return openDate;
            }
            set
            {
                if (openDate != value)
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
                if (closedDate != value)
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
                if (isActive != value)
                {
                    isActive = value;
                }
            }
        }

        private String shortName;
        public String ShortName
        {
            get
            {
                return shortName;
            }
            set
            {
                if (shortName != value)
                {
                    shortName = value;
                }
            }
        }

        private String longName;
        public String LongName
        {
            get
            {
                return longName;
            }
            set
            {
                if (longName != value)
                {
                    longName = value;
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

        public ProjectDTO()
        {
            id = 0;
            longName = "longName";
            shortName = "shortName";
            openDate = DateTime.MinValue;
            closedDate = DateTime.MaxValue;
            isActive = true;
            clientId = 0;
        }
        public ProjectDTO(Project project)
        {
            this.Id = project.Id;
            this.LongName = project.LongName;
            this.ShortName = project.ShortName;
            this.OpenDate = project.OpenDate;
            this.ClosedDate = project.ClosedDate;
            this.IsActive = project.IsActive;
            this.ClientId = project.ClientId;
        }

        public override string ToString()
        {
            string isActiveString = "not an Active";
            if (isActive == true)
                isActiveString = "an Active";
            string linked = "not linked to a client.";
            if (clientId != 0)
                linked = $"linked to a client {clientId}.";
            return $"{id}. Short name {shortName}, long name {longName} is {isActiveString} project open from {openDate} up until {closedDate}. This project {linked}";
        }
    }
}
