"use strict";

$(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/entryImportHub").build();

    //Disable send button until connection is established
    document.getElementById("startImportBtn").disabled = true;

    connection.on("ImportMessage", function (message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var option = document.createElement("option");
        option.textContent = msg;
        document.getElementById("messages").appendChild(option);
    });

    connection.start().then(function () {
        document.getElementById("startImportBtn").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    $("#startImportBtn").click(function (e) {
        e.preventDefault();

        this.disabled = true;
        this.innerText = "Started...";

        $.ajax({
            type: "POST",
            url: this.formAction,
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (result) {
                //alert('ok');
            },
            error: function (result) {
                //alert('error');
            }
            
        });
    });
    
});