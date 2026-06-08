/* ------ Global Variable ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var brandTable = "";


/* ------ Change Cases DDL's ------ */
function changeEventHandler() {
    $("#ButtonSaveData, #ButtonUpdateData").on("click", function (e) {
        if (validater()) {
            e.preventDefault();
            createUpdateDataIntoDB();
        }
    });
}

/* ------ MPO Operation ------ */
function domBrandTable() {
    brandTable = $('#TableBrand').DataTable({
        "processing": true,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": true,
        "ajax": {
            "url": window.basePath + "Inventory/IBrandManagement/populateBrandMasterListByParam",
            "data": { 'operationType': operationType },
            "type": "GET",
            "dataSrc": "data",
            "error": function (xhr, error, thrown) {
                toastr.error("Failed to load table data: " + thrown);
            }
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
        "lengthMenu": [5, 10, 25, 50, 75, 100],
        "columns": [
            { "data": null, "title": "#" },
            { "data": "description", "title": "Brand Name" },
            {
                "title": "Action(s)",
                "data": null,
                "render": function (data, type, row) {
                    return 'N/A'
                }
            },
            { "data": "guID", "title": "GuID", visible: false },
        ],
        columnDefs: [
        ],
    });
    brandTable.on('order.dt search.dt', function () {
        brandTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

/* ------ Call Initial Components ------ */
function initialize() {
    changeEventHandler();
    $('.select2').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });
    domBrandTable();
}
/* ------ Validation for user input ------ */
function validater() {
    var form = document.getElementById("IBrandForm");
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

/* ------ Add/Edit/Delete Operation ------ */
function createUpdateDataIntoDB() {
    var operationType = $("#OperationType").val();
    var guID = $("#GuID").val();
    var description = $("#TextBoxDescription").val();

    var jsonData = {
        OperationType: operationType,
        GuID: guID ? guID : null,
        Description: description,
    };
    $.ajax({
        url: window.basePath + "Inventory/IBrandManagement/createUpdateBrand",
        type: "POST",
        data: JSON.stringify(jsonData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            initLoading();
        },
        success: function (response) {
            if (response.IsSuccess == true) {
                toastr.success(response.message);
                $("#IBrandForm").removeClass('was-validated');
            }
            else {
                toastr.info(response.message);
            }
        },
        error: function (xhr) {
            toastr.error("System Error: " + xhr.statusText);
        },
        complete: function () {
            stopLoading();
            clearInputFields();
            brandTable.ajax.reload();
        }
    });
}
function clearInputFields() {
    $(".form-control").val('');
    $(".select2").val('-1').trigger("change");

}

$(function () {
    if (typeof setupGlobalAjax === "function") setupGlobalAjax();
    initialize();
});