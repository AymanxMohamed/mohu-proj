var surveyFields = {
    caseorigincode: "caseorigincode",
    ldv_processid: "ldv_processid",
    ldv_serviceid: "ldv_serviceid",
    ldv_cxdecisioncode: "ldv_cxdecisioncode",
    ldv_closurereason: "ldv_closurereason",

    Enums: {
        cxDecisions: {
            close: 1,
            createNewTicket: 2,
        }
    }
}

var ServiceType = {
    FinancialComplainInternalPilgrimspostHajj: {
        serviceDefinitionId: "8580A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - Internal Pilgrims post-Hajj",
        serviceDefinitionName: "Financial complain - Internal Pilgrims post-Hajj",
    },
    FinancialCompensationComplain: {
        serviceDefinitionId: "8780a868-2dcc-ee11-907a-6045bd8c92a2",
        serviceDefinitionName: "Financial compensation complain",
    },

    TechnicalComplainMomentaryHajj: {
        serviceDefinitionId: "7b80a868-2dcc-ee11-907a-6045bd8c92a2",
        serviceDefinitionName: "Technical complain - Momentary Hajj",
    },
    TechnicalComplainCompanyService: {
        serviceDefinitionId: "7980a868-2dcc-ee11-907a-6045bd8c92a2",
        serviceDefinitionName: "Technical complain - Momentary Umrah",
    },
    TechnicalComplainMomentaryUmrahForCompanies: {
        serviceDefinitionId: "DE69552C-B0D0-EE11-9079-6045BD895E74",
        serviceDefinitionName: "Technical complain - Momentary Umrah for companies",
    },
    TechnicalComplainNonMomentaryHajj: {
        serviceDefinitionId: "7D80A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technical complain - Non-momentary Hajj",
    },
    TechnicalComplainNonMomentaryUmrah: {
        serviceDefinitionId: "7780A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technical complain - Non-momentary Umrah",
    },
    Inquiry: {
        serviceDefinitionId: "E8015016-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName: "Inquiry",
    },
    Suggestions: {
        serviceDefinitionId: "E5C52C61-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName: "Suggestions",
    },
    BeneficiaryDissatisfactionService: {
        serviceDefinitionId: "B8F53FA2-F091-EF11-8A69-6045BD8FAE55",
        serviceDefinitionName: "Survey - Beneficiary Dissatisfaction Service",
    },

    //Integration
    BusinessSectorComplain: {
        serviceDefinitionId: "8980A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Business sector Complain",
    },
    FinancialComplainExternalPilgrims: {
        serviceDefinitionId: "8380A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - External Pilgrims",
    },
    FinancialComplainInternalPilgrims: {
        serviceDefinitionId: "8180A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - Internal Pilgrims",
    },
    FinancialComplainNusukServices: {
        serviceDefinitionId: "7F80A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - Nusuk services",
    },
    TechnologicalComplain: {
        serviceDefinitionId: "7580A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technological Complain",
    },
};


var BPFs = {
    SurveyService: {
        name: "BPF | Survey Service",
        id: "E70B75EF-8A8C-EF11-AC21-6045BDA22907",
        stages: {
            cx: {
                name: "CX",
                id: "9829ddf4-3beb-41c4-a6db-209e5229c7e2",
            },
            customerApproval: {
                name: "Customer Approval",
                id: "425b4a7b-29f5-4ab2-a61a-17f66fd444c0",
            },
            closed: {
                name: "Closed",
                id: "68b1e19f-7495-4044-910f-246084a55c1c",
            },
        },
    },
}


var Tabs = {
    tab_requestinformation: "tab_requestinformation",
    tab_administiration: "tab_administiration",
};


var Sections = {
};


function OnLoad(executionContext) {
    debugger;
    //const formContext = executionContext.getFormContext();
    HandleBPF(executionContext, GetDecisionsBasedOnService_OnLoad);

}

function addOnChangeHandlerEvents(executionContext) {
    var formContext = executionContext.getFormContext();

    //formContext.getAttribute(caseFields.ldv_serviceid).addOnChange(function () {
    //    OnChange_RequestType(formContext);
    //});

}

