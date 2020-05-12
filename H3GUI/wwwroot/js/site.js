// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(function () {
    var canvas = document.getElementById('canvas');
    var canvasContext = canvas.getContext('2d');
    //setInterval(drawcircel(), 500);

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
            drawPosData()
        }

        window.addEventListener('resize', resizeBoard, false);
    })();


    function drawPosData() {

        getPosData();
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

    function getPosData() {
        $.get("/controller/api", function (data, status) {
            console.log(data);

            $.each(data, function (index, value) {
                if (value.lastKnownLocation != null) {
                    console.log(value.lastKnownLocation.latitude)
                    console.log(value.lastKnownLocation.longtitude)
                    lat = Math.floor(value.lastKnownLocation.latitude);
                    long = Math.floor(value.lastKnownLocation.longtitude);

                    canvasContext.beginPath();
                    canvasContext.arc(lat, long, 20, 0, 2 * Math.PI);
                    canvasContext.stroke();
                }
            });
        });
    }
    setInterval(drawPosData, 10000);
    PostGpsLocation();
})

function GetGeoLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            infoWindow.setPosition(pos);
            infoWindow.setContent('You are here');
            infoWindow.open(map);
            map.setCenter(pos);


        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
    PostGpsLocation(pos);
}
//Hvis man ikke acceptere localition
function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
}

function PostGpsLocation(Pos) {
    var userId = $("#MemberIdInput").val();

    console.log(userId);
    $.post("/controller/api", { userId: userId, longitude: Pos.long, latitude: Pos.lat });
}