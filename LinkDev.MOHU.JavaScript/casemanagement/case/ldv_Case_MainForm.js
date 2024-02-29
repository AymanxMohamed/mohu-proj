var caseFields = {
    complainType: "ContractServiceLevelCode",
    customerid: "customerid",
    ldv_serviceid: "ldv_serviceid",      //Request Type
    ldv_maincategoryid: "ldv_maincategoryid",
    ldv_subcategoryid: "ldv_subcategoryid",
    ldv_secondarysubcategoryid: "ldv_secondarysubcategoryid",
    ldv_processid: "ldv_processid",
    ldv_issubmitted: "header_process_ldv_issubmitted",
    createdon: "createdon",
    ldv_qualityofficerdecisioncode: "ldv_qualityofficerdecisioncode",
    ldv_qualityofficerneededinformation: "ldv_qualityofficerneededinformation",
    ldv_qualityofficerdecisioncode2: "ldv_qualityofficerdecisioncode2",
    ldv_qualityofficerneededinformation2:"ldv_qualityofficerneededinformations",
    ldv_closurereason: "ldv_closurereason",
    ldv_closurereasons: "ldv_closurereasons",
    ldv_companiesadministrationdecisioncode: "ldv_companiesadministrationdecisioncode",
    ldv_companiesadministrationneededinformation: "ldv_companiesadministrationneededinformation",
    ldv_errorcodeid: "ldv_errorcodeid",
    customerid: "customerid",
    ldv_complaintypecode: "ldv_complaintypecode",
    ldv_prioritycode: "ldv_prioritycode",
    ldv_ministryshajjagencydecisisoncode: "ldv_ministryshajjagencydecisisoncode",
    ldv_ministryshajjagencyneededinformation: "ldv_ministryshajjagencyneededinformation",
    ldv_umrahscompanyservicedecisioncode: "ldv_umrahscompanyservicedecisioncode",
    ldv_umrahscompanyserviceneededinformation: "ldv_umrahscompanyserviceneededinformation",


    Enums: {
        errorRecord: {
            name: "التطبيق",
            id: "0B6E8AD6-98D4-EE11-904D-6045BD8C9FF4"
        },
        departmentDecision: {
            closeTheTicket: 1,
            needMoreDetails: 2
        },
        qualityDecision: {
            closeTheTicket: 1,
            needMoreDetails: 2
        },
        qualityDecision2: {
            closeTheTicket: 1,
            transferTheTicket: 2
        }
    },



};

var ServiceType = {

    FinancialComplainInternalPilgrimspostHajj: {
        serviceDefinitionId: "8580A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - Internal Pilgrims post-Hajj"
    },
    FinancialCompensationComplain: {
        serviceDefinitionId: "8780A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial compensation complain"
    },
    TechnicalComplainMomentaryUmrahForCompanies: {
        serviceDefinitionId: "DE69552C-B0D0-EE11-9079-6045BD895E74",
        serviceDefinitionName: "Technical complain - Momentary Umrah for companies"
    },
    Inquiry:{
        serviceDefinitionId: "E8015016-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName:"Inquiry"
    },
    Suggestions: {
        serviceDefinitionId: "E5C52C61-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName:"Suggestions"
    }

};

