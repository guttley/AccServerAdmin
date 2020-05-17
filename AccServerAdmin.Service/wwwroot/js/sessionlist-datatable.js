$(function () {
    var dataTable = $('#sessionTable').DataTable({
        "columnDefs": [
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ],
        "order": [[5, "desc"]],
        "searching": false,
        responsive: {
            details: false
        }
    });


    $('#sessionTable').on('click', 'tbody tr', function () {
        var data = dataTable.row(this).data();
        window.location.href = '/Results/Session?sessionId=' + data[1];
    });

});