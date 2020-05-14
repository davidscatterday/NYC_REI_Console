using NYC_REI_Console.DataAccessLayer;
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
            socrataDataDAL.GetAllMapPluto();
            //socrataDataDAL.InsertAllPermits();
            //socrataDataDAL.InsertAllViolations();
            //socrataDataDAL.InsertAllEnergy();
            //socrataDataDAL.InsertAllEvictions();
        }
    }
}