var BPFs = {
    FinancialComplainInternalPilgrimspostHajj: {
        name: "BPF | 5-FC- Internal Pilgrims post-Hajj",
        id: "0E85B287-591B-481A-8DFF-18C940C7D433",
        stages: {
            submit: {
                name: "Submit",
                id: "7cd10b41-cf7a-4bc3-9406-9029347c5de2"
            },
            companiesAdminstration: {
                name: "Companies Administration",
                id: "a37f5442-2123-4ef6-a63c-f4875085e8ca"
            },
            quality: {
                name: "Quality",
                id: "5209c457-21e1-420c-830a-5c83466cdf69"
            },
            resolved: {
                name: "Resolved",
                id: "340df39c-8893-4c60-a86f-e3ca5300747e"
            }

        }
    },

    FinancialCompensationComplain: {
        name: "BPF | 6-Financial compensation complain",
        id: "412D36C6-FC36-475C-B679-DCFFC1856326",
        stages: {
            submit: {
                name: "Submit",
                id: "a677a23e-a96f-423d-8547-606e433f61cf"
            },
            ministrySHajjAgency: {
                name: "Ministry’S Hajj Agency",
                id: "5fafb075-cca2-4359-8431-806679d70732"
            },
            quality: {
                name: "Quality",
                id: "14a664e8-21a5-4027-9145-dc8e00e8d5f0"
            },
            resolved: {
                name: "Resolved",
                id: "66ea1212-338a-4005-a079-2c580717953a"
            }

        }
    },


    TechnicalComplainMomentaryUmrahForCompanies: {
        name: "BPF | 7-TC- Momentary Umrah for companies",
        id: "62A95EB9-D108-4B71-BE1E-4D786F685ED3",
        stages: {
            submit: {
                name: "Submit",
                id: "054a0655-cbe0-4c94-9797-05da8bfd567d"
            },
            umrahsCompanyService: {
                name: "Umrah's Company Service",
                id: "cef9ddbe-0e55-40df-9850-6c8ef2adf962"
            },
            quality: {
                name: "Quality",
                id: "113fe11a-3404-4a03-bcb8-72bdefa9cfc7"
            },
            resolved: {
                name: "Resolved",
                id: "c6a62666-af5d-4e1d-9527-8360e6ed868c"
            }

        }
    },

    Inquiry: {
        name: "BPF | Inquiry Request Process",
        id: "371D39F9-85D8-4DBF-A96C-16FD839D3B27",
        stages: {
            submit: {
                name: "Submit",
                id: "c366cb3d-f8a7-48cb-b299-d892f32c0f3a"
            },
            quality: {
                name: "Quality",
                id: "2875d1ba-fd30-4725-a786-fb64202184bb"
            },
            resolved: {
                name: "Resolved",
                id: "317f7384-9be0-46ae-94e2-2c69981c373f"
            }

        }
    },

    Suggestions: {
        name: "BPF | Suggestions Request Process",
        id: "0E95C412-83A2-46F8-8DDB-D18543CE4C77",
        stages: {
            submit: {
                name: "Submit",
                id: "b9e77c7f-cb09-4dfe-b306-8ea4fcad4a4c"
            },
            quality: {
                name: "Quality",
                id: "3621d5b3-d55f-4081-a561-68cce60f34d3"
            },
            concerned_department: {
                name: "Concerned Department",
                id: "c652bb0d-4cc4-4862-afba-b5a11a3cfc6d"
            },
            quality_2: {
                name: "Quality_2",
                id: "183022ef-9135-4a8d-9bd5-f85cc97355c7"
            },
            resolved: {
                name: "Resolved",
                id: "abce898f-a2c2-4cf9-94fa-de3e5ca47704"
            }
        }
    },
};

function OnLoad(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();

    HideUCIButtons();
    HandelBPF(executionContext);
    GetDecisionsBasedOnService_OnLoad(formContext);
    OnChange_MainCategory(formContext);
    OnChange_SubCategory(formContext);
    HideAndShowComplainTypeAndPriority(formContext);
    LockFormFieldsAfterSubmit(formContext);
    formContext.getAttribute(caseFields.ldv_serviceid).addOnChange(function () {
        OnChange_RequestType(formContext);

    });

    formContext.getAttribute(caseFields.ldv_processid).addOnChange(function () {
        OnChange_ProcessId(formContext);
    });

    formContext.getAttribute(caseFields.ldv_maincategoryid).addOnChange(function () {
        OnChange_MainCategory(formContext);

    });

    formContext.getAttribute(caseFields.ldv_subcategoryid).addOnChange(function () {
        OnChange_SubCategory(formContext);
    });
    formContext.getAttribute(caseFields.ldv_secondarysubcategoryid).addOnChange(function () {
        CommonGeneric.EmptyField(formContext, caseFields.ldv_errorcodeid);
    });


};

function OnSave(executionContext) {
    var saveEvent = executionContext.getEventArgs();
};

function SetProcessFieldFromService(formContext) {
    debugger;
    //getting service record with process field
    var serviceRecord = GetLookUpRecordWithExpandedValues(formContext, caseFields.ldv_serviceid, "$select=ldv_name,ldv_iscomplain&$expand=ldv_processid($select=name)");
    if (serviceRecord != null) {
        var processLookup = serviceRecord["ldv_processid"];
        if (processLookup != null) {
            // Accessing the process ID and name directly from the expanded entity
            var processId = processLookup.workflowid;
            var processName = processLookup.name;

            // Check if the bpfName field is empty or different from the new value
            var bpfNameAttr = formContext.getAttribute(caseFields.ldv_processid);
            if (bpfNameAttr !== null && bpfNameAttr !== undefined) {
                var currentBpfName = bpfNameAttr.getValue();
                if (currentBpfName === null || currentBpfName !== processName) {
                    // Set the new value for the bpfName field
                    CommonGeneric.SetLookupRecord(formContext, caseFields.ldv_processid, processId, "workflow", processName);
                    formContext.getAttribute(caseFields.ldv_processid).fireOnChange();

                }
            }
        } else {
            console.log("ldv_processid field is not populated on the service record.");
        }

    };
};

