var caseFields = {
    customerid: "customerid",
    title: "title",
    ldv_serviceid: "ldv_serviceid",
    ldv_maincategoryid: "ldv_maincategoryid",
    ldv_subcategoryid: "ldv_subcategoryid",
    ldv_secondarysubcategoryid: "ldv_secondarysubcategoryid",
    ldv_processid: "ldv_processid",
    ldv_issubmitted: "ldv_issubmitted",
    createdon: "createdon",
    ldv_seasoncode: "ldv_seasoncode",
    ldv_locationcode: "ldv_locationcode",
    ldv_beneficiarytypecode: "ldv_beneficiarytypecode",
    ldv_company: "ldv_company",
    ldv_errorcodeid: "ldv_errorcodeid",
    ldv_complaintypecode: "ldv_complaintypecode",
    ldv_prioritycode: "ldv_prioritycode",
    ldv_qualitydecisioncode: "ldv_qualitydecisioncode",
    ldv_qualityofficerdecisioncode: "ldv_qualityofficerdecisioncode",
    ldv_qualityofficerneededinformation: "ldv_qualityofficerneededinformation",
    ldv_qualityofficerdecisioncode2: "ldv_qualityofficerdecisioncode2",
    ldv_qualityofficerneededinformation2: "ldv_qualityofficerneededinformations",
    ldv_closurereason: "ldv_closurereason",
    ldv_closurereasons: "ldv_closurereasons",
    ldv_companiesadministrationdecisioncode: "ldv_companiesadministrationdecisioncode",
    ldv_companiesadministrationneededinformation: "ldv_companiesadministrationneededinformation",
    ldv_umrahscompanyservicedecisioncode: "ldv_umrahscompanyservicedecisioncode",
    ldv_umrahscompanyserviceneededinformation: "ldv_umrahscompanyserviceneededinformation",
    ldv_departmentneededinformation: "ldv_departmentneededinformation",
    ldv_departmentclosurereason: "ldv_departmentclosurereason",
    ldv_departmentdecisioncode: "ldv_departmentdecisioncode",
    ldv_supervisorid: "ldv_supervisorid",
    ldv_companiesservicedecisioncode: "ldv_companiesservicedecisioncode",
    ldv_agentemployeeneededinformation: "ldv_agentemployeeneededinformation",
    ldv_castingofficerneededinformation: "ldv_castingofficerneededinformation",

    ldv_coordinationcouncildecisioncode: "ldv_coordinationcouncildecisioncode",
    ldv_castingofficerdecisioncode: "ldv_castingofficerdecisioncode",
    ldv_agentemployeedecisioncode: "ldv_agentemployeedecisioncode",
    ldv_coordinationcouncilneededinformation: "ldv_coordinationcouncilneededinformation",
    ldv_companiesserviceneededinformation: "ldv_companiesserviceneededinformation",

    ldv_ministryshajjagencyneededinformation: "ldv_ministryshajjagencyneededinformation",
    ldv_ministryshajjagencydecisisoncode: "ldv_ministryshajjagencydecisisoncode",
    ldv_ministrysumarahagencydecisisoncode: "ldv_ministrysumarahagencydecisisoncode",
    ldv_ministrysumarahagencyneededinformation: "ldv_ministrysumarahagencyneededinformation",
    ldv_needsmoredetails: "ldv_needsmoredetails",
    caseorigincode: "caseorigincode",

    ldv_isfcr: "ldv_isfcr",

    Enums: {
        location: {
            Mekkah: 1,
            Madina: 2,
            BorderCrossings: 3,
            Arrafat: 4,
            Muzdalifa: 5,
            Mina: 6,
            Jada: 7

        },
        season: {
            Hajj: 1,
            Umrah: 2
        },
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
        },
        qualityDecisR1: {
            CloseTheTicket: 1,
            NotResolved: 2
        },
        CaseOrigin: {
            CallCenter: 1,
            Email: 2,
            Nusuk: 3,
            ExternalGate: 4,
            SocialMedia: 5
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

    TechnicalComplainMomentaryHajj: {
        serviceDefinitionId: "7b80a868-2dcc-ee11-907a-6045bd8c92a2",
        serviceDefinitionName: "Technical complain - Momentary Hajj"
    },
    TechnicalComplainMomentaryUmrah: {
        serviceDefinitionId: "7980A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technical complain - Momentary Umrah"
    },
    TechnicalComplainMomentaryUmrahForCompanies: {
        serviceDefinitionId: "DE69552C-B0D0-EE11-9079-6045BD895E74",
        serviceDefinitionName: "Technical complain - Momentary Umrah for companies"
    },
    TechnicalComplainNonMomentaryHajj: {
        serviceDefinitionId: "7D80A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technical complain - Non-momentary Hajj"
    },
    TechnicalComplainNonMomentaryUmrah: {
        serviceDefinitionId: "7780A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technical complain - Non-momentary Umrah"
    },
    Inquiry: {
        serviceDefinitionId: "E8015016-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName: "Inquiry"
    },
    Suggestions: {
        serviceDefinitionId: "E5C52C61-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName: "Suggestions"
    },

    //Integration
    BusinessSectorComplain: {
        serviceDefinitionId: "8980A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Business sector Complain"
    },
    FinancialComplainExternalPilgrims: {
        serviceDefinitionId: "8380A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - External Pilgrims"
    },
    FinancialComplainInternalPilgrims: {
        serviceDefinitionId: "8180A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - Internal Pilgrims"
    },
    FinancialComplainNusukServices: {
        serviceDefinitionId: "7F80A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - Nusuk services"
    },
    TechnologicalComplain: {
        serviceDefinitionId: "7580A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technological Complain"
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

    TechnicalComplainMomentaryUmrah: {
        name: "BPF | TC - Momentary Umrah Process",
        id: "6BCF0E9E-E5A7-408A-A287-2CF733EF55FB",
        stages: {
            submit: {
                name: "Submit",
                id: "58a48aed-219a-42b2-b6e1-040678181e2d"
            },
            Makkah: {
                name: "Makkah",
                id: "79ed7322-d5b8-4c02-963d-5da08dd5774b"
            },
            Jada: {
                name: "Jada",
                id: "096dda7f-c691-4040-b6dc-721aa4fbe511"
            },
            Madina: {
                name: "Madina",
                id: "bb68b1ad-ce4f-4740-bab3-c3d644c0565f"
            },
            quality: {
                name: "Quality",
                id: "f43fe841-1303-4159-851f-a353ce7964b7"
            },
            resolved: {
                name: "Resolved",
                id: "48050991-24dc-4d21-a459-d5679351219c"
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
                name: "Quality",
                id: "183022ef-9135-4a8d-9bd5-f85cc97355c7"
            },
            resolved: {
                name: "Resolved",
                id: "abce898f-a2c2-4cf9-94fa-de3e5ca47704"
            }
        }
    },

    TechnicalComplainMomentaryHajj: {
        name: "BPF |TC - Momentary Hajj",
        id: "BE764FE2-27F8-4858-B265-3670C79DD222",
        stages: {
            Submit: {
                name: "Submit",
                id: "e1837ccd-7810-4d4e-baa8-cb2281547250"
            },
            Madina: {
                name: "Madina",
                id: "d14d0f04-5792-47fa-b5da-2872e0c708b4"
            },
            Makkah: {
                name: "Makkah",
                id: "f7a03640-d8c1-4d04-a64c-1036ad7209c5"
            },

            BorderCrossing: {
                name: "BorderCrossing",
                id: "855e43be-68a7-4901-90f7-3ae469008ea8"
            },
            CoordinationCouncil: {
                name: "CoordinationCouncil",
                id: "e44adc4f-9947-48da-8d83-94546fbe51a1"
            },
            Quality: {
                name: "Quality",
                id: "ac37d1ec-87e3-4ea0-8a9d-b43126ed80a7"
            },
            Resolved: {
                name: "Resolved",
                id: "68ccc8b3-1c5d-4f66-9187-21d5dd06269e"
            },
        }
    },
    TechnicalComplainNotMomentaryHijjAndUmarah: {
        name: "BPF | TC - Not Momentary Hajj and Umrah Process",
        id: "705D3F16-E646-4CE0-B375-A43444597753",
        stages: {
            Submit: {
                name: "Submit",
                id: "ef1eab3c-ac6e-4375-af03-851d40ff3c8f"
            },
            Hajj: {
                name: "Hajj",
                id: "ae916fe5-4185-42f1-854b-e2bf14875be8"
            },
            Umrah: {
                name: "Umrah",
                id: "bc6ab56a-8979-4178-aa16-b2d0965a2a19"
            },

            Quality: {
                name: "Quality",
                id: "c11ceeb2-f5c2-41ed-8e51-1f8e9d0f6391"
            },
            Resolved: {
                name: "Resolved",
                id: "d32598c5-e107-4488-ad85-c76896169a3d"
            },
        },
    },
    RequestComplainsIntegration: {
        name: "BPF | Requesting Complaints | Integration | Flow",
        id: "C1FEA096-843C-4573-BD77-D5C767618B8F",

        stages: {
            Submit: {
                name: "Submit",
                id: "a3ad32dd-5716-43b0-8be5-dc4b08a40a71"
            },
            FCR: {
                name: "FCR",
                id: "26161fe6-3027-4642-8d0d-ae53ea5ccf01"
            },
            ServiceDesk: {
                name: "Service Desk",
                id: "3d8e178d-421d-440e-9dd8-1be829d5e7c7"
            },
            Kadana: {
                name: "Kadana",
                id: "9e1605ab-8b8e-4c07-b603-951fe39aa2af"
            },
            Tashir: {
                name: "Tashir",
                id: "263be85a-861d-48f4-9132-d5ff8387f70c"
            },
            Quality: {
                name: "Quality",
                id: "101210ae-ae2d-40dd-9ef1-e559397dd455"
            },
            Quality2: {
                name: "Quality 2",
                id: "b48d01d6-3aa9-41e6-815c-c664c6685cd7"
            },
            Quality3: {
                name: "Quality 3",
                id: "d2afa263-c463-4b44-b09b-c48ebc676d0c"
            },
            Resolved: {
                name: "Resolved",
                id: "236a7caa-861a-4dc5-8c33-f330f4aa85a6"
            },
        },
    }
};

var Tabs = {
    tab_requestinformation: "tab_requestinformation",
    tab_administiration: "tab_administiration",

};

var Sections = {
    SLATimer: "tab_requestinformation_section_slatimer",
    IntegrationSection: "tab_administiration_section_7"
}

function OnLoad(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();

    //HideUCIButtons();
    //HandleBPF(executionContext);
    //GetDecisionsBasedOnService_OnLoad(formContext);

    HandleBPF(executionContext, GetDecisionsBasedOnService_OnLoad);
    OnChange_MainCategory(formContext);
    OnChange_SubCategory(formContext);

    HideAndShowComplainPriority(formContext);
    ShowAndHideCompany(formContext);
    ShowAndHideBeneficiaryType(formContext);
    ShowAndHideSeason(formContext);
    ShowAndHideLocation(formContext);
    ShowAndHideComplainType(formContext);


    UnlockCategoryFieldsBeforeSubmit(formContext);
    LockFormFieldsAfterSubmit(formContext);

    ChangeTicketRequestLabel(formContext);
    //RemoveCaseOriginOptionsInCreate(formContext);

    ShowAndHideIntegrationSection(formContext);
    ShowAndHideSLATimerSection(formContext);

    RestrictCustomerToBeIndividualOnly(formContext);

    if (formContext.ui.getFormType() !== 1) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_serviceid, true); // Lock the Request Type Field
        CommonGeneric.DisableField(formContext, caseFields.caseorigincode, true); // Lock the Origin Field
        ShowAndHidePassportNumberAndIdNumber(executionContext);

    }

    formContext.getAttribute(caseFields.ldv_serviceid).addOnChange(function () {
        OnChange_RequestType(formContext);

    });

    formContext.getAttribute(caseFields.ldv_processid).addOnChange(function () {
        OnChange_ProcessId(formContext);
    });

    formContext.getAttribute(caseFields.ldv_maincategoryid).addOnChange(function () {

        var fieldsToBeEmpty = [caseFields.ldv_subcategoryid, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_errorcodeid];
        fieldsToBeEmpty.forEach(function (fieldSchemaName) {

            CommonGeneric.EmptyField(formContext, fieldSchemaName);
        });
        OnChange_MainCategory(formContext);
        CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_maincategoryid);


    });

    formContext.getAttribute(caseFields.ldv_subcategoryid).addOnChange(function () {
        var fieldsToBeEmpty = [caseFields.ldv_secondarysubcategoryid, caseFields.ldv_errorcodeid];
        fieldsToBeEmpty.forEach(function (fieldSchemaName) {

            CommonGeneric.EmptyField(formContext, fieldSchemaName);
        });
        OnChange_SubCategory(formContext);
        CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_subcategoryid);
        SettingComplainTypePrioritySeasonBeneficiaryFromSubCategory(formContext);
    });

    formContext.getAttribute(caseFields.ldv_secondarysubcategoryid).addOnChange(function () {
        CommonGeneric.EmptyField(formContext, caseFields.ldv_errorcodeid);
    });

    formContext.getAttribute(caseFields.ldv_locationcode).addOnChange(function () {
        OnChange_Location(formContext);
    });

    formContext.getAttribute(caseFields.ldv_seasoncode).addOnChange(function () {
        OnChange_Season(formContext);
    });

    formContext.getAttribute(caseFields.customerid).addOnChange(function () {
        ShowAndHidePassportNumberAndIdNumber(executionContext);
    });

    SetReqForMainCategoryForInquiry(formContext);
    SetReqForSubCategoryForInquiry(formContext);
    SetReqForSecondarySubCategoryForInquiry(formContext);
    unLockFieldsOnQualityStageInquiry(formContext);
};

