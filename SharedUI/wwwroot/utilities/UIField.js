class inputField {
    static textBox(label, name, cssClass, value, disabled, attributeId = "") {
        var html = `<div class="col-sm-12 col-md-3 mb-3">
                    <label for="${name}" class="col-form-label ${cssClass}">${label}</label>
                    <input type="text"
                           id="TextBox${name}" 
                           name="${name}" 
                           value="${value}"
                           data-attribute-id="${attributeId}" 
                           class="form-control form-control-sm attr-field"
                           placeholder="Enter ${label}"
                           ${disabled ? 'disabled' : ''}>
                </div>`;
        return html;
    }
}

class gridButton {
    static print(Id,Title,URL) {
        return "<a onclick=" + URL + " id=" + Id + " title='Click here to View " + Title + "' class='btn btn-sm PRINT'><i class='far fa-eye'></i> " + '' + "</a>";
    }
    static edit (Id, Title, URL) {
        return "<a onclick=" + URL + " id=" + Id + " title='Click here to Edit " + Title + "' class='btn btn-sm EDIT'><i class='far fa-edit'></i> " + '' + "</a>";
    }
    static editInList(Id, Title) {
        return "<a id=" + Id + " title='Click here to Edit " + Title + "' class='btn btn-sm EDIT_IN_LIST'><i class='far fa-edit'></i> " + '' + "</a>";
    }
    static delete(Id, Title, URL) {
        return "<a onclick=" + URL + " id=" + Id + " title='Click here to Delete " + Title + "' class='btn btn-sm DELETE'><i class='far fa-trash'></i> " + '' + "</a>";
    }
    static deleteInList(Id, Title, URL) {
        return "<a title='Click here to Delete " + Title + "' class='btn btn-sm delete'><i class='far fa-trash-alt'></i> " + '' + "</a>";
    }
    static childRowControl(Class) {
        return "<a class='btn btn-sm  " + Class + " view'><i class='far fa-plus " + Class + "'></i></a>";
    }
}
class conversion {
    static toDisplayBoolean(Value) {
        const TruthValue = ["1", "true", "on", "yes"];
        const FalseValue = ["0", "false", "off", "no"];
        if (TruthValue.includes(String(Value))) {
            return "YES";
        } else if (FalseValue.includes(String(Value))) {
            return "NO";
        }
    }
    static toDislayDate(ServerSideDate, Format) {
        if (!ServerSideDate) return "";
        let jsDate;
        if (/\/Date\((\d+)\)\//.test(ServerSideDate)) {
            jsDate = new Date(parseInt(ServerSideDate.match(/\d+/)[0], 10));
        } else {
            jsDate = new Date(ServerSideDate);
        }
        if (isNaN(jsDate)) return "";
        return flatpickr.formatDate(jsDate, Format);
    }
    static toBoolean(Value) {
        if (Value == null) return false;
        const str = String(Value).toLowerCase().trim();
        const TruthValue = ["1", "true", "on", "yes"];
        const FalseValue = ["0", "false", "off", "no"];
        if (TruthValue.indexOf(str) >= 0) return true;
        if (FalseValue.indexOf(str) >= 0) return false;
        return Boolean(Value);
    }
}