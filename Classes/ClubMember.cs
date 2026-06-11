using System;

namespace SpectrumWebForms.Models
{
    public sealed class ClubMember
    {
        public int MemberId { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Bio { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}