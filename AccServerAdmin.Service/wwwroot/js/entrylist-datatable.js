$(function () {
    var dataTable = $('#entriesTable').DataTable({
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

