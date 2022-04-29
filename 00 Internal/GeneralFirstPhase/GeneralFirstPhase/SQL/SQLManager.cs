using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace QIXLPTesting.SQL
{
    class SQLManager
    {
        static string connectionString = @"Data Source=tcp:DESKTOP-FM862QL,1433;Initial Catalog=Master_Generic;User ID=admin;Password=qi2008";


        internal static bool AddRow(string sn, string type, string model, string firmware, DateTime edited, string note = "")
        {
            int ret = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Add_NPM", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = sn;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type;
                    cmd.Parameters.Add("@model", SqlDbType.VarChar).Value = model;
                    cmd.Parameters.Add("@firmware", SqlDbType.VarChar).Value = firmware;
                    cmd.Parameters.Add("@edited", SqlDbType.DateTime).Value = edited;
                    cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = note;

                    con.Open();
                    try
                    {
                        ret = cmd.ExecuteNonQuery();
                    }
                    catch { Debug.WriteLine("Unable to add to Table"); }
                }
            }
            return ret == 0;
        }

        internal static bool EditSerial(string oldSn, string newSN)
        {
            int ret = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Change_Serial", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = oldSn;
                    cmd.Parameters.Add("@newID", SqlDbType.VarChar).Value = newSN;

                    con.Open();
                    ret = cmd.ExecuteNonQuery();
                }
            }
            return ret == 0;
        }

        internal static List<string> GetAllSerials()
        {
            List<string> allSerials = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_All_Keys", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        allSerials.Add(dataReader.GetString(0));
                    }

                }
            }


            return allSerials;
        }

        internal static NPMData GetInfo(string serial)
        {
            NPMData ret = new NPMData();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Info", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;

                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();

                    ret.Sn = dataReader.GetString(0);
                    ret.Family = dataReader.GetString(1);
                    ret.Model = dataReader.GetString(2);
                    ret.Firmware = dataReader.GetString(3);
                    ret.Tube = dataReader.GetString(4);
                    ret.Edited = dataReader.GetDateTime(5);
                    ret.Note = dataReader.GetString(6);

                }
            }
            return ret;
        }
        internal static void GetTestInfo(ref NPMData ret)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = ret.Sn;

                    con.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();

                    if (!dataReader.IsDBNull(1))
                    {
                        ret.Volt = dataReader.GetBoolean(1);
                    }
                    if (!dataReader.IsDBNull(2))
                    {
                        ret.Led = dataReader.GetBoolean(2);
                    }
                    if (!dataReader.IsDBNull(3))
                    {
                        ret.Sdev = dataReader.GetBoolean(3);
                    }
                    if (!dataReader.IsDBNull(4))
                    {
                        ret.Pulsesim = dataReader.GetBoolean(4);
                    }
                    if (!dataReader.IsDBNull(5))
                    {
                        ret.Temp = dataReader.GetBoolean(5);
                    }
                    if (!dataReader.IsDBNull(6))
                    {
                        ret.Sdi = dataReader.GetBoolean(6);
                    }

                }
            }
        }

        internal static void UpdateModel(string val, string serial)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Table_Generic", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@model", SqlDbType.VarChar).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void UpdateFamily(string val, string serial)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Table_Generic", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void UpdateFirmware(string val, string serial)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Table_Generic", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@firmware", SqlDbType.VarChar).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void UpdateNote(string val, string serial)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Table_Generic", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@note", SqlDbType.VarChar).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }
        internal static void UpdateTube(string val, string serial)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Table_Generic", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@tube", SqlDbType.VarChar).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void UpdateVoltTest(string serial, bool? val)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Gen_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@volt", SqlDbType.Bit).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void UpdateSDITest(string serial, bool? val)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Gen_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@sdi", SqlDbType.Bit).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void UpdateLEDTest(string serial, bool? val)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Gen_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@led", SqlDbType.Bit).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void RemoveNPM(string serial)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Remove_NPM", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = serial;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void UpdateAllTests(string serial, bool? volt, bool? sdev, bool? temp, bool? led, bool? pulsesim, bool? sdi)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Gen_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@doAll", SqlDbType.VarChar).Value = true;
                    cmd.Parameters.Add("@pulse", SqlDbType.Bit).Value = pulsesim;
                    cmd.Parameters.Add("@led", SqlDbType.Bit).Value = led;
                    cmd.Parameters.Add("@volt", SqlDbType.Bit).Value = volt;
                    cmd.Parameters.Add("@sdev", SqlDbType.Bit).Value = sdev;
                    cmd.Parameters.Add("@temp", SqlDbType.Bit).Value = temp;
                    cmd.Parameters.Add("@sdi", SqlDbType.Bit).Value = sdi;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void UpdatePSTest(string serial, bool? val)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Gen_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@pulse", SqlDbType.Bit).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void UpdateSdevTest(string serial, bool? val)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Gen_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@sdev", SqlDbType.Bit).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        internal static void UpdateTempTest(string serial, bool? val)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Alter_Gen_Tests", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = serial;
                    cmd.Parameters.Add("@temp", SqlDbType.Bit).Value = val;

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        // Not final version of this, just to get the tests going
        internal static void AddHeaterData(string sn, DateTime time, double voltage, int npmTemp, int cs215Temp, string PSCommaDelim, string SDEVCommaDelim)
        {
            // build query
            string date = time.ToString("yyyy-MM-dd");
            date += " " + time.ToString("HH:mm:ss");
            string query = $"INSERT INTO Heat_Testing VALUES('{sn}', '{date}',{voltage},{npmTemp},{cs215Temp}";
            foreach (string psB in PSCommaDelim.Split(','))
            {
                query += $",{psB}";
            }
            foreach (string sdevB in SDEVCommaDelim.Split(','))
            {
                query += $",{sdevB}";
            }
            query += ");";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}
