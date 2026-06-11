using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SpectrumWebForms.Models;

namespace SpectrumWebForms.Data
{
    internal static class ClubMemberRepository
    {
        public static IList<ClubMember> GetActiveMembers()
        {
            EnsureSeedData();

            var members = new List<ClubMember>();

            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT MemberId, FullName, Position, Department, Email, Phone, Bio, PhotoUrl, IsActive, CreatedAt, UpdatedAt
FROM ClubMembers
WHERE IsActive = 1
ORDER BY MemberId;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        members.Add(MapMember(reader));
                    }
                }
            }

            return members;
        }

        public static IList<ClubMember> GetAll()
        {
            EnsureSeedData();

            var members = new List<ClubMember>();

            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT MemberId, FullName, Position, Department, Email, Phone, Bio, PhotoUrl, IsActive, CreatedAt, UpdatedAt
FROM ClubMembers
ORDER BY MemberId;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        members.Add(MapMember(reader));
                    }
                }
            }

            return members;
        }

        public static void Insert(ClubMember member)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
INSERT INTO ClubMembers
    (FullName, Position, Department, Email, Phone, Bio, PhotoUrl, IsActive)
VALUES
    (@FullName, @Position, @Department, @Email, @Phone, @Bio, @PhotoUrl, @IsActive);";
                AddMemberParameters(command, member);
                command.ExecuteNonQuery();
            }
        }

        public static void Update(ClubMember member)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
UPDATE ClubMembers
SET FullName = @FullName,
    Position = @Position,
    Department = @Department,
    Email = @Email,
    Phone = @Phone,
    Bio = @Bio,
    PhotoUrl = @PhotoUrl,
    IsActive = @IsActive,
    UpdatedAt = SYSDATETIME()
WHERE MemberId = @MemberId;";
                command.Parameters.AddWithValue("@MemberId", member.MemberId);
                AddMemberParameters(command, member);
                command.ExecuteNonQuery();
            }
        }

        public static void Delete(int memberId)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM ClubMembers WHERE MemberId = @MemberId;";
                command.Parameters.AddWithValue("@MemberId", memberId);
                command.ExecuteNonQuery();
            }
        }

        public static ClubMember GetById(int memberId)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT TOP 1 MemberId, FullName, Position, Department, Email, Phone, Bio, PhotoUrl, IsActive, CreatedAt, UpdatedAt
FROM ClubMembers
WHERE MemberId = @MemberId;";
                command.Parameters.AddWithValue("@MemberId", memberId);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return MapMember(reader);
                }
            }
        }

        private static void EnsureSeedData()
        {
            try
            {
                using (var connection = DbGateway.OpenConnection())
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(1) FROM ClubMembers;";
                    var count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        return;
                    }

                    var seedMembers = new[]
                    {
                        new ClubMember
                        {
                            FullName = "Arafat Rahman",
                            Position = "President",
                            Department = "Mechanical Engineering",
                            Email = "arafat@spectrum.kuet",
                            Phone = "+880000000001",
                            Bio = "Leads club strategy, partnerships, and flagship events.",
                            PhotoUrl = "https://images.unsplash.com/photo-1504593811423-6dd665756598?auto=format&fit=crop&w=500&q=80",
                            IsActive = true
                        },
                        new ClubMember
                        {
                            FullName = "Nusaiba Karim",
                            Position = "General Secretary",
                            Department = "Computer Science and Engineering",
                            Email = "nusaiba@spectrum.kuet",
                            Phone = "+880000000002",
                            Bio = "Coordinates communication, planning, and registration operations.",
                            PhotoUrl = "https://images.unsplash.com/photo-1544717305-2782549b5136?auto=format&fit=crop&w=500&q=80",
                            IsActive = true
                        },
                        new ClubMember
                        {
                            FullName = "Tamim Hossain",
                            Position = "Creative Lead",
                            Department = "Industrial Engineering and Management",
                            Email = "tamim@spectrum.kuet",
                            Phone = "+880000000003",
                            Bio = "Drives visual identity, design systems, and event storytelling.",
                            PhotoUrl = "https://images.unsplash.com/photo-1463453091185-61582044d556?auto=format&fit=crop&w=500&q=80",
                            IsActive = true
                        }
                    };

                    foreach (var member in seedMembers)
                    {
                        Insert(member);
                    }
                }
            }
            catch
            {
                // If the database is not available yet, the public site will fall back to static content.
            }
        }

        private static void AddMemberParameters(SqlCommand command, ClubMember member)
        {
            command.Parameters.AddWithValue("@FullName", member.FullName ?? string.Empty);
            command.Parameters.AddWithValue("@Position", member.Position ?? string.Empty);
            command.Parameters.AddWithValue("@Department", string.IsNullOrWhiteSpace(member.Department) ? (object)DBNull.Value : member.Department);
            command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(member.Email) ? (object)DBNull.Value : member.Email);
            command.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(member.Phone) ? (object)DBNull.Value : member.Phone);
            command.Parameters.AddWithValue("@Bio", string.IsNullOrWhiteSpace(member.Bio) ? (object)DBNull.Value : member.Bio);
            command.Parameters.AddWithValue("@PhotoUrl", string.IsNullOrWhiteSpace(member.PhotoUrl) ? (object)DBNull.Value : member.PhotoUrl);
            command.Parameters.AddWithValue("@IsActive", member.IsActive);
        }

        private static ClubMember MapMember(SqlDataReader reader)
        {
            return new ClubMember
            {
                MemberId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Position = reader.GetString(2),
                Department = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                Email = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                Phone = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                Bio = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                PhotoUrl = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                IsActive = reader.GetBoolean(8),
                CreatedAt = reader.GetDateTime(9),
                UpdatedAt = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10)
            };
        }
    }
}