function HandleBPF(executionContext, callback) {
    var formContext = executionContext.getFormContext();

    // In case we want to hide the BPF in the create form
    if (formContext.ui.getFormType() === 1) {
        CommonGeneric.hideBusinessProcessFlow(formContext);
    } else {
        // Switch the BPF
        SwitchBPF(formContext, surveyFields.ldv_processid, () => {
            // Call the callBackFN after switching the BPF
            if (typeof callback === "function") {
                callback(formContext);
            }
        });
        CommonGeneric.BusinessProcessFlowTools.Disable_InactiveBPFStages(
            executionContext,
            () => { }
        );

        HideUCIButtons();

        formContext.data.process.addOnStageChange(function () {
            OnChange_addOnStageChange(executionContext);
        });
        formContext.data.process.addOnStageSelected(function () {
            OnChange_addOnStageChange(executionContext);
        });

        formContext.data.process.addOnPreStageChange(function () {
            OnChange_addOnStageChange(executionContext);
        });

        formContext.data.process.addOnPreStageChange(
            PreventDefaultSetActiveAndBackOnPreChange
        );
    }
}

function OnChange_addOnStageChange(executionContext) {
    var formContext = executionContext.getFormContext();

    HideUCIButtons(executionContext);

    GetActiveStageDecisionsBasedOnService(executionContext);
    if (formContext.ui.getFormType() !== 1) {
        CommonGeneric.DisableField(formContext, surveyFields.ldv_serviceid, true); // Lock the Request Type Field
    }
}


function PreventDefaultSetActiveAndBackOnPreChange(executionContext) {
    var formContext = executionContext.getFormContext();
    var process = formContext.data.process;
    var eventArgs = executionContext.getEventArgs();
    var userLanguage = formContext.context.getUserLcid();

    var currentStage = process.getActiveStage();
    var selectedStage = process.getSelectedStage();
    if (!currentStage || !selectedStage || !eventArgs) {
        return;
    }

    var currentStageId = currentStage.getId();
    var selectedStageId = selectedStage.getId();

    //dont allow to go back using the set active button
    if (currentStageId != selectedStageId) {
        eventArgs.preventDefault();
        if (userLanguage === 1025) {
            Xrm.Utility.alertDialog("لا يمكنك استخدام الزر تعيين نشط.");
            return;
        } else {
            Xrm.Utility.alertDialog("You cannot use the Set Active button.");
            return;
        }
    }
    if (eventArgs.getDirection() === "Previous") {
        var previousButtonLabel = parent.document.getElementById(
            "MOHUAddLabelPreviousStage"
        );
        if (previousButtonLabel == null) {
            eventArgs.preventDefault();
            if (userLanguage === 1025) {
                Xrm.Utility.alertDialog("حركة المرحلة الخلفية غير مسموح بها.");
            } else {
                Xrm.Utility.alertDialog("Back stage movement is not allowed.");
            }
        }
    }
}

//Decisions Management
function GetActiveStageDecisionsBasedOnService(executionContext) {
    var formContext = executionContext.getFormContext();
    var service = formContext.getAttribute(surveyFields.ldv_serviceid).getValue();
    var currentStage = formContext.data.process.getActiveStage();

    if (!service || !currentStage) {
        console.error("Service or current stage is null or undefined.");
        return;
    }

    var serviceId = service[0].id.replace("{", "").replace("}", "").toLowerCase();

    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    }

    var currentStageId = currentStage.getId().toLowerCase();

    switch (serviceId) {

        case ServiceType.BeneficiaryDissatisfactionService.serviceDefinitionId.toLowerCase():
            var cxStageId =
                BPFs.SurveyService.stages.cx.id.toLowerCase();
            var closeStageId =
                BPFs.SurveyService.stages.closed.id.toLowerCase();

            if (currentStageId === cxStageId) {
                GeneralCXDecision_OnChange(executionContext);
                formContext
                    .getAttribute(surveyFields.ldv_cxdecisioncode)
                    .addOnChange(function () {
                        GeneralCXDecision_OnChange(executionContext);
                    });
            }

            break;
    }
}
function GeneralCXDecision_OnChange(executionContext) {
    var cxClosureReasonOnBpf = "header_process_" + surveyFields.ldv_closurereason;
    CxDecisionWithClosureReason_OnChange(executionContext, surveyFields.ldv_cxdecisioncode, cxClosureReasonOnBpf);
}
function CxDecisionWithClosureReason_OnChange(executionContext, decisionSchemaName, closureReasonSchemaNameOnBpf) {
    debugger;
    var formContext = executionContext.getFormContext();

    if (!decisionSchemaName || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        return;
    }


    // Show and set required status for closure reason field based on decision
    if (decision === surveyFields.Enums.cxDecisions.close) {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        ShowNextStage(executionContext);
    }
    if (decision === surveyFields.Enums.cxDecisions.createNewTicket) {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        ShowNextStage(executionContext);
    }
};