function HideAndShowComplainTypeAndPriority(formContext) {
    var serviceRecord = GetLookUpRecordWithExpandedValues(formContext, caseFields.ldv_serviceid, "$select=ldv_name,ldv_iscomplain");
    if (serviceRecord != null) {
        var isComplain = serviceRecord["ldv_iscomplain"];

        if (isComplain === true) {

            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_prioritycode, true, true);
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_complaintypecode, true, true);

        } else {
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_prioritycode, false, false);
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_complaintypecode, true, false);
        }
    } else {
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_prioritycode, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_complaintypecode, true, false);
    }
};
function OnChange_RequestType(formContext) {
    SetProcessFieldFromService(formContext);
    HideAndShowComplainTypeAndPriority(formContext);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_maincategoryid, caseFields.ldv_serviceid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_serviceid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_serviceid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_errorcodeid, caseFields.ldv_serviceid);
    CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_maincategoryid, caseFields.ldv_serviceid);


};
function OnChange_ProcessId(formContext) {
    debugger;
    SwitchBPF(formContext, caseFields.ldv_processid, () => { });

};
function OnChange_MainCategory(formContext) {
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_maincategoryid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_maincategoryid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_errorcodeid, caseFields.ldv_maincategoryid);
    CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_maincategoryid);
    showHideSubcategoryField(formContext);
};
function OnChange_SubCategory(formContext) {
    debugger;
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_subcategoryid);
    CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_subcategoryid);

    var subCategory = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_subcategoryid);
    if (subCategory != null && subCategory != undefined) {
        var subCategoryId = subCategory.id.replace('{', '').replace('}', '').toLowerCase();
        if (subCategoryId === caseFields.Enums.errorRecord.id.toLowerCase()) {
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_errorcodeid, true, true);
        }
        else {
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_errorcodeid, false, false);
        }
    }
    else {
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_errorcodeid, false, false);

    }

    showHideSecondarySubcategoryField(formContext);
};

function showHideSubcategoryField(formContext) {
    var mainCategoryId = formContext.getAttribute(caseFields.ldv_maincategoryid).getValue();

    if (mainCategoryId) {
        var mainCategoryIdWithoutBrackets = mainCategoryId[0].id.replace("{", "").replace("}", "");

        // Construct the query to check for related "Secondary Subcategory" records
        var fetchXml =
            "<fetch count='1'>" +
            "<entity name='ldv_casecategory'>" +
            "<filter>" +
            "<condition attribute='ldv_parentcategoryid' operator='eq' value='" + mainCategoryIdWithoutBrackets + "' />" +
            "</filter>" +
            "</entity>" +
            "</fetch>";

        // Use Xrm.WebApi to retrieve records
        Xrm.WebApi.retrieveMultipleRecords("ldv_casecategory", "?fetchXml=" + encodeURIComponent(fetchXml))
            .then(function (results) {
                if (results.entities.length > 0) {
                    // If related records exist, show and make the "Secondary Subcategory" field mandatory
                    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_subcategoryid, true, true);

                } else {
                    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_subcategoryid, false, false);

                }
            }, function (error) {
                console.log("Error retrieving related records: " + error.message);
            });
    } else {
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_subcategoryid, false, false);

    }
};

function showHideSecondarySubcategoryField(formContext) {
    var subCategoryId = formContext.getAttribute(caseFields.ldv_subcategoryid).getValue();

    if (subCategoryId) {
        var subCategoryIdWithoutBrackets = subCategoryId[0].id.replace("{", "").replace("}", "");

        // Construct the query to check for related "Secondary Subcategory" records
        var fetchXml =
            "<fetch count='1'>" +
            "<entity name='ldv_casecategory'>" +
            "<filter>" +
            "<condition attribute='ldv_subcategoryid' operator='eq' value='" + subCategoryIdWithoutBrackets + "' />" +
            "</filter>" +
            "</entity>" +
            "</fetch>";

        // Use Xrm.WebApi to retrieve records
        Xrm.WebApi.retrieveMultipleRecords("ldv_casecategory", "?fetchXml=" + encodeURIComponent(fetchXml))
            .then(function (results) {
                if (results.entities.length > 0) {
                    // If related records exist, show and make the "Secondary Subcategory" field mandatory
                    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_secondarysubcategoryid, true, true);

                } else {
                    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_secondarysubcategoryid, false, false);

                }
            }, function (error) {
                console.log("Error retrieving related records: " + error.message);
            });
    } else {
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_secondarysubcategoryid, false, false);

    }
};

