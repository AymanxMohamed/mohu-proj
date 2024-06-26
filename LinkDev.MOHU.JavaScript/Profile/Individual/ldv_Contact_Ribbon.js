var contact_Ribbon = {
    createdon: "createdon"
};

function UmrahGate_Visibility(formContext) {
    debugger;
    //const formContext = executionContext.getFormContext();

    var createdonnew = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonnew != null)
        return true;
    else
        return false;
}

function UmrahGate_Action() {
    debugger;
    var alertStrings = { confirmButtonLabel: "Yes", text: "Not Working Yet.", title: "Sample title" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}

function InternalHajjGate_Action() {
    debugger;
    var alertStrings = { confirmButtonLabel: "Yes", text: "Not Working Yet.", title: "Sample title" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}

function InternalHajjGate_Visibility(formContext) {
    debugger;
    //const formContext = executionContext.getFormContext();

    var createdonnew1 = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonnew1 != null)
        return true;
    else
        return false;
}

//----Nusuk gates----

function NusukGates_Visibility(formContext) {
    debugger;

    var createdonnew = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonnew != null)
        return true;
    else
        return false;
}

function NusukGates_Action(formContext) {
    debugger;
    var alertStrings = { confirmButtonLabel: "Yes", text: "Nusuk Gates Not Working Yet.", title: "Nussuk title" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}

//----External Hajj
function ExternalHajj_Visibility(formContext) {
    debugger;

    var createdonnew = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonnew != null)
        return true;
    else
        return false;
}

function ExternalHajj_Action(formContext) {
    debugger;
    var alertStrings = { confirmButtonLabel: "Yes", text: "External Hajj Not Working Yet.", title: "External Hajj title" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}

// B2C Hajj Ribbon
function B2cHujj_Action() {
    var alertStrings = { confirmButtonLabel: "OK", text: "For Testing B2C", title: "B2C Test" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}
function B2cHujj_Visibility(formContext) {
    var createdonField = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonField != null)
        return true;
    else
        return false;
}

// El Mugamla Gate
function MugamalaGate_Action() {
    var alertStrings = { confirmButtonLabel: "OK", text: "For Testing Mugamla Gate", title: "Mugamla Test" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}
function MugamalaGate_Visibility(formContext) {
    var createdonField = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonField != null)
        return true;
    else
        return false;
} 

// Nusuk App
function NusukApp_Action() {
    var alertStrings = { confirmButtonLabel: "OK", text: "For Testing Nusuk App", title: "Nusuk App" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}
function NusukApp_Visibility(formContext) {
    var createdonField = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonField != null)
        return true;
    else
        return false;
} 