"use strict";

$(function () {

    window.setInterval(function() {

        $.ajax({
            type: 'GET',
            url: '/index?handler=ServerStatus',
            contentType: 'application/json',
            dataType: 'json',
            success: function (result) {
                var stats = document.getElementById("stats");
                stats.innerHTML = 'Cpu: ' + result.cpuUsage + '%';
            },
            error: function (result) {
                alert(error);
            }
            
        });

    }, 1000);

});