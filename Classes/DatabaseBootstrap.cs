using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SpectrumWebForms.Data
{
    internal static class DatabaseBootstrap
    {
        private static bool isInitialized;
        private static readonly object SyncRoot = new object();

        public static void EnsureInitialized(string appConnectionString)
        {
            if (isInitialized)
            {
                return;
            }

            lock (SyncRoot)
            {
                if (isInitialized)
                {
                    return;
                }

                var builder = new SqlConnectionStringBuilder(appConnectionString);
                var databaseName = builder.InitialCatalog;

                if (string.IsNullOrWhiteSpace(databaseName))
                {
                    throw new InvalidOperationException("SpectrumConnection must define Initial Catalog.");
                }

                EnsureDatabaseExists(builder, databaseName);
                EnsureSchemaExists(builder, databaseName);
                isInitialized = true;
            }
        }

        private static void EnsureDatabaseExists(SqlConnectionStringBuilder builder, string databaseName)
        {
            var masterBuilder = new SqlConnectionStringBuilder(builder.ConnectionString)
            {
                InitialCatalog = "master"
            };

            using (var connection = new SqlConnection(masterBuilder.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
IF DB_ID(N'SpectrumWebForms') IS NULL
BEGIN
    CREATE DATABASE [SpectrumWebForms];
END";
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void EnsureSchemaExists(SqlConnectionStringBuilder builder, string databaseName)
        {
            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                Execute(connection, @"
IF OBJECT_ID('dbo.AdminUsers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AdminUsers
    (
        UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FullName NVARCHAR(120) NOT NULL,
        Email NVARCHAR(128) NOT NULL,
        Username NVARCHAR(60) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(256) NOT NULL,
        PasswordSalt NVARCHAR(128) NOT NULL,
        DateOfBirth DATE NOT NULL,
        Phone NVARCHAR(30) NULL,
        IsAdmin BIT NOT NULL CONSTRAINT DF_AdminUsers_IsAdmin DEFAULT (1),
        IsActive BIT NOT NULL CONSTRAINT DF_AdminUsers_IsActive DEFAULT (1),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_AdminUsers_CreatedAt DEFAULT (SYSDATETIME())
    );
END");

                Execute(connection, @"
IF OBJECT_ID('dbo.ClubMembers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ClubMembers
    (
        MemberId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        FullName NVARCHAR(120) NOT NULL,
        Position NVARCHAR(120) NOT NULL,
        Department NVARCHAR(120) NULL,
        Email NVARCHAR(128) NULL,
        Phone NVARCHAR(40) NULL,
        Bio NVARCHAR(MAX) NULL,
        PhotoUrl NVARCHAR(400) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_ClubMembers_IsActive DEFAULT (1),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubMembers_CreatedAt DEFAULT (SYSDATETIME()),
        UpdatedAt DATETIME2 NULL
    );
END");

                Execute(connection, @"
IF OBJECT_ID('dbo.ClubEvents', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ClubEvents
    (
        EventId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Slug NVARCHAR(80) NOT NULL UNIQUE,
        Title NVARCHAR(160) NOT NULL,
        Tagline NVARCHAR(220) NULL,
        Summary NVARCHAR(MAX) NULL,
        EventDate NVARCHAR(40) NOT NULL,
        Format NVARCHAR(60) NOT NULL,
        Eligibility NVARCHAR(240) NULL,
        Fee NVARCHAR(40) NULL,
        PaymentNote NVARCHAR(240) NULL,
        Guidelines NVARCHAR(MAX) NULL,
        BackgroundUrl NVARCHAR(400) NULL,
        Venue NVARCHAR(160) NULL,
        IsUpcoming BIT NOT NULL CONSTRAINT DF_ClubEvents_IsUpcoming DEFAULT (0),
        IsActive BIT NOT NULL CONSTRAINT DF_ClubEvents_IsActive DEFAULT (1),
        CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_ClubEvents_CreatedAt DEFAULT (SYSDATETIME()),
        UpdatedAt DATETIME2 NULL
    );
END");
            }
        }

        private static void Execute(SqlConnection connection, string sql)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
    }
}