function LockFormFieldsAfterSubmit(formContext) {

    var currentStageName = formContext.data.process.getActiveStage().getName();

    if (currentStageName !== null && currentStageName !== undefined) {
        if (formContext.ui.getFormType() !== 1 && currentStageName != "Submit") {
            CommonGeneric.LockFormControlsExecptBPF(formContext);
        };
    }

};

function HandelBPF(executionContext) {
    var formContext = executionContext.getFormContext();

    //in case we want to hide the bpf in the create >>>>
    //if (formContext.ui.getFormType() === 1) // if form is create
    //{
    //    CommonGeneric.hideBusinessProcessFlow(formContext);
    //} else {
    //    SwitchBPF(formContext, caseFields.ldv_processid, () => { });
    //    CommonGeneric.BusinessProcessFlowTools.Disable_InactiveBPFStages(executionContext, () => { });
    //    HideBPFButtons(formContext);



    //}


    //Draw BPF Based on the process

    SwitchBPF(formContext, caseFields.ldv_processid, () => { });

    //lock inactive Stages Fields
    CommonGeneric.BusinessProcessFlowTools.Disable_InactiveBPFStages(executionContext, () => { });

    formContext.data.process.addOnStageChange(function () {
        OnChange_addOnStageChange(executionContext);
    });

    formContext.data.process.addOnStageSelected(function () {
        OnChange_addOnStageChange(executionContext);

        //Lock isSubmitted field in create
        if (formContext.ui.getFormType() === 1) // if form is create
        {
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_issubmitted, caseFields.createdon);
        };

        HideBPFNextButtonForSubmitStage(formContext);

    });

};
function HideBPFNextButtonForSubmitStage(formContext) {
    var currentStageName = formContext.data.process.getActiveStage().getName();
    if (currentStageName === "Submit") {
        CommonGeneric.HideNextStageUCI();
    }
};
function OnChange_addOnStageChange(executionContext) {
    debugger;
    var formContext = executionContext.getFormContext()
    HideUCIButtons(executionContext);
    GetActiveStageDecisionsBasedOnService(executionContext, formContext);

};


// #region Decisions
function GetActiveStageDecisionsBasedOnService(executionContext) {
    debugger;
    var formContext = executionContext.getFormContext();
    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
    var currentStage = formContext.data.process.getActiveStage();

    if (!service || !currentStage) {
        console.error("Service or current stage is null or undefined.");
        return;
    };

    var serviceId = service[0].id.replace('{', '').replace('}', '').toLowerCase();


    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    };

    var currentStageId = currentStage.getId().toLowerCase();

    switch (serviceId) {

        case ServiceType.FinancialComplainInternalPilgrimspostHajj.serviceDefinitionId.toLowerCase():

            var umrahsCompanyServiceStageId = BPFs.FinancialComplainInternalPilgrimspostHajj.stages.companiesAdminstration.id.toLowerCase();
            var qualityStageId = BPFs.FinancialComplainInternalPilgrimspostHajj.stages.quality.id.toLowerCase();

            if (currentStageId === umrahsCompanyServiceStageId) {

                FC_InternalPilgrimspostHajj_CompaniesAdministrationStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_companiesadministrationdecisioncode).addOnChange(function () {
                    FC_InternalPilgrimspostHajj_CompaniesAdministrationStageDecision_OnChange(executionContext);
                });
            };

            if (currentStageId === qualityStageId) {

                FC_InternalPilgrimspostHajj_QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualityofficerdecisioncode).addOnChange(function () {
                    FC_InternalPilgrimspostHajj_QualityStageDecision_OnChange(executionContext);
                });
            };
            break;

        case ServiceType.FinancialCompensationComplain.serviceDefinitionId.toLowerCase():

            var umrahsCompanyServiceStageId = BPFs.FinancialCompensationComplain.stages.ministrySHajjAgency.id.toLowerCase();
            var qualityStageId = BPFs.FinancialCompensationComplain.stages.quality.id.toLowerCase();

            if (currentStageId === umrahsCompanyServiceStageId) {

                FinancialCompensationComplain_MinistrySHajjAgencyStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_ministryshajjagencydecisisoncode).addOnChange(function () {
                    FinancialCompensationComplain_MinistrySHajjAgencyStageDecision_OnChange(executionContext);
                });
            };

            if (currentStageId === qualityStageId) {

                FinancialCompensationComplain_QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualityofficerdecisioncode).addOnChange(function () {
                    FinancialCompensationComplain_QualityStageDecision_OnChange(executionContext);
                });
            };
            break;

        case ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase():

            var umrahsCompanyServiceStageId = BPFs.TechnicalComplainMomentaryUmrahForCompanies.stages.umrahsCompanyService.id.toLowerCase();
            var qualityStageId = BPFs.TechnicalComplainMomentaryUmrahForCompanies.stages.quality.id.toLowerCase();

            if (currentStageId === umrahsCompanyServiceStageId) {

                TechnicalComplainMomentaryUmrahForCompanies_UmrahsCompanyServiceStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_umrahscompanyservicedecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryUmrahForCompanies_UmrahsCompanyServiceStageDecision_OnChange(executionContext);
                });
            };

            if (currentStageId === qualityStageId) {

                TechnicalComplainMomentaryUmrahForCompanies_QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualityofficerdecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryUmrahForCompanies_QualityStageDecision_OnChange(executionContext);
                });
            };
            break;

        case ServiceType.Inquiry.serviceDefinitionId.toLowerCase():
            var qualityStageId = BPFs.Inquiry.stages.quality.id.toLowerCase();

            if (currentStageId === qualityStageId) {
                Inquiry_QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualityofficerdecisioncode2).addOnChange(function () {
                    Inquiry_QualityStageDecision_OnChange(executionContext);
                })
            }
            break;

        case ServiceType.Suggestions.serviceDefinitionId.toLowerCase():
            var suggestionQualityStageId = BPFs.Suggestions.stages.quality.id.toLowerCase();

            if (currentStageId === suggestionQualityStageId) {
                ShowNextStageUCI(executionContext);
            }
            break;
    };



};
function GetDecisionsBasedOnService_OnLoad(formContext) {

    debugger;

    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
    if (!service) {
        console.error("Service is null or undefined.");
        return;
    };
    var serviceId = service[0].id.replace('{', '').replace('}', '').toLowerCase();
    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    };


    switch (serviceId) {

        case ServiceType.FinancialComplainInternalPilgrimspostHajj.serviceDefinitionId.toLowerCase():
            FC_InternalPilgrimspostHajj_OnLoad(formContext);
            break;

        case ServiceType.FinancialCompensationComplain.serviceDefinitionId.toLowerCase():
            FinancialCompensationComplain_OnLoad(formContext);
            break;

        case ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase():
            TechnicalComplainMomentaryUmrahForCompanies_OnLoad(formContext);
            break;

        case ServiceType.Inquiry.serviceDefinitionId.toLowerCase():
            Inquiry_QualityStageDecision_OnLoad(formContext);
            break;

    };

};

