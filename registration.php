<?php
$eventId = $_GET['event'] ?? 'ignite';

$events = [
  'ignite' => ['title' => 'IgniteX Vision Forum', 'bg' => 'https://images.unsplash.com/photo-1498050108023-c5249f4df085?auto=format&fit=crop&w=1400&q=80', 'format' => 'Intra-university', 'eligibility' => 'KUET students only'],
  'quantum' => ['title' => 'Quantum Craft Hacknight', 'bg' => 'https://images.unsplash.com/photo-1461749280684-dccba630e2f6?auto=format&fit=crop&w=1400&q=80', 'format' => 'Inter-university', 'eligibility' => 'Students from KUET and other recognized universities'],
  'atlas' => ['title' => 'Atlas Career Launchpad', 'bg' => 'https://images.unsplash.com/photo-1519389950473-47ba0277781c?auto=format&fit=crop&w=1400&q=80', 'format' => 'Intra-university', 'eligibility' => 'KUET students from all departments']
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
    <title>Registration | <?php echo esc($event['title']); ?></title>
    <link
      href="https://fonts.googleapis.com/css2?family=Manrope:wght@400;600;700;800&family=Sora:wght@600;700&display=swap"
      rel="stylesheet"
    />
    <style>
      * { box-sizing: border-box; }
      body {
        margin: 0;
        font-family: "Manrope", sans-serif;
        color: #09253a;
        min-height: 100vh;
        background:
          linear-gradient(130deg, rgba(236, 247, 255, 0.86), rgba(255, 242, 228, 0.86)),
          url("<?php echo esc($event['bg']); ?>") center / cover fixed no-repeat;
      }
      .wrap {
        width: min(860px, 92%);
        margin: 2rem auto;
      }
      .back {
        display: inline-block;
        text-decoration: none;
        color: #0b3b5d;
        font-weight: 800;
        margin-bottom: 0.8rem;
      }
      .card {
        background: rgba(255, 255, 255, 0.9);
        border: 1px solid #d7e9f7;
        border-radius: 20px;
        padding: 1.2rem;
        box-shadow: 0 16px 32px rgba(12, 52, 79, 0.15);
      }
      h1 {
        margin-top: 0;
        font-family: "Sora", sans-serif;
      }
      .event-title {
        color: #0f628d;
        font-weight: 800;
      }
      .event-meta {
        margin-bottom: 0.9rem;
        color: #1e4f6f;
      }
      .grid {
        display: grid;
        grid-template-columns: repeat(2, minmax(0, 1fr));
        gap: 0.72rem;
      }
      label {
        display: flex;
        flex-direction: column;
        gap: 0.35rem;
        font-weight: 700;
      }
      .full { grid-column: 1 / -1; }
      .hidden { display: none; }
      input, select, textarea {
        border: 1px solid #c7dfef;
        border-radius: 10px;
        padding: 0.58rem 0.64rem;
        font: inherit;
      }
      button {
        margin-top: 0.8rem;
        border: 0;
        cursor: pointer;
        color: #fff;
        background: linear-gradient(135deg, #ff7f50, #ffa95f);
        border-radius: 10px;
        padding: 0.68rem 1rem;
        font-weight: 800;
        transition: opacity 0.2s;
      }
      button:hover:not(:disabled) { opacity: 0.9; }
      button:disabled { opacity: 0.6; cursor: not-allowed; }
      .error-msg {
        color: #d84a4a;
        font-size: 0.9rem;
        margin-top: 0.25rem;
        display: none;
      }
      .error-msg.show { display: block; }
      input.invalid, select.invalid, textarea.invalid {
        border-color: #d84a4a;
        background-color: #fff5f5;
      }
      @media (max-width: 680px) {
        .grid { grid-template-columns: 1fr; }
      }
    </style>
  </head>
  <body>
    <main class="wrap">
      <a class="back" href="event.php?event=<?php echo esc($eventId); ?>">Back to Event Details</a>
      <section class="card">
        <h1>Event Registration Form</h1>
        <p class="event-title"><?php echo esc($event['title']); ?></p>
        <p class="event-meta">
          <strong>Format:</strong> <?php echo esc($event['format']); ?> | 
          <strong>Eligible Students:</strong> <?php echo esc($event['eligibility']); ?>
        </p>

        <form method="POST" action="register.php" id="regForm">
          <input type="hidden" name="event_id" value="<?php echo esc($eventId); ?>" />
          <input type="hidden" name="event_title" value="<?php echo esc($event['title']); ?>" />
          <input type="hidden" name="event_format" value="<?php echo esc($event['format']); ?>" />

          <div class="grid">
            <label>
              Full Name
              <input type="text" name="full_name" id="fullName" required />
              <span class="error-msg" id="err_fullName">Please enter your full name</span>
            </label>
            <label>
              Email
              <input type="email" name="email" id="email" required />
              <span class="error-msg" id="err_email">Please enter a valid email address</span>
            </label>
            <label>
              Department
              <input type="text" name="department" id="department" placeholder="CSE" required />
              <span class="error-msg" id="err_department">Please enter your department</span>
            </label>
            <label>
              Academic Year
              <select name="academic_year" id="academicYear" required>
                <option value="">Select</option>
                <option>1st Year</option>
                <option>2nd Year</option>
                <option>3rd Year</option>
                <option>4th Year</option>
              </select>
              <span class="error-msg" id="err_academicYear">Please select your academic year</span>
            </label>
            <label class="full">
              Why are you interested?
              <textarea name="motivation" id="motivation" rows="4" required></textarea>
              <span class="error-msg" id="err_motivation">Please tell us why you're interested (at least 10 characters)</span>
            </label>
            <label class="full" id="institutionWrap">
              Institution Name
              <input type="text" name="institution_name" id="institutionName" placeholder="KUET or your university name" />
              <span class="error-msg" id="err_institutionName">Institution name is required for this event</span>
            </label>
            <label>
              Payment Method
              <select name="payment_method" id="paymentMethod" required>
                <option value="">Select</option>
                <option>bKash</option>
                <option>Nagad</option>
                <option>Rocket</option>
                <option>Cash (On Campus)</option>
              </select>
              <span class="error-msg" id="err_paymentMethod">Please select a payment method</span>
            </label>
            <label>
              Payment Transaction ID
              <input type="text" name="payment_reference" id="paymentReference" placeholder="Optional for cash payment" />
              <span class="error-msg" id="err_paymentReference">Please provide a valid transaction reference</span>
            </label>
          </div>

          <button type="submit" id="submitBtn">Submit Registration</button>
        </form>

        <script>
          const form = document.getElementById('regForm');
          const submitBtn = document.getElementById('submitBtn');

          function validateForm() {
            const fullName = document.getElementById('fullName').value.trim();
            const email = document.getElementById('email').value.trim();
            const department = document.getElementById('department').value.trim();
            const academicYear = document.getElementById('academicYear').value;
            const motivation = document.getElementById('motivation').value.trim();
            const institutionName = document.getElementById('institutionName').value.trim();
            const paymentMethod = document.getElementById('paymentMethod').value;
            const paymentReference = document.getElementById('paymentReference').value.trim();
            const isInterUniversity = <?php echo json_encode($eventId === 'quantum'); ?>;

            let isValid = true;

            // Validate Full Name
            if (fullName.length < 2) {
              showError('fullName', true);
              isValid = false;
            } else {
              showError('fullName', false);
            }

            // Validate Email
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(email)) {
              showError('email', true);
              isValid = false;
            } else {
              showError('email', false);
            }

            // Validate Department
            if (department.length < 2) {
              showError('department', true);
              isValid = false;
            } else {
              showError('department', false);
            }

            // Validate Academic Year
            if (!academicYear) {
              showError('academicYear', true);
              isValid = false;
            } else {
              showError('academicYear', false);
            }

            // Validate Motivation
            if (motivation.length < 10) {
              showError('motivation', true);
              isValid = false;
            } else {
              showError('motivation', false);
            }

            // Validate Institution for inter-university event
            if (isInterUniversity && institutionName.length < 2) {
              showError('institutionName', true);
              isValid = false;
            } else {
              showError('institutionName', false);
            }

            // Validate Payment Method
            if (!paymentMethod) {
              showError('paymentMethod', true);
              isValid = false;
            } else {
              showError('paymentMethod', false);
            }

            // Validate transaction reference for digital payment methods
            if (paymentMethod && paymentMethod !== 'Cash (On Campus)' && paymentReference.length < 6) {
              showError('paymentReference', true);
              isValid = false;
            } else {
              showError('paymentReference', false);
            }

            return isValid;
          }

          function showError(fieldId, show) {
            const field = document.getElementById(fieldId);
            const error = document.getElementById('err_' + fieldId);
            
            if (show) {
              field.classList.add('invalid');
              error.classList.add('show');
            } else {
              field.classList.remove('invalid');
              error.classList.remove('show');
            }
          }

          // Real-time validation on input
          ['fullName', 'email', 'department', 'academicYear', 'motivation', 'institutionName', 'paymentMethod', 'paymentReference'].forEach(id => {
            const field = document.getElementById(id);
            field.addEventListener('input', () => {
              if (field.classList.contains('invalid')) {
                validateForm();
              }
            });
          });

          const institutionWrap = document.getElementById('institutionWrap');
          const isInterUniversity = <?php echo json_encode($eventId === 'quantum'); ?>;
          if (!isInterUniversity) {
            institutionWrap.classList.add('hidden');
          }

          document.getElementById('paymentMethod').addEventListener('change', (event) => {
            const paymentReference = document.getElementById('paymentReference');
            if (event.target.value === 'Cash (On Campus)') {
              paymentReference.value = '';
              paymentReference.placeholder = 'Not required for cash payment';
            } else {
              paymentReference.placeholder = 'Enter transaction ID';
            }
          });

          // Form submission
          form.addEventListener('submit', (e) => {
            e.preventDefault();
            if (validateForm()) {
              submitBtn.disabled = true;
              submitBtn.textContent = 'Submitting...';
              form.submit();
            }
          });
        </script>
      </section>
    </main>
  </body>
</html>