function OnSave(executionContext) {
    var saveEvent = executionContext.getEventArgs();
    var formContext = executionContext.getFormContext();

};

function ShowAndHideBeneficiaryType(formContext) {

    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (service === null || service === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_beneficiarytypecode, false, false);
    }
    else {
        ///Aya check service contain data
        var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();

        if (serviceId === ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase()) {
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_beneficiarytypecode, true, true);
            // Check if ldv_beneficiarytypecode contains data
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_beneficiarytypecode);
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(formContext, caseFields.ldv_beneficiarytypecode, true); // Lock the field
            }
        }
        else if (serviceId === ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase() || serviceId === ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase()) {
            OnChange_Season(formContext);
        }
    }

};
function ShowAndHideSeason(formContext) {
    var fields = [caseFields.ldv_seasoncode];
    var serviceIdsToShow = [ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
    ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase()];
    ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow);
    // Check if ldv_seasoncode contains data
    var seasonValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_seasoncode);
    if (seasonValue !== null && seasonValue !== undefined) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_seasoncode, true); // Lock the field
    }
};
function ShowAndHideLocation(formContext) {
    var fields = [caseFields.ldv_locationcode];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryUmrah.serviceDefinitionId.toLowerCase()
    ];
    ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow);
};
function ShowAndHideComplainType(formContext) {
    var fields = [caseFields.ldv_complaintypecode];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryUmrah.serviceDefinitionId.toLowerCase()
    ];
    ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow);
    // Check if ldv_complaintypecode contains data
    var complainTypeValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_complaintypecode);
    if (complainTypeValue !== null && complainTypeValue !== undefined) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_complaintypecode, true); // Lock the field
    }
};
//function ShowAndHideCompany(formContext) {
//    debugger;
//    var location = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_locationcode);
//    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);

//    if (!location || !service) {
//        return;
//    }

//    var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();

//    if (serviceId === ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase()) {
//        var validLocations = [
//            caseFields.Enums.location.Mekkah,
//            caseFields.Enums.location.Mina,
//            caseFields.Enums.location.Arrafat,
//            caseFields.Enums.location.Muzdalifa
//        ];

//        var shouldShowCompanyField = validLocations.includes(location);
//        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, shouldShowCompanyField, shouldShowCompanyField);
//    }

//};

