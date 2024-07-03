var LinkDev = {};
LinkDev.StcPay = {};
LinkDev.StcPay.TicketCategory = {};

LinkDev.StcPay.TicketCategory.Fields = {
    ParentCategory: "ldv_parentcategoryid",
    SubCategory: "ldv_subcategoryid",
    TashirID: "ldv_tasherid",
    KadanaID: "ldv_kedanaid",
    ServiceDeskID: "ldv_sdid",
    SahabID: "ldv_sahabid",
    IntegrationAvailableFor: "ldv_integrationavailablefor",
    Availableforcode: "ldv_availableforcode",
    ldv_slahourlevel1id: "ldv_slahourlevel1id",
    ldv_slahourlevel2id: "ldv_slahourlevel2id",
    ldv_slahourlevel3id: "ldv_slahourlevel3id",

    Enums: {
        Integration: {
            Kadana: 1,
            Sahab: 2,
            Tasher: 3,
            ServiceDesk: 4
        },
        CaseOrigin: {
            CallCenter: 1,
            Email: 2,
            Nusuk: 3,
            ExternalGate: 4,
            SocialMedia: 5,
            Rasel: 6,
            WirelessCommunications: 7,
            IoT: 700610000,
            Facebook: 2483,
            Twitter: 3986


        }
    },
}

LinkDev.StcPay.TicketCategory.Tabs = {
    tab_TransactionType: "tab_4",
    tab_RelatedSubCategory: "tab_5",
    tab_DocumntsForSubCategory: "tab_6",
    tab_RelatedFields: "tab_7",
    tab_RelatedSecondarySubCategory: "tab_9"

}

LinkDev.StcPay.TicketCategory.Functions = {
};


LinkDev.StcPay.TicketCategory.Functions.FormOnLoad = function (executionContext) {
    debugger;
    var formContext = executionContext.getFormContext();
    ShowHideTabs(formContext);
    RemoveCaseOriginOptionsFromAvailableForField(formContext);
    showAndHideSubcategoryField(formContext);
    checkReqLevelForSLAHoursFields(formContext);

    formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.IntegrationAvailableFor).addOnChange(makeFieldsRequired);
    formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.ParentCategory).addOnChange(function () {
        showAndHideSubcategoryField(formContext);

    });

    formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel1id).addOnChange(function () {
        checkReqLevelForSLAHoursFields(formContext);

    });
    formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel2id).addOnChange(function () {
        checkReqLevelForSLAHoursFields(formContext);

    });
    formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel3id).addOnChange(function () {
        checkReqLevelForSLAHoursFields(formContext);

    });

    formContext.getControl(LinkDev.StcPay.TicketCategory.Fields.SubCategory).addPreSearch(function () {
        filterSubCategoryBasedOnParentCategory(formContext);
    });

}

LinkDev.StcPay.TicketCategory.Functions.ParentCategory_OnChange = function (executionContext) {
    var formContext = executionContext.getFormContext();

    ShowHideTabs(formContext);

}

LinkDev.StcPay.TicketCategory.Functions.SubCategory_OnChange = function (executionContext) {
    var formContext = executionContext.getFormContext();

    ShowHideTabs(formContext);

}

ShowHideTabs = function (formContext) {
    debugger;
    var parentCategory = Common.GetFieldValue(formContext,
        LinkDev.StcPay.TicketCategory.Fields.ParentCategory);
    var subCategory = Common.GetFieldValue(formContext,
        LinkDev.StcPay.TicketCategory.Fields.SubCategory);

    if (parentCategory) {
        //Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_TransactionType, true);
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_DocumntsForSubCategory, true);
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_RelatedFields, true);
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_RelatedSubCategory, false);

    } else {
        //Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_TransactionType, false);
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_DocumntsForSubCategory, false);
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_RelatedFields, false);
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_RelatedSubCategory, true);

    }

    if (parentCategory && !subCategory) {
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_RelatedSecondarySubCategory, true);

    }
    else {
        Common.TabVisability(formContext, LinkDev.StcPay.TicketCategory.Tabs.tab_RelatedSecondarySubCategory, false);

    }

}


function makeFieldsRequired(executionContext) {
    debugger;
    var formContext = executionContext.getFormContext();
    var selectedValues = formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.IntegrationAvailableFor).getValue();
    var requiredFieldsMap = {
        1: [LinkDev.StcPay.TicketCategory.Fields.KadanaID],
        2: [LinkDev.StcPay.TicketCategory.Fields.SahabID],
        3: [LinkDev.StcPay.TicketCategory.Fields.TashirID],
        4: [LinkDev.StcPay.TicketCategory.Fields.ServiceDeskID],
    };

    if (selectedValues != null) {
        // Iterate through each selected value and make the associated fields required
        for (var i = 0; i < selectedValues.length; i++) {
            var option = selectedValues[i];
            if (requiredFieldsMap[option]) {
                var requiredFields = requiredFieldsMap[option];
                for (var j = 0; j < requiredFields.length; j++) {
                    formContext.getAttribute(requiredFields[j]).setRequiredLevel("required");
                }
            }
        }
        // Make all other fields optional
        for (var option in requiredFieldsMap) {
            if (!selectedValues.includes(parseInt(option))) {
                var fieldsToMakeOptional = requiredFieldsMap[option];
                for (var k = 0; k < fieldsToMakeOptional.length; k++) {
                    formContext.getAttribute(fieldsToMakeOptional[k]).setRequiredLevel("none");
                }
            }
        }


    } else {
        // Make the string field optional
        Common.SetReqLevel(formContext, LinkDev.StcPay.TicketCategory.Fields.KadanaID, false);
        Common.SetReqLevel(formContext, LinkDev.StcPay.TicketCategory.Fields.SahabID, false);
        Common.SetReqLevel(formContext, LinkDev.StcPay.TicketCategory.Fields.TashirID, false);
        Common.SetReqLevel(formContext, LinkDev.StcPay.TicketCategory.Fields.ServiceDeskID, false);

    }
}

