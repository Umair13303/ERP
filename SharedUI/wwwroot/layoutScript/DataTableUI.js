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

    static HTML_INPUT_BTN(ElementId, Class, FunctionCall,Display) {
        const Btn = '<input type="button" class="btn btn-sm btn-success' + Class + '" onclick="createUpdateDataIntoDB(this)" id="' + ElementId + '" value="' + Display + '" />'
        return Btn;
    }
    static HTML_TBL_DELETE_BTN(text, functionCall) {
        return `<button type="button" class="btn btn-danger btn-sm delete" onclick="${functionCall}" title="${text}" aria-label="${text}">
                <i class="fas fa-trash-alt" aria-hidden="true"></i>
            </button>`;
    }
    static HTML_TBL_INPUT(name, cssClass, value, isDisabled) {
        var fieldName = name || "";
        var elementClass = cssClass || "";
        var elementValue = (value !== undefined && value !== null) ? value : "";
        var disabledAttribute = isDisabled ? "disabled" : "";
        var elementId = "TextBox" + fieldName;

        return `<input type="text" 
                   id="${elementId}" 
                   name="${fieldName}" 
                   class="form-control  ${name} ${elementClass}" 
                   value="${elementValue}" 
                   ${disabledAttribute} />`;
    }
    static HTML_GENERIC_ATTRIBUTE(attIdsString, attributeList) {

        if (!attIdsString || attIdsString.toString().trim() === "") {
            return "N/A";
        }

        var productAttIds = attIdsString.toString().split(',').map(id => id.trim());
        var htmlContent = "";

        $.each(productAttIds, function (index, attId) {
            var matchedAttribute = attributeList.find(attr => (attr.id || attr.Id).toString() === attId);
            if (matchedAttribute) {
                var label = matchedAttribute.description || matchedAttribute.Description || "Attribute";

                htmlContent += `
                <div class="d-flex align-items-center mb-1 me-2" style="gap: 5px;">
                    <span class="fw-bold text-secondary small">${label}:</span>
                    <input type="text" 
                           class="form-control form-control-sm attr-field" 
                           data-attribute-id="${attId}" 
                           placeholder="Value..." 
                           style="width: 100px; height: 30px;" />
                </div>`;
            }
        });

        return htmlContent !== "" ? `<div class="d-flex flex-wrap">${htmlContent}</div>` : "N/A";

    }
    static HTML_TBL_SAVE_BTN(text, guID, cssClass, functionCall) {
        return `<button type="button" class="btn btn-icon ${cssClass} shadow-sm" 
                    onclick="${functionCall}" 
                    id="ButtonSubmitDown${guID}" 
                    title="${text}" 
                    style="display: inline-flex; align-items: center; justify-content: center; width: 36px; height: 36px; border-radius: 8px; border: 1px solid #dee2e6; background-color: #ffffff; transition: all 0.2s ease-in-out;"
                    onmouseover="this.style.backgroundColor='#f8f9fa'; this.style.borderColor='#adb5bd';"
                    onmouseout="this.style.backgroundColor='#ffffff'; this.style.borderColor='#dee2e6';">
                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="text-success" style="color: #198754;">
                        <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path>
                        <polyline points="17 21 17 13 7 13 7 21"></polyline>
                        <polyline points="7 3 7 8 15 8"></polyline>
                    </svg>
                </button>`;
    }

    static HTML_TBL_PRINT_BTN(text, guID, cssClass, functionCall) {
        return `<button type="button" class="btn btn-icon ${cssClass} shadow-sm" 
                    onclick="${functionCall}" 
                    id="PrintDocument${guID}" 
                    title="${text}" 
                    style="display: inline-flex; align-items: center; justify-content: center; width: 36px; height: 36px; border-radius: 8px; border: 1px solid #dee2e6; background-color: #ffffff; transition: all 0.2s ease-in-out;"
                    onmouseover="this.style.backgroundColor='#f8f9fa'; this.style.borderColor='#adb5bd';"
                    onmouseout="this.style.backgroundColor='#ffffff'; this.style.borderColor='#dee2e6';">
                    <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="text-primary" style="color: #0d6efd;">
                        <polyline points="6 9 6 2 18 2 18 9"></polyline>
                        <path d="M6 18H4a2 2 0 0 1-2-2v-5a2 2 0 0 1 2-2h16a2 2 0 0 1 2 2v5a2 2 0 0 1-2 2h-2"></path>
                        <rect x="6" y="14" width="12" height="8"></rect>
                    </svg>
                </button>`;
    }
}