function ShowAndHideCompany(formContext) {
    debugger;
    var location = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_locationcode);
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);

    if (!service) {
        return;
    }

    var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();

    if (serviceId === ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase() && location) {
        // Check if location is valid for TechnicalComplainMomentaryHajj
        var validLocations = [
            caseFields.Enums.location.Mekkah,
            caseFields.Enums.location.Mina,
            caseFields.Enums.location.Arrafat,
            caseFields.Enums.location.Muzdalifa
        ];

        var shouldShowCompanyField = validLocations.includes(location);
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, shouldShowCompanyField, shouldShowCompanyField);
    } else if (serviceId === ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase() || serviceId === ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase()) {
        // Show the company field without checking the location
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, true, true);
    }
    else {
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, false, false);

    }
};
function HideAndShowComplainPriority(formContext) {
    var serviceRecord = GetLookUpRecordWithExpandedValues(formContext, caseFields.ldv_serviceid, "$select=ldv_name,ldv_iscomplain");
    if (serviceRecord !== null) {
        var isComplain = serviceRecord["ldv_iscomplain"];

        if (isComplain === true) {

            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_prioritycode, true, true);

            // Check if ldv_prioritycode contains data
            var priorityValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_prioritycode);
            if (priorityValue !== null && priorityValue !== undefined) {
                CommonGeneric.DisableField(formContext, caseFields.ldv_prioritycode, true); // Lock the field
            }
        } else {
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_prioritycode, false, false);
        }
    }
    else {
        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_prioritycode, false, false);
    }
};
function SettingComplainTypePrioritySeasonBeneficiaryFromSubCategory(formContext) {
    debugger;
    var subCategoryRecord = GetLookUpRecordWithExpandedValues(formContext, caseFields.ldv_subcategoryid, "$select=ldv_name,ldv_complaintypecode,ldv_prioritycode,ldv_seasoncode,ldv_beneficiarytypecode");
    if (subCategoryRecord !== null) {
        // Check the visibility of each field
        var complaintTypeVisible = IsFieldVisible(formContext, caseFields.ldv_complaintypecode);
        var priorityVisible = IsFieldVisible(formContext, caseFields.ldv_prioritycode);
        var seasonVisible = IsFieldVisible(formContext, caseFields.ldv_seasoncode);
        var beneficiaryTypeVisible = IsFieldVisible(formContext, caseFields.ldv_beneficiarytypecode);

        // Use the retrieved values if the fields are visible
        if (complaintTypeVisible && subCategoryRecord.ldv_complaintypecode !== null) {
            formContext.getAttribute(caseFields.ldv_complaintypecode).setValue(subCategoryRecord.ldv_complaintypecode);
            CommonGeneric.DisableField(formContext, caseFields.ldv_complaintypecode, true);
        }
        if (priorityVisible && subCategoryRecord.ldv_prioritycode !== null) {
            formContext.getAttribute(caseFields.ldv_prioritycode).setValue(subCategoryRecord.ldv_prioritycode);
            CommonGeneric.DisableField(formContext, caseFields.ldv_prioritycode, true);

        }
        if (seasonVisible && subCategoryRecord.ldv_seasoncode !== null) {
            formContext.getAttribute(caseFields.ldv_seasoncode).setValue(subCategoryRecord.ldv_seasoncode);
            formContext.getAttribute(caseFields.ldv_seasoncode).fireOnChange();
            CommonGeneric.DisableField(formContext, caseFields.ldv_seasoncode, true);


        }
        if (beneficiaryTypeVisible && subCategoryRecord.ldv_beneficiarytypecode !== null) {
            formContext.getAttribute(caseFields.ldv_beneficiarytypecode).setValue(subCategoryRecord.ldv_beneficiarytypecode);
            CommonGeneric.DisableField(formContext, caseFields.ldv_beneficiarytypecode, true);

        }
    }
};
function SetProcessFieldFromService(formContext) {
    debugger;
    //getting service record with process field
    var serviceRecord = GetLookUpRecordWithExpandedValues(formContext, caseFields.ldv_serviceid, "$select=ldv_name,ldv_iscomplain&$expand=ldv_processid($select=name)");
    if (serviceRecord !== null) {
        var processLookup = serviceRecord["ldv_processid"];
        if (processLookup !== null) {
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
function OnChange_Location(formContext) {

    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, false, false);
    ShowAndHideCompany(formContext);
};
function OnChange_Season(formContext) {
    debugger;
    var season = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_seasoncode);
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);

    if (!season || !service) {
        return;
    }
    var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
    if (serviceId === ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase() || serviceId === ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase()) {
        if (season === caseFields.Enums.season.Hajj) {
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_beneficiarytypecode, true, true);
            // Check if ldv_beneficiarytypecode contains data
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_beneficiarytypecode);
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(formContext, caseFields.ldv_beneficiarytypecode, true); // Lock the field
            }
        }
        else {
            CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_beneficiarytypecode, false, false);

        }
    }
};
function OnChange_RequestType(formContext) {

    var fieldsToBeHiddenOrShown = [
        caseFields.ldv_prioritycode,
        caseFields.ldv_beneficiarytypecode,
        caseFields.ldv_seasoncode,
        caseFields.ldv_locationcode,
        caseFields.ldv_complaintypecode,
        caseFields.ldv_company,
        caseFields.ldv_errorcodeid
    ];
    fieldsToBeHiddenOrShown.forEach(function (fieldSchemaName) {
        CommonGeneric.ShowAndReuiredField(formContext, fieldSchemaName, false, false);
    });

    var fieldsToEmpty = [
        caseFields.ldv_maincategoryid,
        caseFields.ldv_subcategoryid,
        caseFields.ldv_secondarysubcategoryid,
        caseFields.ldv_errorcodeid
    ];
    fieldsToEmpty.forEach(function (fieldSchemaName) {

        CommonGeneric.EmptyField(formContext, fieldSchemaName);
    });
    SetProcessFieldFromService(formContext);
    HideAndShowComplainPriority(formContext);
    ShowAndHideBeneficiaryType(formContext);
    ShowAndHideSeason(formContext);
    ShowAndHideLocation(formContext);
    ShowAndHideCompany(formContext);
    ShowAndHideComplainType(formContext);
    ChangeTicketRequestLabel(formContext);
    ShowAndHideIntegrationSection(formContext);
    ShowAndHideSLATimerSection(formContext);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_maincategoryid, caseFields.ldv_serviceid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_serviceid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_serviceid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_errorcodeid, caseFields.ldv_serviceid);
    CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_maincategoryid, caseFields.ldv_serviceid);

    SetReqForMainCategoryForInquiry(formContext);
    showAndHideSubcategoryField(formContext);
    showAndHideSecondarySubcategoryField(formContext);
};
function OnChange_ProcessId(formContext) {
    debugger;
    if (formContext.ui.getFormType() !== 1) {
        SwitchBPF(formContext, caseFields.ldv_processid, () => { });
    }
};
function OnChange_MainCategory(formContext) {

    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_maincategoryid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_maincategoryid);
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_errorcodeid, caseFields.ldv_maincategoryid);
    showAndHideSubcategoryField(formContext);
    showAndHideSecondarySubcategoryField(formContext);
    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_errorcodeid, false, false);
};
function OnChange_SubCategory(formContext) {
    debugger;
    CommonGeneric.Empty_field1_BasedOn_field2(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_subcategoryid);

    var subCategory = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_subcategoryid);
    if (subCategory !== null && subCategory !== undefined) {

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

    showAndHideSecondarySubcategoryField(formContext);
};
function showAndHideSubcategoryField(formContext) {
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
                    SetReqForSubCategoryForInquiry(formContext);

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
function showAndHideSecondarySubcategoryField(formContext) {
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
                    SetReqForSecondarySubCategoryForInquiry(formContext);

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
function SetReqForMainCategoryForInquiry(formContext) {
    debugger;
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (service !== null && service !== undefined) {
        var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
        if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
            CommonGeneric.SetReqLevel(formContext, caseFields.ldv_maincategoryid, false);
        }
    };
}
function SetReqForSubCategoryForInquiry(formContext) {
    debugger;
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (service !== null && service !== undefined) {
        var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
        if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
            CommonGeneric.SetReqLevel(formContext, caseFields.ldv_subcategoryid, false);
        }
    };
}
function SetReqForSecondarySubCategoryForInquiry(formContext) {
    debugger;
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (service !== null && service !== undefined) {
        var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
        if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
            CommonGeneric.SetReqLevel(formContext, caseFields.ldv_secondarysubcategoryid, false);
        }
    };
}

