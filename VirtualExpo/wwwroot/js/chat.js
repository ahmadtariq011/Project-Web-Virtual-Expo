"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("myBtn").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says : " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    li.style.background = "#e5e8e8";
    li.style.padding = "8px";
    li.style.borderRadius = "10px";
    li.style.marginTop = "8px";
    li.style.background = "#e5e8e8";

    //li.appendChild("<span class="material - icons">sms</span >")
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("myBtn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("myBtn").addEventListener("click", function (event) {
    var sender = document.getElementById("senderInput").value;
    var receiver = document.getElementById("receiverInput").value;
    var message = document.getElementById("messageInput").value;

    if (receiver != "") {

        connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err) {
            return false;
        });
    }
    else {
        connection.invoke("SendMessage", sender, message).catch(function (err) {
            return false;
        });
    }


    event.preventDefault();
});