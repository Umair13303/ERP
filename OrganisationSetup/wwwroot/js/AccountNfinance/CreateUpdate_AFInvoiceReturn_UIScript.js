/* ------ Global Variables ------ */
var operationType = $("#OperationType").val();
var dropDownListInitOption = "<option value='-1'>Select an option</option>";
var invoiceTable;

/* ------ UI COMPONENTS ------ */
function initializeDataTable() {
    invoicePPITable = $('#InvoiceDetailTable').DataTable({
        "processing": false,
        "serverSide": false,
        "responsive": true,
        "ordering": false,
        "searching": false,
        "paging": false,
        "info": false,
        "lengthChange": false,
        "columns": [
            {
                title: 'PRODUCT',
                data: 'ProductName',
                className: 'text-start td-product-name',
                render: function (data, type, row) {
                    if (!data) return '';
                    return `
                        <div class="product-text-wrapper truncated text-truncate" style="max-width: 180px; cursor: pointer;" title="Click to expand">
                            ${data}
                         </div>`;
                }
            },
            {
                title: 'UNIT PRC', data: 'UnitSalePrice',
            },
            { title: 'QTY', data: 'Quantity', className: 'text-danger fw-bold' },
            { title: 'TOTAL PRC', data: 'ActualAmount' },
            { title: 'DISC', data: 'DiscountAmount', },
            { title: 'SUB NET', data: 'ChargedAmount', },
            {
                title: 'ACTIONS',
                data: null,
                className: 'text-center',
                orderable: false,
                searchable: false,
                render: function (data, type, row, meta) {
                    return HTML_DATATABLE_UTIL.HTML_TBL_DELETE_BTN("", "");
                }
            },
            { title: 'BATCH', data: 'Batch', visible: false },
            { title: 'EXPIRY', data: 'Expiry', visible: false },
        ],
        language: {
            emptyTable: `
        <div class="d-flex flex-column align-items-center justify-content-center py-4 w-100" style="min-height: 120px;">
            <div class="mb-2 p-3 bg-light rounded-circle d-inline-flex align-items-center justify-content-center shadow-sm" style="width: 60px; height: 60px;">
                <i class="fa-solid fa-cart-shopping text-primary-subtle" style="font-size: 1.8rem;"></i>
            </div>
            <h6 class="text-secondary fw-semibold mb-1" style="font-size: 0.8rem;">Invoice Slate is Empty</h6>
            <span class="text-muted" style="font-size: 0.72rem;">Search & select a product from the dropdown above to add items.</span>
        </div>
    `
        },
        initComplete: function () {
            var $tableBody = $('#InvoiceDetailTable').find('tbody');
            $tableBody.off('click', '.btn-danger, .btn-table-delete');
            $tableBody.on('click', '.btn-danger, .btn-table-delete', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $rowElement = $(this).closest('tr');
                if (invoicePPITable) {
                    invoicePPITable.row($rowElement).remove().draw(false);
                    recalculateSummary();
                }
            });
            $tableBody.off('click', '.product-text-wrapper');
            $tableBody.on('click', '.product-text-wrapper', function () {
                var $wrapper = $(this);
                if ($wrapper.hasClass('truncated')) {
                    $wrapper.removeClass('truncated text-truncate').css('max-width', 'none');
                } else {
                    $wrapper.addClass('truncated text-truncate').css('max-width', '180px');
                }
            });
        }
    });
}


