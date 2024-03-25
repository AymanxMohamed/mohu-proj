var account = {
    logicalName: "account",
    ldv_contactpersonmobile: "ldv_contactpersonmobile",
    ldv_name_ar: "ldv_name_ar",
    name: "name",
    ldv_contactpersonemail: "ldv_contactpersonemail",
    telephone1: "telephone1"
};

function OnLoad(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();

    OnLoad_MobileNumber();

    //formContext.getAttribute(account.ldv_contactpersonmobile).addOnChange(function () {
    //    ValidateMobilePhoneNumber(formContext);

    //});
    formContext.getAttribute(account.telephone1).addOnChange(function () {
        ValidateMobilePhoneNumber(formContext);

    });

    formContext.getAttribute(account.name).addOnChange(function () {
        CommonGeneric.ValidateEnglishCharacters(formContext, account.name);

    });

    formContext.getAttribute(account.ldv_name_ar).addOnChange(function () {
        CommonGeneric.ValidateArabicCharacters(formContext, account.ldv_name_ar);

    });

    //formContext.getAttribute(account.ldv_contactpersonemail).addOnChange(function () {
    //    ValidateEmailFormat(formContext, account.ldv_contactpersonemail);

    //});
};

function OnSave(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();
    var saveEvent = executionContext.getEventArgs();


    // mobile number
    if (
        window.parent.intlTelInputGlobals.instances[0] != null &&
        window.parent.intlTelInputGlobals.instances[0] != undefined
    ) {
        if (
            window.parent.intlTelInputGlobals.instances[0].isValidNumber() != true &&
            window.parent.intlTelInputGlobals.instances[0]?.getNumber() != "" &&
            window.parent.intlTelInputGlobals.instances[0]?.getNumber() != null &&
            window.parent.intlTelInputGlobals.instances[0]?.getNumber() != undefined
        ) {
            saveEvent.preventDefault();
            ValidateMobilePhoneNumber();
        }
    }
};

function OnLoad_MobileNumber() {
    debugger;
    if (window.parent.intlTelInputGlobals)
        if (
            window.parent.intlTelInputGlobals.instances &&
            (Object.keys(window.parent.intlTelInputGlobals.instances)?.length > 1 ||
                window.parent.intlTelInputGlobals.instances?.length > 1)
        ) {
            var lastInstnace;
            if (typeof window.parent.intlTelInputGlobals.instances == "object") {
                var Instances = window.parent.intlTelInputGlobals.instances;
                lastInstnace = Instances[Object.keys(Instances).length - 1];
            } else {
                window.parent.intlTelInputGlobals.instances =
                    window.parent.intlTelInputGlobals.instances.filter(function (el) {
                        return el != null;
                    });

                var Instances = window.parent.intlTelInputGlobals.instances;
                lastInstnace = Instances[Instances.length - 1];
            }

            window.parent.intlTelInputGlobals.instances = [];
            window.parent.intlTelInputGlobals.instances.push(lastInstnace);
        }
};


//function ValidateMobilePhoneNumber() {
//    debugger;
//    var formContext = Xrm.Page;
//    //var MobileField = formContext.getControl(account.ldv_contactpersonmobile);
//    var MobileField = formContext.getControl(account.telephone1);

//    var isvalid = window.parent.intlTelInputGlobals.instances[0].isValidNumber();
//    if (
//        window.parent.intlTelInputGlobals.instances[0] != null &&
//        window.parent.intlTelInputGlobals.instances[0] != undefined &&
//        !isvalid &&
//        window.parent.intlTelInputGlobals.instances[0]?.getNumber() != "" &&
//        window.parent.intlTelInputGlobals.instances[0]?.getNumber() != null &&
//        window.parent.intlTelInputGlobals.instances[0]?.getNumber() != undefined
//    ) {
//        //CommonGeneric.showNotification(MobileField, "1056", "MobilePhoneValidator");
//        CommonGeneric.showNotification(MobileField, "2", "MobilePhoneValidator");

//    } else {
//        MobileField.clearNotification("MobilePhoneValidator");
//    }
//};

function ValidateMobilePhoneNumber() {
    debugger;
    var formContext = Xrm.Page;
    var MobileField = formContext.getControl(account.telephone1);

    // Check if intlTelInputGlobals.instances[0] is defined and isValidNumber function exists
    var isvalid = window.parent.intlTelInputGlobals &&
        window.parent.intlTelInputGlobals.instances[0] &&
        typeof window.parent.intlTelInputGlobals.instances[0].isValidNumber === 'function' &&
        window.parent.intlTelInputGlobals.instances[0].isValidNumber();

    if (isvalid === false && window.parent.intlTelInputGlobals.instances[0]?.getNumber()) {
        CommonGeneric.showNotification(MobileField, "2", "MobilePhoneValidator");
    } else {
        MobileField.clearNotification("MobilePhoneValidator");
    }
};

function ValidateEmailFormat(formContext,fieldName) {
    var emailValidator = /^([a-zA-Z0-9_\.\-])+@[a-zA-Z_]+?\.[a-zA-Z]{2,4}$|^([a-zA-Z0-9_\.\-])+@[a-zA-Z_]+?\.[a-zA-Z]{2,4}\.[a-zA-Z]{2,4}$/;
    var email = formContext.getAttribute(fieldName).getValue();

    if (email !== null && email !== undefined) {
        if (!email.match(emailValidator)) {
            //var errorMessage = "Please enter a valid email address (e.g., name@indust.com)";
            //formContext.getControl(fieldName).setNotification(errorMessage);
            var emailField = formContext.getControl(fieldName);
            CommonGeneric.showNotification(emailField, "5", "EmailValidator");

        } else {
            //formContext.getControl(fieldName).clearNotification();
            formContext.getControl(fieldName).clearNotification("EmailValidator");

        }
    } else {
        formContext.getControl(fieldName).clearNotification("EmailValidator");

    }
};

