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

function InternalHajjGate_Action()
{
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

function InternalHajjGate_Visibility(formContext)
{
    debugger;
    //const formContext = executionContext.getFormContext();

    var createdonnew1 = formContext.getAttribute(contact_Ribbon.createdon).getValue();

    if (createdonnew1 != null)
        return true;
    else
        return false;
}
