﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var dataReuse;
// Write your Javascript code.
var messegesRunOnce = true;
var getData = true;
var runonce1 = true;
async function GetDataAsynk() {
    responce = await fetch("/controller/api");
    data = await responce.json()
    return data;
}

$(function () {

    if (("canvas").length > 0) {

        var canvas = document.getElementById('canvas');
        var canvasContext = canvas.getContext('2d');

        getPosData();


        function resizeBoard() {

            var clientWidth = canvas.parentElement.clientWidth;
            var ratio = canvas.height / canvas.width;
            var clientHeight = clientWidth * ratio;

            var newWidth = String(clientWidth + 'px');
            var newHeight = String(clientHeight + 'px');

            console.log(newWidth, newHeight);

            canvas.style.width = newWidth;
            canvas.style.height = newHeight;

            console.log('canvas: ', canvas.style.width, canvas.height);
            getPosData()

            window.addEventListener('resize', resizeBoard, false);
        }


        
        async function getPosData() {

            if (getData == true) {
                getData = await GetDataAsynk()
            }
            if (runonce1 == true) {
                setInterval(RunGetscript, 10000);
                setInterval(getLocation, 10000);
                runonce1 = false
            }
            canvasContext.clearRect(0, 0, canvas.width, canvas.height);

            var c = document.getElementById("canvas");
            var ctx = c.getContext("2d");
                

            var clientWidth = canvas.parentElement.clientWidth
            var ratio = canvas.height / canvas.width;
            var clientHeight = clientWidth * ratio;
            var canvasWidth = clientWidth;
            var canvasHeight = canvas.parentElement.clientHeight

            sliderValue = parseInt($('#sliderValue').html())
            userId = parseInt($('#sessionUser').val())
                
            var sessionUser = getData.find(x => x.id == userId);

                
            $.each(getData, function (index, value) {
                locationValue = value.lastKnownLocation




                if (value.lastKnownLocation != null) {

                    x = ((locationValue.longtitude - sessionUser.lastKnownLocation.longtitude) * sliderValue) + (canvas.width / 2);
                    y = ((sessionUser.lastKnownLocation.latitude - locationValue.latitude) * sliderValue) + (canvas.width / 2);

                    if ($('#sessionUser').val() == value.id) {
                           
                        ctx.font = "15px Arial";
                            
                        canvasContext.beginPath();
                        ctx.fillText(value.username, (x), (y)-15);
                        canvasContext.arc((canvas.height / 2), (canvas.width / 2), 5, 0, 2 * Math.PI);

                        canvasContext.fillStyle = 'blue';
                        canvasContext.fill();   
                        canvasContext.lineWidth = 5;
                        canvasContext.stroke();

                    } else {
     
                        if(location)
                        ctx.font = "15px Arial";
                        //lat er vandret og long parameteren er lodret
                        ctx.fillText(value.username, (x), (y)-15);
                        canvasContext.beginPath();
                            
                        canvasContext.arc(x, y , 5, 0, 2 * Math.PI);
                        canvasContext.fillStyle = 'green';
                        canvasContext.fill();
                        canvasContext.lineWidth = 5;
                        canvasContext.stroke();
                        
                        
                    }
                }
                
            });
        }

        //Checks if bar value is changed
        slider.oninput = function () {
            output.innerHTML = this.value;
            getPosData();
        }


        
    }

})

async function updateTable() {


    data = await GetDataAsynk()
    $(".table tr").remove();
    $(".table").append("<tr><th>ID</th><th>Username</th><th>E-mail</th><th>Latitude</th><th>Longitude</th><th>Chat<th/></tr>");
    $.each(data, function (index, value) {
        locationValue = value.lastKnownLocation
        $(".table").append("<tr><td> " + value.id + "</td > <td>" + value.username + "</td><td>" + value.email + "</td><td>" + (locationValue ? locationValue.latitude : "") + "</td><td>" + (locationValue ? locationValue.longtitude : "") + "</td><td><button onClick='showModal(" + value.id + ")' class='btn btn-primary' id='" + value.id + "'>Chat</button><td/></tr > ")
    })

        
}

//bliver kørt når åbner index eller login
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    }
}


function GetSpecificUser() {
    var SessionId = parseInt($("#sessionUser").val());

    $.get("/controller/api/getSpecificUser?Id=" + SessionId, function (data, status) {

        return data;


    })
}

//Finder localition
function showPosition(position) {
    lat = position.coords.latitude;
    lng = position.coords.longitude;
    userId = parseInt($('#sessionUser').val());
    json = JSON.stringify({ "userId": userId, "lat": lat, "lng": lng }); //konventere om til JSON
    if (userId>1 && lat > 0) {
        $.ajax({
            url: "/controller/api/geoloc", //sender det over til api
            type: "POST",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                console.log("upload data");
                // update table here

            }
        })
        
        updateTable(data);
    }
}

//åbner modal og hiver fat i recipient Id
function showModal(recipientId) {
    $('.modal').modal('show')
    $('.recipientId').val(recipientId)
    GetMessages()
}  

function createMessageBox(messages) {
   // var messages = [{ "id": 2, "senderPersonId": 18, "recipientPersonId": 17, "messageText": "asd", "timeSent": "2020-05-14T12:08:30" }, { "id": 1, "senderPersonId": 17, "recipientPersonId": 18, "messageText": "sut mig", "timeSent": "2020-05-14T13:08:30" }, { "id": 3, "senderPersonId": 17, "recipientPersonId": 18, "messageText": "sdf", "timeSent": "2020-05-14T15:08:30" }]
    console.log(messages)
    $('.messageDiv').remove();
    $.each(messages, function (index, value) {
        if (value.senderPersonId == parseInt($("#sessionUser").val())) {
            $('.messageBox').append('<div class="messageDiv" style = "text-align: right">' + value.messageText + '</div>')
        } else {
            $('.messageBox').append('<div class="messageDiv">' + value.messageText + '</div>')
        }

    })
   $('.messages').append()
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
            console.log("opdateret messages")
            createMessageBox(data)

        }
    })
    if (messegesRunOnce) {
        setInterval(GetMessages, 5000);

        messegesRunOnce = false;
    }

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
            GetMessages();
        }
    })
}

$(function () {
    $(".SendButton").click(function () {
        var MessageText = $(".inputTextArea").val();
        SendMessage(MessageText);
    })
})
function RunGetscript() {
    getData = true;

}

