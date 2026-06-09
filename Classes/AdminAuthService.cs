using System;
using System.Data;
using System.Data.SqlClient;
using SpectrumWebForms.Models;

namespace SpectrumWebForms.Data
{
    internal static class AdminAuthService
    {
        public static AuthResult RegisterAdmin(AdminRegistrationRequest request, string inviteCode)
        {
            if (!string.Equals(inviteCode ?? string.Empty, DbGateway.GetAdminInviteCode(), StringComparison.Ordinal))
            {
                return AuthResult.Fail("The admin invite code is invalid.");
            }

            if (request == null)
            {
                return AuthResult.Fail("Registration request is missing.");
            }

            using (var connection = DbGateway.OpenConnection())
            {
                if (UsernameExists(connection, request.Username))
                {
                    return AuthResult.Fail("That username is already registered.");
                }

                if (EmailExists(connection, request.Email))
                {
                    return AuthResult.Fail("That email is already registered.");
                }

                var salt = PasswordHasher.CreateSalt();
                var passwordHash = PasswordHasher.HashPassword(request.Password, salt);

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
INSERT INTO AdminUsers
    (FullName, Email, Username, PasswordHash, PasswordSalt, DateOfBirth, Phone, IsAdmin, IsActive)
VALUES
    (@FullName, @Email, @Username, @PasswordHash, @PasswordSalt, @DateOfBirth, @Phone, 1, 1);";
                    command.Parameters.AddWithValue("@FullName", request.FullName);
                    command.Parameters.AddWithValue("@Email", request.Email);
                    command.Parameters.AddWithValue("@Username", request.Username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@PasswordSalt", salt);
                    command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = request.DateOfBirth.Date;
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(request.Phone) ? (object)DBNull.Value : request.Phone);
                    command.ExecuteNonQuery();
                }
            }

            return AuthResult.Success("Registration completed. You can log in now.");
        }

        public static AdminUser Authenticate(string username, string password)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT TOP 1 UserId, FullName, Email, Username, PasswordHash, PasswordSalt, DateOfBirth, Phone, IsAdmin, IsActive, CreatedAt
FROM AdminUsers
WHERE Username = @Username;";
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    var user = new AdminUser
                    {
                        UserId = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Email = reader.GetString(2),
                        Username = reader.GetString(3),
                        PasswordHash = reader.GetString(4),
                        PasswordSalt = reader.GetString(5),
                        DateOfBirth = reader.GetDateTime(6),
                        Phone = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                        IsAdmin = reader.GetBoolean(8),
                        IsActive = reader.GetBoolean(9),
                        CreatedAt = reader.GetDateTime(10)
                    };

                    if (!user.IsActive || !user.IsAdmin)
                    {
                        return null;
                    }

                    if (!PasswordHasher.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
                    {
                        return null;
                    }

                    return user;
                }
            }
        }

        private static bool UsernameExists(SqlConnection connection, string username)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(1) FROM AdminUsers WHERE Username = @Username;";
                command.Parameters.AddWithValue("@Username", username);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private static bool EmailExists(SqlConnection connection, string email)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(1) FROM AdminUsers WHERE Email = @Email;";
                command.Parameters.AddWithValue("@Email", email);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }
    }

    internal sealed class AuthResult
    {
        private AuthResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }

        public string Message { get; }

        public static AuthResult Success(string message)
        {
            return new AuthResult(true, message);
        }

        public static AuthResult Fail(string message)
        {
            return new AuthResult(false, message);
        }
    }
}