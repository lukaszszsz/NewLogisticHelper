// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getValue() {
    var a = document.getElementById("inputId").value
    window.location = "https://www.google.com/maps/search/"+a;
}