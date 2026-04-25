const upcomingCards = document.querySelectorAll(".upcoming-card");
const detailPanels = document.querySelectorAll(".event-detail");

function activateEvent(eventId) {
  upcomingCards.forEach((card) => {
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
