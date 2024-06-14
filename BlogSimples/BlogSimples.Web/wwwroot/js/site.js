// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const socket = new WebSocket('https://localhost:7080/ws');

socket.onopen = function (event) {
    console.log('WebSocket is open now.');
};

socket.onmessage = function (event) {
    console.log('WebSocket message received:', event.data);
    alert(event.data);
};

socket.onclose = function (event) {
    console.log('WebSocket is closed now.');
};

socket.onerror = function (error) {
    console.log('WebSocket error:', error);
};

