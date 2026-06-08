<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
Inherits="SpectrumWebForms.DefaultPage" MasterPageFile="~/Site.Master" %>
<asp:Content
  ID="TitleContent"
  ContentPlaceHolderID="TitleContent"
  runat="server"
>
  Spectrum KUET Club
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <main class="container">
    <section id="home" class="hero">
      <div class="hero-content">
        <span class="hero-tag">Future Leaders Start Here</span>
        <h2>Build Skills, Spark Innovation, Lead with Purpose</h2>
        <p>
          Spectrum is a Professional Skill Development Club of KUET. SPECTRUM
          provides a guided platform to nurture creativity, innovation, passion
          and efficiency for future leaders.
        </p>
        <div class="hero-actions">
          <a href="#about" class="btn btn-primary">Explore Spectrum</a>
          <a href="#events" class="btn btn-ghost">Upcoming Events</a>
        </div>
      </div>
      <div class="hero-stats">
        <article class="stat-card">
          <i class="fa-solid fa-users"></i>
          <h3>350+</h3>
          <p>Active Members</p>
        </article>
        <article class="stat-card">
          <i class="fa-solid fa-calendar-check"></i>
          <h3>48</h3>
          <p>Events Conducted</p>
        </article>
        <article class="stat-card">
          <i class="fa-solid fa-user-graduate"></i>
          <h3>120+</h3>
          <p>Successful Alumni</p>
        </article>
      </div>
    </section>

    <section id="about" class="card scroll-animate">
      <h3><i class="fa-solid fa-circle-info"></i> About Spectrum</h3>
      <p class="section-intro">
        Spectrum KUET is a long-term skill development platform where students
        build professional confidence through practical events, team-based
        execution, and mentorship. We focus on communication, leadership, design
        thinking, technical growth, and career readiness so members can perform
        effectively in campus leadership, internships, and industry projects.
      </p>

      <div class="feature-grid">
        <article>
          <i class="fa-solid fa-lightbulb"></i>
          <h4>Creative Thinking</h4>
          <p>
            Design challenges and innovation labs to solve meaningful problems.
          </p>
        </article>
        <article>
          <i class="fa-solid fa-laptop-code"></i>
          <h4>Technical Mastery</h4>
          <p>
            Hands-on skill tracks in coding, design systems, and project
            delivery.
          </p>
        </article>
        <article>
          <i class="fa-solid fa-people-group"></i>
          <h4>Leadership Growth</h4>
          <p>
            Team-driven execution that builds confidence, communication, and
            vision.
          </p>
        </article>
        <article>
          <i class="fa-solid fa-hand-holding-dollar"></i>
          <h4>Structured Event Funding</h4>
          <p>
            Paid registrations help us provide better logistics, speakers, and
            learning materials for every participant.
          </p>
        </article>
      </div>

      <!-- Advanced Statistics Section -->
      <div class="counter-section" style="margin-top: 2rem">
        <div class="counter-card">
          <div class="counter-number">350+</div>
          <div class="counter-label">Active Members</div>
        </div>
        <div class="counter-card">
          <div class="counter-number">48</div>
          <div class="counter-label">Events Conducted</div>
        </div>
        <div class="counter-card">
          <div class="counter-number">120+</div>
          <div class="counter-label">Success Stories</div>
        </div>
        <div class="counter-card">
          <div class="counter-number">15</div>
          <div class="counter-label">Years of Excellence</div>
        </div>
      </div>
    </section>

    <!-- Event card hover effect-->

    <section id="events" class="card">
      <h3><i class="fa-solid fa-calendar-days"></i> Featured Events</h3>
      <div class="grid-3">
        <a
          href="Event.aspx?event=innovation2025"
          class="info-card"
          style="
            text-decoration: none;
            color: inherit;
            cursor: pointer;
            transition:
              transform 0.3s ease,
              box-shadow 0.3s ease;
          "
        >
          <img
            src="https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=900&q=80"
            alt="Students at innovation workshop"
          />
          <h4>Innovation Sprint 2025</h4>
          <p>3-day ideation and prototype challenge from past event.</p>
        </a>
        <a
          href="Event.aspx?event=leadership2025"
          class="info-card"
          style="
            text-decoration: none;
            color: inherit;
            cursor: pointer;
            transition:
              transform 0.3s ease,
              box-shadow 0.3s ease;
          "
        >
          <img
            src="https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=900&q=80"
            alt="Audience listening to seminar"
          />
          <h4>Leadership Bootcamp 2025</h4>
          <p>Interactive sessions on teamwork, communication, and execution.</p>
        </a>
        <a
          href="Event.aspx?event=atlas"
          class="info-card"
          style="
            text-decoration: none;
            color: inherit;
            cursor: pointer;
            transition:
              transform 0.3s ease,
              box-shadow 0.3s ease;
          "
        >
          <img
            src="https://images.unsplash.com/photo-1531482615713-2afd69097998?auto=format&fit=crop&w=900&q=80"
            alt="Students collaborating on laptops"
          />
          <h4>Atlas Career Launchpad</h4>
          <p>Portfolio reviews, mock interviews, and mentorship from alumni.</p>
        </a>
      </div>
    </section>

    <section id="upcoming" class="card">
      <h3><i class="fa-solid fa-bullhorn"></i> Upcoming Events</h3>
      <p class="section-intro">
        Click View Details to open a dedicated event page with full description
        and registration access.
      </p>

      <div class="upcoming-grid" aria-label="Upcoming Event List">
        <asp:Repeater ID="UpcomingEventsRepeater" runat="server">
          <ItemTemplate>
            <article class="upcoming-card">
              <span class="upcoming-chip"><%# Eval("Date") %></span>
              <h4><%# Eval("Title") %></h4>
              <p><%# Eval("Summary") %></p>
              <a
                class="btn btn-primary event-open-link"
                href='Event.aspx?event=<%# Eval("Slug") %>'
              >
                View Details
              </a>
            </article>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </section>

    <section id="members" class="card">
      <h3><i class="fa-solid fa-medal"></i> Core Members</h3>
      <div class="grid-3 people-grid">
        <asp:Repeater ID="MembersRepeater" runat="server">
          <ItemTemplate>
            <article class="person-card">
              <img
                src='<%# Eval("PhotoUrl") %>'
                alt='Portrait of <%# Eval("FullName") %>'
              />
              <h4><%# Eval("FullName") %></h4>
              <p><%# Eval("Position") %></p>
            </article>
          </ItemTemplate>
        </asp:Repeater>
      </div>
    </section>

    <section id="alumni" class="card scroll-animate">
      <h3>
        <i class="fa-solid fa-user-graduate"></i> Testimonials & Success Stories
      </h3>
      <p class="section-intro">
        Hear from our alumni and members about their journey with Spectrum
      </p>

      <div class="testimonials-grid">
        <article class="testimonial-card">
          <div class="testimonial-quote">"</div>
          <div class="testimonial-header">
            <div class="testimonial-avatar">SA</div>
            <div class="testimonial-info">
              <h4>Shafin Ahmed</h4>
              <p class="testimonial-role">Software Engineer, Grameenphone IT</p>
            </div>
          </div>
          <div class="testimonial-rating">★★★★★</div>
          <p class="testimonial-text">
            "Spectrum transformed my career trajectory. The leadership bootcamp
            and technical workshops gave me skills that were directly applicable
            in my professional roles."
          </p>
        </article>

        <article class="testimonial-card">
          <div class="testimonial-quote">"</div>
          <div class="testimonial-header">
            <div class="testimonial-avatar">TN</div>
            <div class="testimonial-info">
              <h4>Tasnia Noor</h4>
              <p class="testimonial-role">Product Designer, StartUp Dhaka</p>
            </div>
          </div>
          <div class="testimonial-rating">★★★★★</div>
          <p class="testimonial-text">
            "The innovation sprint taught me how to think outside the box and
            execute ideas under constraints. It was the best learning experience
            during my university years."
          </p>
        </article>

        <article class="testimonial-card">
          <div class="testimonial-quote">"</div>
          <div class="testimonial-header">
            <div class="testimonial-avatar">MC</div>
            <div class="testimonial-info">
              <h4>Mahir Chowdhury</h4>
              <p class="testimonial-role">Research Assistant, KUET</p>
            </div>
          </div>
          <div class="testimonial-rating">★★★★★</div>
          <p class="testimonial-text">
            "Being part of Spectrum's core team shaped my academic and
            professional growth. The mentorship I received continues to guide my
            career decisions."
          </p>
        </article>
      </div>
    </section>

    <section class="card scroll-animate">
      <h3><i class="fa-solid fa-sparkles"></i> Why Join Spectrum?</h3>
      <div class="spotlight-container">
        <div class="spotlight-content">
          <h3>Transform Your Future with Spectrum</h3>
          <p>
            Spectrum isn't just a club—it's your launchpad to professional
            excellence. We believe in nurturing well-rounded leaders and
            innovators.
          </p>
          <div class="spotlight-features">
            <div class="spotlight-feature">
              <i class="fa-solid fa-check-circle"></i>
              <span>Expert-Led Workshops & Training</span>
            </div>
            <div class="spotlight-feature">
              <i class="fa-solid fa-check-circle"></i>
              <span>Networking with Industry Professionals</span>
            </div>
            <div class="spotlight-feature">
              <i class="fa-solid fa-check-circle"></i>
              <span>Practical Project Execution</span>
            </div>
            <div class="spotlight-feature">
              <i class="fa-solid fa-check-circle"></i>
              <span>Career Mentorship & Guidance</span>
            </div>
            <div class="spotlight-feature">
              <i class="fa-solid fa-check-circle"></i>
              <span>Leadership Development Program</span>
            </div>
            <div class="spotlight-feature">
              <i class="fa-solid fa-check-circle"></i>
              <span>Community & Team Spirit</span>
            </div>
          </div>
          <div style="margin-top: 1.5rem">
            <a href="Registration.aspx" class="btn btn-primary"
              >Join Spectrum Today</a
            >
          </div>
        </div>
        <div class="spotlight-image">
          <img
            src="https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=800&q=80"
            alt="Spectrum community members collaborating"
          />
          <span class="spotlight-badge">Best Club 2025</span>
        </div>
      </div>
    </section>

    <!-- Call to Action -->
    <section class="cta-box scroll-animate">
      <div class="cta-content">
        <h3>Ready to Join a Community of Leaders?</h3>
        <p>
          Be part of Spectrum and unlock your potential. Whether you're
          interested in technical skills, leadership development, or
          innovation—we have a place for you.
        </p>
        <div
          style="
            display: flex;
            gap: 0.8rem;
            justify-content: center;
            flex-wrap: wrap;
            margin-top: 1.5rem;
          "
        >
          <a href="Registration.aspx" class="btn btn-primary">Register Now</a>
          <a href="#about" class="btn btn-secondary">Learn More</a>
        </div>
      </div>
    </section>

    <section id="alumni-list" class="card scroll-animate">
      <h3><i class="fa-solid fa-user-graduate"></i> Notable Alumni</h3>
      <div class="grid-3 people-grid">
        <article class="person-card">
          <img
            src="https://images.unsplash.com/photo-1599566150163-29194dcaad36?auto=format&fit=crop&w=500&q=80"
            alt="Portrait of Shafin Ahmed"
          />
          <h4>Shafin Ahmed</h4>
          <p>Software Engineer, Grameenphone IT</p>
        </article>
        <article class="person-card">
          <img
            src="https://images.unsplash.com/photo-1544005313-94ddf0286df2?auto=format&fit=crop&w=500&q=80"
            alt="Portrait of Tasnia Noor"
          />
          <h4>Tasnia Noor</h4>
          <p>Product Designer, StartUp Dhaka</p>
        </article>
        <article class="person-card">
          <img
            src="https://images.unsplash.com/photo-1500648767791-00dcc994a43e?auto=format&fit=crop&w=500&q=80"
            alt="Portrait of Mahir Chowdhury"
          />
          <h4>Mahir Chowdhury</h4>
          <p>Research Assistant, KUET Robotics Lab</p>
        </article>
      </div>
    </section>

    <section class="card">
      <h3><i class="fa-solid fa-envelope-open-text"></i> Contact</h3>
      <p>Email: spectrum@kuet.ac.bd</p>
      <p>Campus: KUET, Khulna, Bangladesh</p>
    </section>
  </main>
</asp:Content>
