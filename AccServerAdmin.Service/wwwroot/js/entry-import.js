"use strict";

$(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/entryImportHub").build();

    //Disable send button until connection is established
    document.getElementById("startImportBtn").disabled = true;

    connection.on("ImportMessage", function (message) {
        var table = document.getElementById("messages");
        var row = table.insertRow(-1);
        var cell = row.insertCell(0);
        cell.innerHTML = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
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