function HideFieldsOnSubmitStageInquiry(formContext) {
    var subCategoryOnBpf = "header_process_" + caseFields.ldv_subcategoryid;
    CommonGeneric.ShowAndReuiredField(formContext, subCategoryOnBpf, false, false);
};

function HideFieldsOnSubmitStageRequestComplainsIntegration(formContext) {
    var reqTypeOnBpf = "header_process_" + caseFields.ldv_serviceid;
    var isFCROnBpf = "header_process_" + caseFields.ldv_isfcr;
    CommonGeneric.ShowAndReuiredField(formContext, reqTypeOnBpf, false, false);
    CommonGeneric.ShowAndReuiredField(formContext, isFCROnBpf, false, false);
}
function HideFieldsOnSubmitStageTechnicalComplainMomentaryUmrah(formContext) {
    var locationOnBpf = "header_process_" + caseFields.ldv_locationcode;
    CommonGeneric.ShowAndReuiredField(formContext, locationOnBpf, false, false);
}

function HideFieldsOnSubmitStageTechnicalComplainMomentaryHajj(formContext) {
    var locationFieldOnBpf = "header_process_" + caseFields.ldv_locationcode;
    var beneficiaryTypeOnBpf = "header_process_" + caseFields.ldv_beneficiarytypecode;
    CommonGeneric.ShowAndReuiredField(formContext, locationFieldOnBpf, false, false);
    CommonGeneric.ShowAndReuiredField(formContext, beneficiaryTypeOnBpf, false, false);
}

function HideFieldsOnSubmitTechnicalComplainNonMomentaryHajjAndUmrah(formContext) {
    var seasonOnBpf = "header_process_" + caseFields.ldv_seasoncode;
    CommonGeneric.ShowAndReuiredField(formContext, seasonOnBpf, false, false);

}
function LockFormFieldsAfterSubmit(formContext) {

    var submitValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_issubmitted);

    if (submitValue !== null && submitValue !== undefined) {
        if (formContext.ui.getFormType() !== 1 && submitValue === true) {
            CommonGeneric.LockFormControlsExecptBPF(formContext);
        }

    }


};

function UnlockCategoryFieldsBeforeSubmit(formContext) {
    var submitValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_issubmitted);
    if (submitValue !== null && submitValue !== undefined) {
        if (formContext.ui.getFormType() !== 1 && submitValue !== true) {

            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_maincategoryid, caseFields.ldv_serviceid);
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_maincategoryid);
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_subcategoryid);
        }
    }
};

function HandleBPF(executionContext, callback) {
    var formContext = executionContext.getFormContext();

    // In case we want to hide the BPF in the create form
    if (formContext.ui.getFormType() === 1) {
        CommonGeneric.hideBusinessProcessFlow(formContext);
    } else {
        // Switch the BPF
        SwitchBPF(formContext, caseFields.ldv_processid, () => {
            // Call the callBackFN after switching the BPF
            if (typeof callback === 'function') {
                callback(formContext);
            }
        });
        CommonGeneric.BusinessProcessFlowTools.Disable_InactiveBPFStages(executionContext, () => { });
        HideUCIButtons();

        formContext.data.process.addOnStageChange(function () {
            OnChange_addOnStageChange(executionContext);
        });
        formContext.data.process.addOnStageSelected(function () {
            OnChange_addOnStageChange(executionContext);
        });

    }
}

function HideBPFNextButtonForSubmitStage(formContext) {
    var currentStageName = formContext.data.process.getActiveStage().getName();
    if (currentStageName === "Submit") {
        CommonGeneric.HideNextStageUCI();
    }
};

function ShowBPFNextButtonForSubmitStage(executionContext) {
    var formContext = executionContext.getFormContext();
    var currentStage = formContext.data.process.getActiveStage();
    if (currentStage) {
        var currentStageName = currentStage.getName();
        if (currentStageName === "Submit" || currentStageName === "تقديم") {
            ShowNextStage(executionContext);
        }

    }
};
function OnChange_addOnStageChange(executionContext) {
    debugger;
    var formContext = executionContext.getFormContext();
    HideUCIButtons(executionContext);
    ShowBPFNextButtonForSubmitStage(executionContext);
    GetActiveStageDecisionsBasedOnService(executionContext, formContext);
    if (formContext.ui.getFormType() !== 1) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_serviceid, true); // Lock the Request Type Field
    }
};
function ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow) {
    debugger;
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (service === null || service === undefined) {
        fields.forEach(function (fieldName) {
            CommonGeneric.ShowAndReuiredField(formContext, fieldName, false, false);
        });
    } else {
        var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
        var shouldShowFields = serviceIdsToShow.includes(serviceId);
        fields.forEach(function (fieldName) {
            CommonGeneric.ShowAndReuiredField(formContext, fieldName, shouldShowFields, shouldShowFields);
        });
    }
};

function ChangeTicketRequestLabel(formContext) {
    var serviceRecord = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (serviceRecord && serviceRecord.id) {
        var serviceId = serviceRecord.id.replace('{', '').replace('}', '').toLowerCase();
        var userLanguage = formContext.context.getUserLcid();

        var inquiryLabel = "Request of Interest Number";
        var inquiryArabicLabel = "رقم الطلب";
        var suggestionLabel = "Request Number";
        var suggestionArabicLabel = "رقم الطلب ";
        var complainLabel = "Complaint Number";
        var complainArabicLabel = "رقم الشكوى";

        if (userLanguage === 1025) {
            if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
                ChangeFieldLabel(formContext, caseFields.title, inquiryArabicLabel);
            }
            else if (serviceId === ServiceType.Suggestions.serviceDefinitionId.toLowerCase()) {
                ChangeFieldLabel(formContext, caseFields.title, suggestionArabicLabel);

            }
            else {
                ChangeFieldLabel(formContext, caseFields.title, complainArabicLabel);

            }
        }
        else {
            if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
                ChangeFieldLabel(formContext, caseFields.title, inquiryLabel);
            }
            else if (serviceId === ServiceType.Suggestions.serviceDefinitionId.toLowerCase()) {
                ChangeFieldLabel(formContext, caseFields.title, suggestionLabel);

            }
            else {
                ChangeFieldLabel(formContext, caseFields.title, complainLabel);

            }
        }
    }

};

function RemoveCaseOriginOptionsInCreate(formContext) {
    var removedValues = [
        caseFields.Enums.CaseOrigin.Nusuk,
        caseFields.Enums.CaseOrigin.ExternalGate,
        caseFields.Enums.CaseOrigin.Email,
        caseFields.Enums.CaseOrigin.SocialMedia
    ];

    if (formContext.ui.getFormType() === 1) {

        var caseOriginField = formContext.getAttribute(caseFields.caseorigincode);

        if (caseOriginField !== null && caseOriginField !== undefined) {

            RemoveOptionSetValues(formContext, caseFields.caseorigincode, removedValues);
        }


    }
};

function ShowAndHideIntegrationSection(formContext) {
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (!service) {
        CommonGeneric.SectionVisibility(formContext, Tabs.tab_administiration, Sections.IntegrationSection, false);
        return;
    }

    var allowedServiceIds = [
        ServiceType.BusinessSectorComplain.serviceDefinitionId.toLowerCase(),
        ServiceType.FinancialComplainExternalPilgrims.serviceDefinitionId.toLowerCase(),
        ServiceType.FinancialComplainInternalPilgrims.serviceDefinitionId.toLowerCase(),
        ServiceType.FinancialComplainNusukServices.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnologicalComplain.serviceDefinitionId.toLowerCase()
    ];

    var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();


    if (allowedServiceIds.includes(serviceId)) {

        CommonGeneric.SectionVisibility(formContext, Tabs.tab_administiration, Sections.IntegrationSection, true);
    } else {
        CommonGeneric.SectionVisibility(formContext, Tabs.tab_administiration, Sections.IntegrationSection, false);
    }
};