function GetDecisionsBasedOnService_OnLoad(formContext) {
    debugger;
    var service = formContext.getAttribute(surveyFields.ldv_serviceid).getValue();
    if (!service) {
        console.error("Service is null or undefined.");
        return;
    }
    var serviceId = service[0].id.replace("{", "").replace("}", "").toLowerCase();
    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    }

    switch (serviceId) {

        case ServiceType.BeneficiaryDissatisfactionService.serviceDefinitionId.toLowerCase():
            BeneficiaryDissatisfactionService_OnLoad(formContext);

            break;

    }
}

function BeneficiaryDissatisfactionService_OnLoad(formContext) {
    var cxClosureReasonOnBpf =
        "header_process_" + surveyFields.ldv_closurereason;


    //CX Stage
    CxDecision_OnLoad(
        formContext,
        surveyFields.ldv_cxdecisioncode,
        cxClosureReasonOnBpf
    );
}

function CxDecision_OnLoad(
    formContext,
    decisionSchemaName,
    closureReasonSchemaNameOnBpf
) {
    if (!decisionSchemaName || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
    }
}


// UCI Controls
function ShowNextStage(executionContext) {
    ShowNextStageUCI(true);
    ShowPreviusButtonInUCI(false);
    onClickStageInUCI(executionContext, true, false);
    ShowFinishButtonInUCI(false);
    ShowSetActiveButtonInUCI(false);
    hideDockModeButtonUCI();
}
function ShowPreviousStage(executionContext) {
    ShowNextStageUCI(false);
    ShowPreviusButtonInUCI(true);
    onClickStageInUCI(executionContext, false, true);
    ShowFinishButtonInUCI(false);
    ShowSetActiveButtonInUCI(false);
    hideDockModeButtonUCI();
}
function HideUCIButtons(executionContext) {
    ShowNextStageUCI(false);
    ShowPreviusButtonInUCI(false);
    onClickStageInUCI(executionContext, false, false);
    ShowFinishButtonInUCI(false);
    ShowSetActiveButtonInUCI(false);
    hideDockModeButtonUCI();
}

