class UIClass {
    email = ".email";
    contact = ".contact"; // Often used for mobile
    website = ".website";
    phone = ".phone";     // Landline
    cnic = ".cnic";
    ntnnumber = ".ntnnumber";
    numberOnly = ".numberOnly";
}

class UIMasking {
    constructor() {
        this.ui = new UIClass();
    }

    initialize() {
        $(this.ui.cnic).mask('00000-0000000-0', { placeholder: "_____-_______-_" });
        $(this.ui.contact).mask('0000-0000000', { placeholder: "____-_______" });
        $(this.ui.phone).mask('000-0000000', { placeholder: "__-_______" });
        $(this.ui.ntnnumber).mask('0000000-0', { placeholder: "_______-_" });
        $(this.ui.email).on('keypress', function (e) {
            if (e.which === 32) return false;
        });
        $(this.ui.numberOnly).on('blur', function () {
            if ($(this).val().trim() === "") {
                $(this).val(0);
            }
        });
        $("form").on("reset", () => {
            setTimeout(() => {
                $(this.ui.numberOnly).val(0);
            }, 10);
        });
    }
}