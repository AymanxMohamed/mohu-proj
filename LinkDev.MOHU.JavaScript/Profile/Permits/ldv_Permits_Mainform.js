var permits = {
    LogicalName: "ldv_permits",
    ldv_customerid: "ldv_customerid",
    ldv_identitytype: "ldv_identitytype",
    ldv_identitynumber: "ldv_identitynumber",
    ldv_reservationnumber: "ldv_reservationnumber",
    ldv_gatename: "ldv_gatename",
    ldv_permitcategorycode: "ldv_permitcategorycode",
    ldv_permitnumber: "ldv_permitnumber",
    ldv_permittype: "ldv_permittype",
    ldv_permitdate: "ldv_permitdate",
    ldv_permittime: "ldv_permittime",
    ldv_permitstatus: "ldv_permitstatus",
    ldv_passportnumber: "ldv_passportnumber",
    ldv_selectedgathering: "ldv_selectedgathering",
    ldv_maincompanionid: "ldv_maincompanionid",
    ldv_visatype: "ldv_visatype",
    ldv_visanumber: "ldv_visanumber",
    ldv_visaduration: "ldv_visaduration",
    ldv_visadate: "ldv_visadate",
    ldv_bookingdate: "ldv_bookingdate",
    ldv_cancellationdate: "ldv_cancellationdate",
    ldv_canceledby: "ldv_canceledby",
    ldv_mobilenumber: "ldv_mobilenumber",
    ldv_fullname: "ldv_fullname",
    ldv_nationality: "ldv_nationality",
    createdon: "createdon",
    createdby: "createdby",
    modifiedon: "modifiedon",
    modifiedby: "modifiedby",
    ownerid: "ownerid",

    Tabs: {
        permitsinformation: "tab_permitsinformation",
        companionspermits: "tab_companionspermits",
        administration: "tab_administration",

    },


    Enums: {
        ldv_permitcategorycodeEnum: {
            Internal: 1,
            External: 2
        }
    }

};


function OnLoad (executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();

    OnChange_PermitCategory(formContext);
    formContext.getAttribute(permits.ldv_permitcategorycode).addOnChange(function () {
        OnChange_PermitCategory(formContext);
    });
    formContext.getAttribute(permits.ldv_maincompanionid).addOnChange(function () {
        OnChange_MainCompanion(formContext);
    });

};

function OnChange_PermitCategory(formContext) {
    var permitCategory = CommonGeneric.GetFieldValue(formContext, permits.ldv_permitcategorycode);
    var mainCompanion = CommonGeneric.GetFieldValue(formContext, permits.ldv_maincompanionid);
    var internalFieldsToBeShown = [permits.ldv_identitytype, permits.ldv_identitynumber, permits.ldv_maincompanionid];
    var externalFieldsToBeShown = [permits.ldv_passportnumber, permits.ldv_visatype, permits.ldv_visadate, permits.ldv_nationality, permits.ldv_visanumber, permits.ldv_visaduration];

    // Show or hide internal fields based on permit category
    CommonGeneric.ShowFields(formContext, internalFieldsToBeShown, permitCategory === permits.Enums.ldv_permitcategorycodeEnum.Internal);

    // Show or hide external fields based on permit category
    CommonGeneric.ShowFields(formContext, externalFieldsToBeShown, permitCategory === permits.Enums.ldv_permitcategorycodeEnum.External);

    // Show or hide companions permits tab based on permit category and main companion
    var showCompanionsTab = permitCategory === permits.Enums.ldv_permitcategorycodeEnum.Internal && mainCompanion !== null && mainCompanion !== undefined;
    CommonGeneric.TabVisability(formContext, permits.Tabs.companionspermits, showCompanionsTab);

};

function OnChange_MainCompanion (formContext) {

    var permitCategory = CommonGeneric.GetFieldValue(formContext, permits.ldv_permitcategorycode);
    var mainCompanion = CommonGeneric.GetFieldValue(formContext, permits.ldv_maincompanionid);


    var showCompanionsTab = permitCategory === permits.Enums.ldv_permitcategorycodeEnum.Internal && permitCategory !== null && permitCategory !== undefined && mainCompanion !== null && mainCompanion !== undefined;
    CommonGeneric.TabVisability(formContext, permits.Tabs.companionspermits, showCompanionsTab);
};





