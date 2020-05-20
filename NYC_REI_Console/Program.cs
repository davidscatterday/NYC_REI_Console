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
            socrataDataDAL.GetAllMapPluto(result.OBJECTID);
            socrataDataDAL.InsertAllEnergy(result.generation_date);
            socrataDataDAL.InsertAllPermits(result.dobrundate);
            socrataDataDAL.InsertAllViolations(result.issue_date);
            socrataDataDAL.InsertAllEvictions(result.EXECUTED_DATE);
        }
    }
}
