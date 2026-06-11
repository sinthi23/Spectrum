using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SpectrumWebForms.Data
{
    internal static class DbGateway
    {
        public static SqlConnection OpenConnection()
        {
            var connectionSetting = ConfigurationManager.ConnectionStrings["SpectrumConnection"];

            if (connectionSetting == null || string.IsNullOrWhiteSpace(connectionSetting.ConnectionString))
            {
                throw new InvalidOperationException("Missing SpectrumConnection connection string.");
            }

            DatabaseBootstrap.EnsureInitialized(connectionSetting.ConnectionString);

            var connection = new SqlConnection(connectionSetting.ConnectionString);
            connection.Open();
            return connection;
        }

        public static string GetAdminInviteCode()
        {
            var inviteCode = ConfigurationManager.AppSettings["AdminInviteCode"];
            return string.IsNullOrWhiteSpace(inviteCode) ? "SPECTRUM-ADMIN-2026" : inviteCode;
        }
    }
}