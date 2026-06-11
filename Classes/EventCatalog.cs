using System;
using System.Collections.Generic;
using System.Linq;
using SpectrumWebForms.Data;

namespace SpectrumWebForms.Models
{
    public sealed class EventInfo
    {
        public int EventId { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public DateTime EventDate { get; set; }

        public string Tagline { get; set; }

        public string Summary { get; set; }

        public string Format { get; set; }

        public string Eligibility { get; set; }

        public string Fee { get; set; }

        public string PaymentNote { get; set; }

        public string[] Guidelines { get; set; }

        public string BackgroundUrl { get; set; }

        public string Venue { get; set; }

        public bool IsUpcoming { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsInterUniversity => string.Equals(Format, "Inter-university", StringComparison.OrdinalIgnoreCase);

        // New properties for detailed event information
        public string Sponsor { get; set; }

        public string Winner { get; set; }

        public string RunnerUpFirst { get; set; }

        public string RunnerUpSecond { get; set; }

        public string HostedYear { get; set; }

        public string DetailedDescription { get; set; }

        public string[] Highlights { get; set; }

        public int? ParticipantCount { get; set; }
    }

    public static class EventCatalog
    {
        private static readonly Dictionary<string, EventInfo> SeedEvents = new Dictionary<string, EventInfo>(StringComparer.OrdinalIgnoreCase)
        {
            ["ignite"] = new EventInfo
            {
                Slug = "ignite",
                Title = "IgniteX Vision Forum",
                Date = "May 10, 2026",
                EventDate = new DateTime(2026, 5, 10),
                Tagline = "Lead with courage and strategy.",
                Summary = "A one-day signature forum with keynote sessions, mini workshops, and practical planning to shape future student leaders.",
                Format = "Intra-university",
                Eligibility = "Only current KUET undergraduate students are eligible.",
                Fee = "BDT 300",
                PaymentNote = "Pay via bKash, Nagad, Rocket, or on-campus cash counter before confirmation.",
                Guidelines = new[]
                {
                    "Only KUET students are eligible for this event.",
                    "Please use an active email address for updates.",
                    "Seats are limited, so early registration is recommended."
                },
                BackgroundUrl = "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=1400&q=80",
                Venue = "KUET Campus",
                IsUpcoming = true,
                IsActive = true,
                Sponsor = "TBD",
                DetailedDescription = "IgniteX Vision Forum is a flagship leadership development event designed to inspire and empower student leaders across KUET. Through interactive keynote sessions, participants engage with successful entrepreneurs and industry leaders who share insights on strategic thinking and courageous decision-making. Mini-workshops cover topics like team management, innovation strategy, and personal branding.",
                Highlights = new[]
                {
                    "Keynote speeches from renowned entrepreneurs",
                    "Interactive workshops on leadership skills",
                    "Networking sessions with industry professionals",
                    "Live case study discussions",
                    "Certificate of participation for all attendees"
                },
                HostedYear = "2026"
            },
            ["innovation2025"] = new EventInfo
            {
                Slug = "innovation2025",
                Title = "Innovation Sprint 2025",
                Date = "March 15, 2025",
                EventDate = new DateTime(2025, 3, 15),
                Tagline = "From idea to impact in 48 hours.",
                Summary = "A 2-day hackathon where students ideate, design, and prototype solutions to real-world problems.",
                Format = "Inter-university",
                Eligibility = "Open to all university students",
                Fee = "BDT 400",
                PaymentNote = "Payment via online or on-site registration",
                Guidelines = new[]
                {
                    "Team size: 2-4 members",
                    "Laptops and development tools required",
                    "Judging based on innovation, feasibility, and presentation"
                },
                BackgroundUrl = "https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=1400&q=80",
                Venue = "Innovation Lab, KUET",
                IsUpcoming = false,
                IsActive = true,
                Sponsor = "Tech Innovation Hub & Microsoft Bangladesh",
                Winner = "Team CodeBreakers - Developed an AI-based educational platform for rural areas",
                RunnerUpFirst = "Team VisionAI - Smart waste management system using IoT sensors",
                RunnerUpSecond = "Team CloudSync - Real-time collaborative learning platform",
                DetailedDescription = "Innovation Sprint 2025 was a landmark event that brought together over 200 students from 15+ universities. Participants worked under high-pressure conditions to develop innovative solutions addressing healthcare, education, and environmental challenges.",
                Highlights = new[]
                {
                    "200+ participants from 15 universities",
                    "BDT 3,00,000 in prize money distributed",
                    "Mentorship from 20+ industry experts",
                    "Opportunity to pitch to venture capitalists",
                    "Media coverage from major tech publications"
                },
                HostedYear = "2025",
                ParticipantCount = 215
            },
            ["leadership2025"] = new EventInfo
            {
                Slug = "leadership2025",
                Title = "Leadership Bootcamp 2025",
                Date = "February 8-10, 2025",
                EventDate = new DateTime(2025, 2, 8),
                Tagline = "Interactive sessions on teamwork, communication, and execution.",
                Summary = "A 3-day intensive bootcamp focused on developing core leadership competencies through interactive sessions and group activities.",
                Format = "Intra-university",
                Eligibility = "KUET students interested in leadership development",
                Fee = "BDT 250",
                PaymentNote = "Online payment or cash at the venue",
                Guidelines = new[]
                {
                    "Active participation in all sessions mandatory",
                    "Group activities and team challenges included",
                    "Bring a notebook and open mind",
                    "Networking dinner on final day"
                },
                BackgroundUrl = "https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=1400&q=80",
                Venue = "Seminar Hall, KUET",
                IsUpcoming = false,
                IsActive = true,
                Sponsor = "KUET Alumni Association & Leadership Institute",
                Winner = "Best Team Leadership Award: Team Phoenix",
                RunnerUpFirst = "Best Individual Leader: Fahim Ahmed (CSE-18)",
                RunnerUpSecond = "Most Improved Award: Tamanna Islam (EEE-19)",
                DetailedDescription = "The Leadership Bootcamp 2025 was attended by 150 KUET students eager to develop their leadership potential. Over three intensive days, participants learned and practiced essential skills including effective communication, team management, conflict resolution, and strategic thinking.",
                Highlights = new[]
                {
                    "150+ participants trained",
                    "Interactive group challenges and simulations",
                    "Mentorship from KUET alumni leaders",
                    "Certificate programs for top performers",
                    "Alumni networking and career guidance sessions"
                },
                HostedYear = "2025",
                ParticipantCount = 150
            },
            ["quantum"] = new EventInfo
            {
                Slug = "quantum",
                Title = "Quantum Craft Hacknight",
                Date = "May 24, 2026",
                EventDate = new DateTime(2026, 5, 24),
                Tagline = "Build bold ideas overnight.",
                Summary = "An intense innovation night where teams brainstorm, design, and prototype impactful campus solutions from concept to demo.",
                Format = "Inter-university",
                Eligibility = "Open to undergraduate students from KUET and other recognized universities.",
                Fee = "BDT 500",
                PaymentNote = "Team registrations are confirmed after per-person payment via mobile banking or campus desk.",
                Guidelines = new[]
                {
                    "You can register individually or as a 2-3 member team.",
                    "Bring your laptop and a valid student ID from your institution.",
                    "Participants should attend from opening to final demo."
                },
                BackgroundUrl = "https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=1400&q=80",
                Venue = "Innovation Lab",
                IsUpcoming = true,
                IsActive = true,
                Sponsor = "TBD",
                DetailedDescription = "Quantum Craft Hacknight is an overnight innovation challenge bringing together brilliant minds from multiple universities. Teams race against time to conceptualize, design, and build working prototypes of solutions to pressing campus and community challenges. The event emphasizes creativity, technical execution, and presentation skills.",
                Highlights = new[]
                {
                    "24-hour non-stop hacking session",
                    "Free food and refreshments throughout the night",
                    "Access to mentors and technical experts",
                    "Prizes for best innovation and execution",
                    "Opportunity to connect with like-minded innovators"
                },
                HostedYear = "2026"
            },
            ["atlas"] = new EventInfo
            {
                Slug = "atlas",
                Title = "Atlas Career Launchpad",
                Date = "June 07, 2026",
                EventDate = new DateTime(2026, 6, 7),
                Tagline = "Prepare, perform, and progress.",
                Summary = "A career growth event with portfolio review clinics, mock interviews, and focused mentorship from alumni and industry professionals.",
                Format = "Intra-university",
                Eligibility = "Open to KUET students from all departments and academic years.",
                Fee = "BDT 250",
                PaymentNote = "Payment can be completed online or at the registration help desk.",
                Guidelines = new[]
                {
                    "Bring your latest CV or portfolio draft.",
                    "Be ready to share one clear career goal.",
                    "Mentoring slots are assigned by registration order."
                },
                BackgroundUrl = "https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=1400&q=80",
                Venue = "Seminar Hall",
                IsUpcoming = true,
                IsActive = true,
                Sponsor = "TBD",
                DetailedDescription = "Atlas Career Launchpad is designed to bridge the gap between academic excellence and career readiness. Students receive personalized guidance from industry professionals and successful alumni, participate in mock interviews, and get detailed portfolio feedback to enhance their competitiveness in the job market.",
                Highlights = new[]
                {
                    "One-on-one portfolio review sessions",
                    "Mock interview practice with HR professionals",
                    "Industry insights and career path discussions",
                    "Resume optimization workshops",
                    "Alumni networking and mentorship opportunities"
                },
                HostedYear = "2026"
            }
        };

        public static EventInfo GetEvent(string eventId)
        {
            try
            {
                var dbEvent = EventRepository.GetBySlug(eventId);
                if (dbEvent != null)
                {
                    return dbEvent;
                }
            }
            catch
            {
            }

            EventInfo fallbackEvent;
            if (!SeedEvents.TryGetValue(eventId ?? string.Empty, out fallbackEvent))
            {
                fallbackEvent = SeedEvents["ignite"];
            }

            return fallbackEvent;
        }

        public static IList<EventInfo> GetUpcomingEvents()
        {
            try
            {
                return EventRepository.GetUpcoming();
            }
            catch
            {
                return SeedEvents.Values
                    .Where(eventInfo => eventInfo.IsActive && eventInfo.IsUpcoming)
                    .OrderBy(eventInfo => eventInfo.EventDate)
                    .Take(3)
                    .ToList();
            }
        }

        public static IEnumerable<EventInfo> GetSeedEvents()
        {
            return SeedEvents.Values;
        }
    }
}