function RemoveCaseOriginOptionsFromAvailableForField(formContext) {
    debugger;

    var ifExistRemoveValues = [
        LinkDev.StcPay.TicketCategory.Fields.Enums.CaseOrigin.IoT,
        LinkDev.StcPay.TicketCategory.Fields.Enums.CaseOrigin.Facebook,
        LinkDev.StcPay.TicketCategory.Fields.Enums.CaseOrigin.Twitter
    ];

    var availableForField = formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.Availableforcode);

    if (availableForField !== null && availableForField !== undefined) {


        // Remove options if they exist
        var options = availableForField.getOptions();
        for (var i = 0; i < ifExistRemoveValues.length; i++) {
            var valueToRemove = ifExistRemoveValues[i];
            var index = options.findIndex(function (option) {
                return option.value === valueToRemove;
            });
            if (index !== -1) {
                RemoveOptionSetValues(formContext, LinkDev.StcPay.TicketCategory.Fields.Availableforcode, [valueToRemove]);
            }
        }
    }

};

function RemoveOptionSetValues(formContext, optionSetName, values) {

    var optionSet = formContext.ui.controls.get(optionSetName);

    if (optionSet != null) {
        for (var i = 0; i < values.length; i++) {
            optionSet.removeOption(values[i]);
        }
    }
}

function filterSubCategoryBasedOnParentCategory(formContext) {
    debugger;
    // Get the selected parent category lookup value
    var parentCategoryLookup = formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.ParentCategory).getValue();

    if (parentCategoryLookup && parentCategoryLookup.length > 0) {
        // Retrieve the ID of the selected parent category
        var parentCategoryId = parentCategoryLookup[0].id;
        var subCategoryFilter = "<filter><condition attribute='ldv_parentcategoryid' operator='eq' value='" + parentCategoryId + "' /></filter>";
        formContext.getControl(LinkDev.StcPay.TicketCategory.Fields.SubCategory).addCustomFilter(subCategoryFilter, "ldv_casecategory");
    }
    else {

        formContext.getControl(LinkDev.StcPay.TicketCategory.Fields.SubCategory).removePreSearch();
    }
};

function showAndHideSubcategoryField(formContext) {
    var parentCategoryLookup = formContext.getAttribute(LinkDev.StcPay.TicketCategory.Fields.ParentCategory).getValue();
    if (parentCategoryLookup) {
        ShowField(formContext, LinkDev.StcPay.TicketCategory.Fields.SubCategory, true);
    }
    else {
        ShowField(formContext, LinkDev.StcPay.TicketCategory.Fields.SubCategory, false);

    }
};

function ShowField(formContext, fieldName, isShown) {
    if (IsNull(isShown))
        isShown = true;

    if (fieldName !== null) {
        var fieldControl = formContext.getControl(fieldName);
        if (fieldControl !== null && fieldControl !== "undefined") {
            fieldControl.setVisible(isShown);
            if (isShown == false) { EmptyField(formContext, fieldName) }

        }
    }
};

function EmptyField(formContext, fieldName) {

    if (fieldName !== null) {
        var fieldAttribute = formContext.getAttribute(fieldName);

        if (fieldAttribute !== null && fieldAttribute !== "undefined") {
            if (fieldAttribute.getAttributeType && fieldAttribute.getAttributeType() == "multiselectoptionset")
                fieldAttribute.setValue([0]);
            else
                fieldAttribute.setValue(null);

        }
    }
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

function SetReqLevel (formContext, fieldName, isRequired) {

    if (fieldName !== null) {
        var fieldAttribute = null;

        if (isRequired === true) {

            fieldAttribute = formContext.getControl(fieldName).getAttribute();

            if (fieldAttribute !== null && fieldAttribute !== "undefined") {
                fieldAttribute.setRequiredLevel("required");
            }

        } else if (isRequired === false) {

            fieldAttribute = formContext.getControl(fieldName).getAttribute();

            if (fieldAttribute !== null && fieldAttribute !== "undefined") {
                fieldAttribute.setRequiredLevel("none");
            }
        }
    }
}
function checkReqLevelForSLAHoursFields(formContext) {

    var level1 = GetFieldValue(formContext, LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel1id);
    var level2 = GetFieldValue(formContext, LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel2id);
    var level3 = GetFieldValue(formContext, LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel3id);

    var slaHoursFields = [level1, level2, level3];
    var isRequired = slaHoursFields.some(field => field !== null);

    SetReqLevel(formContext, LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel1id, isRequired);
    SetReqLevel(formContext, LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel2id, isRequired);
    SetReqLevel(formContext, LinkDev.StcPay.TicketCategory.Fields.ldv_slahourlevel3id, isRequired);
}

