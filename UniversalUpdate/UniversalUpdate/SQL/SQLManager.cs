using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace UniversalUpdate.SQL
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

    }
}
