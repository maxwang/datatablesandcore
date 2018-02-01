$(function () {
    $('#datatable-inovices tfoot th').each(function () {
        $(this).html('<input type="text" />');
    });

    var oTable = $('#datatable-inovices').DataTable({
        "serverSide": true,
        "ajax": {
            "type": "POST",
            "url": '/Home/ListInvoices',
            "contentType": 'application/json; charset=utf-8',
            "data": function (data) { return data = JSON.stringify(data); }
        },
        "dom": 'frtiS',
        "pageLength": 30,
        //"scrollY": 500,
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": {
            loadingIndicator: false
        },
        
        "processing": true,
        "paging": true,
        "deferRender": true,
        "columns": [
            { "data": "invoice_number" },
            { "data": "customer_name" },
            { "data": "total" },
            { "data": "balance" },
            { "data": "date" }
        ],
        "order": [0, "asc"]

    });

    oTable.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            that
                .search(this.value)
                .draw();
        });
    });

});