// #region FC Internal Pilgrims post Hajj service
function FC_InternalPilgrimspostHajj_CompaniesAdministrationStageDecision_OnChange(executionContext) {
    var departmentNeedInfoOnBpf = "header_process_" + caseFields.ldv_companiesadministrationneededinformation;
    var departmentClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnChange(executionContext, caseFields.ldv_companiesadministrationdecisioncode, departmentNeedInfoOnBpf, departmentClosureReasonOnBpf);
};
function FC_InternalPilgrimspostHajj_QualityStageDecision_OnChange(executionContext) {
    var qualityNeedInfoOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation;
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    DepartmentQualityDecision_OnChange(executionContext, caseFields.ldv_qualityofficerdecisioncode, qualityNeedInfoOnBpf, qualityClosureReasonOnBpf);
};

function FC_InternalPilgrimspostHajj_OnLoad(formContext) {
    var departmentNeedInfoOnBpf = "header_process_" + caseFields.ldv_companiesadministrationneededinformation;
    var departmentClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    var qualityNeedInfoOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation;
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;

    DepartmentDecision_OnLoad(formContext, caseFields.ldv_companiesadministrationdecisioncode, departmentNeedInfoOnBpf, departmentClosureReasonOnBpf);
    DepartmentQualityDecision_OnLoad(formContext, caseFields.ldv_qualityofficerdecisioncode, qualityNeedInfoOnBpf, qualityClosureReasonOnBpf);
};

//#endregion

// #region Financial compensation complain
function FinancialCompensationComplain_MinistrySHajjAgencyStageDecision_OnChange(executionContext) {
    var ministryHajjNeedInfoOnBpf = "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var ministryHajjClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnChange(executionContext, caseFields.ldv_ministryshajjagencydecisisoncode, ministryHajjNeedInfoOnBpf, ministryHajjClosureReasonOnBpf);
};
function FinancialCompensationComplain_QualityStageDecision_OnChange(executionContext) {
    var qualityNeedInfoOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation;
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    DepartmentQualityDecision_OnChange(executionContext, caseFields.ldv_qualityofficerdecisioncode, qualityNeedInfoOnBpf, qualityClosureReasonOnBpf);
};

