using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SpectrumWebForms.Models;

namespace SpectrumWebForms.Data
{
    internal static class EventRepository
    {
        public static IList<EventInfo> GetAll()
        {
            EnsureSeedData();

            var events = new List<EventInfo>();

            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT EventId, Slug, Title, Tagline, Summary, EventDate, Format, Eligibility, Fee, PaymentNote, Guidelines, BackgroundUrl, IsUpcoming, IsActive, CreatedAt, UpdatedAt
FROM ClubEvents
ORDER BY IsUpcoming DESC, EventId DESC;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(MapEvent(reader));
                    }
                }
            }

            return events;
        }

        public static IList<EventInfo> GetUpcoming()
        {
            return GetAll()
                .Where(eventInfo => eventInfo.IsActive && eventInfo.IsUpcoming)
                .OrderBy(eventInfo => eventInfo.EventDate)
                .ToList();
        }

        public static EventInfo GetBySlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            EnsureSeedData();

            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT TOP 1 EventId, Slug, Title, Tagline, Summary, EventDate, Format, Eligibility, Fee, PaymentNote, Guidelines, BackgroundUrl, IsUpcoming, IsActive, CreatedAt, UpdatedAt
FROM ClubEvents
WHERE Slug = @Slug AND IsActive = 1;";
                command.Parameters.AddWithValue("@Slug", slug);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapEvent(reader);
                    }
                }
            }

            return null;
        }

        public static EventInfo GetById(int eventId)
        {
            EnsureSeedData();

            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT TOP 1 EventId, Slug, Title, Tagline, Summary, EventDate, Format, Eligibility, Fee, PaymentNote, Guidelines, BackgroundUrl, IsUpcoming, IsActive, CreatedAt, UpdatedAt
FROM ClubEvents
WHERE EventId = @EventId;";
                command.Parameters.AddWithValue("@EventId", eventId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapEvent(reader);
                    }
                }
            }

            return null;
        }

        public static void Insert(EventInfo eventInfo)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
INSERT INTO ClubEvents
    (Slug, Title, Tagline, Summary, EventDate, Format, Eligibility, Fee, PaymentNote, Guidelines, BackgroundUrl, IsUpcoming, IsActive)
VALUES
    (@Slug, @Title, @Tagline, @Summary, @EventDate, @Format, @Eligibility, @Fee, @PaymentNote, @Guidelines, @BackgroundUrl, @IsUpcoming, @IsActive);";
                AddEventParameters(command, eventInfo);
                command.ExecuteNonQuery();
            }
        }

        public static void Update(EventInfo eventInfo)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
UPDATE ClubEvents
SET Slug = @Slug,
    Title = @Title,
    Tagline = @Tagline,
    Summary = @Summary,
    EventDate = @EventDate,
    Format = @Format,
    Eligibility = @Eligibility,
    Fee = @Fee,
    PaymentNote = @PaymentNote,
    Guidelines = @Guidelines,
    BackgroundUrl = @BackgroundUrl,
    IsUpcoming = @IsUpcoming,
    IsActive = @IsActive,
    UpdatedAt = SYSDATETIME()
WHERE EventId = @EventId;";
                command.Parameters.AddWithValue("@EventId", eventInfo.EventId);
                AddEventParameters(command, eventInfo);
                command.ExecuteNonQuery();
            }
        }

        public static void Delete(int eventId)
        {
            using (var connection = DbGateway.OpenConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM ClubEvents WHERE EventId = @EventId;";
                command.Parameters.AddWithValue("@EventId", eventId);
                command.ExecuteNonQuery();
            }
        }

        private static void EnsureSeedData()
        {
            try
            {
                using (var connection = DbGateway.OpenConnection())
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(1) FROM ClubEvents;";
                    var count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        return;
                    }

                    foreach (var seedEvent in EventCatalog.GetSeedEvents())
                    {
                        Insert(seedEvent);
                    }
                }
            }
            catch
            {
                // If the database is not configured yet, the site continues to use the in-memory catalog.
            }
        }

        private static void AddEventParameters(SqlCommand command, EventInfo eventInfo)
        {
            command.Parameters.AddWithValue("@Slug", eventInfo.Slug ?? string.Empty);
            command.Parameters.AddWithValue("@Title", eventInfo.Title ?? string.Empty);
            command.Parameters.AddWithValue("@Tagline", string.IsNullOrWhiteSpace(eventInfo.Tagline) ? (object)DBNull.Value : eventInfo.Tagline);
            command.Parameters.AddWithValue("@Summary", string.IsNullOrWhiteSpace(eventInfo.Summary) ? (object)DBNull.Value : eventInfo.Summary);
            command.Parameters.AddWithValue("@EventDate", eventInfo.Date ?? string.Empty);
            command.Parameters.AddWithValue("@Format", eventInfo.Format ?? string.Empty);
            command.Parameters.AddWithValue("@Eligibility", string.IsNullOrWhiteSpace(eventInfo.Eligibility) ? (object)DBNull.Value : eventInfo.Eligibility);
            command.Parameters.AddWithValue("@Fee", string.IsNullOrWhiteSpace(eventInfo.Fee) ? (object)DBNull.Value : eventInfo.Fee);
            command.Parameters.AddWithValue("@PaymentNote", string.IsNullOrWhiteSpace(eventInfo.PaymentNote) ? (object)DBNull.Value : eventInfo.PaymentNote);
            command.Parameters.AddWithValue("@Guidelines", string.Join("\n", eventInfo.Guidelines ?? Array.Empty<string>()));
            command.Parameters.AddWithValue("@BackgroundUrl", string.IsNullOrWhiteSpace(eventInfo.BackgroundUrl) ? (object)DBNull.Value : eventInfo.BackgroundUrl);
            command.Parameters.AddWithValue("@IsUpcoming", eventInfo.IsUpcoming);
            command.Parameters.AddWithValue("@IsActive", eventInfo.IsActive);
        }

        private static EventInfo MapEvent(SqlDataReader reader)
        {
            DateTime parsedDate;
            var eventDateText = reader.GetString(5);
            if (!DateTime.TryParse(eventDateText, out parsedDate))
            {
                parsedDate = DateTime.Now;
            }

            return new EventInfo
            {
                EventId = reader.GetInt32(0),
                Slug = reader.GetString(1),
                Title = reader.GetString(2),
                Tagline = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                Summary = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                Date = eventDateText,
                EventDate = parsedDate,
                Format = reader.GetString(6),
                Eligibility = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                Fee = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                PaymentNote = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                Guidelines = SplitGuidelines(reader.IsDBNull(10) ? string.Empty : reader.GetString(10)),
                BackgroundUrl = reader.IsDBNull(11) ? string.Empty : reader.GetString(11),
                IsUpcoming = reader.GetBoolean(12),
                IsActive = reader.GetBoolean(13),
                CreatedAt = reader.GetDateTime(14),
                UpdatedAt = reader.IsDBNull(15) ? (DateTime?)null : reader.GetDateTime(15)
            };
        }

        private static string[] SplitGuidelines(string guidelines)
        {
            if (string.IsNullOrWhiteSpace(guidelines))
            {
                return new string[0];
            }

            return guidelines.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}