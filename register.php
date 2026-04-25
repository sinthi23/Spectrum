<?php
$method = $_SERVER['REQUEST_METHOD'] ?? 'GET';

function clean_value(string $value): string
{
    return htmlspecialchars(trim($value), ENT_QUOTES, 'UTF-8');
}

if ($method !== 'POST') {
    http_response_code(405);
    ?>
<!doctype html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registration Method Not Allowed</title>
    <style>
      body {
        margin: 0;
        font-family: "Manrope", sans-serif;
        color: #0a2336;
        background: linear-gradient(140deg, #edf7ff, #fff4e7);
      }
      .box {
        width: min(760px, 92%);
        margin: 4rem auto;
        background: #ffffff;
        border: 1px solid #d7e8f5;
        border-radius: 16px;
        padding: 1.5rem;
        box-shadow: 0 16px 30px rgba(8, 44, 66, 0.14);
      }
      h1 {
        margin-top: 0;
        font-family: "Sora", sans-serif;
      }
      a {
        color: #0f74aa;
        font-weight: 700;
      }
    </style>
  </head>
  <body>
    <main class="box">
      <h1>Use POST to Submit Registration</h1>
      <p>
        This page processes form data through HTTP POST only. Please submit a
        registration form from the website.
      </p>
      <p><strong>Detected Method:</strong> <?php echo clean_value($method); ?></p>
      <p><a href="spectrum.html#upcoming">Return to Upcoming Events</a></p>
    </main>
  </body>
</html>
<?php
    exit;
}

$eventTitle = clean_value($_POST['event_title'] ?? 'Unknown Event');
$eventFormat = clean_value($_POST['event_format'] ?? 'Intra-university');
$fullName = clean_value($_POST['full_name'] ?? '');
$email = clean_value($_POST['email'] ?? '');
$department = clean_value($_POST['department'] ?? '');
$academicYear = clean_value($_POST['academic_year'] ?? '');
$motivation = clean_value($_POST['motivation'] ?? '');
$institutionName = clean_value($_POST['institution_name'] ?? '');
$paymentMethod = clean_value($_POST['payment_method'] ?? '');
$paymentReference = clean_value($_POST['payment_reference'] ?? '');

$errors = [];

if ($fullName === '') {
    $errors[] = 'Full Name is required.';
}
if ($email === '') {
    $errors[] = 'Email is required.';
}
if ($department === '') {
    $errors[] = 'Department is required.';
}
if ($academicYear === '') {
    $errors[] = 'Academic Year is required.';
}
if ($motivation === '') {
    $errors[] = 'Motivation is required.';
}
if ($paymentMethod === '') {
  $errors[] = 'Payment Method is required.';
}
if ($eventFormat === 'Inter-university' && $institutionName === '') {
  $errors[] = 'Institution Name is required for inter-university events.';
}
if ($paymentMethod !== '' && $paymentMethod !== 'Cash (On Campus)' && strlen($paymentReference) < 6) {
  $errors[] = 'A valid Payment Transaction ID is required for digital payment methods.';
}

$registrationId = 'SPC-' . strtoupper(bin2hex(random_bytes(3)));
$submittedAt = date('Y-m-d H:i:s');
?>
<!doctype html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registration Confirmation</title>
    <style>
      body {
        margin: 0;
        font-family: "Manrope", sans-serif;
        color: #0a2336;
        background:
          radial-gradient(circle at 8% 10%, #d9f1ff 0, transparent 28%),
          radial-gradient(circle at 92% 18%, #ffe6bf 0, transparent 31%),
          linear-gradient(140deg, #eef8ff, #fff7ec);
      }
      .wrap {
        width: min(820px, 92%);
        margin: 2.4rem auto;
        background: #ffffff;
        border-radius: 18px;
        border: 1px solid #d5e8f4;
        padding: 1.4rem;
        box-shadow: 0 18px 34px rgba(8, 48, 74, 0.14);
      }
      h1 {
        margin: 0 0 0.8rem;
        font-family: "Sora", sans-serif;
      }
      .meta {
        background: #f1f9ff;
        border: 1px solid #cfe7f6;
        border-radius: 12px;
        padding: 0.8rem;
        margin-bottom: 1rem;
      }
      .meta p {
        margin: 0.28rem 0;
      }
      .error-list {
        background: #fff3f2;
        border: 1px solid #f8cfca;
        border-radius: 12px;
        padding: 0.8rem 1rem;
      }
      dl {
        display: grid;
        grid-template-columns: minmax(165px, auto) 1fr;
        gap: 0.4rem 0.9rem;
      }
      dt {
        font-weight: 800;
        color: #0e4162;
      }
      dd {
        margin: 0;
        color: #284a60;
      }
      .motivation {
        white-space: pre-wrap;
      }
      .actions {
        margin-top: 1.2rem;
      }
      .actions a {
        text-decoration: none;
        color: #ffffff;
        background: linear-gradient(135deg, #ff7f50, #ff9b5d);
        border-radius: 10px;
        padding: 0.62rem 0.9rem;
        font-weight: 800;
        display: inline-block;
      }
      @media (max-width: 640px) {
        dl {
          grid-template-columns: 1fr;
        }
      }
    </style>
  </head>
  <body>
    <main class="wrap">
      <h1>Registration Confirmation</h1>
      <div class="meta">
        <p><strong>HTTP Method:</strong> <?php echo clean_value($method); ?></p>
        <p><strong>Registration ID:</strong> <?php echo $registrationId; ?></p>
        <p><strong>Submitted At:</strong> <?php echo clean_value($submittedAt); ?></p>
      </div>

      <?php if (count($errors) > 0) { ?>
      <div class="error-list">
        <h2>Registration Failed</h2>
        <ul>
          <?php foreach ($errors as $error) { ?>
          <li><?php echo clean_value($error); ?></li>
          <?php } ?>
        </ul>
      </div>
      <?php } else { ?>
      <p>Your registration has been received successfully with the details below:</p>
      <dl>
        <dt>Event</dt>
        <dd><?php echo $eventTitle; ?></dd>
        <dt>Event Format</dt>
        <dd><?php echo $eventFormat; ?></dd>
        <dt>Full Name</dt>
        <dd><?php echo $fullName; ?></dd>
        <dt>Email</dt>
        <dd><?php echo $email; ?></dd>
        <dt>Institution Name</dt>
        <dd><?php echo ($institutionName !== '') ? $institutionName : 'N/A'; ?></dd>
        <dt>Department</dt>
        <dd><?php echo $department; ?></dd>
        <dt>Academic Year</dt>
        <dd><?php echo $academicYear; ?></dd>
        <dt>Payment Method</dt>
        <dd><?php echo $paymentMethod; ?></dd>
        <dt>Payment Transaction ID</dt>
        <dd><?php echo ($paymentReference !== '') ? $paymentReference : 'Cash payment selected'; ?></dd>
        <dt>Motivation</dt>
        <dd class="motivation"><?php echo $motivation; ?></dd>
      </dl>
      <?php } ?>

      <div class="actions">
        <a href="spectrum.html#upcoming">Back to Upcoming Events</a>
      </div>
    </main>
  </body>
</html>
