/* ------ Global Variables ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var invoiceTable;

/* ------ UI COMPONENTS ------ */
function inputsUISetup() {
    $(".simpleDatePicker").attr("type", "date");
}

/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#ButtonSearchInvoiceMaster").on("click", function (e) {
        e.preventDefault();
        if (validater() && invoiceTable) {
            invoiceTable.ajax.reload(null, false);
        }
    });
}

/* ------ MPO Operation ------ */
function domInvoiceTable() {
    invoiceTable = $('#TableInvoice').DataTable({
        "processing": true,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": true,
        "ajax": {
            "url": window.basePath + "AccountNfinance/AFInvoiceManagement/populateInvoiceMasterListBySearch",
            "data": function (d) {
                d.operationType = operationType;
                d.transactionDate = $("#TextBoxTransactionDate").val();
            },
            "type": "GET",
            "dataSrc": "data",
        },
        "oLanguage": {
            "oPaginate": {
                "sPrevious": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-left"><line x1="19" y1="12" x2="5" y2="12"></line><polyline points="12 19 5 12 12 5"></polyline></svg>',
                "sNext": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>'
            },
            "sInfo": "Showing page _PAGE_ of _PAGES_",
            "sSearch": '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-search"><circle cx="11" cy="11" r="8"></circle><line x1="21" y1="21" x2="16.65" y2="16.65"></line></svg>',
            "sSearchPlaceholder": "Search...",
            "sLengthMenu": "Results :  _MENU_"
        },
        columns: [
            { title: 'Date', data: 'transactionDate' },
            { title: 'Code', data: 'code' },
            { title: 'Customer', data: 'customerName' },
            { title: 'NET AMT', data: 'netAmount' },
            {
                "title": "",
                "data": null,
                "render": function (data, type, row) {
                    return GetInvoiceStatus(data.invoiceStatus)
                }
            },
        ],
    });
}
function getInvoiceDetailByGuID(invoiceGuID) {

}

/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("AFInvoiceReturnForm");
    if (!form.checkValidity()) {
        form.classList.add('was-validated');

        var $firstInvalid = $(form).find(":invalid").first();
        if ($firstInvalid.length) {
            $firstInvalid.trigger("focus");
        }

        toastr.warning("Please fill in all required fields correctly.");
        return false;
    }
    return true;
}

function initialize() {
    const intputMasking = new UIMasking();
    intputMasking.initialize();
    inputsUISetup();
    changeEventHandler();
    $('.select2').select2({
        width: '100%'
    });
    domInvoiceTable();
}

$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});