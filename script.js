const upcomingCards = document.querySelectorAll(".upcoming-card");
const detailPanels = document.querySelectorAll(".event-detail");

function activateEvent(eventId) {
  upcomingCards.forEach((card) => {

/* Highlight current nav link based on pathname or hash */
(() => {
  const navLinks = document.querySelectorAll('.nav a');
  if (!navLinks.length) return;

  const markActive = () => {
    const currentPath = window.location.pathname.split('/').pop() || 'Default.aspx';
    const currentHash = window.location.hash || '';

    navLinks.forEach((a) => {
      a.removeAttribute('aria-current');
      try {
        const url = new URL(a.getAttribute('href'), window.location.origin);
        const linkPath = url.pathname.split('/').pop() || '';
        const linkHash = url.hash || '';

        if (linkPath.toLowerCase() === currentPath.toLowerCase()) {
          if (linkHash) {
            if (linkHash === currentHash) a.setAttribute('aria-current', 'page');
          } else {
            a.setAttribute('aria-current', 'page');
          }
        }
      } catch (e) {
        // ignore invalid URLs
      }
    });
  };

  window.addEventListener('hashchange', markActive);
  window.addEventListener('popstate', markActive);
  document.addEventListener('DOMContentLoaded', markActive);
})();

/* Mobile nav toggle: open/close slide panel */
(() => {
  const navToggle = document.querySelector('.nav-toggle');
  const nav = document.querySelector('.nav');
  if (!navToggle || !nav) return;
  let prevActive = null;
  let focusables = [];

  const getFocusables = () => {
    const selector = 'a, button, input, select, textarea, [tabindex]:not([tabindex="-1"])';
    return Array.from(nav.querySelectorAll(selector)).filter((el) => !el.hasAttribute('disabled'));
  };

  const setOpen = (isOpen) => {
    document.documentElement.classList.toggle('nav-open', isOpen);
    nav.classList.toggle('open', isOpen);
    nav.setAttribute('aria-hidden', isOpen ? 'false' : 'true');
    navToggle.setAttribute('aria-expanded', isOpen ? 'true' : 'false');

    if (isOpen) {
      prevActive = document.activeElement;
      focusables = getFocusables();
      if (focusables.length) focusables[0].focus();
    } else {
      if (prevActive && typeof prevActive.focus === 'function') prevActive.focus();
      prevActive = null;
      focusables = [];
    }
  };

  navToggle.addEventListener('click', (e) => {
    e.stopPropagation();
    setOpen(!nav.classList.contains('open'));
  });

  document.addEventListener('click', (e) => {
    if (!nav.classList.contains('open')) return;
    if (!e.target.closest('.nav') && !e.target.closest('.nav-toggle')) {
      setOpen(false);
    }
  });

  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
      setOpen(false);
      return;
    }

    if (!nav.classList.contains('open')) return;

    if (e.key === 'Tab') {
      // refresh list in case DOM changed
      focusables = getFocusables();
      if (!focusables.length) return;

      const first = focusables[0];
      const last = focusables[focusables.length - 1];

      if (e.shiftKey) {
        if (document.activeElement === first) {
          e.preventDefault();
          last.focus();
        }
      } else {
        if (document.activeElement === last) {
          e.preventDefault();
          first.focus();
        }
      }
    }
  });
})();
    card.classList.toggle("active", card.dataset.eventId === eventId);
  });

  detailPanels.forEach((panel) => {
    panel.classList.toggle("active", panel.id === eventId);
  });
}

upcomingCards.forEach((card) => {
  card.addEventListener("click", (event) => {
    if (
      event.target.classList.contains("event-open-btn") ||
      !event.target.closest("button")
    ) {
      activateEvent(card.dataset.eventId);
    }
  });
});

document.querySelectorAll(".event-open-btn").forEach((button) => {
  button.addEventListener("click", () => {
    activateEvent(button.dataset.eventId);
  });
});

document.querySelectorAll(".registration-form").forEach((form) => {
  const previewButton = form.querySelector(".preview-btn");
  const submitButton = form.querySelector(".submit-btn");
  const confirmationBox = form.querySelector(".confirmation-box");
  const confirmationList = form.querySelector(".confirmation-list");

  form.dataset.previewReady = "false";

  previewButton.addEventListener("click", () => {
    if (!form.reportValidity()) {
      return;
    }

    const formData = new FormData(form);
    const fields = [
      ["Event", formData.get("event_title")],
      ["Full Name", formData.get("full_name")],
      ["Email", formData.get("email")],
      ["Department", formData.get("department")],
      ["Academic Year", formData.get("academic_year")],
      ["Motivation", formData.get("motivation")],
    ];

    confirmationList.innerHTML = "";

    fields.forEach(([label, value]) => {
      const dt = document.createElement("dt");
      dt.textContent = label;

      const dd = document.createElement("dd");
      dd.textContent = value ? String(value).trim() : "-";

      confirmationList.append(dt, dd);
    });

    confirmationBox.hidden = false;
    submitButton.disabled = false;
    form.dataset.previewReady = "true";
  });

  form.addEventListener("input", () => {
    if (!confirmationBox.hidden) {
      submitButton.disabled = true;
      form.dataset.previewReady = "false";
    }
  });

  form.addEventListener("submit", (event) => {
    if (form.dataset.previewReady !== "true") {
      event.preventDefault();
      alert("Please click 'Preview Input' before confirming registration.");
    }
  });
});

if (upcomingCards.length > 0) {
  activateEvent(upcomingCards[0].dataset.eventId);
}

/* Admin submenu (inline) behavior */
(() => {
  const adminToggle = document.querySelector('.admin-link-toggle');
  const adminSub = document.getElementById('adminSubmenu');
  if (!adminToggle || !adminSub) return;

  const setOpen = (isOpen) => {
    adminToggle.setAttribute('aria-expanded', isOpen ? 'true' : 'false');
    adminSub.hidden = !isOpen;
    if (isOpen) {
      const first = adminSub.querySelector('a');
      if (first) first.focus();
    } else {
      adminToggle.focus();
    }
  };

  adminToggle.addEventListener('click', (e) => {
    e.preventDefault();
    setOpen(adminSub.hidden);
  });

  document.addEventListener('click', (e) => {
    if (!adminSub.hidden && !e.target.closest('.admin-menu')) {
      setOpen(false);
    }
  });

  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') setOpen(false);
    if (e.key === 'Tab' && !adminSub.hidden) {
      // basic focus trap within submenu
      const focusables = Array.from(adminSub.querySelectorAll('a'));
      if (!focusables.length) return;
      const first = focusables[0];
      const last = focusables[focusables.length - 1];
      if (e.shiftKey && document.activeElement === first) {
        e.preventDefault();
        last.focus();
      } else if (!e.shiftKey && document.activeElement === last) {
        e.preventDefault();
        first.focus();
      }
    }
  });
})();
