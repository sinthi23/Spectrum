using System;
using System.Collections.Generic;
using System.Web.UI;
using SpectrumWebForms.Data;
using SpectrumWebForms.Models;

namespace SpectrumWebForms
{
    public partial class DefaultPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContent();
            }
        }

        private void BindContent()
        {
            UpcomingEventsRepeater.DataSource = EventCatalog.GetUpcomingEvents();
            UpcomingEventsRepeater.DataBind();

            MembersRepeater.DataSource = GetMembersOrFallback();
            MembersRepeater.DataBind();
        }

        private static IList<ClubMember> GetMembersOrFallback()
        {
            try
            {
                return ClubMemberRepository.GetActiveMembers();
            }
            catch
            {
                return new List<ClubMember>
                {
                    new ClubMember
                    {
                        FullName = "Arafat Rahman",
                        Position = "President",
                        PhotoUrl = "https://images.unsplash.com/photo-1504593811423-6dd665756598?auto=format&fit=crop&w=500&q=80"
                    },
                    new ClubMember
                    {
                        FullName = "Nusaiba Karim",
                        Position = "General Secretary",
                        PhotoUrl = "https://images.unsplash.com/photo-1544717305-2782549b5136?auto=format&fit=crop&w=500&q=80"
                    },
                    new ClubMember
                    {
                        FullName = "Tamim Hossain",
                        Position = "Creative Lead",
                        PhotoUrl = "https://images.unsplash.com/photo-1463453091185-61582044d556?auto=format&fit=crop&w=500&q=80"
                    }
                };
            }
        }
    }
}