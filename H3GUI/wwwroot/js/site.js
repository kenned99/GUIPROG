// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(function () {
    if (("canvas").length > 0) {

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

            var radius = Math.floor($(".container").width() / 20);
            console.log(radius)

            console.log("redrew");
        }

        function getPosData() {
            $.get("/controller/api", function (data, status) {
                console.log(data);
                var c = document.getElementById("canvas");
                var ctx = c.getContext("2d");

                $(".table tr").remove();
                $(".table").append("<tr><th>ID</th><th>Username</th><th>E-mail</th><th>Latitude</th><th>Longitude</th><th>Chat<th/></tr>");
                $.each(data, function (index, value) {
                    locationValue = value.lastKnownLocation

                    $(".table").append("<tr><td> " + value.id + "</td > <td>" + value.username + "</td><td>" + value.email + "</td><td>" + (locationValue ? locationValue.latitude : "") + "</td><td>" + (locationValue ? locationValue.longtitude : "") + "</td><td><button onClick='showModal(" + value.id + ")' class='btn btn-primary' id='" + value.id + "'>Chat</button><td/></tr > ")
                    if (value.lastKnownLocation != null) {



                        console.log(value.lastKnownLocation.latitude)
                        console.log(value.lastKnownLocation.longtitude)
                        console.log(value.username);
                        lat = Math.floor(value.lastKnownLocation.latitude);
                        long = Math.floor(value.lastKnownLocation.longtitude);

                        ctx.font = "15px Arial";
                        //lat er vandret og long parametren er lodret
                        ctx.fillText(value.username, (lat - 12), (long + 20));
                        canvasContext.beginPath();
                        canvasContext.arc(lat, long, 5, 0, 2 * Math.PI);
                        canvasContext.fillStyle = 'green';
                        canvasContext.fill();
                        canvasContext.lineWidth = 5;
                        canvasContext.stroke();
                    }
                });
            });
        }
        setInterval(drawPosData, 10000);
        setInterval(getLocation(), 10000);
    }
})

//bliver kørt når åbner index eller login
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.watchPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

//Finder localition
function showPosition(position) {
    lat = position.coords.latitude;
    lng = position.coords.longitude;
    userId = parseInt($('#sessionUser').val());
    json = JSON.stringify({ "userId": userId, "lat": lat, "lng": lng }); //konventere om til JSON
    console.log(json);
    $.ajax({
        url: "/controller/api/geoloc", //sender det over til api
        type: "POST",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            console.log("upload data");
        }
    })
}

//åbner modal og hiver fat i recipient Id
function showModal(recipientId) {
    $('.modal').modal('show')
    $('.recipientId').val(recipientId)
    GetMessages();
}

function createMessageBox() {
    console.log($('.messageBox'))
    $('.messageBox').append('<div>hello</div>')
    var messages = [{ "id": 2, "senderPersonId": 18, "recipientPersonId": 17, "messageText": "asd", "timeSent": "2020-05-14T12:08:30" }, { "id": 1, "senderPersonId": 17, "recipientPersonId": 18, "messageText": "sut mig", "timeSent": "2020-05-14T13:08:30" }, { "id": 3, "senderPersonId": 17, "recipientPersonId": 18, "messageText": "sdf", "timeSent": "2020-05-14T15:08:30" }]

}
function GetMessages() {
    var SessionId = parseInt($("#sessionUser").val());
    var RecipientId = parseInt($(".recipientId").val());

    json = ({"SenderId": SessionId, "RecipientId": RecipientId }); //konventere om til JSON
    console.log(json);
    $.ajax({
        url: "/controller/api/Message", //sender det over til api
        type: "GET",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
        }
    })
}

function SendMessage(MessageText) {
    var SessionId = parseInt($("#sessionUser").val());
    var RecipientId = parseInt($(".recipientId").val());
    //var MessageText = "insert text here"

    json = JSON.stringify({ "SenderId": SessionId, "RecipientId": RecipientId, "MessageText": MessageText }); //konventere om til JSON
    console.log(json);
    $.ajax({
        url: "/controller/api/SendMessage", //sender det over til api
        type: "POST",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
        }
    })
}

$(function () {
    $(".SendButton").click(function () {
        var MessageText = $(".inputTextArea").val();
        SendMessage(MessageText);
    })
})