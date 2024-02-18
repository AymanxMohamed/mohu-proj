var formContext;

var caseFields = {
    complainType: "ContractServiceLevelCode",
    customerid: "customerid",
    ldv_serviceid:"ldv_serviceid",      //Request Type
    ldv_maincategoryid: "ldv_maincategoryid",
    ldv_subcategoryid: "ldv_subcategoryid",
    ldv_secondarysubcategoryid:"ldv_secondarysubcategoryid"
}

function OnLoad(executionContext) {
    const formContext = executionContext.getFormContext();

    //unlock categories based on request type
    formContext.getAttribute(caseFields.ldv_serviceid).addOnChange(function () {
        CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_maincategoryid, caseFields.ldv_serviceid);
    });
    formContext.getAttribute(caseFields.ldv_maincategoryid).addOnChange(function () {
        CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_maincategoryid);
    });
    formContext.getAttribute(caseFields.ldv_subcategoryid).addOnChange(function () {
        CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_subcategoryid);
    });

}


function OnSave(executionContext) {
    var saveEvent = executionContext.getEventArgs();
}