function ShowAndHideSLATimerSection(formContext) {
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    if (!service) {
        CommonGeneric.SectionVisibility(formContext, Tabs.tab_requestinformation, Sections.SLATimer, false);
        return;
    }

    var notAllowedServiceIds = [
        ServiceType.Inquiry.serviceDefinitionId.toLowerCase(),
        ServiceType.Suggestions.serviceDefinitionId.toLowerCase()
    ];

    var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();

    if (notAllowedServiceIds.includes(serviceId)) {
        CommonGeneric.SectionVisibility(formContext, Tabs.tab_requestinformation, Sections.SLATimer, false);
    } else {
        CommonGeneric.SectionVisibility(formContext, Tabs.tab_requestinformation, Sections.SLATimer, true);
    }
};

function RestrictCustomerToBeIndividualOnly(formContext) {
    const customer = formContext.getControl(caseFields.customerid);

    if (!customer) return;

    // in case applicant is customer lookup (allowing account and contact)
    if (customer.getEntityTypes().length > 1)
        customer.setEntityTypes(["contact"]); // restrict customer to allow only contacts
};

function ShowAndHidePassportNumberAndIdNumber(executionContext) {
    var formContext = executionContext.getFormContext();
    var quickViewControl = formContext.ui.quickForms.get("Individual");

    if (quickViewControl) {
        if (quickViewControl.isLoaded()) {
            var passportAttribute = quickViewControl.getAttribute("governmentid");
            var idNumberAttribute = quickViewControl.getAttribute("ldv_idnumber");

            if (passportAttribute) {
                var passportNumber = passportAttribute.getValue();
                if (passportNumber !== null && passportNumber !== undefined && passportNumber !== "") {
                    quickViewControl.getControl("governmentid").setVisible(true);
                } else {
                    quickViewControl.getControl("governmentid").setVisible(false);
                }
            } else {
                console.error("Attribute 'governmentid' not found in Quick View control.");
            }

            if (idNumberAttribute) {
                var idNumber = idNumberAttribute.getValue();
                if (idNumber !== null && idNumber !== undefined && idNumber !== "") {
                    quickViewControl.getControl("ldv_idnumber").setVisible(true);
                } else {
                    quickViewControl.getControl("ldv_idnumber").setVisible(false);
                }
            } else {
                console.error("Attribute 'ldv_idnumber' not found in Quick View control.");
            }

        } else {
            // Control is not yet loaded, retry after a short delay
            setTimeout(function () {
                ShowAndHidePassportNumberAndIdNumber(executionContext);
            }, 100);
        }
    } else {
        console.error("Quick View control 'Individual' not found.");
    }
}



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

                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext);
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

                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext);
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

                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext);
                });
            };

            break;

        case ServiceType.TechnicalComplainMomentaryUmrah.serviceDefinitionId.toLowerCase():

            var makkahStageId = BPFs.TechnicalComplainMomentaryUmrah.stages.Makkah.id.toLowerCase();
            var jadaStageId = BPFs.TechnicalComplainMomentaryUmrah.stages.Jada.id.toLowerCase();
            var madinaStageId = BPFs.TechnicalComplainMomentaryUmrah.stages.Madina.id.toLowerCase();
            var qualityStageId = BPFs.TechnicalComplainMomentaryUmrah.stages.quality.id.toLowerCase();

            if (currentStageId === makkahStageId) {

                TechnicalComplainMomentaryUmrah_MakkahStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_companiesservicedecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryUmrah_MakkahStageDecision_OnChange(executionContext);
                });
            };

            if (currentStageId === jadaStageId) {

                TechnicalComplainMomentaryUmrah_JadaStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_agentemployeedecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryUmrah_JadaStageDecision_OnChange(executionContext);
                });
            };

            if (currentStageId === madinaStageId) {

                TechnicalComplainMomentaryUmrah_MadinaStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_castingofficerdecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryUmrah_MadinaStageDecision_OnChange(executionContext);
                });
            };

            if (currentStageId === qualityStageId) {

                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext);
                });
            };
            break;

        case ServiceType.Inquiry.serviceDefinitionId.toLowerCase():
            var qualityStageId = BPFs.Inquiry.stages.quality.id.toLowerCase();


            if (currentStageId === qualityStageId) {
                Inquiry_QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualityofficerdecisioncode2).addOnChange(function () {
                    Inquiry_QualityStageDecision_OnChange(executionContext);
                });
            };

            break;

        case ServiceType.Suggestions.serviceDefinitionId.toLowerCase():
            var suggestionQualityStageId = BPFs.Suggestions.stages.quality.id.toLowerCase();
            var concernedDepartmetnStageId = BPFs.Suggestions.stages.concerned_department.id.toLowerCase();
            var suggestionQuality2_StageId = BPFs.Suggestions.stages.quality_2.id.toLowerCase();

            if (currentStageId === suggestionQualityStageId) {
                ShowNextStageUCI(executionContext);
            }

            if (currentStageId == concernedDepartmetnStageId) {
                Suggestions_ConcernedDepartmentDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_departmentdecisioncode).addOnChange(function () {
                    Suggestions_ConcernedDepartmentDecision_OnChange(executionContext)
                });
            }

            if (currentStageId == suggestionQuality2_StageId) {
                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext);
                })
            }

            break;

        case ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase():
            var tcMomentaryHajjSubmitStageId = BPFs.TechnicalComplainMomentaryHajj.stages.Submit.id.toLowerCase();
            var tcMomentaryHajjQualityStageId = BPFs.TechnicalComplainMomentaryHajj.stages.Quality.id.toLowerCase();
            var tcMomentaryHajjBorderCrossingStageId = BPFs.TechnicalComplainMomentaryHajj.stages.BorderCrossing.id.toLowerCase();
            var tcMomentaryHajjMakkahStageId = BPFs.TechnicalComplainMomentaryHajj.stages.Makkah.id.toLowerCase();
            var tcMomentaryHajjMadinaStageId = BPFs.TechnicalComplainMomentaryHajj.stages.Madina.id.toLowerCase();
            var tcMomentaryHajjCoordinationCouncilStageId = BPFs.TechnicalComplainMomentaryHajj.stages.CoordinationCouncil.id.toLowerCase();
            var tcMomentaryHajjResolvedStageId = BPFs.TechnicalComplainMomentaryHajj.stages.Resolved.id.toLowerCase();

            if (currentStageId === tcMomentaryHajjQualityStageId) {
                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext);
                });
            };
            if (currentStageId === tcMomentaryHajjMakkahStageId) {
                TechnicalComplainMomentaryHajj_MakkahDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_companiesservicedecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryHajj_MakkahDecision_OnChange(executionContext);
                });
            };
            if (currentStageId === tcMomentaryHajjMadinaStageId) {
                TechnicalComplainMomentaryHajj_MadinaDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_castingofficerdecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryHajj_MadinaDecision_OnChange(executionContext);
                });
            };
            if (currentStageId === tcMomentaryHajjCoordinationCouncilStageId) {
                TechnicalComplainMomentaryHajj_CoordinationCouncilDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_coordinationcouncildecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryHajj_CoordinationCouncilDecision_OnChange(executionContext);
                });
            };
            if (currentStageId === tcMomentaryHajjBorderCrossingStageId) {
                TechnicalComplainMomentaryHajj_BorderCrossingDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_agentemployeedecisioncode).addOnChange(function () {
                    TechnicalComplainMomentaryHajj_BorderCrossingDecision_OnChange(executionContext);
                });
            };
            break;

        case ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase():

            var notMomentaryHijjAndUmarahQualityStageId = BPFs.TechnicalComplainNotMomentaryHijjAndUmarah.stages.Quality.id.toLowerCase();
            var notMomentaryHijjAndUmarahUmrahStageId = BPFs.TechnicalComplainNotMomentaryHijjAndUmarah.stages.Umrah.id.toLowerCase();
            var notMomentaryHijjAndUmarahHajjStageId = BPFs.TechnicalComplainNotMomentaryHijjAndUmarah.stages.Hajj.id.toLowerCase();

            if (currentStageId === notMomentaryHijjAndUmarahHajjStageId) {
                TechnicalComplainNotMomentaryHijjAndUmarah_HajjDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_ministryshajjagencydecisisoncode).addOnChange(function () {
                    TechnicalComplainNotMomentaryHijjAndUmarah_HajjDecision_OnChange(executionContext);
                });
            }

            if (currentStageId === notMomentaryHijjAndUmarahUmrahStageId) {
                TechnicalComplainNotMomentaryHijjAndUmarah_UmarahDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_ministrysumarahagencydecisisoncode).addOnChange(function () {
                    TechnicalComplainNotMomentaryHijjAndUmarah_UmarahDecision_OnChange(executionContext)
                });
            }

            if (currentStageId === notMomentaryHijjAndUmarahQualityStageId) {
                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext)
                });
            }

            break;

        case ServiceType.BusinessSectorComplain.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnologicalComplain.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainInternalPilgrims.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainExternalPilgrims.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainNusukServices.serviceDefinitionId.toLowerCase():

            var qualityStageId = BPFs.RequestComplainsIntegration.stages.Quality.id.toLowerCase();
            var quality2StageId = BPFs.RequestComplainsIntegration.stages.Quality2.id.toLowerCase();
            var quality3StageId = BPFs.RequestComplainsIntegration.stages.Quality3.id.toLowerCase();
            var FCRStageId = BPFs.RequestComplainsIntegration.stages.FCR.id.toLowerCase();
            if (currentStageId === qualityStageId) {
                IntegrationServices_QualityDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    IntegrationServices_QualityDecision_OnChange(executionContext);
                });
            }
            if (currentStageId === quality2StageId) {
                QualityStageDecision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    QualityStageDecision_OnChange(executionContext);
                });
            }
            if (currentStageId === quality3StageId) {
                IntegrationServices_Quality3Decision_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
                    IntegrationServices_Quality3Decision_OnChange(executionContext);
                });
            }

            if (currentStageId === FCRStageId) {

                IntegrationServices_IsFCR_OnChange(executionContext);
                formContext.getAttribute(caseFields.ldv_isfcr).addOnChange(function () {
                    IntegrationServices_IsFCR_OnChange(executionContext);
                });
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

        case ServiceType.TechnicalComplainMomentaryUmrah.serviceDefinitionId.toLowerCase():
            TechnicalComplainMomentaryUmrah_OnLoad(formContext);
            HideFieldsOnSubmitStageTechnicalComplainMomentaryUmrah(formContext);
            break;

        case ServiceType.Inquiry.serviceDefinitionId.toLowerCase():
            Inquiry_QualityStageDecision_OnLoad(formContext);
            HideFieldsOnSubmitStageInquiry(formContext);
            break;

        case ServiceType.Suggestions.serviceDefinitionId.toLowerCase():
            Suggestions_OnLoad(formContext);
            break;

        case ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase():
            TechnicalComplainMomentaryHajj_OnLoad(formContext); ///Aya I think it's executionContext
            HideFieldsOnSubmitStageTechnicalComplainMomentaryHajj(formContext);
            break;

        case ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase():
            TechnicalComplainNotMomentaryHijjAndUmarah_OnLoad(formContext);
            HideFieldsOnSubmitTechnicalComplainNonMomentaryHajjAndUmrah(formContext);
            break;
        case ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase():
            TechnicalComplainNotMomentaryHijjAndUmarah_OnLoad(formContext);
            HideFieldsOnSubmitTechnicalComplainNonMomentaryHajjAndUmrah(formContext);
            break;

        case ServiceType.BusinessSectorComplain.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnologicalComplain.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainInternalPilgrims.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainExternalPilgrims.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainNusukServices.serviceDefinitionId.toLowerCase():
            IntegrationServices_OnLoad(formContext);
            HideFieldsOnSubmitStageRequestComplainsIntegration(formContext);
            break;
    };

};


