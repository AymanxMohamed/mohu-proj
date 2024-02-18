var formContext;

var caseFields = {
    Origin: "CaseOriginCode",
    ComplainType: "ContractServiceLevelCode",
    customerid: "customerid",
    AgentEmployeeDecision: "ldv_AgentEmployeeDecisioncode",
}

function OnLoad(executionContext) {
    const formContext = executionContext.getFormContext();

    formContext.getAttribute(caseFields.c).addOnChange(function () {
    RestrictApplicantToBeOrganizationOnly(formContext);
    });
}


function OnSave(executionContext) {
    var saveEvent = executionContext.getEventArgs();
}

function RestrictApplicantToBeOrganizationOnly(formContext) {
    const applicant = formContext.getControl(caseFields.customerid);

    if (!applicant) return;
    // in case applicant is customer lookup (allowing account and contact)
    if (applicant.getEntityTypes().length > 1)
        applicant.setEntityTypes(["contact"]); // restrict applicant to allow only accounts
}