function FinancialCompensationComplain_OnLoad(formContext) {
    var ministryHajjNeedInfoOnBpf = "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var ministryHajjClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    var qualityNeedInfoOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation;
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;

    DepartmentDecision_OnLoad(formContext, caseFields.ldv_ministryshajjagencydecisisoncode, ministryHajjNeedInfoOnBpf, ministryHajjClosureReasonOnBpf);
    DepartmentQualityDecision_OnLoad(formContext, caseFields.ldv_qualityofficerdecisioncode, qualityNeedInfoOnBpf, qualityClosureReasonOnBpf);
};

//#endregion

// #region Financial compensation complain
function TechnicalComplainMomentaryUmrahForCompanies_UmrahsCompanyServiceStageDecision_OnChange(executionContext) {
    var umrahsCompanyNeedInfoOnBpf = "header_process_" + caseFields.ldv_umrahscompanyserviceneededinformation;
    var umrahsCompanyClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnChange(executionContext, caseFields.ldv_umrahscompanyservicedecisioncode, umrahsCompanyNeedInfoOnBpf, umrahsCompanyClosureReasonOnBpf);
};
function TechnicalComplainMomentaryUmrahForCompanies_QualityStageDecision_OnChange(executionContext) {
    var qualityNeedInfoOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation;
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    DepartmentQualityDecision_OnChange(executionContext, caseFields.ldv_qualityofficerdecisioncode, qualityNeedInfoOnBpf, qualityClosureReasonOnBpf);
};

function TechnicalComplainMomentaryUmrahForCompanies_OnLoad(formContext) {
    var umrahsCompanyNeedInfoOnBpf = "header_process_" + caseFields.ldv_umrahscompanyserviceneededinformation;
    var umrahsCompanyClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    var qualityNeedInfoOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation;
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;

    DepartmentDecision_OnLoad(formContext, caseFields.ldv_umrahscompanyservicedecisioncode, umrahsCompanyNeedInfoOnBpf, umrahsCompanyClosureReasonOnBpf);
    DepartmentQualityDecision_OnLoad(formContext, caseFields.ldv_qualityofficerdecisioncode, qualityNeedInfoOnBpf, qualityClosureReasonOnBpf);
};

//#endregion

// #region Inquiry
function Inquiry_QualityStageDecision_OnChange(executionContext) {
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    QualityOfficerDecision2_OnChange(executionContext, caseFields.ldv_qualityofficerdecisioncode2, qualityClosureReasonOnBpf);
};

function Inquiry_QualityStageDecision_OnLoad(formContext) {
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    QualityOfficerDecision2_OnLoad(formContext, caseFields.ldv_qualityofficerdecisioncode2,qualityClosureReasonOnBpf)
};
//#endregion

// #region Suggestions
function Suggestions_QualityStageDecision_OnChange(executionContext) {
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
}
//#endregion




// #region Common Functions For Decisions

//if need more information >> hide next and back buttons and the agent assign it manually
function DepartmentDecision_OnChange(executionContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf) {
    debugger;
    var formContext = executionContext.getFormContext();
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }



    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        var needInfoSchemaNameOnForm = getLogicalFieldName(formContext, needInformationSchemaNameOnBpf);
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
        }
        ShowNextStage(executionContext);

    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) { //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        var closureReasonSchemaNameOnForm = getLogicalFieldName(formContext, closureReasonSchemaNameOnBpf);
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        HideUCIButtons(executionContext);

    }

};
function DepartmentDecision_OnLoad(formContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf) {
    debugger;
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }



    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);

    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) { //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);


    }

};

//if need more information >> hide next and Show back button to go to the previous stage
function DepartmentQualityDecision_OnChange(executionContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf) {
    debugger;
    var formContext = executionContext.getFormContext();
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }



    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        var needInfoSchemaNameOnForm = getLogicalFieldName(formContext, needInformationSchemaNameOnBpf);
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
        }
        ShowNextStage(executionContext);


    } else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) { //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        var closureReasonSchemaNameOnForm = getLogicalFieldName(formContext, closureReasonSchemaNameOnBpf);
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }

        ShowPreviousStage(executionContext);



    }

};
function DepartmentQualityDecision_OnLoad(formContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf) {
    debugger;
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }



    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);


    } else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) { //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);



    }

};

//if Transfer >> hide next and back buttons and the agent assign it manually
function QualityOfficerDecision2_OnChange(executionContext, decisionSchemaName, closureReasonSchemaNameOnBpf) {
    debugger;
    var formContext = executionContext.getFormContext();

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }

    if (decision === caseFields.Enums.qualityDecision2.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        
        ShowNextStage(executionContext);


    } else if (decision === caseFields.Enums.qualityDecision2.transferTheTicket) { //Trnasfer 

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        var closureReasonSchemaNameOnForm = getLogicalFieldName(formContext, closureReasonSchemaNameOnBpf);
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        HideUCIButtons(executionContext);

    }

};
function QualityOfficerDecision2_OnLoad(formContext, decisionSchemaName, closureReasonSchemaNameOnBpf) {
    debugger;
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }

    if (decision === caseFields.Enums.qualityDecision2.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);

    } else if (decision === caseFields.Enums.qualityDecision2.transferTheTicket) { //Trnasfer 

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);

    }

};

