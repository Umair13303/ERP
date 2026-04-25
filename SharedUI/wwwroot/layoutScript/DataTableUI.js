class HTML_DATATABLE_UTIL {
    static COLUMN_DROPDOWN_BY_CLASS(TableId, CSSClass, DropDownListId) {
        const Table = $('#' + TableId).DataTable();
        const FilteredColumnList = Table.columns().indexes().toArray().filter(i =>
            $(Table.column(i).header()).hasClass(CSSClass)
        ).map(i => ({
            Id: i,
            Description: $(Table.column(i).header()).text().trim()
        }));
        if (DropDownListId && $('#' + DropDownListId).length) {
            const $DropDown = $('#' + DropDownListId).empty().append('<option value="-1">Select an option</option>');
            FilteredColumnList.forEach(Col => $DropDown.append(`<option value="${Col.Id}">${Col.Description}</option>`));
        }
        return FilteredColumnList;
    }
    static APPEND_FOOTER(Id, TableId, ColumnIndex, Display) {
        const $table = $(`#${TableId}`);
        const dataTable = $table.DataTable();
        const targetColumnIndex = parseInt(ColumnIndex);
        const columnVisibility = dataTable.columns().indexes().map(i => dataTable.column(i).visible()).toArray();

        // --- 1. Prepare <tfoot> ---
        let $tfoot = $table.find('tfoot');
        if ($tfoot.length === 0) $tfoot = $('<tfoot></tfoot>').appendTo($table);

        // Always create a new row for each call
        let $tr = $('<tr></tr>').appendTo($tfoot);

        // --- 2. Determine colspan ---
        let colspanCount = 0;
        for (let i = 0; i < targetColumnIndex; i++) {
            if (columnVisibility[i]) colspanCount++;
        }

        // --- 3. Build footer cells ---
        let currentVisibleIndex = 0;
        let labelCellCreated = false;

        for (let i = 0; i < columnVisibility.length; i++) {
            if (!columnVisibility[i]) continue;
            let $newCell;

            if (i === targetColumnIndex) {
                $newCell = $(`<td id="${Id}"></td>`).html('0.00');
            } else if (!labelCellCreated && currentVisibleIndex === 0) {
                $newCell = $('<td></td>')
                    .attr('colspan', colspanCount)
                    .html(Display)
                    .css({ 'text-align': 'right', 'font-weight': 'bold' });
                labelCellCreated = true;
                currentVisibleIndex += colspanCount - 1;
            } else if (labelCellCreated && i < targetColumnIndex) {
                currentVisibleIndex++;
                continue;
            } else {
                $newCell = $('<td></td>').html('');
            }

            $tr.append($newCell);
            currentVisibleIndex++;
        }
    }
}
