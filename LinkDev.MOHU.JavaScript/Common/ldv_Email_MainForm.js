var emailFields = {
    from: 'from',
    to: 'to',
    cc: 'cc',
    bcc: 'bcc',
    subject: 'subject',
    regardingobjectid: 'regardingobjectid',
    actualdurationminutes: 'actualdurationminutes',
    statecode:'statecode'

}



function OnLoad(executionContext) {
    debugger;
    var formContext = executionContext.getFormContext(); // get the form context
    lockUnlockFields(formContext);

    formContext.getAttribute(emailFields.statecode).addOnChange(function () {
        lockUnlockFields(formContext);
    });

}

function lockUnlockFields(formContext) {
    var stateCode = GetFieldValue(formContext, emailFields.statecode);

    // Define the fields to lock/unlock
    var fields = [
        emailFields.from,
        emailFields.to,
        emailFields.cc,
        emailFields.bcc,
        emailFields.subject,
        emailFields.regardingobjectid,
        emailFields.actualdurationminutes
    ];

    // Check if statecode is equal to 1
    var shouldUnlock = (stateCode === 0); //Draft

    // Iterate over each field and lock/unlock based on the statecode value
    fields.forEach(function (field) {
        var control = formContext.getControl(field);
        if (control) {
            control.setDisabled(!shouldUnlock); // Unlock if shouldUnlock is true, else lock
        }
    });
}

function GetFieldValue (formContext, fieldName) {
    var fieldValue = null;

    if (fieldName !== null) {
        fieldAttribute = formContext.getAttribute(fieldName);

        if (fieldAttribute !== null && fieldAttribute !== "undefined") {

            fieldValue = fieldAttribute.getValue();
        }
    }

    return fieldValue;
}