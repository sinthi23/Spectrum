<?php
$eventId = $_GET['event'] ?? 'ignite';

$events = [
    'ignite' => [
        'title' => 'IgniteX Vision Forum',
        'date' => 'May 10, 2026',
        'tagline' => 'Lead with courage and strategy.',
        'summary' => 'A one-day signature forum with keynote sessions, mini workshops, and practical planning to shape future student leaders.',
      'format' => 'Intra-university',
      'eligibility' => 'Only current KUET undergraduate students are eligible.',
      'fee' => 'BDT 300',
      'payment_note' => 'Pay via bKash, Nagad, Rocket, or on-campus cash counter before confirmation.',
        'guidelines' => [
            'Only KUET students are eligible for this event.',
            'Please use an active email address for updates.',
            'Seats are limited, so early registration is recommended.'
        ],
        'bg' => 'https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=1400&q=80'
    ],
    'quantum' => [
        'title' => 'Quantum Craft Hacknight',
        'date' => 'May 24, 2026',
        'tagline' => 'Build bold ideas overnight.',
        'summary' => 'An intense innovation night where teams brainstorm, design, and prototype impactful campus solutions from concept to demo.',
      'format' => 'Inter-university',
      'eligibility' => 'Open to undergraduate students from KUET and other recognized universities.',
      'fee' => 'BDT 500',
      'payment_note' => 'Team registrations are confirmed after per-person payment via mobile banking or campus desk.',
        'guidelines' => [
            'You can register individually or as a 2-3 member team.',
        'Bring your laptop and a valid student ID from your institution.',
            'Participants should attend from opening to final demo.'
        ],
        'bg' => 'https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=1400&q=80'
    ],
    'atlas' => [
        'title' => 'Atlas Career Launchpad',
        'date' => 'June 07, 2026',
        'tagline' => 'Prepare, perform, and progress.',
        'summary' => 'A career growth event with portfolio review clinics, mock interviews, and focused mentorship from alumni and industry professionals.',
      'format' => 'Intra-university',
      'eligibility' => 'Open to KUET students from all departments and academic years.',
      'fee' => 'BDT 250',
      'payment_note' => 'Payment can be completed online or at the registration help desk.',
        'guidelines' => [
            'Bring your latest CV or portfolio draft.',
            'Be ready to share one clear career goal.',
            'Mentoring slots are assigned by registration order.'
        ],
        'bg' => 'https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=1400&q=80'
    ]
];

if (!isset($events[$eventId])) {
    $eventId = 'ignite';
}

$event = $events[$eventId];