// #endregion

// #endregion


// #region Helpers
function SwitchBPF(formContext, processIdField_Custom, callBackFn) {

    let currentBPF = formContext.data.process.getActiveProcess();
    let newBPF = formContext.getAttribute(processIdField_Custom).getValue();

    if (currentBPF !== null && newBPF !== null) {
        let currentBPFId = currentBPF.getId();
        let newBPFId = newBPF[0].id.replace("{", "").replace("}", "");
        if (currentBPFId !== null && newBPFId !== null && currentBPFId.toLowerCase() !== newBPFId.toLowerCase()) {
            formContext.data.process.setActiveProcess(newBPFId, callBackFn);
        }
    } else if (currentBPF === null && newBPF !== null) {
        let newBPFId = newBPF[0].id.replace("{", "").replace("}", "");
        if (newBPFId !== null) {
            formContext.data.process.setActiveProcess(newBPFId, callBackFn);
        }
    }
};

function GetLookUpRecordWithExpandedValues(formContext, lookupField, expandField) {
    debugger;
    var lookupRecord = CommonGeneric.GetLookUpRecord(formContext, lookupField);
    if (lookupRecord != null) {
        var entityId = lookupRecord.id;
        var lookupLogicalName = lookupRecord.entityType;

        entityId = entityId.replace('{', '').replace('}', '');

        // Construct URL with expand parameter and entityId
        var entityName = lookupLogicalName.toLowerCase() + "s";
        var url = Xrm.Utility.getGlobalContext().getClientUrl() + "/api/data/v9.0/" + entityName + "(" + entityId + ")";
        if (expandField) {
            url += "?" + expandField;
        }

        // Send a synchronous request to retrieve expanded lookup value
        var req = new XMLHttpRequest();
        req.open("GET", url, false);
        req.setRequestHeader("OData-MaxVersion", "4.0");
        req.setRequestHeader("OData-Version", "4.0");
        req.setRequestHeader("Accept", "application/json");
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        req.setRequestHeader("Prefer", "odata.include-annotations=\"*\"");
        req.send(null);

        if (req.status === 200) {
            var result = JSON.parse(req.responseText);
            return result;
        } else {
            console.log("Error occurred while retrieving expanded lookup value: " + req.statusText);
            return null;
        }
    }
    return null;
};

function getLogicalFieldName(formContext, schemaName) {
    var logicalFieldName = null;
    if (schemaName) {
        // Remove the "header_process_" prefix if present
        logicalFieldName = schemaName.replace("header_process_", "");
        // Check if the field exists without suffix
        if (!formContext.getControl(logicalFieldName)) {
            // If not found, check for the field with suffix "_number"
            var suffixIndex = schemaName.lastIndexOf("_");
            if (suffixIndex !== -1) {
                logicalFieldName = schemaName.substring(0, suffixIndex);
            }
        }
    }
    return logicalFieldName;
}



