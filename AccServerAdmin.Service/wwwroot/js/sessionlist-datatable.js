$(function () {
    var dataTable = $('#sessionTable').DataTable({
        "columnDefs": [
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ],
        responsive: {
            details: false
        }
    });


    $('#sessionTable').on('click', 'tbody tr', function () {
        var data = dataTable.row(this).data();
        ///alert('You clicked on ' + data[1] + '\'s row');
        window.location.href = 'Session?sessionId=' + data[1];
    });

});

