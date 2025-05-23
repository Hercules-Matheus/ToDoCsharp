// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function initTooltips() {
  var tooltipTriggerList = [].slice.call(
    document.querySelectorAll('[data-bs-toggle="tooltip"]')
  );
  var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl);
  });
}

function initPageTransitions() {
  setTimeout(function () {
    const main = document.getElementById("main");

    main.style.opacity = 1;
    main.style.transform = "scale(1)";
    main.style.transition = "opacity 1s ease, transform 1s ease";
  }, 200);

  setTimeout(function () {
    const table = document.getElementById("table");

    table.style.opacity = 1;
    table.style.transform = "scale(1)";
    table.style.transition = "opacity 1s ease, transform 1s ease";
  }, 700);
}

function toggleBtnAnimations() {
  document.querySelectorAll(".toggle-btn").forEach(function (btn) {
    btn.addEventListener("click", function (e) {
      e.preventDefault();
      const main = document.getElementById("main");
      if (main) {
        main.classList.remove("fade-in"); // remove animação padrão
        main.classList.add("pulse-out");
        setTimeout(function () {
          window.location = btn.href;
        }, 350); // tempo igual ao da animação
      } else {
        window.location = btn.href;
      }
    });
  });
}

document.addEventListener("DOMContentLoaded", function () {
  initTooltips();
  initPageTransitions();
  toggleBtnAnimations();
});
