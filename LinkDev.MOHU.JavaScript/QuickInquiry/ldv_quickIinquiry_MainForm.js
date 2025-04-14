var Applicant = {
    LogicalName: "contact",
    firstName: "ldv_firstname",
    lastName: "ldv_lastname",
    mobilephone: "ldv_mobilenumber"


}

function OnLoad(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();
    OnLoad_MobileNumber();

    formContext.getAttribute(Applicant.mobilephone).addOnChange(function () {
        OnChange_MobileNumber(formContext);
    });


    //var tessttttt = CommonGeneric.GetFieldValue(
    //    formContext,
    //    Applicant.firstName
    //);
    //console.log(tessttttt);

}

function OnSave(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();
}

function OnChange_MobileNumber(formContext) {
    //SetRequiredEmailIfNotSaudiMobile(formContext);
    ValidateMobilePhoneNumber(formContext);
};

function ValidateMobilePhoneNumber() {
    console.log("validateeeeee");

    debugger;
    var formContext = Xrm.Page;
    var MobileField = formContext.getControl(Applicant.mobilephone);

    // Check if intlTelInputGlobals.instances[0] is defined and isValidNumber function exists
    var isvalid = window.parent.intlTelInputGlobals &&
        window.parent.intlTelInputGlobals.instances[0] &&
        typeof window.parent.intlTelInputGlobals.instances[0].isValidNumber === 'function' &&
        window.parent.intlTelInputGlobals.instances[0].isValidNumber();

    if (isvalid === false && window.parent.intlTelInputGlobals.instances[0]?.getNumber()) {
        // CommonGeneric.showNotification(MobileField, "2", "MobilePhoneValidator");
        MobileField.setNotification("Please enter a valid mobile phone number", "MobilePhoneValidator");
    } else {
        MobileField.clearNotification("MobilePhoneValidator");
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