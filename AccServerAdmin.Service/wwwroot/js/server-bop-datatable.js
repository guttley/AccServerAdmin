$(function () {
    var dataTable = $('#bopTable').DataTable({
            "searching": false,
            "paging": false,
            "info": false,
        responsive: {
            details: false
        }
    }
    );

    $(document).on('sidebarChanged', function () {
        dataTable.columns.adjust();
        dataTable.responsive.recalc();
        dataTable.responsive.rebuild();
    });

});

