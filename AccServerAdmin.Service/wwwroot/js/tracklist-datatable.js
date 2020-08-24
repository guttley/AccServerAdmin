$(function () {
    var dataTable = $('#trackTable').DataTable({
        "columnDefs": [
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ],
        "searching": false,
        responsive: {
            details: false
        }
    });


    $('#trackTable').on('click', 'tbody tr', function () {
        var data = dataTable.row(this).data();
        window.location.href = '/Results/TrackLaps?track=' + data[1];
    });

});