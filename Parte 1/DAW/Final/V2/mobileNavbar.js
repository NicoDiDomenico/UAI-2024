"use strict";

var openButton = d.getElementById("open-menu-button");
var closeButton = d.getElementById("close-menu-button");
var menuLinks = d.getElementById("mobile-navbar-links");

openButton.addEventListener("click", () => { /* me parece que si usaste funcionaes anonimas tenes que seguir con esa y no con otra como este caso que se uso una funcion flecha */
  openButton.classList.add("hidden");
  closeButton.classList.remove("hidden");
  menuLinks.classList.remove("hidden");
});

closeButton.addEventListener("click", () => {
  openButton.classList.remove("hidden");
  closeButton.classList.add("hidden");
  menuLinks.classList.add("hidden");
});

