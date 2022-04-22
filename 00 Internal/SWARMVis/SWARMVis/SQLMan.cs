using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWARMVis
{
    class SQLMan
    {
        private string connectionString = @"Data Source=tcp:DESKTOP-FM862QL,1433;Initial Catalog=SWARM;User ID=admin;Password=qi2008";
        public bool CheckServer()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        internal void RefreshDevs(
            int deviceType, 
            int deviceId, 
            string deviceName, 
            string comments,
            DateTime hiveCreationTime, 
            DateTime hiveLastheardTime,
            string firmwareVersion, 
            string hardwareVersion,
            int lastTelemetryReportPacketId, 
            int lastHeardByDeviceType, 
            int lastHeardByDeviceId,
            int counter,
            int dayofyear,
            int lastHeardCounter,
            int lastHeardDayofyear,
            int lastHeardByGroundstationId,
            int status, 
            int twoWayEnabled, 
            int dataEncryptionEnabled)
        {

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("InsertNewDevice", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })

            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("deviceType",deviceType));
                command.Parameters.Add(new SqlParameter("deviceId", deviceId));
                command.Parameters.Add(new SqlParameter("deviceName", deviceName));
                command.Parameters.Add(new SqlParameter("comments", comments));
                command.Parameters.Add(new SqlParameter("hiveCreationTime", hiveCreationTime));
                command.Parameters.Add(new SqlParameter("hiveLastheardTime", hiveLastheardTime));
                command.Parameters.Add(new SqlParameter("firmwareVersion", firmwareVersion));
                command.Parameters.Add(new SqlParameter("hardwareVersion", hardwareVersion));
                command.Parameters.Add(new SqlParameter("lastTelemetryReportPacketId", lastTelemetryReportPacketId));
                command.Parameters.Add(new SqlParameter("lastHeardByDeviceType", lastHeardByDeviceType));
                command.Parameters.Add(new SqlParameter("lastHeardByDeviceId", lastHeardByDeviceId));
                command.Parameters.Add(new SqlParameter("counter", counter));
                command.Parameters.Add(new SqlParameter("dayofyear", dayofyear));
                command.Parameters.Add(new SqlParameter("lastHeardCounter", lastHeardCounter));
                command.Parameters.Add(new SqlParameter("lastHeardDayofyear", lastHeardDayofyear));
                command.Parameters.Add(new SqlParameter("lastHeardByGroundstationId", lastHeardByGroundstationId));
                command.Parameters.Add(new SqlParameter("status", status));
                command.Parameters.Add(new SqlParameter("twoWayEnabled", twoWayEnabled));
                command.Parameters.Add(new SqlParameter("dataEncryptionEnabled", dataEncryptionEnabled));
                command.ExecuteNonQuery();
            }
        }

        internal void RefreshMsgs(
            int packetId, 
            int deviceType,
            int deviceId,
            int viaDeviceId, 
            int dataType,
            int userApplicationId, 
            int organizationId,
            int len, 
            int ackPacketId, 
            int status,
            DateTime hiveRxTime,
            byte[] data)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("InsertNewMessage", conn)
            {
                CommandType = CommandType.StoredProcedure
            })

            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("packetId", packetId));
                command.Parameters.Add(new SqlParameter("deviceType", deviceType));
                command.Parameters.Add(new SqlParameter("deviceId", deviceId));
                command.Parameters.Add(new SqlParameter("viaDeviceId", viaDeviceId));
                command.Parameters.Add(new SqlParameter("dataType", dataType));
                command.Parameters.Add(new SqlParameter("userApplicationId", userApplicationId));
                command.Parameters.Add(new SqlParameter("organizationId", organizationId));
                command.Parameters.Add(new SqlParameter("len", len));
                command.Parameters.Add(new SqlParameter("ackPacketId", ackPacketId));
                command.Parameters.Add(new SqlParameter("hiverxtime", hiveRxTime));
                command.Parameters.Add(new SqlParameter("status", status));
                command.Parameters.Add(new SqlParameter("data", data));

                command.ExecuteNonQuery();
            }
        }
    }
}