// #region FC Internal Pilgrims post Hajj service
function FC_InternalPilgrimspostHajj_CompaniesAdministrationStageDecision_OnChange(executionContext) {
    var departmentNeedInfoOnBpf = "header_process_" + caseFields.ldv_companiesadministrationneededinformation;
    var departmentClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_companiesadministrationdecisioncode, departmentNeedInfoOnBpf, departmentClosureReasonOnBpf);
};

function FC_InternalPilgrimspostHajj_OnLoad(formContext) {
    var departmentNeedInfoOnBpf = "header_process_" + caseFields.ldv_companiesadministrationneededinformation;
    var departmentClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnLoad(formContext, caseFields.ldv_companiesadministrationdecisioncode, departmentNeedInfoOnBpf, departmentClosureReasonOnBpf);
    QualityStageDecision_OnLoad(formContext);
};

//#endregion

// #region Financial compensation complain
function FinancialCompensationComplain_MinistrySHajjAgencyStageDecision_OnChange(executionContext) {
    var ministryHajjNeedInfoOnBpf = "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var ministryHajjClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_ministryshajjagencydecisisoncode, ministryHajjNeedInfoOnBpf, ministryHajjClosureReasonOnBpf);
};

function FinancialCompensationComplain_OnLoad(formContext) {
    var ministryHajjNeedInfoOnBpf = "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var ministryHajjClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;


    DepartmentDecision_OnLoad(formContext, caseFields.ldv_ministryshajjagencydecisisoncode, ministryHajjNeedInfoOnBpf, ministryHajjClosureReasonOnBpf);
    QualityStageDecision_OnLoad(formContext);
};

//#endregion

// #region Technical Complain Momentary Umrah For Companies
function TechnicalComplainMomentaryUmrahForCompanies_UmrahsCompanyServiceStageDecision_OnChange(executionContext) {
    var umrahsCompanyNeedInfoOnBpf = "header_process_" + caseFields.ldv_umrahscompanyserviceneededinformation;
    var umrahsCompanyClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_umrahscompanyservicedecisioncode, umrahsCompanyNeedInfoOnBpf, umrahsCompanyClosureReasonOnBpf);
};

function TechnicalComplainMomentaryUmrahForCompanies_OnLoad(formContext) {
    var umrahsCompanyNeedInfoOnBpf = "header_process_" + caseFields.ldv_umrahscompanyserviceneededinformation;
    var umrahsCompanyClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnLoad(formContext, caseFields.ldv_umrahscompanyservicedecisioncode, umrahsCompanyNeedInfoOnBpf, umrahsCompanyClosureReasonOnBpf);
    QualityStageDecision_OnLoad(formContext);
};

//#endregion

// #region Technical Complain Momentary Umrah 
function TechnicalComplainMomentaryUmrah_MakkahStageDecision_OnChange(executionContext) {
    var MakkahyNeedInfoOnBpf = "header_process_" + caseFields.ldv_companiesadministrationneededinformation;
    var MakkahClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons + "_2";

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_companiesservicedecisioncode, MakkahyNeedInfoOnBpf, MakkahClosureReasonOnBpf);
};

function TechnicalComplainMomentaryUmrah_JadaStageDecision_OnChange(executionContext) {
    var JadaNeedInfoOnBpf = "header_process_" + caseFields.ldv_agentemployeeneededinformation
    var JadaClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons + "_1";

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_agentemployeedecisioncode, JadaNeedInfoOnBpf, JadaClosureReasonOnBpf);
};

function TechnicalComplainMomentaryUmrah_MadinaStageDecision_OnChange(executionContext) {
    var MadinaNeedInfoOnBpf = "header_process_" + caseFields.ldv_castingofficerneededinformation;
    var MadinaClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_castingofficerdecisioncode, MadinaNeedInfoOnBpf, MadinaClosureReasonOnBpf);
};
function TechnicalComplainMomentaryUmrah_OnLoad(formContext) {
    var MakkahNeedInfoOnBpf = "header_process_" + caseFields.ldv_companiesadministrationneededinformation;
    var MakkahClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons + "_2";

    var JadaNeedInfoOnBpf = "header_process_" + caseFields.ldv_agentemployeeneededinformation;
    var JadaClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons + "_1";

    var MadinaNeedInfoOnBpf = "header_process_" + caseFields.ldv_castingofficerneededinformation;
    var MadinaClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;



    //Makkah Stage
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_companiesservicedecisioncode, MakkahNeedInfoOnBpf, MakkahClosureReasonOnBpf);
    //Jada Stage
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_agentemployeedecisioncode, JadaNeedInfoOnBpf, JadaClosureReasonOnBpf);
    // Madina
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_castingofficerdecisioncode, MadinaNeedInfoOnBpf, MadinaClosureReasonOnBpf);

    QualityStageDecision_OnLoad(formContext);
};

//#endregion

