using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYC_REI_Console.Entities
{
    class hpd_contacts_ent
    {
        public string RegistrationContactID { get; set; }
        public string RegistrationID { get; set; }
        public string Type { get; set; }
        public string ContactDescription { get; set; }
        public string CorporationName { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string BusinessHouseNumber { get; set; }
        public string BusinessStreetName { get; set; }
        public string BusinessApartment { get; set; }
        public string BusinessCity { get; set; }
        public string BusinessState { get; set; }
        public string BusinessZip { get; set; }

    }
    class hpd_registrations_ent
    {
        public string RegistrationID { get; set; }
        public string BuildingID { get; set; }
        public string BoroID { get; set; }
        public string Boro { get; set; }
        public string HouseNumber { get; set; }
        public string LowHouseNumber { get; set; }
        public string HighHouseNumber { get; set; }
        public string StreetName { get; set; }
        public string Zip { get; set; }
        public int? Block { get; set; }
        public int? Lot { get; set; }
        public int? BIN { get; set; }
        public int? CommunityBoard { get; set; }
        public DateTime? LastRegistrationDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }

    }
}
