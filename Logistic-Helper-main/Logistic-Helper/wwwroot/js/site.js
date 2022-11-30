// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let mapOptions = {
    center: [51.958, 9.141],
    zoom: 10
}

let map = new L.map('map', mapOptions);

let layer = new L.TileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png');
map.addLayer(layer);

let customIcon = {
    iconUrl: "https://image.flaticon.com/icons/png/512/1397/1397898.png",
    iconSize: [40, 40]
}

let myIcon = L.icon(customIcon);
//let myIcon = L.divIcon();

let iconOptions = {
    title: "company name",
    draggable: true,
    icon: myIcon
}

let marker = new L.Marker([51.958, 9.141], iconOptions);
marker.addTo(map);
marker.bindPopup("content").openPopup();

let popup = L.popup().setLatLng([51.958, 9.797]).setContent("<p>new popup</br> more complicated</p>").openOn(map);
console.log("MAPA: ", map);
document.getElementById("map").innerHTML = map