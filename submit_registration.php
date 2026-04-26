<?php
if (($_SERVER['REQUEST_METHOD'] ?? 'GET') !== 'POST') {
    header('Location: spectrum.html#upcoming');
    exit;
}

function esc(string $text): string
{
    return htmlspecialchars(trim($text), ENT_QUOTES, 'UTF-8');
}

$eventId = esc($_POST['event_id'] ?? 'ignite');
$eventTitle = esc($_POST['event_title'] ?? 'Unknown Event');
$fullName = esc($_POST['full_name'] ?? '');
$email = esc($_POST['email'] ?? '');
$department = esc($_POST['department'] ?? '');
$academicYear = esc($_POST['academic_year'] ?? '');
$motivation = esc($_POST['motivation'] ?? '');

$photoMap = [
    'ignite' => 'https://images.unsplash.com/photo-1522202176988-66273c2fd55f?auto=format&fit=crop&w=1400&q=80',
    'quantum' => 'https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=1400&q=80',
    'atlas' => 'https://images.unsplash.com/photo-1515169067868-5387ec356754?auto=format&fit=crop&w=1400&q=80'
];

$successImage = $photoMap[$eventId] ?? $photoMap['ignite'];
?>
<!doctype html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registration Submitted</title>
    <link
      href="https://fonts.googleapis.com/css2?family=Manrope:wght@400;600;700;800&family=Sora:wght@600;700&display=swap"
      rel="stylesheet"
    />
    <style>
      * { box-sizing: border-box; }
      body {
        margin: 0;
        font-family: "Manrope", sans-serif;
        color: #0a2235;
        background: linear-gradient(140deg, #edf7ff, #fff2e5);
      }
      .wrap {
        width: min(860px, 92%);
        margin: 2rem auto;
      }
      .card {
        background: #fff;
        border: 1px solid #d5e8f5;
        border-radius: 20px;
        padding: 1.2rem;
        box-shadow: 0 16px 32px rgba(12, 52, 79, 0.14);
      }
      h1 {
        font-family: "Sora", sans-serif;
        margin-top: 0;
      }
      dl {
        display: grid;
        grid-template-columns: minmax(145px, auto) 1fr;
        gap: 0.36rem 0.8rem;
      }
      dt { font-weight: 800; color: #0f4467; }
      dd { margin: 0; color: #2a4a5f; }
      .photo {
        margin-top: 1rem;
        border-radius: 16px;
        overflow: hidden;
        border: 1px solid #d4e7f3;
      }
      .photo img {
        width: 100%;
        height: min(380px, 52vw);
        object-fit: cover;
        display: block;
      }
      .actions {
        margin-top: 1rem;
      }
      .actions a {
        display: inline-block;
        text-decoration: none;
        color: #fff;
        background: linear-gradient(135deg, #ff7f50, #ffa95f);
        border-radius: 10px;
        padding: 0.65rem 0.95rem;
        font-weight: 800;
      }
      @media (max-width: 640px) {
        dl { grid-template-columns: 1fr; }
      }
    </style>
  </head>
  <body>
    <main class="wrap">
      <section class="card">
        <h1>Registration Submitted Successfully</h1>
        <p>Thank you for registering. Your information has been received.</p>

        <dl>
          <dt>Event</dt><dd><?php echo $eventTitle; ?></dd>
          <dt>Full Name</dt><dd><?php echo $fullName; ?></dd>
          <dt>Email</dt><dd><?php echo $email; ?></dd>
          <dt>Department</dt><dd><?php echo $department; ?></dd>
          <dt>Academic Year</dt><dd><?php echo $academicYear; ?></dd>
          <dt>Motivation</dt><dd><?php echo $motivation; ?></dd>
        </dl>

        <div class="photo">
          <img src="<?php echo $successImage; ?>" alt="Celebration image after successful registration" />
        </div>

        <div class="actions">
          <a href="spectrum.html#upcoming">Back to Upcoming Events</a>
        </div>
      </section>
    </main>
  </body>
</html>