// #region Inquiry
function Inquiry_QualityStageDecision_OnChange(executionContext) {
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    var transferReasonOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation2;

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_qualityofficerdecisioncode2, transferReasonOnBpf, qualityClosureReasonOnBpf);
};

function Inquiry_QualityStageDecision_OnLoad(formContext) {
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    var transferReasonOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation2;

    DepartmentDecision_OnLoad(formContext, caseFields.ldv_qualityofficerdecisioncode2, transferReasonOnBpf, qualityClosureReasonOnBpf);
};


function unLockFieldsOnQualityStageInquiry(formContext) {
    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    var currentStage = formContext.data.process.getActiveStage();
    var currentStageId = currentStage.getId().toLowerCase();

    if (!service || !currentStage) {
        console.error("Service or current stage is null or undefined.");
        return;
    };

    if (service !== null && service !== undefined) {
        var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
        if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
            var submitStageId = BPFs.Inquiry.stages.quality.id.toLowerCase();
            if (currentStageId === submitStageId) {
                CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_maincategoryid, caseFields.ldv_serviceid);
                CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_subcategoryid, caseFields.ldv_maincategoryid);
                CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(formContext, caseFields.ldv_secondarysubcategoryid, caseFields.ldv_subcategoryid);
            }
        }
    }
};

//#endregion

// #region Suggestions
function Suggestions_ConcernedDepartmentDecision_OnChange(executionContext) {
    var suggestionsCDNeededInfoOnBpf = "header_process_" + caseFields.ldv_departmentneededinformation;
    var suggestionCDClosureReason = "header_process_" + caseFields.ldv_departmentclosurereason;

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_departmentdecisioncode, suggestionsCDNeededInfoOnBpf, suggestionCDClosureReason)
}

function Suggestions_QualityStageDecision_OnChange(executionContext) {
    var qualityNeededInfoOnBpf = "header_process_" + caseFields.ldv_qualityofficerneededinformation;
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;

    DepartmentQualityDecision_OnChange(executionContext, caseFields.ldv_qualityofficerdecisioncode, qualityNeededInfoOnBpf, qualityClosureReasonOnBpf);

};

function Suggestions_OnLoad(formContext) {
    var suggestionsCDNeededInfoOnBpf = "header_process_" + caseFields.ldv_departmentneededinformation;
    var suggestionCDClosureReason = "header_process_" + caseFields.ldv_departmentclosurereason;


    DepartmentDecision_OnLoad(formContext, caseFields.ldv_departmentdecisioncode, suggestionsCDNeededInfoOnBpf, suggestionCDClosureReason);
    QualityStageDecision_OnLoad(formContext);
};
//#endregion


// #region TC-Momentary Hajj
function TechnicalComplainMomentaryHajj_OnLoad(formContext) {


    QualityStageDecision_OnLoad(formContext);

    var needInfoOnBpfMadina = "header_process_" + caseFields.ldv_castingofficerneededinformation;
    var closureReasonOnBpfMadina = "header_process_" + caseFields.ldv_closurereasons + "_2";
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_castingofficerdecisioncode, needInfoOnBpfMadina, closureReasonOnBpfMadina);
    var needInfoOnBpfMakkah = "header_process_" + caseFields.ldv_companiesserviceneededinformation;
    var closureReasonOnBpfMakkah = "header_process_" + caseFields.ldv_closurereasons + "_3";
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_companiesservicedecisioncode, needInfoOnBpfMakkah, closureReasonOnBpfMakkah);
    var needInfoOnBpfBorderCrossing = "header_process_" + caseFields.ldv_agentemployeeneededinformation;
    var closureReasonOnBpfBorderCrossing = "header_process_" + caseFields.ldv_closurereasons + "_1";
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_agentemployeedecisioncode, needInfoOnBpfBorderCrossing, closureReasonOnBpfBorderCrossing);
    var needInfoOnBpfCoordinationCouncil = "header_process_" + caseFields.ldv_coordinationcouncilneededinformation;
    var closureReasonOnBpfCoordinationCouncil = "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_coordinationcouncildecisioncode, needInfoOnBpfCoordinationCouncil, closureReasonOnBpfCoordinationCouncil);

}
function TechnicalComplainMomentaryHajj_MadinaDecision_OnChange(executionContext) {
    //ldv_supervisorid_2 
    var needInfoOnBpfMadina = "header_process_" + caseFields.ldv_castingofficerneededinformation;
    var closureReasonOnBpfMadina = "header_process_" + caseFields.ldv_closurereasons + "_2";

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_castingofficerdecisioncode, needInfoOnBpfMadina, closureReasonOnBpfMadina);
}
function TechnicalComplainMomentaryHajj_MakkahDecision_OnChange(executionContext) {
    //ldv_supervisorid_3
    var needInfoOnBpfMakkah = "header_process_" + caseFields.ldv_companiesserviceneededinformation;
    var closureReasonOnBpfMakkah = "header_process_" + caseFields.ldv_closurereasons + "_3";

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_companiesservicedecisioncode, needInfoOnBpfMakkah, closureReasonOnBpfMakkah);
}
function TechnicalComplainMomentaryHajj_BorderCrossingDecision_OnChange(executionContext) {
    // ldv_supervisorid_1 
    var needInfoOnBpfBorderCrossing = "header_process_" + caseFields.ldv_agentemployeeneededinformation;
    var closureReasonOnBpfBorderCrossing = "header_process_" + caseFields.ldv_closurereasons + "_1";

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_agentemployeedecisioncode, needInfoOnBpfBorderCrossing, closureReasonOnBpfBorderCrossing);
}
function TechnicalComplainMomentaryHajj_CoordinationCouncilDecision_OnChange(executionContext) {
    // ldv_supervisorid
    var needInfoOnBpfCoordinationCouncil = "header_process_" + caseFields.ldv_coordinationcouncilneededinformation;
    var closureReasonOnBpfCoordinationCouncil = "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(executionContext, caseFields.ldv_coordinationcouncildecisioncode, needInfoOnBpfCoordinationCouncil, closureReasonOnBpfCoordinationCouncil);
}



//#endregion


// #region  TechnicalComplainNonMomentaryHajj&Umrah 
function TechnicalComplainNotMomentaryHijjAndUmarah_OnLoad(formContext) {

    QualityStageDecision_OnLoad(formContext);

    var needInfoOnBpfHajj = "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var closureReasonOnBpfHajj = "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_ministryshajjagencydecisisoncode, needInfoOnBpfHajj, closureReasonOnBpfHajj);

    var needInfoOnBpfUmarah = "header_process_" + caseFields.ldv_ministrysumarahagencyneededinformation;
    var closureReasonOnBpfUmarah = "header_process_" + caseFields.ldv_closurereasons + "_1";
    DepartmentDecision_OnLoad(formContext, caseFields.ldv_ministrysumarahagencydecisisoncode, needInfoOnBpfUmarah, closureReasonOnBpfUmarah);

}

function TechnicalComplainNotMomentaryHijjAndUmarah_HajjDecision_OnChange(executionContext) {

    var needInfoOnBpfHajj = "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var closureReasonOnBpfHajj = "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnChange(executionContext, caseFields.ldv_ministryshajjagencydecisisoncode, needInfoOnBpfHajj, closureReasonOnBpfHajj);

}
function TechnicalComplainNotMomentaryHijjAndUmarah_UmarahDecision_OnChange(executionContext) {
    var needInfoOnBpfUmarah = "header_process_" + caseFields.ldv_ministrysumarahagencyneededinformation;
    var closureReasonOnBpfUmarah = "header_process_" + caseFields.ldv_closurereasons + "_1";
    DepartmentDecision_OnChange(executionContext, caseFields.ldv_ministrysumarahagencydecisisoncode, needInfoOnBpfUmarah, closureReasonOnBpfUmarah);

}

//#endregion

//#region Integration Services

//using the general quality method directly

function IntegrationServices_QualityDecision_OnChange(executionContext) {
    var quality2ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason + "_2";
    DepartmentQualityDecision_OnChange(executionContext, caseFields.ldv_qualitydecisioncode, quality2ClosureReasonOnBpf);
};

function IntegrationServices_Quality3Decision_OnChange(executionContext) {
    var quality3ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason + "_1";
    DepartmentQualityDecision_OnChange(executionContext, caseFields.ldv_qualitydecisioncode, quality3ClosureReasonOnBpf);
};