function esc(string $text): string
{
    return htmlspecialchars($text, ENT_QUOTES, 'UTF-8');
}
?>
<!doctype html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><?php echo esc($event['title']); ?> | Spectrum KUET</title>
    <link
      href="https://fonts.googleapis.com/css2?family=Manrope:wght@400;600;700;800&family=Sora:wght@600;700&display=swap"
      rel="stylesheet"
    />
    <link
      rel="stylesheet"
      href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css"
    />
    <style>
      * { box-sizing: border-box; }
      body {
        margin: 0;
        font-family: "Manrope", sans-serif;
        color: #f4fbff;
        min-height: 100vh;
        background:
          linear-gradient(130deg, rgba(2, 18, 34, 0.8), rgba(5, 51, 82, 0.75)),
          url("<?php echo esc($event['bg']); ?>") center / cover fixed no-repeat;
      }
      .wrap {
        width: min(980px, 92%);
        margin: 2rem auto;
      }
      .home-link {
        display: inline-flex;
        align-items: center;
        gap: 0.45rem;
        text-decoration: none;
        color: #f5fbff;
        background: rgba(255, 255, 255, 0.14);
        border: 1px solid rgba(255, 255, 255, 0.25);
        padding: 0.5rem 0.8rem;
        border-radius: 999px;
        font-weight: 700;
      }
      .panel {
        margin-top: 1rem;
        border-radius: 22px;
        padding: clamp(1.1rem, 2vw, 1.8rem);
        background: rgba(4, 22, 40, 0.62);
        backdrop-filter: blur(8px);
        border: 1px solid rgba(255, 255, 255, 0.2);
        box-shadow: 0 22px 40px rgba(0, 0, 0, 0.28);
      }
      .date-chip {
        display: inline-block;
        background: rgba(255, 203, 132, 0.25);
        border: 1px solid rgba(255, 203, 132, 0.55);
        border-radius: 999px;
        padding: 0.28rem 0.7rem;
        font-weight: 700;
        margin-bottom: 0.8rem;
      }
      h1 {
        margin: 0 0 0.35rem;
        font-family: "Sora", sans-serif;
        font-size: clamp(1.5rem, 1.1rem + 2vw, 2.4rem);
      }
      .tagline {
        margin: 0 0 0.8rem;
        color: #ffd89f;
        font-weight: 700;
      }
      .summary {
        margin-bottom: 1rem;
        max-width: 64ch;
        color: #d6ecff;
      }
      .meta-grid {
        display: grid;
        grid-template-columns: repeat(2, minmax(0, 1fr));
        gap: 0.65rem;
        margin-bottom: 1rem;
      }
      .meta-item {
        background: rgba(255, 255, 255, 0.12);
        border: 1px solid rgba(255, 255, 255, 0.2);
        border-radius: 12px;
        padding: 0.6rem 0.75rem;
      }
      .meta-item strong {
        display: block;
        color: #ffd89f;
        margin-bottom: 0.2rem;
        font-size: 0.9rem;
      }
      h2 {
        font-family: "Sora", sans-serif;
        margin: 1.1rem 0 0.5rem;
        font-size: 1.12rem;
      }
      ul {
        margin: 0;
        padding-left: 1.1rem;
      }
      li { margin-bottom: 0.45rem; color: #e8f5ff; }
      .register-btn {
        margin-top: 1.1rem;
        display: inline-flex;
        align-items: center;
        gap: 0.55rem;
        text-decoration: none;
        color: #fff;
        font-weight: 800;
        background: linear-gradient(135deg, #ff7f50, #ffad62);
        border-radius: 12px;
        padding: 0.72rem 1.05rem;
        box-shadow: 0 12px 24px rgba(255, 127, 80, 0.35);
      }
      @media (max-width: 720px) {
        .meta-grid {
          grid-template-columns: 1fr;
        }
      }
    </style>
  </head>
  <body>
    <main class="wrap">
      <a class="home-link" href="spectrum.html#upcoming">
        <i class="fa-solid fa-arrow-left"></i>
        Back to Upcoming Events
      </a>

      <section class="panel">
        <span class="date-chip"><?php echo esc($event['date']); ?></span>
        <h1><?php echo esc($event['title']); ?></h1>
        <p class="tagline"><?php echo esc($event['tagline']); ?></p>
        <p class="summary"><?php echo esc($event['summary']); ?></p>

        <div class="meta-grid">
          <article class="meta-item">
            <strong>Event Format</strong>
            <span><?php echo esc($event['format']); ?></span>
          </article>
          <article class="meta-item">
            <strong>Eligibility</strong>
            <span><?php echo esc($event['eligibility']); ?></span>
          </article>
          <article class="meta-item">
            <strong>Registration Fee</strong>
            <span><?php echo esc($event['fee']); ?></span>
          </article>
          <article class="meta-item">
            <strong>Payment Guideline</strong>
            <span><?php echo esc($event['payment_note']); ?></span>
          </article>
        </div>

        <h2>Registration Guidelines</h2>
        <ul>
          <?php foreach ($event['guidelines'] as $line) { ?>
          <li><?php echo esc($line); ?></li>
          <?php } ?>
        </ul>

        <a class="register-btn" href="registration.php?event=<?php echo esc($eventId); ?>">
          <i class="fa-solid fa-user-plus"></i>
          Registration
        </a>
      </section>
    </main>
  </body>
</html>
