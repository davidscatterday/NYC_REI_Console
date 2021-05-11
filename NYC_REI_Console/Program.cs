using NYC_REI_Console.DataAccessLayer;
using NYC_REI_Console.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYC_REI_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SocrataDataDAL socrataDataDAL = new SocrataDataDAL();
            DatabaseMaxValues result = socrataDataDAL.GetMaxValues();
            socrataDataDAL.InsertAllMapPluto(result.OBJECTID);
            socrataDataDAL.InsertAllEnergy(result.generation_date);
            socrataDataDAL.InsertAllPermits(result.dobrundate);
            socrataDataDAL.InsertAllViolations(result.issue_date);
            socrataDataDAL.InsertAllEvictions(result.EXECUTED_DATE);
            socrataDataDAL.InsertAllDistricts(result.DistrictOBJECTID);
            socrataDataDAL.InsertAllElevators(result.filing_date);
            socrataDataDAL.InsertAllPropertySales(result.sale_date);
            socrataDataDAL.InsertAllEcbViolations(result.ecb_issue_date);
            socrataDataDAL.InsertAllSafetyFacadesComplianceFilings(result.filing_date_sfcf);
            socrataDataDAL.CheckAlerts(result.OBJECTID);


            //socrataDataDAL.InsertAllDesignations(0);
            //socrataDataDAL.InsertAllConsumerProfiles(2018);
            //socrataDataDAL.ReadTextFile();
            //socrataDataDAL.DownloadAllTextFiles();
            //socrataDataDAL.DownloadBls();


            //socrataDataDAL.hpd_contacts_insert();
            //socrataDataDAL.hpd_registrations_insert();
            //socrataDataDAL.hpd_violations_insert();
        }
    }
}