function IntegrationServices_IsFCR_OnChange(executionContext) {
    var formContext = executionContext.getFormContext();
    var isFCR = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_isfcr);
    if (isFCR === null || isFCR === undefined) {
        HideUCIButtons(executionContext);
        return;
    }
    if (isFCR) {
        ShowNextStage(executionContext);
    }
    else {
        HideUCIButtons(executionContext);
    }
}

function IntegrationServices_OnLoad(formContext) {
    var quality2ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason + "_2";
    var quality3ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason + "_1";
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;

    DepartmentQualityDecision_OnLoad(formContext, caseFields.ldv_qualitydecisioncode, quality2ClosureReasonOnBpf);
    DepartmentQualityDecision_OnLoad(formContext, caseFields.ldv_qualitydecisioncode, quality3ClosureReasonOnBpf);
    DepartmentQualityDecision_OnLoad(formContext, caseFields.ldv_qualitydecisioncode, qualityClosureReasonOnBpf);


};

//#endregion


// General Quality method
function QualityStageDecision_OnChange(executionContext) {
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    DepartmentQualityDecision_OnChange(executionContext, caseFields.ldv_qualitydecisioncode, qualityClosureReasonOnBpf);
};

function QualityStageDecision_OnLoad(formContext) {
    var qualityClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    DepartmentQualityDecision_OnLoad(formContext, caseFields.ldv_qualitydecisioncode, qualityClosureReasonOnBpf);
};


// #region Common Functions For Decisions

//if need more information >> hide next and back buttons and the agent assign it manually
function DepartmentDecision_OnChange(executionContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf, supervisorSchemaNameOnBpf) {
    debugger;
    var formContext = executionContext.getFormContext();

    if (!decisionSchemaName || !needInformationSchemaNameOnBpf || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    // Clear the value of the needMoreDetails field if the decision is not "needMoreDetails"
    if (decision !== caseFields.Enums.departmentDecision.needMoreDetails) {
        CommonGeneric.EmptyField(formContext, caseFields.ldv_needsmoredetails);
    }

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);

        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(formContext, supervisorSchemaNameOnBpf, false, false);
        }
    }



    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(formContext, supervisorSchemaNameOnBpf, false, false);
            var supervisorSchemaNameOnForm = GetLogicalFieldName(formContext, supervisorSchemaNameOnBpf);
            if (supervisorSchemaNameOnForm) {
                CommonGeneric.EmptyField(formContext, supervisorSchemaNameOnForm);

            }
        }
        var needInfoSchemaNameOnForm = GetLogicalFieldName(formContext, needInformationSchemaNameOnBpf);
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
        }
        ShowNextStage(executionContext);

    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) { //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(formContext, supervisorSchemaNameOnBpf, true, true);
        }
        var closureReasonSchemaNameOnForm = GetLogicalFieldName(formContext, closureReasonSchemaNameOnBpf);
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        HideUCIButtons(executionContext);


        var needInformationSchemaNameOnForm = GetLogicalFieldName(formContext, needInformationSchemaNameOnBpf);
        if (needInformationSchemaNameOnForm) {
            formContext.getAttribute(needInformationSchemaNameOnForm).addOnChange(function () {
                // Set the value of the other field based on the changed value of needInformationSchemaNameOnBpf
                var needInformationValue = CommonGeneric.GetFieldValue(formContext, needInformationSchemaNameOnForm);

                CommonGeneric.SetFieldValue(formContext, caseFields.ldv_needsmoredetails, needInformationValue);
            });
        }


    }

};
function DepartmentDecision_OnLoad(formContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf, supervisorSchemaNameOnBpf) {
    debugger;

    if (!decisionSchemaName || !needInformationSchemaNameOnBpf || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(formContext, supervisorSchemaNameOnBpf, false, false);
        }
    }



    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(formContext, supervisorSchemaNameOnBpf, false, false);
        }

    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) { //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(formContext, supervisorSchemaNameOnBpf, true, true);
        }


    }

};

//in all choises >> show next button to go to the next stage
function DepartmentQualityDecision_OnChange(executionContext, decisionSchemaName, closureReasonSchemaNameOnBpf) {
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
    if (decision === caseFields.Enums.qualityDecisR1.CloseTheTicket || decision === caseFields.Enums.qualityDecisR1.NotResolved) {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        ShowNextStage(executionContext);
    }
};
function DepartmentQualityDecision_OnLoad(formContext, decisionSchemaName, closureReasonSchemaNameOnBpf) {
    debugger;
    if (!decisionSchemaName || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }
    else {
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);

    }

};
//if Transfer >> hide next and back buttons and the agent assign it manually
function QualityOfficerDecision2_OnChange(executionContext, decisionSchemaName, closureReasonSchemaNameOnBpf) {
    debugger;
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

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
        var closureReasonSchemaNameOnForm = GetLogicalFieldName(formContext, closureReasonSchemaNameOnBpf);
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        HideUCIButtons(executionContext);

    }

};
function QualityOfficerDecision2_OnLoad(formContext, decisionSchemaName, closureReasonSchemaNameOnBpf) {
    debugger;
    if (!decisionSchemaName || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

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
///////////////////////////FOR R2////////if need more information >> hide next and Show back button to go to the previous stage
function DepartmentQualityDecisionForR2_OnChange(executionContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf) {
    debugger;
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !needInformationSchemaNameOnBpf || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
    }



    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) { //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, false, false);
        var needInfoSchemaNameOnForm = GetLogicalFieldName(formContext, needInformationSchemaNameOnBpf);
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
        }
        ShowNextStage(executionContext);


    } else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) { //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(formContext, needInformationSchemaNameOnBpf, true, true);
        CommonGeneric.ShowAndReuiredField(formContext, closureReasonSchemaNameOnBpf, false, false);
        var closureReasonSchemaNameOnForm = GetLogicalFieldName(formContext, closureReasonSchemaNameOnBpf);
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }

        ShowPreviousStage(executionContext);
        //HideUCIButtons();




    }

};
function DepartmentQualityDecisionForR2_OnLoad(formContext, decisionSchemaName, needInformationSchemaNameOnBpf, closureReasonSchemaNameOnBpf) {
    debugger;
    if (!decisionSchemaName || !needInformationSchemaNameOnBpf || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

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
        } else if (currentBPFId !== null && newBPFId !== null && currentBPFId.toLowerCase() === newBPFId.toLowerCase()) {
            // If the current BPF is the same as the new BPF, execute the callback function directly
            callBackFn(); // Execute the callback function 
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
    if (lookupRecord !== null) {
        var entityId = lookupRecord.id;
        var lookupLogicalName = lookupRecord.entityType;

        entityId = entityId.replace('{', '').replace('}', '');

        // Construct URL with expand parameter and entityId
        var entityName = lookupLogicalName.toLowerCase();
        if (entityName.endsWith('y')) {
            entityName = entityName.slice(0, -1) + 'ies';
        } else {
            entityName += 's';
        }
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

function GetLogicalFieldName(formContext, schemaName) {
    var logicalFieldName = null;
    if (schemaName) {
        // Remove the "header_process_" prefix if present
        logicalFieldName = schemaName.replace(/^header_process_/i, "");
        // Check if the field exists without suffix
        if (!formContext.getControl(logicalFieldName)) {
            // Check if the field has "_number" at the end
            logicalFieldName = logicalFieldName.replace(/_(\d+)?$/, "");
        }
    }
    return logicalFieldName;
};

function IsFieldVisible(formContext, fieldName) {
    var control = formContext.getControl(fieldName);
    if (control) {
        return control.getVisible();
    } else {
        console.error("Control for field " + fieldName + " not found.");
        return false;
    }
};

function ChangeFieldLabel(formContext, fieldName, newLabel) {
    var control = formContext.getControl(fieldName);

    if (control) {
        control.setLabel(newLabel);
    } else {
        console.error("Field control not found:", fieldName);
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
function overrideSetActiveButton() {
    var setActiveButton = parent.document.querySelector("[aria-label='Set Active']");
    if (setActiveButton) {
        setActiveButton.onclick = function (event) {
            event.preventDefault(); // Prevent the default behavior of the button
            alert("This action is not allowed.");
        };
    }
}