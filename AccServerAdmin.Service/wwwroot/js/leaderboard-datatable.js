$(function () {
    var dataTable = $('#leaderboardTable').DataTable({
        "columnDefs": [
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ],
        "searching": false,
        "ordering": false,
        "paging": false,
        "info": false,
        responsive: {
            details: false
        }
    });

    $('#leaderboardTable').on('click', 'tbody tr', function () {
        var urlParams = new URLSearchParams(window.location.search);
        var sessionId = urlParams.get("sessionId");
        var data = dataTable.row(this).data();
        window.location.href = '/Results/Laps?sessionId=' + sessionId + '&carId=' + data[1];
    });

});

