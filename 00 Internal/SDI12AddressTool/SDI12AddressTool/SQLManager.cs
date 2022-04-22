using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDI12AddressTool
{
    class SQLManager
    {
        SqlConnection cnn;
        string connectionString = @"Data Source=tcp:DESKTOP-FM862QL,1433;Initial Catalog=pcbtesting;User ID=admin;Password=qi2008";


        public void SetDeviceParams(
            string tubesn,
            string tubetype,
            string tubeowner,
            string devSn,
            string fw,
            string note,
            string sdi)
        {
            connectionString = @"Data Source=tcp:DESKTOP-FM862QL,1433;Initial Catalog=CRNStesting;User ID=admin;Password=qi2008";

            cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand($"SetDevInfo", cnn)
            {
                CommandType = System.Data.CommandType.StoredProcedure,
            };
            cmd.Parameters.Add(new SqlParameter("npmSn", devSn));
            cmd.Parameters.Add(new SqlParameter("tubeSn", tubesn));
            cmd.Parameters.Add(new SqlParameter("tubeType", tubetype));
            cmd.Parameters.Add(new SqlParameter("npmFwVersion", fw));
            cmd.Parameters.Add(new SqlParameter("tubeOwner", tubeowner));
            cmd.Parameters.Add(new SqlParameter("hv", -1));
            cmd.Parameters.Add(new SqlParameter("thOk", 1));
            cmd.Parameters.Add(new SqlParameter("vTest", -1));
            cmd.Parameters.Add(new SqlParameter("sdiAddress", sdi));
            cmd.Parameters.Add(new SqlParameter("gain", -1));
            cmd.Parameters.Add(new SqlParameter("discLow", -1));
            cmd.Parameters.Add(new SqlParameter("discHigh", -1));
            cmd.Parameters.Add(new SqlParameter("nbins", -1));
            cmd.Parameters.Add(new SqlParameter("dac", "NULL"));
            cmd.Parameters.Add(new SqlParameter("lpJumper", "NULL"));
            cmd.Parameters.Add(new SqlParameter("note", note));

            cmd.ExecuteNonQuery();

            cnn.Close();
        }

    }
}
