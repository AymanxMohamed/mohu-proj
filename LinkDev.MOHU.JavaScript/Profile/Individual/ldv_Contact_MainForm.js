var contact = {
    LogicalName: "contact",
    firstName: "fullname_compositionLinkControl_firstname",
    lastName: "fullname_compositionLinkControl_lastname",
    FullName: "FullName",
    ldv_name_ar: "ldv_name_ar",
    emailaddress1: "emailaddress1",
    mobilephone: "mobilephone",
    gendercode: "gendercode",
    birthdate: "birthdate",
    ldv_hijribirthdate: "ldv_hijribirthdate",
    ldv_nationalitycountryid: "ldv_nationalitycountryid",
    ldv_countryofresidenceid: "ldv_countryofresidenceid",
    jobtitle: "jobtitle",
    ldv_idtypecode: "ldv_idtypecode",
    ldv_idnumber: "ldv_idnumber",
    governmentid: "governmentid",
    ldv_visanumber: "ldv_visanumber",
    ldv_bordernumber: "ldv_bordernumber",
    createdon: "createdon",
    createdby: "createdby",
    modifiedon: "modifiedon",
    modifiedby: "modifiedby",
    ownerid: "ownerid",


    Tabs: {
        individualinformation: "tab_individualinformation",
        almutamirinformation: "tab_almutamirinformation",
        hajjinformation: "tab_hajjinformation",
        nusukinformation: "tab_nusukinformation",
        administration: "tab_administration",


    },

    Sections: {
        individualdetails: "tab_individualinformation_section_individualdetails",
        almutamirinfo: "tab_almutamirinformation_section_almutamirinfo",
        internalhajj: "tab_hijjinformation_section_internalhajj",
        externalhajj: "tab_hajjinformation_section_externalhajj",
        internalpermits: "tab_nusukinformation_section_internalpermits",
        externalpermits: "tab_nusukinformation_section_externalpermits",
        bookanelectriccar: "tab_nusukinformation_section_bookanelectriccar",

    },


    Enums: {
        gendercodeEnum: {
            Male: 1,
            Female: 2
        },
        ldv_idtypecodeEnum: {
            NationalIdentity: 1,
            Accommodation: 2,
            GulfCitizen: 3,
            Passport: 4
        }
    }


};

function OnLoad(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();

    ShowAndReqFieldsBasedOnIdType(formContext);
    OnLoad_MobileNumber();
    SetRequiredEmailIfNotSaudiMobile(formContext);
    EmptyHijriDateOnCreate(formContext);
    formContext.getAttribute(contact.ldv_idtypecode).addOnChange(function () {
        OnChange_IDType(formContext);
    });

    formContext.getAttribute(contact.birthdate).addOnChange(function () {
        OnChange_BirthDate(formContext);
    });

    formContext.getAttribute(contact.mobilephone).addOnChange(function () {
        OnChange_MobileNumber(formContext);
    });

    formContext.getAttribute(contact.ldv_name_ar).addOnChange(function () {
        CommonGeneric.ValidateArabicCharacters(formContext, contact.ldv_name_ar);

    });


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

function OnChange_IDType(formContext) {

    //Hide and not require all fields initially
    var fieldsToBeHiddenOrShown = [contact.ldv_idnumber, contact.governmentid, contact.ldv_visanumber, contact.ldv_bordernumber];
    fieldsToBeHiddenOrShown.forEach(function (fieldSchemaName) {
        CommonGeneric.ShowAndReuiredField(formContext, fieldSchemaName, false, false);
    });

    // Show and require fields based on idType
    ShowAndReqFieldsBasedOnIdType(formContext);
};

function ShowAndReqFieldsBasedOnIdType(formContext) {

    var idType = CommonGeneric.GetFieldValue(formContext, contact.ldv_idtypecode);

    if (idType == contact.Enums.ldv_idtypecodeEnum.NationalIdentity || idType == contact.Enums.ldv_idtypecodeEnum.Accommodation || idType == contact.Enums.ldv_idtypecodeEnum.GulfCitizen) {
        CommonGeneric.ShowAndReuiredField(formContext, contact.ldv_idnumber, true, true);
    }

    if (idType == contact.Enums.ldv_idtypecodeEnum.Passport || idType == contact.Enums.ldv_idtypecodeEnum.GulfCitizen) {
        CommonGeneric.ShowAndReuiredField(formContext, contact.governmentid, true, true);

    }

    if (idType == contact.Enums.ldv_idtypecodeEnum.Passport) {
        CommonGeneric.ShowAndReuiredField(formContext, contact.ldv_visanumber, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, contact.ldv_bordernumber, true, true);
    }

};

//function EmptyHijriDateOnCreate(formContext) {
//    setTimeout(function () {
//        if (formContext.ui.getFormType() === 1) {
//            CommonGeneric.EmptyField(formContext, contact.ldv_hijribirthdate);
//            CommonGeneric.EmptyField(formContext, contact.birthdate);

//            // Temporarily set the fields as not required to trigger validation
//            CommonGeneric.SetReqLevel(formContext, contact.ldv_hijribirthdate, false);

//            // Set the fields back to required after a short delay
//            setTimeout(function () {
//                CommonGeneric.SetReqLevel(formContext, contact.ldv_hijribirthdate, true);
//            }, 0);
//        }
//    }, 500);
//}

function EmptyHijriDateOnCreate(formContext) {
    setTimeout(function () {
        if (formContext.ui.getFormType() === 1) {
            CommonGeneric.EmptyField(formContext, contact.ldv_hijribirthdate);
            CommonGeneric.EmptyField(formContext, contact.birthdate);

        }
    }, 500);
}

function OnChange_BirthDate(formContext) {

    CommonGeneric.ValidateFieldDateNotToBeInFuture(formContext, contact.birthdate, contact.birthdate);
};

function IsSaudiMobileNumber(mobileNumber) {
    if (mobileNumber === null || mobileNumber === undefined) {
        return false;
    }

    // Remove any non-digit characters
    var cleanedNumber = mobileNumber.replace(/\D/g, '');

    // Regular expression to match Saudi mobile numbers
    var saudiMobileRegex = /^(966|00966|\+966)?5[0-9]{8}$/;

    return saudiMobileRegex.test(cleanedNumber);
}

function SetRequiredEmailIfNotSaudiMobile(formContext) {
    var mobileNumber = CommonGeneric.GetFieldValue(formContext, contact.mobilephone);

    if (mobileNumber === null || mobileNumber === undefined) {
        return;
    }

    if (!IsSaudiMobileNumber(mobileNumber)) {
        CommonGeneric.SetReqLevel(formContext, contact.emailaddress1, true);
    } else {
        CommonGeneric.SetReqLevel(formContext, contact.emailaddress1, false);
    }
}

function OnChange_MobileNumber(formContext) {
    SetRequiredEmailIfNotSaudiMobile(formContext);
    ValidateMobilePhoneNumber(formContext);
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
//    var MobileField = formContext.getControl(contact.mobilephone);
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
    var MobileField = formContext.getControl(contact.mobilephone);

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



