var LinkDev = {};
LinkDev.StcPay = {};
LinkDev.StcPay.TicketCategory = {};

LinkDev.StcPay.TicketCategory.Fields = {
    ParentCategory: "ldv_parentcategoryid",
    SubCategory:"ldv_subcategoryid"
}

LinkDev.StcPay.TicketCategory.Tabs = {
    tab_TransactionType: "tab_4",
    tab_RelatedSubCategory: "tab_5",
    tab_DocumntsForSubCategory: "tab_6",
    tab_RelatedFields: "tab_7",
    tab_RelatedSecondarySubCategory:"tab_9"

}

LinkDev.StcPay.TicketCategory.Functions = {
};


LinkDev.StcPay.TicketCategory.Functions.FormOnLoad = function (executionContext) {
    debugger;
    var formContext = executionContext.getFormContext();
    ShowHideTabs(formContext);

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