///////////////////////////////BPF Buttons
HideBPFButtonsUCI = function (executionContext) {
    ShowNextStageUCI(true);
    ShowPreviusButtonInUCI(false);
    ShowSetActiveButtonInUCI(false);
    ShowFinishButtonInUCI(false);
    onClickStageInUCI(executionContext, true, false);
};
onClickStageInUCI = function (executionContext, isShowNext = false, ShowPrevius = false) {
    try {
        $(window.parent.document).find("body").on("click",
            "[data-id='MscrmControls.Containers.ProcessBreadCrumb-headerStageContainer'] li",
            function () {

                setTimeout(function () {
                    ShowNextStageUCI(isShowNext);
                    ShowPreviusButtonInUCI(ShowPrevius);
                    ShowSetActiveButtonInUCI(false);
                    ShowFinishButtonInUCI(false);
                },
                    50);
            });
    } catch (e) {
        console.log("onClickStageInUCI" + e.errorMessage);
    }
};
ShowNextStageUCI = function (isVisible = true) {
    try {

        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-nextButtonContainer']")
                .find('button').each(function (index) {
                    alert(this);
                    isVisible ? $(this).show() : $(this).hide();
                });
        },
            100);

        var interval = setInterval(function () {
            var element5 = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-nextButtonContainer");
            if (element5 != null) {

                element5.style.visibility = isVisible ? "visible" : "hidden";
                clearInterval(interval);
            }
        }, 150);
    } catch (e) {
        console.log("ShowNextStageUCI" + e.errorMessage);
    }

    //MscrmControls.Containers.ProcessStageControl-nextButtonContainerbuttonInnerContainer

};
ShowPreviusButtonInUCI = function (isVisible = true) {
    try {
        setTimeout(
            function () {
                $(window.parent.document).find("[data-id = 'MscrmControls.Containers.ProcessStageControl-previousButtonContainer']").find('button').each(
                    function (index) {
                        if (this.ariaLabel == "Back") {
                            isVisible ? $(this).show() : $(this).hide();
                            return;
                        }
                    });
            }, 100);

        var intervalForBackButton = setInterval(
            function () {
                var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-previousButtonContainer");
                if (processStageFooter != null) {
                    var previousButtonElement = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-previousButtonContainer");
                    if (previousButtonElement != null) {

                        previousButtonElement.style.visibility = isVisible ? "visible" : "hidden";
                        previousButtonElement.style.display = isVisible ? "" : "none";
                        clearInterval(intervalForBackButton);
                    }
                }
            }, 150);
    } catch (e) {
        console.log("ShowPreviusButtonInUCI" + e.errorMessage);
    }

};
ShowSetActiveButtonInUCI = function (isVisible = true) {
    try {
        setTimeout(
            function () {
                $(window.parent.document).find("[data-id = 'MscrmControls.Containers.ProcessStageControl-businessProcessFlowFlyoutFooterContainer']").find('button').each(
                    function (index) {
                        if (this.ariaLabel == "Set Active") {
                            isVisible ? $(this).show() : $(this).hide();
                            return;
                        }
                    });
            }, 100);
        var hide = false;
        var intervalForBackButton = setInterval(
            function () {
                var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-businessProcessFlowFlyoutFooterContainer");
                if (processStageFooter != null) {
                    var setActiveButtonElement = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer");
                    if (setActiveButtonElement != null) {
                        hide = true;
                        setActiveButtonElement.style.visibility = isVisible ? "visible" : "hidden";
                        clearInterval(intervalForBackButton);
                    }
                }
            }, 150);
    } catch (e) {
        console.log("ShowSetActiveButtonInUCI" + e.errorMessage);
    }

};
ShowFinishButtonInUCI = function (isVisible = false) {

    try {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-finishButtonContainerbuttonInnerContainer']")
                .find('button').each(function (index) {
                    alert(this);
                    isVisible ? $(this).show() : $(this).hide();
                });
        },
            100);

        var interval = setInterval(function () {
            var element5 = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-finishButtonContainer");
            if (element5 != null) {

                element5.style.visibility = isVisible ? "visible" : "hidden";
                clearInterval(interval);
            }
        }, 150);
    } catch (e) {
        console.log("ShowFinishButtonInUCI" + e.errorMessage);
    }

};
hideDockModeButtonUCI = function () {
    setTimeout(function () {
        $(window.parent.document)
            .find(
                "[data-id = 'MscrmControls.Containers.ProcessStageControl-stageDockModeButton']")
            .find('button').each(function (index) {
                if (this.id === "MscrmControls.Containers.ProcessStageControl-stageDockModeButton") {
                    $(this).hide();
                    return;
                }
            });
    },
        100);
    var hide = false;
    var intervalForBackButton = setInterval(function () {
        var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-stageDockModeButton");
        if (processStageFooter != null) {
            var previousButtonElement =
                parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-stageDockModeButton");
            if (previousButtonElement != null) {
                hide = true;
                previousButtonElement.style.display = "none";
                clearInterval(intervalForBackButton);
            }
        }
    }, 150);
};


/////
function ShowNextStage(executionContext) {
    ShowNextStageUCI(true);
    ShowPreviusButtonInUCI(false);
    onClickStageInUCI(executionContext, true, false);
    ShowFinishButtonInUCI(false);
    ShowSetActiveButtonInUCI(false);
    hideDockModeButtonUCI()
};
function ShowPreviousStage(executionContext) {
    ShowNextStageUCI(false);
    ShowPreviusButtonInUCI(true);
    onClickStageInUCI(executionContext, false, true);
    ShowFinishButtonInUCI(false);
    ShowSetActiveButtonInUCI(false);
    hideDockModeButtonUCI()
};
function HideUCIButtons(executionContext) {
    ShowNextStageUCI(false);
    ShowPreviusButtonInUCI(false);
    onClickStageInUCI(executionContext, false, false);
    ShowFinishButtonInUCI(false);
    ShowSetActiveButtonInUCI(false);
    hideDockModeButtonUCI()
};

// #endregion



