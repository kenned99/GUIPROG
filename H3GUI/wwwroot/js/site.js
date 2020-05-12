// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(function () {
    var canvas = document.getElementById('canvas');
    var canvasContext = canvas.getContext('2d');
    setInterval(drawcircel(), 500);

    (function () {
    


        function resizeBoard() {
            //var clientWidth = 50;
            //var clientHeight = 50;
            var clientWidth = canvas.parentElement.clientWidth;
            var ratio = canvas.height / canvas.width;
            var clientHeight = clientWidth * ratio;

            var newWidth = String(clientWidth + 'px');
            var newHeight = String(clientHeight + 'px');

            console.log(newWidth, newHeight);

            canvas.style.width = newWidth;
            canvas.style.height = newHeight;

            console.log('canvas: ', canvas.style.width, canvas.height);
            drawcircel()
        }

        window.addEventListener('resize', resizeBoard, false);
    })();
 

    function drawcircel() {
        canvasContext.clearRect(0, 0, canvas.width, canvas.height);

        canvasContext.beginPath();
        var radius = Math.floor($(".container").width() / 20);
        console.log(radius)
        canvasContext.arc(100, 75, radius, 0, 2 * Math.PI);
        canvasContext.stroke();
        canvasContext.beginPath();
        canvasContext.arc(200, 75, 50, 0, 2 * Math.PI);
        canvasContext.stroke();
        console.log("redrew");
    }
setInterval(drawcircel, 10000);
})