ShowNextStageUCI = function (isVisible = true) {
    try {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-nextButtonContainer']"
                )
                .find("button")
                .each(function (index) {
                    alert(this);
                    isVisible ? $(this).show() : $(this).hide();
                });
        }, 100);

        var interval = setInterval(function () {
            var element5 = parent.document.getElementById(
                "MscrmControls.Containers.ProcessStageControl-nextButtonContainer"
            );
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
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-previousButtonContainer']"
                )
                .find("button")
                .each(function (index) {
                    if (this.ariaLabel == "Back") {
                        isVisible ? $(this).show() : $(this).hide();
                        return;
                    }
                });
        }, 100);
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById(
                "MscrmControls.Containers.ProcessStageControl-previousButtonContainer"
            );
            if (processStageFooter != null) {
                var previousButtonElement = parent.document.getElementById(
                    "MscrmControls.Containers.ProcessStageControl-previousButtonContainer"
                );
                if (previousButtonElement != null) {
                    previousButtonElement.style.visibility = isVisible
                        ? "visible"
                        : "hidden";
                    previousButtonElement.style.display = isVisible ? "" : "none";

                    if (isVisible) {
                        var previousButtonElementLabel = parent.document.getElementById(
                            "MscrmControls.Containers.ProcessStageControl-previousButtonContainerbuttonInnerContainer"
                        );
                        var addedLabelElement = parent.document.getElementById(
                            "MOHUAddLabelPreviousStage"
                        );
                        if (addedLabelElement == null) {
                            var Lang = Xrm.Page.context.getUserLcid();
                            if (Lang === 1033) {
                                previousButtonElementLabel.innerHTML +=
                                    "<label id='MOHUAddLabelPreviousStage' style='padding-left: 0.5em;cursor: inherit;'>Previous Stage</label>";
                                previousButtonElement.style.marginRight = "50px";
                            } else {
                                previousButtonElementLabel.innerHTML +=
                                    "<label id='MOHUAddLabelPreviousStage' style='padding-right: 0.5em;cursor: inherit;'>المرحلة السابقة</label>";
                                previousButtonElement.style.marginLeft = "50px";
                            }
                        }

                        var newLookButton = Array.from(
                            parent.document.querySelectorAll("button")
                        ).find(
                            (button) =>
                                button.getAttribute("aria-label") === "New look" ||
                                button.getAttribute("aria-label") === "جرّب المظهر الجديد"
                        );

                        if (
                            newLookButton &&
                            newLookButton.getAttribute("aria-checked") === "false"
                        ) {
                            previousButtonElement.style.width = "900%"; // Default width
                        } else {
                            previousButtonElement.style.width = "50%";
                        }
                        //previousButtonElement.style.width = "100%";
                        //previousButtonElement.style.width = '50%'; // Adjust width as needed
                        previousButtonElement.style.height = "40px"; // Adjust height as needed
                        previousButtonElement.style.display = "flex";
                    }
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
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-businessProcessFlowFlyoutFooterContainer']"
                )
                .find("button")
                .each(function (index) {
                    if (this.ariaLabel == "Set Active") {
                        isVisible ? $(this).show() : $(this).hide();
                        return;
                    }
                });
        }, 100);
        var hide = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById(
                "MscrmControls.Containers.ProcessStageControl-businessProcessFlowFlyoutFooterContainer"
            );
            if (processStageFooter != null) {
                var setActiveButtonElement = parent.document.getElementById(
                    "MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer"
                );
                if (setActiveButtonElement != null) {
                    hide = true;
                    setActiveButtonElement.style.visibility = isVisible
                        ? "visible"
                        : "hidden";
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
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-finishButtonContainerbuttonInnerContainer']"
                )
                .find("button")
                .each(function (index) {
                    alert(this);
                    isVisible ? $(this).show() : $(this).hide();
                });
        }, 100);

        var interval = setInterval(function () {
            var element5 = parent.document.getElementById(
                "MscrmControls.Containers.ProcessStageControl-finishButtonContainer"
            );
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
                "[data-id = 'MscrmControls.Containers.ProcessStageControl-stageDockModeButton']"
            )
            .find("button")
            .each(function (index) {
                if (
                    this.id ===
                    "MscrmControls.Containers.ProcessStageControl-stageDockModeButton"
                ) {
                    $(this).hide();
                    return;
                }
            });
    }, 100);
    var hide = false;
    var intervalForBackButton = setInterval(function () {
        var processStageFooter = parent.document.getElementById(
            "MscrmControls.Containers.ProcessStageControl-stageDockModeButton"
        );
        if (processStageFooter != null) {
            var previousButtonElement = parent.document.getElementById(
                "MscrmControls.Containers.ProcessStageControl-stageDockModeButton"
            );
            if (previousButtonElement != null) {
                hide = true;
                previousButtonElement.style.display = "none";
                clearInterval(intervalForBackButton);
            }
        }
    }, 150);
};

onClickStageInUCI = function (
    executionContext,
    isShowNext = false,
    ShowPrevius = false
) {
    try {
        $(window.parent.document)
            .find("body")
            .on(
                "click",
                "[data-id='MscrmControls.Containers.ProcessBreadCrumb-headerStageContainer'] li",
                function () {
                    setTimeout(function () {
                        ShowNextStageUCI(isShowNext);
                        ShowPreviusButtonInUCI(ShowPrevius);
                        ShowSetActiveButtonInUCI(false);
                        ShowFinishButtonInUCI(false);
                    }, 50);
                }
            );
    } catch (e) {
        console.log("onClickStageInUCI" + e.errorMessage);
    }
};


//General
function SwitchBPF(formContext, processIdField_Custom, callBackFn) {
    let currentBPF = formContext.data.process.getActiveProcess();
    let newBPF = formContext.getAttribute(processIdField_Custom).getValue();

    if (currentBPF !== null && newBPF !== null) {
        let currentBPFId = currentBPF.getId();
        let newBPFId = newBPF[0].id.replace("{", "").replace("}", "");
        if (
            currentBPFId !== null &&
            newBPFId !== null &&
            currentBPFId.toLowerCase() !== newBPFId.toLowerCase()
        ) {
            formContext.data.process.setActiveProcess(newBPFId, callBackFn);
        } else if (
            currentBPFId !== null &&
            newBPFId !== null &&
            currentBPFId.toLowerCase() === newBPFId.toLowerCase()
        ) {
            // If the current BPF is the same as the new BPF, execute the callback function directly
            formContext.data.refresh(false);
            callBackFn(); // Execute the callback function
        }
    } else if (currentBPF === null && newBPF !== null) {
        let newBPFId = newBPF[0].id.replace("{", "").replace("}", "");
        if (newBPFId !== null) {
            formContext.data.process.setActiveProcess(newBPFId, callBackFn);
        }
    }
}