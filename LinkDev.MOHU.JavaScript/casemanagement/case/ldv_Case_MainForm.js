var caseFields = {
    ldv_name: "ldv_name",
    customerid: "customerid",
    ldv_fieldserviceofficeid: "ldv_fieldserviceofficeid",
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
    ldv_requesttypeid: "ldv_requesttypeid",
    ldv_company: "ldv_company",
    ldv_filteredcompanyid: "ldv_filteredcompanyid",
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
    ldv_companiesadministrationdecisioncode:
        "ldv_companiesadministrationdecisioncode",
    ldv_companiesadministrationneededinformation:
        "ldv_companiesadministrationneededinformation",
    ldv_umrahscompanyservicedecisioncode: "ldv_umrahscompanyservicedecisioncode",
    ldv_umrahscompanyserviceneededinformation:
        "ldv_umrahscompanyserviceneededinformation",
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
    ldv_coordinationcouncilneededinformation:
        "ldv_coordinationcouncilneededinformation",
    ldv_companiesserviceneededinformation:
        "ldv_companiesserviceneededinformation",

    ldv_ministryshajjagencyneededinformation:
        "ldv_ministryshajjagencyneededinformation",
    ldv_ministryshajjagencydecisisoncode: "ldv_ministryshajjagencydecisisoncode",
    ldv_ministrysumarahagencydecisisoncode:
        "ldv_ministrysumarahagencydecisisoncode",
    ldv_ministrysumarahagencyneededinformation:
        "ldv_ministrysumarahagencyneededinformation",
    ldv_needsmoredetails: "ldv_needsmoredetails",
    caseorigincode: "caseorigincode",
    ldv_subsourceid: "ldv_subsourceid",
    ldv_companytext: "ldv_companytext",
    ldv_issenttotasher: "ldv_issenttotasher",
    ldv_senttokidana: "ldv_senttokidana",
    ldv_senttoservicedesk: "ldv_senttoservicedesk",
    ldv_issenttosehab: "ldv_issenttosehab",
    ldv_isservicedeskupdated: "ldv_isservicedeskupdated",
    ldv_istashirupdated: "ldv_istashirupdated",
    ldv_iskadanaupdated: "ldv_iskadanaupdated",
    ldv_qualityofficerneededinformations: "ldv_qualityofficerneededinformations",
    ldv_Ispassedcustomerapproval: "ldv_Ispassedcustomerapproval",
    ldv_isfcr: "ldv_isfcr",
    ldv_fcrdecisioncode: "ldv_fcrdecisioncode",

    ldv_socialMediaDecisioncode: "ldv_socialmediadecisioncode",
    ldv_socialMediaComment: "ldv_socialmediacomment",
    ldv_supervisordecisioncode: "ldv_supervisordecisioncode",
    ldv_supervisorcomment: "ldv_supervisorcomment",
    ldv_supervisorDesision2: "ldv_supervisorclosedecisioncode",
    ldv_description: "ldv_description",
    ldv_cxdecisioncode: "ldv_cxdecisioncode",
    ldv_closurereasoncx: "ldv_closurereasoncx",
    ldv_customersatisfycode: "ldv_customersatisfycode",
    ldv_nusukdecisioncode: "ldv_nusukdecisioncode",
    ldv_nusukcarecomment: "ldv_nusukcarecomment",
    ldv_nusukclosedecisioncode: "ldv_nusukclosedecisioncode",
    ldv_inquirysocialmediadecisioncode: "ldv_inquirysocialmediadecisioncode",
    ldv_surveysatisfactioncode: "ldv_surveysatisfactioncode",
    parentcaseid: "parentcaseid",

    Enums: {
        location: {
            Mekkah: 1,
            Madina: 2,
            BorderCrossings: 3,
            Arrafat: 4,
            Muzdalifa: 5,
            Mina: 6,
            Jada: 7,
        },
        season: {
            Hajj: 1,
            Umrah: 2,
        },
        errorRecord: {
            name: "التطبيق",
            id: "0B6E8AD6-98D4-EE11-904D-6045BD8C9FF4",
        },
        departmentDecision: {
            closeTheTicket: 1,
            needMoreDetails: 2,
        },
        qualityDecision: {
            closeTheTicket: 1, //Resolved
            needMoreDetails: 2, //Transfer
        },
        qualityDecision2: {
            closeTheTicket: 1,
            transferTheTicket: 2,
        },
        qualityDecisR1: {
            CloseTheTicket: 1,
            NotResolved: 2,
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
            Twitter: 3986,
        },
        beneficiaryType: {
            InternalHajj: 1,
            ExternalHajj: 2,
            InternalUmrah: 3,
            ExternalUmrah: 4,
            Visitor: 5,
        },
        socialMediaDecision: {
            checkedTicket: 1,
        },
        supervisorDecision2: {
            CloseTicket: 1,
        },
        nusukTeamDecision: {
            Resolved: 1,
            SendBack: 2,
        },
        companyDecision: {
            Resolved: 1,
            NeedMoreInformation: 2,
        },
        socialmediadecision2options: {
            Resolved: 1,
            Transfer: 2,
        },
        surveySatisfaction: {
            satisfied: 1,
            notSatisfied: 2,
            noReply: 3,
        },
    },
};

var ServiceType = {
    FinancialComplainInternalPilgrimspostHajj: {
        serviceDefinitionId: "8580A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Financial complain - Internal Pilgrims post-Hajj",
    },
    FinancialCompensationComplain: {
        serviceDefinitionId: "8780a868-2dcc-ee11-907a-6045bd8c92a2",
        serviceDefinitionName: "Financial compensation complain",
    },
    TechnicalComplainNonMomentaryHajj: {
        serviceDefinitionId: "7D80A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technical complain - Non-momentary Hajj",
    },
    TechnicalComplainNonMomentaryUmrah: {
        serviceDefinitionId: "7780A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technical complain - Non-momentary Umrah",
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
    Inquiry: {
        serviceDefinitionId: "E8015016-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName: "Inquiry",
    },
    Suggestions: {
        serviceDefinitionId: "E5C52C61-4BCB-EE11-9079-6045BD895C76",
        serviceDefinitionName: "Suggestions",
    },
    Testimonials: {
        serviceDefinitionId: "C6C41A5C-8E8C-EF11-AC20-6045BD8FAE55",
        serviceDefinitionName: "Testimonials ",
    },

    //Integration
    BusinessSectorComplainKadana: {
        serviceDefinitionId: "8980A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Business sector Complain Kadana",
    },
    PilgrimsComplaintsDirectHajj: {
        serviceDefinitionId: "8380A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Pilgrims complaints direct Hajj",
    },
    //FinancialComplainInternalPilgrims: {
    //    serviceDefinitionId: "8180A868-2DCC-EE11-907A-6045BD8C92A2",
    //    serviceDefinitionName: "Financial complain - Internal Pilgrims",
    //},
    //FinancialComplainNusukServices: {
    //    serviceDefinitionId: "7F80A868-2DCC-EE11-907A-6045BD8C92A2",
    //    serviceDefinitionName: "Financial complain - Nusuk services",
    //},
    TechnologicalComplainSD: {
        serviceDefinitionId: "7580A868-2DCC-EE11-907A-6045BD8C92A2",
        serviceDefinitionName: "Technological Complain SD",
    },
    BusinessSectorComplainSD: {
        serviceDefinitionId: "365361d4-4094-ef11-8a6a-6045bd9ec6ef",
        serviceDefinitionName: "Business Sector Complaints SD",
    },

    //nusuk services (Mentoring -Missing Persons  )
    MentoringServiceNUSUKEnaya: {
        serviceDefinitionId: "7A8C2CBC-8F8C-EF11-AC20-6045BD8FAE55",
        serviceDefinitionName: "Mentoring Service - NUSUK Enaya",
    },
    MissingPersonsServiceNUSUKEnaya: {
        serviceDefinitionId: "F1CC8DDA-8F8C-EF11-AC20-6045BD8FAE55",
        serviceDefinitionName: "Missing Persons Service - NUSUK Enaya",
    },

    //Sahab Integration
    SahabReportService: {
        serviceDefinitionId: "5654F43C-5B8C-EF11-AC21-6045BD9EC6EF",
        serviceDefinitionName: "Sahab Reports",

    }
};

var BPFs = {
    DepartmentComplain: {
        name: "BPF | Department Complain",
        id: "90FCF696-998F-EF11-8A6A-6045BD9EC6EF",
        stages: {
            submit: {
                name: "Submit",
                id: "714fe783-ad5b-4ee0-ae5a-feabd8603a1b",
            },
            socialMedia: {
                name: "Social Media",
                id: "ad6f87aa-6694-48ff-a07f-2bebff203648",
            },
            department: {
                name: `Department`,
                id: "fd572bb5-921f-41ce-9418-b01ff043c3b6",
            },
            department2: {
                name: `Department2`,
                id: "e582b543-7cc1-448b-8a9c-695588ff1ebe",
            },
            customerApproval: {
                name: "Customer Approval",
                id: "7e3a329d-b187-4fd2-aa0b-6ccdf411a3ad",
            },
            supervisor: {
                name: "Supervisor",
                id: "f3edc09a-680d-4457-bb12-34958826ae58",
            },
            quality: {
                name: "Quality",
                id: "6c7fd148-0a75-44fa-96f6-42c9d2dc9360",
            },
            resolved: {
                name: "Resolved",
                id: "8b610ebd-1ca9-4fa1-8bef-88dae9fddab8",
            },
        },
    },

    TechnicalComplainMomentaryUmrahForCompanies: {
        name: "BPF | 7-TC- Momentary Umrah for companies",
        id: "62A95EB9-D108-4B71-BE1E-4D786F685ED3",
        stages: {
            submit: {
                name: "Submit",
                id: "054a0655-cbe0-4c94-9797-05da8bfd567d",
            },
            socialMedia: {
                name: "Social Media",
                id: "889329db-c1b6-46a1-aeaf-e86742f02ee7",
            },
            umrahsCompanyService: {
                name: "Umrah's Company Service",
                id: "cef9ddbe-0e55-40df-9850-6c8ef2adf962",
            },
            supervisor: {
                name: "Supervisor",
                id: "b7a1ea91-acf8-4bea-9f7f-ec5193e19f86",
            },
            quality: {
                name: "Quality",
                id: "6efe9340-4903-43ce-abe4-5912daf18914",
            },
            resolved: {
                name: "Resolved",
                id: "2a31c770-fdd9-4c62-836d-9735dbe8bb9b",
            },
        },
    },
    TechnicalComplainCompanyService: {
        name: "BPF | Company Service",
        id: "ABD0618A-DB8E-EF11-AC21-6045BD9EC6EF",
        stages: {
            submit: {
                name: "Submit",
                id: "17b09efe-3c54-43b5-8645-dee4b47b7b4d",
            },
            socialMedia: {
                name: "Social Media",
                id: "269c1778-091a-434c-8483-2c670db0c522",
            },
            supervisor: {
                name: "Supervisor",
                id: "5bfbd82c-9ca8-4a8a-9cc9-698ad44d7cb0",
            },
            company: {
                name: "Company",
                id: "d170ccc5-5c2d-4299-b24d-cef5c0aded57",
            },
            company2: {
                name: "Company2",
                id: "8f4aa080-97bc-4514-bd91-5620eaed43a6",
            },
            customerApproval: {
                name: "Customer Approval",
                id: "fbea045c-9ca4-402b-b1b1-b46494a2c2c6",
            },
            quality: {
                name: "Quality",
                id: "551b7836-0fa9-402e-a3da-e497de20352e",
            },
            resolved: {
                name: "Resolve",
                id: "a7dc5ff2-32ff-4b2c-b411-2e6a4b5607c8",
            },
        },
    },
    Inquiry: {
        name: "BPF | Inquiry Request Process",
        id: "371D39F9-85D8-4DBF-A96C-16FD839D3B27",
        stages: {
            submit: {
                name: "Submit",
                id: "c366cb3d-f8a7-48cb-b299-d892f32c0f3a",
            },
            socialMedia: {
                name: "Social Media",
                id: "42099590-e8a9-4cc0-9d4d-4718fd79d5e0",
            },
            quality: {
                name: "Quality",
                id: "992a6841-305e-477c-b477-a966b082ca02",
            },
            quality2: {
                name: "Quality",
                id: "025de776-7a6b-41b1-b9fb-ec58d0f4098e",
            },
            supervisor: {
                name: "Supervisor",
                id: "08af1f16-9895-4995-a6af-69b38adfd07b",
            },
            supervisor2: {
                name: "Supervisor",
                id: "67f49211-7db9-4a96-befc-cf77006fbc8a",
            },
            customerApproval: {
                name: "Customer Approval",
                id: "317f7384-9be0-46ae-94e2-2c69981c373f",
            },
            resolved: {
                name: "Resolved",
                id: "0f13780c-ea1a-44d6-80c2-8410dbb53f63",
            },
        },
    },
    Suggestions: {
        name: "BPF | Suggestions Request Process",
        id: "0E95C412-83A2-46F8-8DDB-D18543CE4C77",
        stages: {
            submit: {
                name: "Submit",
                id: "b9e77c7f-cb09-4dfe-b306-8ea4fcad4a4c",
            },
            socialMedia: {
                name: "Social Media",
                id: "0a7d3e39-bed8-488c-8cd0-15f12cc15c67",
            },
            CX: {
                name: "CX",
                id: "c652bb0d-4cc4-4862-afba-b5a11a3cfc6d",
            },
            Closed: {
                name: "Closed",
                id: "abce898f-a2c2-4cf9-94fa-de3e5ca47704",
            },
        },
    },
    TechnicalComplainMomentaryHajj: {
        name: "BPF |TC - Momentary Hajj",
        id: "BE764FE2-27F8-4858-B265-3670C79DD222",
        stages: {
            Submit: {
                name: "Submit",
                id: "e1837ccd-7810-4d4e-baa8-cb2281547250",
            },
            Madina: {
                name: "Madina",
                id: "d14d0f04-5792-47fa-b5da-2872e0c708b4",
            },
            Makkah: {
                name: "Makkah",
                id: "f7a03640-d8c1-4d04-a64c-1036ad7209c5",
            },

            BorderCrossing: {
                name: "BorderCrossing",
                id: "855e43be-68a7-4901-90f7-3ae469008ea8",
            },
            CoordinationCouncil: {
                name: "CoordinationCouncil",
                id: "e44adc4f-9947-48da-8d83-94546fbe51a1",
            },
            Quality: {
                name: "Quality",
                id: "ac37d1ec-87e3-4ea0-8a9d-b43126ed80a7",
            },
            Quality2: {
                name: "Quality",
                id: "9534e56e-6737-4bb7-88c0-ad87342a2725",
            },
            Quality3: {
                name: "Quality",
                id: "eec59f93-097f-44c6-a509-783bf05252b2",
            },
            Quality4: {
                name: "Quality",
                id: "bd5ef71f-b867-4a7e-b737-64cf44ed529e",
            },
            Resolved: {
                name: "Resolved",
                id: "68ccc8b3-1c5d-4f66-9187-21d5dd06269e",
            },
            SocialMedia: {
                name: "Social Media",
                id: "2a3b7b97-5990-4846-a94e-e46e91e53a5f",
            },
            Supervisor: {
                name: "Supervisor",
                id: "a35c934b-3f37-4ff1-8daf-3dcd1e4205c9",
            },
            Supervisor2: {
                name: "Supervisor",
                id: "a5b5b32e-3211-4426-888e-b7a6d6c06492",
            },
            Supervisor3: {
                name: "Supervisor",
                id: "b8ca92e8-9c23-4967-ac41-4307faa4c76f",
            },
            Supervisor4: {
                name: "Supervisor",
                id: "aef9ac1e-a6cc-4252-8111-5b44807051db",
            },
        },
    },
    TechnicalComplainNotMomentaryHijjAndUmarah: {
        name: "BPF | TC - Not Momentary Hajj and Umrah Process",
        id: "705D3F16-E646-4CE0-B375-A43444597753",
        stages: {
            Submit: {
                name: "Submit",
                id: "ef1eab3c-ac6e-4375-af03-851d40ff3c8f",
            },
            SocialMedia: {
                name: "Social Media",
                id: "944efce3-c44e-4dfa-99b8-23bdaba9dd89",
            },
            Season: {
                name: "Season ",
                id: "8d7648f2-fd9a-4f13-90fe-0a06baead77a",
            },
            Hajj: {
                name: "Hajj",
                id: "ae916fe5-4185-42f1-854b-e2bf14875be8",
            },
            Umrah: {
                name: "Umrah",
                id: "bc6ab56a-8979-4178-aa16-b2d0965a2a19",
            },
            Supervisor: {
                name: "Supervisor ",
                id: "48ea1a3c-bad3-4e10-8250-a1ba15cb7024",
            },
            Supervisor1: {
                name: "Supervisor",
                id: "29737a13-da8a-4edc-88e3-7b8c802d0de0",
            },
            Quality: {
                name: "Quality",
                id: "c11ceeb2-f5c2-41ed-8e51-1f8e9d0f6391",
            },
            Quality1: {
                name: "Quality",
                id: "93cf589a-a8b6-4e6d-ac6b-2ae1a4b8a5a5",
            },
            Resolved: {
                name: "Resolved",
                id: "d32598c5-e107-4488-ad85-c76896169a3d",
            },
            Resolved1: {
                name: "Resolved",
                id: "bc76a5e0-038f-4a40-a731-cc621768e4af",
            },
        },
    },
    RequestComplainsIntegration: {
        name: "BPF | Requesting Complaints | Integration | Flow",
        id: "C1FEA096-843C-4573-BD77-D5C767618B8F",
        stages: {
            Submit: {
                name: "Submit",
                id: "a3ad32dd-5716-43b0-8be5-dc4b08a40a71",
            },
            SocialMedia: {
                name: "Social Media",
                id: "95b4f090-c9b8-4f39-b212-62ce8da39fac",
            },
            FCR: {
                name: "FCR",
                id: "26161fe6-3027-4642-8d0d-ae53ea5ccf01",
            },
            CheckIntegration: {
                name: "Check Integration",
                id: "4123cf2a-8be4-4a3b-a0ef-053ced2cb15a",
            },
            Integration: {
                name: "Integration",
                id: "bc4ee276-49b1-4254-bcbb-407b974fe794",
            },
            Integration2: {
                name: "Integration",
                id: "efbdd49d-27c7-44f5-bb4d-e70ff9aadb48",
            },
            Quality: {
                name: "Quality",
                id: "b1657382-ac54-4f1d-bfa3-9d8de916f840",
            },
            Supervisor: {
                name: "Supervisor",
                id: "9e1605ab-8b8e-4c07-b603-951fe39aa2af",
            },
            CustomerApproval: {
                name: "Customer Approval",
                id: "67d7ec68-60ec-4cae-8ebd-151650ba7d25",
            },
            Resolved: {
                name: "Resolved",
                id: "8c39d033-174f-47e7-a902-a9115af37bde",
            },
            ResolvedTransfer: {
                name: "Resolved",
                id: "787286ac-8292-4ef7-a987-6489155987fe",
            },
        },
    },
    // BPF for new nusuk care services
    NusukCareServicesRequest: {
        name: "BPF| Nusuk Care Guidance Request",
        id: "AA4570AA-4294-EF11-8A6A-6045BD9EC6EF",
        stages: {
            submit: {
                name: "Submit",
                id: "2cce20a4-5a6f-4be1-866d-e744b390590d",
            },
            nusuk1: {
                name: "Nusuk1",
                id: "eb2e6270-b920-404a-a07c-2f291c49a71a",
            },
            company: {
                name: "Company",
                id: "cb629b45-1fc8-426f-9832-58752735ce76",
            },
            nusuk: {
                name: "Nusuk",
                id: "06fadf0f-1552-4586-a857-6ba8f02f3ae2",
            },
            customerApproval: {
                name: "Customer Approval",
                id: "2b539a21-2720-47c4-a3bc-e22e8c3853e4",
            },
            close: {
                name: "Close",
                id: "af4ae1f2-7cfb-4f97-afc7-bcefc802df57",
            },
        },
    },
};

var Tabs = {
    tab_requestinformation: "tab_requestinformation",
    tab_administiration: "tab_administiration",
    tab_surveyServices: "tab_surveyServices",
    tab_SurveyResponse: "tab_SurveyResponse",
};

var Sections = {
    SLATimer: "tab_requestinformation_section_slatimer",
    IntegrationSection: "tab_administiration_section_7",
    Tasheer: "tab_administiration_section_6",
    Kidana: "tab_administiration_section_8",
    ServiceDesk: "tab_administiration_section_9",
    Sahab: "tab_hidden_section_5",
    Decisions: "tab_requestinformation_section_decision",
};
var ApplicantType = {
    individual: 1,
    company: 2,
    both: 3,
};
var jaddaAdded = true;
var madinaAdded = true;

function OnLoad(executionContext) {
    debugger;
    const formContext = executionContext.getFormContext();
    onLoadExtension(formContext);

    HandleBPF(executionContext, GetDecisionsBasedOnService_OnLoad);
    OnChange_MainCategory(formContext);
    OnChange_SubCategory(formContext);
    OnChange_ParentRequestType(formContext);
    HideAndShowComplainPriority(formContext);
    ShowAndHideCompany(formContext);
    ShowAndHideBeneficiaryType_OnLoad(formContext);
    ShowAndHideSeason_OnLoad(formContext);
    ShowAndHideLocation_OnLoad(formContext);
    ShowAndHideComplainType(formContext);

    UnlockCategoryFieldsBeforeSubmit(formContext);
    LockFormFieldsAfterSubmit(formContext);

    ChangeTicketRequestLabel(formContext);
    RemoveCaseOriginOptionsInCreate(formContext);

    ShowAndHideIntegrationSection(formContext);
    ShowAndHide_Sahab_Tasheer_SD_Kidana_Section(formContext);
    ShowAndHideSLATimerSection(formContext);
    // ShowAndHideSurveyServicesTab(formContext);
    showAndHideSubSourceField(formContext);
    FilterCustomerTypeBasedOnService(formContext);

    if (formContext.ui.getFormType() !== 1) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_serviceid, true); // Lock the Request Type Field
        CommonGeneric.DisableField(formContext, caseFields.caseorigincode, true); // Lock the Origin Field
        ShowAndHidePassportNumberAndIdNumber(executionContext);
    }

    UnlockFieldsInSocialMediaStage(formContext);

    addOnChangeHandlerEvents(executionContext);

    formContext.getControl(caseFields.ldv_subsourceid).addPreSearch(function () {
        filterSubSourceBasedOnOrigin(formContext);
    });

    formContext.getControl(caseFields.customerid).addPreSearch(function () {
        FilterCustomerTypeBasedOnService(formContext);
    });

    SetReqForMainCategoryForInquiry(formContext);
    unLockCategoryFieldsOnInquiry(formContext);
    HideEmptyParentCaseField(formContext);
    ShowRequiredFieldsForSahabReportCase(formContext);
}

function addOnChangeHandlerEvents(executionContext) {
    var formContext = executionContext.getFormContext();

    formContext.getAttribute(caseFields.ldv_serviceid).addOnChange(function () {
        OnChange_RequestType(formContext);
    });

    formContext.getAttribute(caseFields.ldv_processid).addOnChange(function () {
        OnChange_ProcessId(formContext);
    });

    formContext
        .getAttribute(caseFields.ldv_maincategoryid)
        .addOnChange(function () {
            var fieldsToBeEmpty = [
                caseFields.ldv_subcategoryid,
                caseFields.ldv_secondarysubcategoryid,
                caseFields.ldv_errorcodeid,
            ];
            fieldsToBeEmpty.forEach(function (fieldSchemaName) {
                CommonGeneric.EmptyField(formContext, fieldSchemaName);
            });
            OnChange_MainCategory(formContext);
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_subcategoryid,
                caseFields.ldv_maincategoryid
            );
        });

    formContext
        .getAttribute(caseFields.ldv_subcategoryid)
        .addOnChange(function () {
            var fieldsToBeEmpty = [
                caseFields.ldv_secondarysubcategoryid,
                caseFields.ldv_errorcodeid,
            ];
            fieldsToBeEmpty.forEach(function (fieldSchemaName) {
                CommonGeneric.EmptyField(formContext, fieldSchemaName);
            });
            OnChange_SubCategory(formContext);
            HandleErrorFieldVisibility(formContext, caseFields.ldv_subcategoryid);
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_secondarysubcategoryid,
                caseFields.ldv_subcategoryid
            );
            /*SettingComplainTypePrioritySeasonFromSubCategory(formContext);*/
        });

    formContext
        .getAttribute(caseFields.ldv_secondarysubcategoryid)
        .addOnChange(function () {
            CommonGeneric.EmptyField(formContext, caseFields.ldv_errorcodeid);
            HandleErrorFieldVisibility(
                formContext,
                caseFields.ldv_secondarysubcategoryid
            );
            saveFormOnChangeFCR(formContext);
            SettingComplainTypePrioritySeasonFromSecondarySubCategory(formContext);
        });
    // formContext
    //   .getAttribute(caseFields.ldv_surveysatisfactioncode)
    //   .addOnChange(function () {
    //     ShowAndHideSurveyServicesTab(formContext);
    //   });

    formContext.getAttribute(caseFields.ldv_errorcodeid).addOnChange(function () {
        saveFormOnChangeFCR(formContext);
    });
    //formContext.getAttribute(caseFields.ldv_locationcode).addOnChange(function () {
    //    OnChange_Location(formContext);
    //});

    formContext.getAttribute(caseFields.ldv_errorcodeid).addOnChange(function () {
        saveFormOnChangeFCR(formContext);
    });
    //formContext.getAttribute(caseFields.ldv_locationcode).addOnChange(function () {
    //    OnChange_Location(formContext);
    //});

    formContext.getAttribute(caseFields.ldv_seasoncode).addOnChange(function () {
        OnChange_Season(formContext);
    });

    formContext.getAttribute(caseFields.customerid).addOnChange(function () {
        ShowAndHidePassportNumberAndIdNumber(executionContext);
    });

    formContext.getAttribute(caseFields.caseorigincode).addOnChange(function () {
        CommonGeneric.EmptyField(formContext, caseFields.ldv_subsourceid);
        showAndHideSubSourceField(formContext);
    });
    formContext
        .getAttribute(caseFields.ldv_requesttypeid)
        .addOnChange(function () {
            OnChange_ParentRequestType(formContext);
        });

    formContext
        .getAttribute(caseFields.ldv_beneficiarytypecode)
        .addOnChange(function () {
            debugger;
            handleBeneificiaryTypeChange();
            //ShowAndHideCompany(formContext);
            //formContext.getAttribute(caseFields.ldv_locationcode).fireOnChange();
            UnLockCompanyFiled(formContext);
            EmptyCompanyFieldOnChangeBeneficiaryType(formContext);
            removeMadinaFromLocation(formContext);
        });

    // formContext.getControl(caseFields.ldv_company).addPreSearch(function () {
    //     filterCompanyBasedOnServiceType(formContext);
    // });
}

function OnSave(executionContext) {
    var saveEvent = executionContext.getEventArgs();
    var formContext = executionContext.getFormContext();
}
function saveFormOnChangeFCR(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service === null || service === undefined) {
        return;
    } else {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();

        if (
            serviceId ===
            ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase() &&
            formContext.ui.getFormType() !== 1
        ) {
            formContext.data.save();
        }
    }
}

function ShowAndHideBeneficiaryType(formContext) {
    debugger;
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service === null || service === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            caseFields.ldv_beneficiarytypecode,
            false,
            false
        );
    } else {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
        RemoveAllOptionsFromBeneficiaryType(formContext);
        AddAllOptionsFromBeneficiaryType(formContext);
        if (
            serviceId ===
            ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            RemoveUmrahOptionsFromBeneficiaryType(formContext);
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                ); // Lock the field
            }
        } else if (
            serviceId ===
            ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase()
        ) {
            //OnChange_Season(formContext);
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            RemoveHajjOptionsFromBeneficiaryType(formContext);
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                );
            }
        } else if (
            serviceId ===
            ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            RemoveVisitorOptionFromBeneficiaryType(formContext);
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                );
            }
        } else if (
            serviceId ===
            ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase() ||
            serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase() ||
            serviceId === ServiceType.Suggestions.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.Testimonials.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.MentoringServiceNUSUKEnaya.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.MissingPersonsServiceNUSUKEnaya.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                );
            }
        }
    }
}
function ShowAndHideBeneficiaryType_OnLoad(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service === null || service === undefined) {
        CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
            formContext,
            caseFields.ldv_beneficiarytypecode,
            false,
            false
        );
    } else {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
        RemoveAllOptionsFromBeneficiaryType(formContext);
        AddAllOptionsFromBeneficiaryType(formContext);
        if (
            serviceId ===
            ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            RemoveUmrahOptionsFromBeneficiaryType(formContext);
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                ); // Lock the field
            }
        } else if (
            serviceId ===
            ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase()
        ) {
            //OnChange_SeasonWithoutEmpty(formContext);
            CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            RemoveHajjOptionsFromBeneficiaryType(formContext);
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                );
            }
        } else if (
            serviceId ===
            ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase()
        ) {
            //OnChange_SeasonWithoutEmpty(formContext);
            CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            RemoveVisitorOptionFromBeneficiaryType(formContext);
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                );
            }
        } else if (
            serviceId ===
            ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase() ||
            serviceId === ServiceType.Suggestions.serviceDefinitionId.toLowerCase() ||
            serviceId ===
            ServiceType.Testimonials.serviceDefinitionId.toLowerCase() ||
            serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                );
            }
        }
    }
}

//// remove  transfer option from inquiry social media decision  if the request have a categoty
function RemoveTransferOptionFromInquirySocialMediaDecision(formContext) {
    debugger;
    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
    var mainCategoryValue = formContext
        .getAttribute(caseFields.ldv_maincategoryid)
        .getValue();
    var removedValues = [caseFields.Enums.socialmediadecision2options.Transfer];
    var AllValues = [
        caseFields.Enums.socialmediadecision2options.Resolved,
        caseFields.Enums.socialmediadecision2options.Transfer,
    ];

    if (!service) {
        console.error("Service or current stage is null or undefined.");
        return;
    }
    if (!mainCategoryValue) {
        RemoveOptionSetValues(
            formContext,
            caseFields.ldv_inquirysocialmediadecisioncode,
            AllValues
        );

        AddOptionSetValues(
            formContext,
            caseFields.ldv_inquirysocialmediadecisioncode,
            AllValues
        );
    }

    var serviceId = service[0].id.replace("{", "").replace("}", "").toLowerCase();
    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    }
    if (
        serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase() &&
        mainCategoryValue &&
        mainCategoryValue.length >= 1
    ) {
        RemoveOptionSetValues(
            formContext,
            caseFields.ldv_inquirysocialmediadecisioncode,
            removedValues
        );
    }
}

/////////////////////

function RemoveTransferOptionFromCompanyDecision(formContext) {
    debugger;
    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
    if (!service) {
        console.error("Service or current stage is null or undefined.");
        return;
    }
    var serviceId = service[0].id.replace("{", "").replace("}", "").toLowerCase();
    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    }
    var removedValues = [caseFields.Enums.companyDecision.NeedMoreInformation];
    if (
        serviceId ===
        ServiceType.MissingPersonsServiceNUSUKEnaya.serviceDefinitionId.toLowerCase() ||
        serviceId ===
        ServiceType.MentoringServiceNUSUKEnaya.serviceDefinitionId.toLowerCase()
    ) {
        RemoveOptionSetValues(
            formContext,
            caseFields.ldv_companiesservicedecisioncode,
            removedValues
        );
    }
}

// hide parent case field in case it was empty
function HideEmptyParentCaseField(formContext) {
    debugger;
    var parentCaseField = formContext.getAttribute(caseFields.parentcaseid);

    if (parentCaseField) {
        var parentCaseFieldValue = parentCaseField.getValue();
        if (
            parentCaseFieldValue !== null &&
            parentCaseFieldValue !== undefined &&
            parentCaseFieldValue !== ""
        ) {
            formContext.getControl(caseFields.parentcaseid).setVisible(true);
        } else {
            formContext.getControl(caseFields.parentcaseid).setVisible(false);
        }
    } else {
        console.error("Attribute 'parentcaseid' not found.");
    }
}
/////

function RemoveUmrahOptionsFromBeneficiaryType(formContext) {
    debugger;
    var beneficiaryTypeField = formContext.getAttribute(
        caseFields.ldv_beneficiarytypecode
    );
    if (!beneficiaryTypeField) {
        return;
    }

    var removedValues = [
        caseFields.Enums.beneficiaryType.InternalUmrah,
        caseFields.Enums.beneficiaryType.ExternalUmrah,
        caseFields.Enums.beneficiaryType.Visitor,
    ];
    RemoveOptionSetValues(
        formContext,
        caseFields.ldv_beneficiarytypecode,
        removedValues
    );
}
function RemoveHajjOptionsFromBeneficiaryType(formContext) {
    var beneficiaryTypeField = formContext.getAttribute(
        caseFields.ldv_beneficiarytypecode
    );
    if (!beneficiaryTypeField) {
        return;
    }

    var removedValues = [
        caseFields.Enums.beneficiaryType.InternalHajj,
        caseFields.Enums.beneficiaryType.ExternalHajj,
        caseFields.Enums.beneficiaryType.Visitor,
    ];
    RemoveOptionSetValues(
        formContext,
        caseFields.ldv_beneficiarytypecode,
        removedValues
    );
}
function RemoveVisitorOptionFromBeneficiaryType(formContext) {
    var beneficiaryTypeField = formContext.getAttribute(
        caseFields.ldv_beneficiarytypecode
    );
    if (!beneficiaryTypeField) {
        return;
    }

    var removedValues = [caseFields.Enums.beneficiaryType.Visitor];
    RemoveOptionSetValues(
        formContext,
        caseFields.ldv_beneficiarytypecode,
        removedValues
    );
}
function RemoveAllOptionsFromBeneficiaryType(formContext) {
    debugger;
    var beneficiaryTypeField = formContext.getAttribute(
        caseFields.ldv_beneficiarytypecode
    );
    if (!beneficiaryTypeField) {
        return;
    }

    var removedValues = [
        caseFields.Enums.beneficiaryType.InternalHajj,
        caseFields.Enums.beneficiaryType.ExternalHajj,
        caseFields.Enums.beneficiaryType.Visitor,
        caseFields.Enums.beneficiaryType.ExternalUmrah,
        caseFields.Enums.beneficiaryType.InternalUmrah,
    ];
    RemoveOptionSetValues(
        formContext,
        caseFields.ldv_beneficiarytypecode,
        removedValues
    );
}
function AddAllOptionsFromBeneficiaryType(formContext) {
    debugger;
    var beneficiaryTypeField = formContext.getAttribute(
        caseFields.ldv_beneficiarytypecode
    );
    if (!beneficiaryTypeField) {
        return;
    }

    var removedValues = [
        caseFields.Enums.beneficiaryType.InternalUmrah,
        caseFields.Enums.beneficiaryType.ExternalUmrah,
        caseFields.Enums.beneficiaryType.InternalHajj,
        caseFields.Enums.beneficiaryType.ExternalHajj,
        caseFields.Enums.beneficiaryType.Visitor,
    ];

    AddOptionSetValues(
        formContext,
        caseFields.ldv_beneficiarytypecode,
        removedValues
    );
}

function ShowAndHideSeason(formContext) {
    var fields = [caseFields.ldv_seasoncode];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
    ];
    ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow);
    // Check if ldv_seasoncode contains data
    var seasonValue = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_seasoncode
    );
    if (seasonValue !== null && seasonValue !== undefined) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_seasoncode, true); // Lock the field
    }
}
function ShowAndHideSeason_OnLoad(formContext) {
    var fields = [caseFields.ldv_seasoncode];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
    ];
    ShowAndHideFieldsBasedOnServiceWithoutEmpty(
        formContext,
        fields,
        serviceIdsToShow
    );
    // Check if ldv_seasoncode contains data
    var seasonValue = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_seasoncode
    );
    if (seasonValue !== null && seasonValue !== undefined) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_seasoncode, true); // Lock the field
    }
}

function ShowAndHideSurveyServicesTab(formContext) {
    debugger;
    // hide survey services tab if survey satisfaction == 'Not Satisfied'
    let surveySatisfaction = formContext
        .getAttribute(caseFields.ldv_surveysatisfactioncode)
        .getValue();

    if (surveySatisfaction === caseFields.Enums.surveySatisfaction.notSatisfied) {
        formContext.ui.tabs.get(Tabs.tab_surveyServices).setVisible(true); //Shows Tab
    } else {
        formContext.ui.tabs.get(Tabs.tab_surveyServices).setVisible(false); //Hide Tab
    }
}
function ShowAndHideLocation(formContext) {
    var fields = [caseFields.ldv_locationcode];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase(),
    ];
    ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow);
    removeJaddaFromLocation(formContext);
    removeMadinaFromLocation(formContext);
}
function ShowAndHideLocation_OnLoad(formContext) {
    var fields = [caseFields.ldv_locationcode];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase(),
    ];
    ShowAndHideFieldsBasedOnServiceWithoutEmpty(
        formContext,
        fields,
        serviceIdsToShow
    );
    removeJaddaFromLocation(formContext);
    removeMadinaFromLocation(formContext);
}
function removeJaddaFromLocation(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (!service) {
        return;
    }

    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
    var locationField = formContext.getAttribute(caseFields.ldv_locationcode);

    if (!locationField) {
        return;
    }

    if (
        serviceId ===
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase()
    ) {
        var removedValues = [caseFields.Enums.location.Jada];
        RemoveOptionSetValues(
            formContext,
            caseFields.ldv_locationcode,
            removedValues
        );
        jaddaAdded = false; // Reset the flag when Jadda is removed
    } else {
        if (jaddaAdded) {
            var removedValues = [caseFields.Enums.location.Jada];
            RemoveOptionSetValues(
                formContext,
                caseFields.ldv_locationcode,
                removedValues
            );
        }

        var jaddaOption = locationField
            .getOptions()
            .find((option) => option.value === caseFields.Enums.location.Jada);
        if (jaddaOption) {
            formContext
                .getControl(caseFields.ldv_locationcode)
                .addOption(jaddaOption);
            jaddaAdded = true; // Set the flag when Jadda is added
        }
    }
}

//show and  hide SurveyResponseTab
function ShowAndHideSurveyResponseTab(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );

    if (!service) {
        formContext.ui.tabs.get(Tabs.tab_SurveyResponse).setVisible(false); // show Tab
        return;
    }

    var notAllowedServiceIds = [
        ServiceType.Suggestions.serviceDefinitionId.toLowerCase(),
        ServiceType.Testimonials.serviceDefinitionId.toLowerCase(),
        ServiceType.MissingPersonsServiceNUSUKEnaya.serviceDefinitionId.toLowerCase(),
    ];

    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();

    if (notAllowedServiceIds.includes(serviceId)) {
        formContext.ui.tabs.get(Tabs.tab_SurveyResponse).setVisible(false); // show Tab
    } else {
        formContext.ui.tabs.get(Tabs.tab_SurveyResponse).setVisible(true); //  hide Tab
    }
}

function removeMadinaFromLocation(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (!service) {
        return;
    }

    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
    var locationField = formContext.getAttribute(caseFields.ldv_locationcode);

    if (!locationField) {
        return;
    }

    if (
        serviceId ===
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase()
    ) {
        var beneficiaryType = CommonGeneric.GetFieldValue(
            formContext,
            caseFields.ldv_beneficiarytypecode
        );
        if (beneficiaryType === caseFields.Enums.beneficiaryType.InternalHajj) {
            var removedValues = [caseFields.Enums.location.Madina];
            RemoveOptionSetValues(
                formContext,
                caseFields.ldv_locationcode,
                removedValues
            );
            madinaAdded = false; // Reset the flag when Madina is removed
        } else {
            if (madinaAdded) {
                var removedValues = [caseFields.Enums.location.Madina];
                RemoveOptionSetValues(
                    formContext,
                    caseFields.ldv_locationcode,
                    removedValues
                );
            }

            var madinaOption = locationField
                .getOptions()
                .find((option) => option.value === caseFields.Enums.location.Madina);
            if (madinaOption) {
                formContext
                    .getControl(caseFields.ldv_locationcode)
                    .addOption(madinaOption);
                madinaAdded = true; // Set the flag when Madina is added
            }
        }
    } else {
        if (madinaAdded) {
            var removedValues = [caseFields.Enums.location.Madina];
            RemoveOptionSetValues(
                formContext,
                caseFields.ldv_locationcode,
                removedValues
            );
        }

        var madinaOption = locationField
            .getOptions()
            .find((option) => option.value === caseFields.Enums.location.Madina);
        if (madinaOption) {
            formContext
                .getControl(caseFields.ldv_locationcode)
                .addOption(madinaOption);
            madinaAdded = true; // Set the flag when Madina is added
        }
    }
}

function ShowAndHideComplainType(formContext) {
    var fields = [caseFields.ldv_complaintypecode];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase(),
    ];
    ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow);
    // Check if ldv_complaintypecode contains data
    var complainTypeValue = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_complaintypecode
    );
    if (complainTypeValue !== null && complainTypeValue !== undefined) {
        CommonGeneric.DisableField(
            formContext,
            caseFields.ldv_complaintypecode,
            true
        ); // Lock the field
    }
}

function ShowAndHideCompany(formContext) {
    // var fields = [caseFields.ldv_company];
    var fields = [];
    var serviceIdsToShow = [
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase(),
        ServiceType.MentoringServiceNUSUKEnaya.serviceDefinitionId.toLowerCase(),
        ServiceType.MissingPersonsServiceNUSUKEnaya.serviceDefinitionId.toLowerCase(),
    ];

    ShowAndHideFieldsBasedOnService(formContext, fields, serviceIdsToShow);
}
//function ShowAndHideCompany(formContext) {
//    debugger;
//    var location = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_locationcode);
//    var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);

//    if (!service) {
//        return;
//    }

//    var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();

//    if (serviceId === ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase() /*&& location*/) {
//        // Check if location is valid for TechnicalComplainMomentaryHajj
//        var validLocations = [
//            caseFields.Enums.location.Mekkah,
//            caseFields.Enums.location.Mina,
//            caseFields.Enums.location.Arrafat,
//            caseFields.Enums.location.Muzdalifa
//        ];

//        var beneficiaryType = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_beneficiarytypecode);

//        if (beneficiaryType !== null && beneficiaryType !== undefined) {
//            if (beneficiaryType === caseFields.Enums.beneficiaryType.InternalHajj) {
//                var shouldShowCompanyField = validLocations.includes(location);
//                CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_companytext, shouldShowCompanyField, shouldShowCompanyField);

//            }
//            else {
//                var shouldShowCompanyField = validLocations.includes(location);
//                CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, shouldShowCompanyField, shouldShowCompanyField);
//            }
//        }

//    } else if (serviceId === ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase() || serviceId === ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase()) {

//        // Show the company field without checking the location
//        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, true, true);
//        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_companytext, false, false);

//    }
//    else {
//        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, false, false);
//        CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_companytext, false, false);

//    }
//};

function HideAndShowComplainPriority(formContext) {
    var serviceRecord = GetLookUpRecordWithExpandedValues(
        formContext,
        caseFields.ldv_serviceid,
        "$select=ldv_name,ldv_iscomplain"
    );
    if (serviceRecord !== null) {
        var isComplain = serviceRecord["ldv_iscomplain"];

        if (isComplain === true) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_prioritycode,
                true,
                true
            );

            // Check if ldv_prioritycode contains data
            var priorityValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_prioritycode
            );
            if (priorityValue !== null && priorityValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_prioritycode,
                    true
                ); // Lock the field
            }
        } else {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_prioritycode,
                false,
                false
            );
        }
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            caseFields.ldv_prioritycode,
            false,
            false
        );
    }
}
function SettingComplainTypePrioritySeason(formContext, categoryField) {
    var categoryRecord = GetLookUpRecordWithExpandedValues(
        formContext,
        categoryField,
        "$select=ldv_name,ldv_complaintypecode,ldv_prioritycode,ldv_seasoncode"
    );
    if (categoryRecord !== null) {
        // Check the visibility of each field
        var complaintTypeVisible = IsFieldVisible(
            formContext,
            caseFields.ldv_complaintypecode
        );
        var priorityVisible = IsFieldVisible(
            formContext,
            caseFields.ldv_prioritycode
        );
        var seasonVisible = IsFieldVisible(formContext, caseFields.ldv_seasoncode);

        // Use the retrieved values if the fields are visible
        if (complaintTypeVisible && categoryRecord.ldv_complaintypecode !== null) {
            formContext
                .getAttribute(caseFields.ldv_complaintypecode)
                .setValue(categoryRecord.ldv_complaintypecode);
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_complaintypecode,
                true
            );
        }
        if (priorityVisible && categoryRecord.ldv_prioritycode !== null) {
            formContext
                .getAttribute(caseFields.ldv_prioritycode)
                .setValue(categoryRecord.ldv_prioritycode);
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_prioritycode,
                true
            );
        }
        if (seasonVisible && categoryRecord.ldv_seasoncode !== null) {
            formContext
                .getAttribute(caseFields.ldv_seasoncode)
                .setValue(categoryRecord.ldv_seasoncode);
            formContext.getAttribute(caseFields.ldv_seasoncode).fireOnChange();

            var currentStage = formContext.data.process.getActiveStage();
            if (currentStage) {
                var currentStageName = currentStage.getName();
                if (
                    currentStageName !== "Social Media" &&
                    currentStageName != "التواصل الإجتماعي" &&
                    currentStageName !== "التواصل الاجتماعي"
                ) {
                    CommonGeneric.DisableField(
                        formContext,
                        caseFields.ldv_seasoncode,
                        true
                    );
                }
            } else {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_seasoncode,
                    true
                );
            }
        }
    }
}

function SettingComplainTypePrioritySeasonFromMainCategory(formContext) {
    var subCategoryVisible = IsFieldVisible(
        formContext,
        caseFields.ldv_subcategoryid
    );
    if (!subCategoryVisible) {
        SettingComplainTypePrioritySeason(
            formContext,
            caseFields.ldv_maincategoryid
        );
    }
}
function SettingComplainTypePrioritySeasonFromSubCategory(formContext) {
    var secondarySubCategoryVisible = IsFieldVisible(
        formContext,
        caseFields.ldv_secondarysubcategoryid
    );
    if (!secondarySubCategoryVisible) {
        SettingComplainTypePrioritySeason(
            formContext,
            caseFields.ldv_subcategoryid
        );
    }
}
function ShowRequiredFieldsForSahabReportCase(formContext) {

    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
    if (!service) {
        console.error("Service is null or undefined.");
        return;
    }
    var serviceId = service[0].id.replace("{", "").replace("}", "").toLowerCase();
    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    }
    var fieldsToShown = [caseFields.ldv_complaintypecode, caseFields.ldv_locationcode, caseFields.ldv_company, caseFields.ldv_beneficiarytypecode, caseFields.ldv_prioritycode, caseFields.ldv_maincategoryid, caseFields.ldv_subcategoryid, caseFields.ldv_description];
    if (serviceId === ServiceType.SahabReportService.serviceDefinitionId.toLowerCase()) {
        fieldsToShown.forEach(function (fieldSchemaName) {
            CommonGeneric.ShowAndReuiredField(formContext, fieldSchemaName,true,true);
        });
    }
}
function SettingComplainTypePrioritySeasonFromSecondarySubCategory(
    formContext
) {
    SettingComplainTypePrioritySeason(
        formContext,
        caseFields.ldv_secondarysubcategoryid
    );
}

function SetProcessFieldFromService(formContext) {
    //getting service record with process field
    var serviceRecord = GetLookUpRecordWithExpandedValues(
        formContext,
        caseFields.ldv_serviceid,
        "$select=ldv_name,ldv_iscomplain&$expand=ldv_processid($select=name)"
    );
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
                    CommonGeneric.SetLookupRecord(
                        formContext,
                        caseFields.ldv_processid,
                        processId,
                        "workflow",
                        processName
                    );
                    formContext.getAttribute(caseFields.ldv_processid).fireOnChange();
                }
            }
        } else {
            console.log(
                "ldv_processid field is not populated on the service record."
            );
        }
    }
}
//function OnChange_Location(formContext) {

//    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_company, false, false);
//    CommonGeneric.ShowAndReuiredField(formContext, caseFields.ldv_companytext, false, false);

//    //ShowAndHideCompany(formContext);
//};
function OnChange_Season(formContext) {
    var season = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_seasoncode
    );
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );

    if (!season || !service) {
        return;
    }
    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
    if (
        serviceId ===
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase() ||
        serviceId ===
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase()
    ) {
        if (season === caseFields.Enums.season.Hajj) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            // Check if ldv_beneficiarytypecode contains data
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                var currentStage = formContext.data.process.getActiveStage();
                if (currentStage) {
                    var currentStageName = currentStage.getName();
                    if (
                        currentStageName !== "Social Media" &&
                        currentStageName !== "التواصل الإجتماعي" &&
                        currentStageName !== "التواصل الإجتماعي"
                    ) {
                        CommonGeneric.DisableField(
                            formContext,
                            caseFields.ldv_beneficiarytypecode,
                            true
                        ); // Lock the field
                    }
                } else {
                    CommonGeneric.DisableField(
                        formContext,
                        caseFields.ldv_seasoncode,
                        true
                    );
                }
            }
        } else {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                false,
                false
            );
        }
    }
}
function OnChange_SeasonWithoutEmpty(formContext) {
    var season = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_seasoncode
    );
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );

    if (!season || !service) {
        return;
    }
    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
    if (
        serviceId ===
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase() ||
        serviceId ===
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase()
    ) {
        if (season === caseFields.Enums.season.Hajj) {
            CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                true,
                true
            );
            // Check if ldv_beneficiarytypecode contains data
            var beneficiaryTypeValue = CommonGeneric.GetFieldValue(
                formContext,
                caseFields.ldv_beneficiarytypecode
            );
            if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_beneficiarytypecode,
                    true
                ); // Lock the field
            }
        } else {
            CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                false,
                false
            );
        }
    }
}
function OnChange_RequestType(formContext) {
    debugger;
    handleServiceChange();
    var fieldsToBeHiddenOrShown = [
        caseFields.ldv_prioritycode,
        caseFields.ldv_beneficiarytypecode,
        caseFields.ldv_seasoncode,
        caseFields.ldv_locationcode,
        caseFields.ldv_complaintypecode,
        // caseFields.ldv_company,
        caseFields.ldv_errorcodeid,
    ];
    fieldsToBeHiddenOrShown.forEach(function (fieldSchemaName) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            fieldSchemaName,
            false,
            false
        );
    });

    var fieldsToEmpty = [
        caseFields.ldv_maincategoryid,
        caseFields.ldv_subcategoryid,
        caseFields.ldv_secondarysubcategoryid,
        caseFields.ldv_errorcodeid,
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

    ShowAndHideSurveyResponseTab(formContext);
    ShowAndHide_Sahab_Tasheer_SD_Kidana_Section(formContext);
    ShowAndHideSLATimerSection(formContext);
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_maincategoryid,
        caseFields.ldv_serviceid
    );
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_subcategoryid,
        caseFields.ldv_serviceid
    );
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_secondarysubcategoryid,
        caseFields.ldv_serviceid
    );
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_errorcodeid,
        caseFields.ldv_serviceid
    );
    CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
        formContext,
        caseFields.ldv_maincategoryid,
        caseFields.ldv_serviceid
    );

    SetReqForMainCategoryForInquiry(formContext);
    showAndHideSubcategoryField(formContext);
    showAndHideSecondarySubcategoryField(formContext);
    UnLockCompanyFiled(formContext);
}
function OnChange_ProcessId(formContext) {
    if (formContext.ui.getFormType() !== 1) {
        SwitchBPF(formContext, caseFields.ldv_processid, () => { });
    }
}
function OnChange_MainCategory(formContext) {
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_subcategoryid,
        caseFields.ldv_maincategoryid
    );
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_secondarysubcategoryid,
        caseFields.ldv_maincategoryid
    );
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_errorcodeid,
        caseFields.ldv_maincategoryid
    );
    showAndHideSubcategoryField(formContext, function () {
        SettingComplainTypePrioritySeasonFromMainCategory(formContext);
    });
    showAndHideSecondarySubcategoryField(formContext, function () {
        HandleErrorFieldVisibility_OnLoad(formContext);
    });
    CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
        formContext,
        caseFields.ldv_errorcodeid,
        false,
        false
    );
    RemoveTransferOptionFromInquirySocialMediaDecision(formContext);
}
function OnChange_SubCategory(formContext) {
    CommonGeneric.Empty_field1_BasedOn_field2(
        formContext,
        caseFields.ldv_secondarysubcategoryid,
        caseFields.ldv_subcategoryid
    );
    showAndHideSecondarySubcategoryField(formContext, function () {
        SettingComplainTypePrioritySeasonFromSubCategory(formContext);
    });
}

function HandleErrorFieldVisibility(formContext, subcategoryField) {
    var subCategory = GetLookUpRecordWithExpandedValues(
        formContext,
        subcategoryField,
        "$select=ldv_name,ldv_ishaserrorcode"
    );

    if (subCategory !== null && subCategory !== undefined) {
        if (subCategory.ldv_ishaserrorcode === true) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_errorcodeid,
                true,
                true
            );
        } else {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                caseFields.ldv_errorcodeid,
                false,
                false
            );
        }
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            caseFields.ldv_errorcodeid,
            false,
            false
        );
    }
}
function HandleErrorFieldVisibility_OnLoad(formContext) {
    var secondarySubCategoryVisible = IsFieldVisible(
        formContext,
        caseFields.ldv_secondarysubcategoryid
    );
    if (secondarySubCategoryVisible) {
        HandleErrorFieldVisibility(
            formContext,
            caseFields.ldv_secondarysubcategoryid
        );
    } else {
        HandleErrorFieldVisibility(formContext, caseFields.ldv_subcategoryid);
    }
}

function showAndHideSubcategoryField(formContext, callback) {
    var mainCategoryId = formContext
        .getAttribute(caseFields.ldv_maincategoryid)
        .getValue();

    if (mainCategoryId) {
        var mainCategoryIdWithoutBrackets = mainCategoryId[0].id
            .replace("{", "")
            .replace("}", "");

        // Construct the query to check for related "Secondary Subcategory" records
        var fetchXml =
            "<fetch count='1'>" +
            "<entity name='ldv_casecategory'>" +
            "<filter>" +
            "<condition attribute='ldv_parentcategoryid' operator='eq' value='" +
            mainCategoryIdWithoutBrackets +
            "' />" +
            "</filter>" +
            "</entity>" +
            "</fetch>";

        // Use Xrm.WebApi to retrieve records
        Xrm.WebApi.retrieveMultipleRecords(
            "ldv_casecategory",
            "?fetchXml=" + encodeURIComponent(fetchXml)
        ).then(
            function (results) {
                if (results.entities.length > 0) {
                    // If related records exist, show and make the "Secondary Subcategory" field mandatory
                    CommonGeneric.ShowAndReuiredField(
                        formContext,
                        caseFields.ldv_subcategoryid,
                        true,
                        true
                    );
                } else {
                    CommonGeneric.ShowAndReuiredField(
                        formContext,
                        caseFields.ldv_subcategoryid,
                        false,
                        false
                    );
                }
                if (callback && typeof callback === "function") {
                    callback(); // Call the callback function if provided
                }
            },
            function (error) {
                console.log("Error retrieving related records: " + error.message);
            }
        );
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            caseFields.ldv_subcategoryid,
            false,
            false
        );
        if (callback && typeof callback === "function") {
            callback(); // Call the callback function if provided
        }
    }
}
function showAndHideSecondarySubcategoryField(formContext, callback) {
    var subCategoryId = formContext
        .getAttribute(caseFields.ldv_subcategoryid)
        .getValue();

    if (subCategoryId) {
        var subCategoryIdWithoutBrackets = subCategoryId[0].id
            .replace("{", "")
            .replace("}", "");

        // Construct the query to check for related "Secondary Subcategory" records
        var fetchXml =
            "<fetch count='1'>" +
            "<entity name='ldv_casecategory'>" +
            "<filter>" +
            "<condition attribute='ldv_subcategoryid' operator='eq' value='" +
            subCategoryIdWithoutBrackets +
            "' />" +
            "</filter>" +
            "</entity>" +
            "</fetch>";

        // Use Xrm.WebApi to retrieve records
        Xrm.WebApi.retrieveMultipleRecords(
            "ldv_casecategory",
            "?fetchXml=" + encodeURIComponent(fetchXml)
        ).then(
            function (results) {
                if (results.entities.length > 0) {
                    // If related records exist, show and make the "Secondary Subcategory" field mandatory
                    CommonGeneric.ShowAndReuiredField(
                        formContext,
                        caseFields.ldv_secondarysubcategoryid,
                        true,
                        true
                    );
                } else {
                    CommonGeneric.ShowAndReuiredField(
                        formContext,
                        caseFields.ldv_secondarysubcategoryid,
                        false,
                        false
                    );
                }
                if (callback && typeof callback === "function") {
                    callback(); // Call the callback function if provided
                }
            },
            function (error) {
                console.log("Error retrieving related records: " + error.message);
            }
        );
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            caseFields.ldv_secondarysubcategoryid,
            false,
            false
        );
        if (callback && typeof callback === "function") {
            callback(); // Call the callback function if provided
        }
    }
}

function showAndHideSubSourceField(formContext) {
    var caseOrigin = formContext
        .getAttribute(caseFields.caseorigincode)
        .getValue();

    if (caseOrigin) {
        // Construct the query to check for related "Secondary Subcategory" records
        var fetchXml =
            "<fetch count='1'>" +
            "<entity name='ldv_subsource'>" +
            "<filter>" +
            "<condition attribute='ldv_origincode' operator='eq' value='" +
            caseOrigin +
            "' />" +
            "</filter>" +
            "</entity>" +
            "</fetch>";

        // Use Xrm.WebApi to retrieve records
        Xrm.WebApi.retrieveMultipleRecords(
            "ldv_subsource",
            "?fetchXml=" + encodeURIComponent(fetchXml)
        ).then(
            function (results) {
                if (results.entities.length > 0) {
                    // If related records exist, show and make the "Subsource" field mandatory
                    CommonGeneric.ShowField(
                        formContext,
                        caseFields.ldv_subsourceid,
                        true
                    );
                } else {
                    CommonGeneric.ShowField(
                        formContext,
                        caseFields.ldv_subsourceid,
                        false
                    );
                }
            },
            function (error) {
                console.log("Error retrieving related records: " + error.message);
            }
        );
    } else {
        CommonGeneric.ShowField(formContext, caseFields.ldv_subsourceid, false);
    }
}

function filterSubSourceBasedOnOrigin(formContext) {
    // Get the selected origin value from the "Origin" option set field
    var selectedOrigin = formContext
        .getAttribute(caseFields.caseorigincode)
        .getValue();

    var customerSubSourceFilter =
        "<filter><condition attribute='ldv_origincode' operator='eq' value='" +
        selectedOrigin +
        "' /></filter>";

    formContext
        .getControl(caseFields.ldv_subsourceid)
        .addCustomFilter(customerSubSourceFilter, "ldv_subsource");
}

// function filterCompanyBasedOnServiceType(formContext) {
//     debugger;
//     var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
//     if (service !== null && service !== undefined) {
//         var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
//         if (
//             serviceId === ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase() ||
//             serviceId === ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase() ||
//             serviceId === ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase()
//         ) {
//             var umrahCompanyFilter = `
//           <filter type="and">
//             <condition attribute="ldv_servicetypecode" operator="eq" value="2" />
//           </filter>
//           `;
//             formContext.getControl(caseFields.ldv_company).addCustomFilter(umrahCompanyFilter, 'account');
//         } else if (
//             serviceId === ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase() ||
//             serviceId === ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase()
//         ) {
//             var beneficiaryTypeValue = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_beneficiarytypecode);
//             if (beneficiaryTypeValue !== null && beneficiaryTypeValue !== undefined) {
//                 formContext.getControl(caseFields.ldv_company).setDisabled(false);
//                 if (beneficiaryTypeValue === caseFields.Enums.beneficiaryType.InternalHajj) {
//                     var internalHajjCompanyFilter = `
//                   <filter type="and">
//                     <condition attribute="ldv_beneficiarytypecode" operator="eq" value="1" />
//                   </filter>`;

//                     // "<filter><condition attribute='ldv_beneficiarytypecode' operator='eq' value='" + beneficiaryTypeValue + "'/></filter>";

//                     formContext
//                         .getControl(caseFields.ldv_company)
//                         .addCustomFilter(internalHajjCompanyFilter, 'account');
//                 } else if (beneficiaryTypeValue === caseFields.Enums.beneficiaryType.ExternalHajj) {
//                     var externalHajjCompanyFilter = `
//                 <filter type="and">
//                   <condition attribute="ldv_beneficiarytypecode" operator="eq" value="2" />
//                 </filter>`;
//                     // var externalHajjCompanyFilter = "<filter><condition attribute='ldv_beneficiarytypecode' operator='eq' value=' " + beneficiaryTypeValue + " '/></filter>";

//                     formContext
//                         .getControl(caseFields.ldv_company)
//                         .addCustomFilter(externalHajjCompanyFilter, 'account');
//                 }
//             } else {
//                 formContext.getControl(caseFields.ldv_company).setDisabled(true);
//             }
//         }
//     }
// }

function EmptyCompanyFieldOnChangeBeneficiaryType(formContext) {
    // var service = CommonGeneric.GetLookUpRecord(formContext, caseFields.ldv_serviceid);
    // if (service !== null && service !== undefined) {
    //     var serviceId = service.id.replace('{', '').replace('}', '').toLowerCase();
    //     if (
    //         serviceId === ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase() ||
    //         serviceId === ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase()
    //     ) {
    //         CommonGeneric.EmptyField(formContext, caseFields.ldv_company);
    //     }
    // }
}

function UnLockCompanyFiled(formContext) {
    // var companyControl = formContext.getControl(caseFields.ldv_company);
    // if (companyControl) {
    //     companyControl.setDisabled(false);
    // }
}

function SetReqForMainCategoryForInquiry(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service !== null && service !== undefined) {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
        if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
            CommonGeneric.SetReqLevel(
                formContext,
                caseFields.ldv_maincategoryid,
                false
            );
        } else {
            CommonGeneric.SetReqLevel(
                formContext,
                caseFields.ldv_maincategoryid,
                true
            );
        }
    }
}

function HideFieldsOnSubmitStageInquiry(formContext) {
    var subCategoryOnBpf = "header_process_" + caseFields.ldv_subcategoryid;
    var mainCategoryOnBpf = "header_process_" + caseFields.ldv_maincategoryid;
    var mainCategoryOnSocialStage =
        "header_process_" + caseFields.ldv_maincategoryid + "_1";

    CommonGeneric.ShowAndReuiredField(
        formContext,
        subCategoryOnBpf,
        false,
        false
    );
    CommonGeneric.ShowAndReuiredField(
        formContext,
        mainCategoryOnBpf,
        false,
        false
    );
    CommonGeneric.ShowAndReuiredField(
        formContext,
        mainCategoryOnSocialStage,
        false,
        false
    );
    HideOriginOnSubmitStage(formContext);
}
function HideFieldsOnSubmitStageSuggestion(formContext) {
    var origin = "header_process_" + caseFields.caseorigincode;
    CommonGeneric.ShowAndReuiredField(formContext, origin, false, false);
}

function HideOriginOnSubmitStage(formContext) {
    var origin = "header_process_" + caseFields.caseorigincode;
    CommonGeneric.ShowAndReuiredField(formContext, origin, false, false);
}
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
    var beneficiaryTypeOnBpf =
        "header_process_" + caseFields.ldv_beneficiarytypecode;
    CommonGeneric.ShowAndReuiredField(
        formContext,
        locationFieldOnBpf,
        false,
        false
    );
    CommonGeneric.ShowAndReuiredField(
        formContext,
        beneficiaryTypeOnBpf,
        false,
        false
    );
}
function HideFieldsOnSubmitTechnicalComplainNonMomentaryHajjAndUmrah(
    formContext
) {
    var seasonOnBpf = "header_process_" + caseFields.ldv_seasoncode;
    CommonGeneric.ShowAndReuiredField(formContext, seasonOnBpf, false, false);
}

function LockFormFieldsAfterSubmit(formContext) {
    var submitValue = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_issubmitted
    );

    if (submitValue !== null && submitValue !== undefined) {
        if (formContext.ui.getFormType() !== 1 && submitValue === true) {
            CommonGeneric.LockFormControlsExecptBPF(formContext);
        }
    }
}

function UnlockCategoryFieldsBeforeSubmit(formContext) {
    var submitValue = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_issubmitted
    );
    if (submitValue !== null && submitValue !== undefined) {
        if (formContext.ui.getFormType() !== 1 && submitValue !== true) {
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_maincategoryid,
                caseFields.ldv_serviceid
            );
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_subcategoryid,
                caseFields.ldv_maincategoryid
            );
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_secondarysubcategoryid,
                caseFields.ldv_subcategoryid
            );
        }
    }
}

//
function HideFieldsOnCompanyStageNusukServices(formContext) {
    var serviceField = "header_process_" + caseFields.ldv_serviceid;
    CommonGeneric.ShowAndReuiredField(formContext, serviceField, true, false);
}

function HandleBPF(executionContext, callback) {
    var formContext = executionContext.getFormContext();

    // In case we want to hide the BPF in the create form
    if (formContext.ui.getFormType() === 1) {
        CommonGeneric.hideBusinessProcessFlow(formContext);
    } else {
        // Switch the BPF
        SwitchBPF(formContext, caseFields.ldv_processid, () => {
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
            CommonGeneric.SectionVisibility(
                formContext,
                Tabs.tab_requestinformation,
                Sections.Decisions,
                false
            );
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
function ShowBPFNextButtonForSubmitStage(executionContext) {
    var formContext = executionContext.getFormContext();
    var currentStage = formContext.data.process.getActiveStage();
    if (currentStage) {
        var currentStageName = currentStage.getName();
        if (currentStageName === "Submit" || currentStageName === "تقديم") {
            ShowNextStage(executionContext);
        }
    }
}
function OnChange_addOnStageChange(executionContext) {
    var formContext = executionContext.getFormContext();

    HideUCIButtons(executionContext);

    ShowBPFNextButtonForSubmitStage(executionContext);
    GetActiveStageDecisionsBasedOnService(executionContext);
    if (formContext.ui.getFormType() !== 1) {
        CommonGeneric.DisableField(formContext, caseFields.ldv_serviceid, true); // Lock the Request Type Field
    }
    UnlockFieldsInSocialMediaStage(formContext);
}
function ShowAndHideFieldsBasedOnService(
    formContext,
    fields,
    serviceIdsToShow
) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service === null || service === undefined) {
        fields.forEach(function (fieldName) {
            CommonGeneric.ShowAndReuiredField(formContext, fieldName, false, false);
        });
    } else {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
        var shouldShowFields = serviceIdsToShow.includes(serviceId);
        fields.forEach(function (fieldName) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                fieldName,
                shouldShowFields,
                shouldShowFields
            );
        });
    }
}
function ShowAndHideFieldsBasedOnServiceWithoutEmpty(
    formContext,
    fields,
    serviceIdsToShow
) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service === null || service === undefined) {
        fields.forEach(function (fieldName) {
            CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
                formContext,
                fieldName,
                false,
                false
            );
        });
    } else {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
        var shouldShowFields = serviceIdsToShow.includes(serviceId);
        fields.forEach(function (fieldName) {
            CommonGeneric.ShowAndReuiredFieldWithoutEmpty(
                formContext,
                fieldName,
                shouldShowFields,
                shouldShowFields
            );
        });
    }
}

function ChangeTicketRequestLabel(formContext) {
    var serviceRecord = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (serviceRecord && serviceRecord.id) {
        var serviceId = serviceRecord.id
            .replace("{", "")
            .replace("}", "")
            .toLowerCase();
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
            } else if (
                serviceId === ServiceType.Suggestions.serviceDefinitionId.toLowerCase()
            ) {
                ChangeFieldLabel(formContext, caseFields.title, suggestionArabicLabel);
            } else {
                ChangeFieldLabel(formContext, caseFields.title, complainArabicLabel);
            }
        } else {
            if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
                ChangeFieldLabel(formContext, caseFields.title, inquiryLabel);
            } else if (
                serviceId === ServiceType.Suggestions.serviceDefinitionId.toLowerCase()
            ) {
                ChangeFieldLabel(formContext, caseFields.title, suggestionLabel);
            } else {
                ChangeFieldLabel(formContext, caseFields.title, complainLabel);
            }
        }
    }
}

function RemoveCaseOriginOptionsInCreate(formContext) {
    debugger;
    var removedValues = [
        caseFields.Enums.CaseOrigin.Nusuk,
        caseFields.Enums.CaseOrigin.ExternalGate,
        caseFields.Enums.CaseOrigin.Email,
    ];

    var ifExistRemoveValues = [
        caseFields.Enums.CaseOrigin.IoT,
        caseFields.Enums.CaseOrigin.Facebook,
        caseFields.Enums.CaseOrigin.Twitter,
    ];

    if (formContext.ui.getFormType() === 1) {
        var caseOriginField = formContext.getAttribute(caseFields.caseorigincode);

        if (caseOriginField !== null && caseOriginField !== undefined) {
            RemoveOptionSetValues(
                formContext,
                caseFields.caseorigincode,
                removedValues
            );

            // Remove options if they exist
            var options = caseOriginField.getOptions();
            for (var i = 0; i < ifExistRemoveValues.length; i++) {
                var valueToRemove = ifExistRemoveValues[i];
                var index = options.findIndex(function (option) {
                    return option.value === valueToRemove;
                });
                if (index !== -1) {
                    RemoveOptionSetValues(formContext, caseFields.caseorigincode, [
                        valueToRemove,
                    ]);
                }
            }
        }
    }
}

function ShowAndHideIntegrationSection(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (!service) {
        CommonGeneric.SectionVisibility(
            formContext,
            Tabs.tab_administiration,
            Sections.IntegrationSection,
            false
        );
        return;
    }

    var allowedServiceIds = [
        ServiceType.BusinessSectorComplainKadana.serviceDefinitionId.toLowerCase(),
        ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase(),
        ServiceType.PilgrimsComplaintsDirectHajj.serviceDefinitionId.toLowerCase(),
    ];

    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();

    if (allowedServiceIds.includes(serviceId)) {
        CommonGeneric.SectionVisibility(
            formContext,
            Tabs.tab_administiration,
            Sections.IntegrationSection,
            true
        );
    } else {
        CommonGeneric.SectionVisibility(
            formContext,
            Tabs.tab_administiration,
            Sections.IntegrationSection,
            false
        );
    }
}
function ShowAndHideSLATimerSection(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (!service) {
        CommonGeneric.SectionVisibility(
            formContext,
            Tabs.tab_requestinformation,
            Sections.SLATimer,
            false
        );
        return;
    }

    var notAllowedServiceIds = [
        ServiceType.Inquiry.serviceDefinitionId.toLowerCase(),
        ServiceType.Suggestions.serviceDefinitionId.toLowerCase(),
        ServiceType.Testimonials.serviceDefinitionId.toLowerCase(),
    ];

    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();

    if (notAllowedServiceIds.includes(serviceId)) {
        CommonGeneric.SectionVisibility(
            formContext,
            Tabs.tab_requestinformation,
            Sections.SLATimer,
            false
        );
    } else {
        CommonGeneric.SectionVisibility(
            formContext,
            Tabs.tab_requestinformation,
            Sections.SLATimer,
            true
        );
    }
}
function ShowAndHide_Sahab_Tasheer_SD_Kidana_Section(formContext) {
    debugger;
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );

    if (!service) return;

    var allowedServiceIds = [
        ServiceType.BusinessSectorComplainKadana.serviceDefinitionId.toLowerCase(),
        ServiceType.PilgrimsComplaintsDirectHajj.serviceDefinitionId.toLowerCase(),
        ServiceType.PilgrimsComplaintsDirectHajj.serviceDefinitionId.toLowerCase(),
    ];

    var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();

    if (allowedServiceIds.includes(serviceId)) {
        var TasheerValue = formContext
            .getAttribute(caseFields.ldv_issenttotasher)
            .getValue();
        var KidanaValue = formContext
            .getAttribute(caseFields.ldv_senttokidana)
            .getValue();
        var SDValue = formContext
            .getAttribute(caseFields.ldv_senttoservicedesk)
            .getValue();
        var SahabValue = formContext
            .getAttribute(caseFields.ldv_issenttosehab)
            .getValue();

        if (TasheerValue == true)
            CommonGeneric.SectionVisibility(
                formContext,
                Tabs.tab_administiration,
                Sections.Tasheer,
                true
            );
        else if (KidanaValue == true) {
            CommonGeneric.SectionVisibility(
                formContext,
                Tabs.tab_administiration,
                Sections.Kidana,
                true
            );
        } else if (SDValue == true) {
            CommonGeneric.SectionVisibility(
                formContext,
                Tabs.tab_administiration,
                Sections.ServiceDesk,
                true
            );
        } else if (SahabValue == true) {
            CommonGeneric.SectionVisibility(
                formContext,
                Tabs.tab_administiration,
                Sections.Sahab,
                true
            );
        }
    }
}
function FilterCustomerTypeBasedOnService(formContext) {
    const customer = formContext.getControl(caseFields.customerid);

    if (!customer) return;
    var serviceRecord = GetLookUpRecordWithExpandedValues(
        formContext,
        caseFields.ldv_serviceid,
        "$select=ldv_name,ldv_applicanttypecode"
    );
    if (serviceRecord !== null) {
        var applicantType = serviceRecord["ldv_applicanttypecode"];
        switch (applicantType) {
            case ApplicantType.individual:
                customer.setEntityTypes(["contact"]); // restrict customer to allow only contacts
                break;
            case ApplicantType.company:
                customer.setEntityTypes(["account"]); // restrict customer to allow only accounts
                break;
            case ApplicantType.both:
            default:
                customer.setEntityTypes(["contact", "account"]);
                break;
        }
    } else {
        customer.setEntityTypes(["contact", "account"]);
    }

    // // in case applicant is customer lookup (allowing account and contact)
    // if (customer.getEntityTypes().length > 1)
    //   customer.setEntityTypes(["contact"]); // restrict customer to allow only contacts
}

function ShowAndHidePassportNumberAndIdNumber(executionContext) {
    var formContext = executionContext.getFormContext();
    var quickViewControl = formContext.ui.quickForms.get("Individual");

    if (quickViewControl) {
        if (quickViewControl.isLoaded()) {
            var passportAttribute = quickViewControl.getAttribute("governmentid");
            var idNumberAttribute = quickViewControl.getAttribute("ldv_idnumber");

            if (passportAttribute) {
                var passportNumber = passportAttribute.getValue();
                if (
                    passportNumber !== null &&
                    passportNumber !== undefined &&
                    passportNumber !== ""
                ) {
                    quickViewControl.getControl("governmentid").setVisible(true);
                } else {
                    quickViewControl.getControl("governmentid").setVisible(false);
                }
            } else {
                console.error(
                    "Attribute 'governmentid' not found in Quick View control."
                );
            }

            if (idNumberAttribute) {
                var idNumber = idNumberAttribute.getValue();
                if (idNumber !== null && idNumber !== undefined && idNumber !== "") {
                    quickViewControl.getControl("ldv_idnumber").setVisible(true);
                } else {
                    quickViewControl.getControl("ldv_idnumber").setVisible(false);
                }
            } else {
                console.error(
                    "Attribute 'ldv_idnumber' not found in Quick View control."
                );
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

function UnlockFieldsInSocialMediaStage(formContext) {
    var currentStage = formContext.data.process.getActiveStage();
    if (currentStage) {
        var currentStageName = currentStage.getName();
        if (
            currentStageName === "Social Media" ||
            currentStageName === "التواصل الإجتماعي" ||
            currentStageName === "التواصل الإجتماعي"
        ) {
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_maincategoryid,
                false
            );
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_subcategoryid,
                false
            );
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_secondarysubcategoryid,
                false
            );

            CommonGeneric.DisableField(formContext, caseFields.ldv_seasoncode, false);
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_beneficiarytypecode,
                false
            );
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_locationcode,
                false
            );
            CommonGeneric.DisableField(formContext, caseFields.ldv_company, false);
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_fieldserviceofficeid,
                false
            );
            CommonGeneric.DisableField(
                formContext,
                caseFields.ldv_description,
                false
            );
        }
    }
}

function ShowAndHideIntegrationFlags(
    formContext,
    sdFlag,
    tashirFlag,
    kadanaFlag
) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service === null || service === undefined) {
        return;
    } else {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();

        if (
            serviceId ===
            ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowField(formContext, sdFlag, true, false);
            CommonGeneric.ShowField(formContext, kadanaFlag, false, false);
            CommonGeneric.ShowField(formContext, tashirFlag, false, false);
        } else if (
            serviceId ===
            ServiceType.BusinessSectorComplainSD.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowField(formContext, sdFlag, true, false);
            CommonGeneric.ShowField(formContext, kadanaFlag, false, false);
            CommonGeneric.ShowField(formContext, tashirFlag, false, false);
        } else if (
            serviceId ===
            ServiceType.BusinessSectorComplainKadana.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowField(formContext, sdFlag, false, false);
            CommonGeneric.ShowField(formContext, kadanaFlag, true, false);
            CommonGeneric.ShowField(formContext, tashirFlag, false, false);
        } else if (
            serviceId ===
            ServiceType.PilgrimsComplaintsDirectHajj.serviceDefinitionId.toLowerCase()
        ) {
            CommonGeneric.ShowField(formContext, sdFlag, false, false);
            CommonGeneric.ShowField(formContext, kadanaFlag, false, false);
            CommonGeneric.ShowField(formContext, tashirFlag, true, false);
        }
    }
}

///////////////////////////////////////////////////////////////////Decisions//////////////////////////////

// #region Decisions
function GetActiveStageDecisionsBasedOnService(executionContext) {
    var formContext = executionContext.getFormContext();
    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
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
        case ServiceType.FinancialCompensationComplain.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainInternalPilgrimspostHajj.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase():
            var departmentStageId =
                BPFs.DepartmentComplain.stages.department.id.toLowerCase();
            var qualityStageId =
                BPFs.DepartmentComplain.stages.quality.id.toLowerCase();
            var socialMediaStageId =
                BPFs.DepartmentComplain.stages.socialMedia.id.toLowerCase();
            var supervisorStageId =
                BPFs.DepartmentComplain.stages.supervisor.id.toLowerCase();
            var customerApprovalStageId =
                BPFs.DepartmentComplain.stages.customerApproval.id.toLowerCase();
            var department2StageId =
                BPFs.DepartmentComplain.stages.department2.id.toLowerCase();

            if (currentStageId === customerApprovalStageId) {
                GeneralCustomerApproval(executionContext);
            } else if (currentStageId === socialMediaStageId) {
                GeneralSocialMediaDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_socialMediaDecisioncode)
                    .addOnChange(function () {
                        GeneralSocialMediaDecision_OnChange(executionContext);
                    });
            } else if (currentStageId === departmentStageId) {
                FC_General_DepartmentAdministrationStageDecision_OnChange(
                    executionContext
                );
                formContext
                    .getAttribute(caseFields.ldv_departmentdecisioncode)
                    .addOnChange(function () {
                        FC_General_DepartmentAdministrationStageDecision_OnChange(
                            executionContext
                        );
                    });
            } else if (currentStageId === department2StageId) {
                FC_General_Department2AdministrationStageDecision_OnChange(
                    executionContext
                );
                formContext
                    .getAttribute(caseFields.ldv_departmentdecisioncode)
                    .addOnChange(function () {
                        FC_General_Department2AdministrationStageDecision_OnChange(
                            executionContext
                        );
                    });
            } else if (currentStageId === supervisorStageId) {
                GeneralSuperVisorDecisionNext_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_supervisordecisioncode)
                    .addOnChange(function () {
                        GeneralSuperVisorDecisionNext_OnChange(executionContext);
                    });
            } else if (currentStageId === qualityStageId) {
                GeneralQualityStageDecisionR2_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_qualitydecisioncode)
                    .addOnChange(function () {
                        GeneralQualityStageDecisionR2_OnChange(executionContext);
                    });
            }
            break;

        case ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase():
            var umrahsCompanyServiceStageId =
                BPFs.TechnicalComplainMomentaryUmrahForCompanies.stages.umrahsCompanyService.id.toLowerCase();
            var qualityStageId =
                BPFs.TechnicalComplainMomentaryUmrahForCompanies.stages.quality.id.toLowerCase();
            var socialMediaStageId =
                BPFs.TechnicalComplainMomentaryUmrahForCompanies.stages.socialMedia.id.toLowerCase();
            var supervisorStageId =
                BPFs.TechnicalComplainMomentaryUmrahForCompanies.stages.supervisor.id.toLowerCase();

            if (currentStageId === socialMediaStageId) {
                GeneralSocialMediaDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_socialMediaDecisioncode)
                    .addOnChange(function () {
                        GeneralSocialMediaDecision_OnChange(executionContext);
                    });
            }
            if (currentStageId === umrahsCompanyServiceStageId) {
                TechnicalComplainMomentaryUmrahForCompanies_UmrahsCompanyServiceStageDecision_OnChange(
                    executionContext
                );
                formContext
                    .getAttribute(caseFields.ldv_umrahscompanyservicedecisioncode)
                    .addOnChange(function () {
                        TechnicalComplainMomentaryUmrahForCompanies_UmrahsCompanyServiceStageDecision_OnChange(
                            executionContext
                        );
                    });
            }
            if (currentStageId === supervisorStageId) {
                GeneralSuperVisorDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_supervisordecisioncode)
                    .addOnChange(function () {
                        GeneralSuperVisorDecision_OnChange(executionContext);
                    });
            }
            if (currentStageId === qualityStageId) {
                DepartmentQualityDecisionForR2_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_qualitydecisioncode)
                    .addOnChange(function () {
                        DepartmentQualityDecisionForR2_OnChange(executionContext);
                    });
            }

            break;

        case ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase():
            TechnicalComplainCompanyService_OnChange_Case(
                executionContext,
                currentStageId,
                formContext
            );
            break;
        case ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase():
            TechnicalComplainCompanyService_OnChange_Case(
                executionContext,
                currentStageId,
                formContext
            );
            break;
        case ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase():
            var qualityStageId =
                BPFs.TechnicalComplainCompanyService.stages.quality.id.toLowerCase();
            var supervisorStageId =
                BPFs.TechnicalComplainCompanyService.stages.supervisor.id.toLowerCase();
            var companyStageId =
                BPFs.TechnicalComplainCompanyService.stages.company.id.toLowerCase();
            var socialMediaStageId =
                BPFs.TechnicalComplainCompanyService.stages.socialMedia.id.toLowerCase();
            //var customerApprovalStageId = BPFs.TechnicalComplainCompanyService.stages.customerApproval.id.toLowerCase();

            if (currentStageId === socialMediaStageId) {
                console.log("socilla");

                GeneralSocialMediaDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_socialMediaDecisioncode)
                    .addOnChange(function () {
                        GeneralSocialMediaDecision_OnChange(executionContext);
                    });
            }

            //if (currentStageId === customerApprovalStageId) {
            //    TechnicalComplainMomentaryUmrah_CustomerApprovalStage_OnChange(executionContext);
            //    formContext.getAttribute(caseFields.ldv_Ispassedcustomerapproval).addOnChange(function () {
            //        TechnicalComplainMomentaryUmrah_CustomerApprovalStage_OnChange(executionContext);
            //    });
            //}

            if (currentStageId === companyStageId) {
                TechnicalComplainMomentaryUmrah_CompanyDecision_OnChange(
                    executionContext
                );
                formContext
                    .getAttribute(caseFields.ldv_departmentdecisioncode)
                    .addOnChange(function () {
                        TechnicalComplainMomentaryUmrah_CompanyDecision_OnChange(
                            executionContext
                        );
                    });
            }

            if (currentStageId === qualityStageId) {
                GeneralQualityStageDecisionR2_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_qualitydecisioncode)
                    .addOnChange(function () {
                        GeneralQualityStageDecisionR2_OnChange(executionContext);
                    });
            }

            if (currentStageId === supervisorStageId) {
                GeneralSuperVisorDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_supervisordecisioncode)
                    .addOnChange(function () {
                        GeneralSuperVisorDecision_OnChange(executionContext);
                    });
            }

            break;
        case ServiceType.Inquiry.serviceDefinitionId.toLowerCase():
            console.log(
                currentStageId + " -------- " + "momentaryUmrahOrHajj company service"
            );
            var socialMediaStageId = BPFs.Inquiry.stages.socialMedia.id.toLowerCase();
            var quality1_StageId = BPFs.Inquiry.stages.quality.id.toLowerCase();
            var quality2_StageId = BPFs.Inquiry.stages.quality2.id.toLowerCase();
            var supervisor1_StageId = BPFs.Inquiry.stages.supervisor.id.toLowerCase();
            var supervisor2_StageId =
                BPFs.Inquiry.stages.supervisor2.id.toLocaleLowerCase();
            var submit_StageId = BPFs.Inquiry.stages.submit.id.toLowerCase();

            //Submit
            if (currentStageId === submit_StageId) {
                unLockCategoryFieldsOnInquiry(formContext);
            }
            //Social Media
            else if (currentStageId === socialMediaStageId) {
                socialMediaDecision_Inquiry_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_inquirysocialmediadecisioncode)
                    .addOnChange(function () {
                        socialMediaDecision_Inquiry_OnChange(executionContext);
                    });

                unLockCategoryFieldsOnInquiry(formContext);
            } //Quality1
            else if (currentStageId === quality1_StageId) {
                Inquiry_QualityStageDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_qualitydecisioncode)
                    .addOnChange(function () {
                        Inquiry_QualityStageDecision_OnChange(executionContext);
                    });
                unLockCategoryFieldsOnInquiry(formContext);
            } //Quality2
            else if (currentStageId === quality2_StageId) {
                Inquiry_QualityStageDecision2_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_qualitydecisioncode)
                    .addOnChange(function () {
                        Inquiry_QualityStageDecision2_OnChange(executionContext);
                    });
                unLockCategoryFieldsOnInquiry(formContext);
            } //Supervisor1
            else if (currentStageId === supervisor1_StageId) {
                supervisorDecision_Inquiry_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_supervisorDesision2)
                    .addOnChange(function () {
                        supervisorDecision_Inquiry_OnChange(executionContext);
                    });
            } //supervisor2
            else if (currentStageId === supervisor2_StageId) {
                supervisorDecision2_Inquiry_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_supervisorDesision2)
                    .addOnChange(function () {
                        supervisorDecision2_Inquiry_OnChange(executionContext);
                    });
            }
            break;
        case ServiceType.Suggestions.serviceDefinitionId.toLowerCase():
        case ServiceType.Testimonials.serviceDefinitionId.toLowerCase():
            //  var suggestionQualityStageId = BPFs.Suggestions.stages.quality.id.toLowerCase();
            var CXStageId = BPFs.Suggestions.stages.CX.id.toLowerCase();
            //  var suggestionQuality2_StageId = BPFs.Suggestions.stages.quality_2.id.toLowerCase();
            var suggestionSocialMediaStageId =
                BPFs.Suggestions.stages.socialMedia.id.toLocaleLowerCase();

            // if (currentStageId === suggestionSocialMediaStageId) {
            //   SocialMediaDecision_Suggestion_OnChange(executionContext);
            //   formContext
            //     .getAttribute(caseFields.ldv_socialMediaDecisioncode)
            //     .addOnChange(function () {
            //       SocialMediaDecision_Suggestion_OnChange(executionContext);
            //     });
            // }
            // if (currentStageId === suggestionQualityStageId) {
            //     ShowNextStage(executionContext);
            // }

            if (currentStageId == CXStageId) {
                CxDecision_Suggestion_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_cxdecisioncode)
                    .addOnChange(function () {
                        CxDecision_Suggestion_OnChange(executionContext);
                    });
            }

            // if (currentStageId == suggestionQuality2_StageId) {
            //     GeneralQualityStageDecisionR2_OnChange(executionContext);
            //     formContext.getAttribute(caseFields.ldv_qualitydecisioncode).addOnChange(function () {
            //         GeneralQualityStageDecisionR2_OnChange(executionContext);
            //     });
            // }

            break;

        case ServiceType.BusinessSectorComplainSD.serviceDefinitionId.toLowerCase():
        case ServiceType.BusinessSectorComplainKadana.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase():
        case ServiceType.PilgrimsComplaintsDirectHajj.serviceDefinitionId.toLowerCase():
            //case ServiceType.FinancialComplainExternalPilgrims.serviceDefinitionId.toLowerCase():
            //case ServiceType.FinancialComplainNusukServices.serviceDefinitionId.toLowerCase():
            var qualityStageId =
                BPFs.RequestComplainsIntegration.stages.Quality.id.toLowerCase();
            //var quality2StageId =
            //    BPFs.RequestComplainsIntegration.stages.Quality2.id.toLowerCase();
            //var quality3StageId =
            //    BPFs.RequestComplainsIntegration.stages.Quality3.id.toLowerCase();
            var FCRStageId =
                BPFs.RequestComplainsIntegration.stages.FCR.id.toLowerCase();
            var socialMediaStageId =
                BPFs.RequestComplainsIntegration.stages.SocialMedia.id.toLowerCase();
            var integrationId =
                BPFs.RequestComplainsIntegration.stages.Integration.id.toLowerCase();
            var integration2Id =
                BPFs.RequestComplainsIntegration.stages.Integration2.id.toLowerCase();
            var supervisorId =
                BPFs.RequestComplainsIntegration.stages.Supervisor.id.toLowerCase();

            if (currentStageId === integrationId) {
                IntegrationServices_Integration_OnLoad(formContext);
            }
            if (currentStageId === integration2Id) {
                IntegrationServices_Integration2_OnLoad(formContext);
            }
            if (currentStageId === supervisorId) {
                GeneralSuperVisorDecisionNext_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_supervisordecisioncode)
                    .addOnChange(function () {
                        GeneralSuperVisorDecisionNext_OnChange(executionContext);
                    });
            }
            if (currentStageId === qualityStageId) {
                GeneralQualityStageDecisionR2_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_qualitydecisioncode)
                    .addOnChange(function () {
                        GeneralQualityStageDecisionR2_OnChange(executionContext);
                    });
            }

            if (currentStageId === FCRStageId) {
                IntegrationServices_IsFCR_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_fcrdecisioncode)
                    .addOnChange(function () {
                        IntegrationServices_IsFCR_OnChange(executionContext);
                    });
            }
            if (currentStageId == socialMediaStageId) {
                // debugger;
                GeneralSocialMediaDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_socialMediaDecisioncode)
                    .addOnChange(function () {
                        GeneralSocialMediaDecision_OnChange(executionContext);
                    });
            }
            break;
        case ServiceType.MentoringServiceNUSUKEnaya.serviceDefinitionId.toLowerCase():
        case ServiceType.MissingPersonsServiceNUSUKEnaya.serviceDefinitionId.toLowerCase():
            var Nusuk1StageId =
                BPFs.NusukCareServicesRequest.stages.nusuk1.id.toLowerCase();
            var CompanyStageId =
                BPFs.NusukCareServicesRequest.stages.company.id.toLowerCase();
            var nusukStageId =
                BPFs.NusukCareServicesRequest.stages.nusuk.id.toLowerCase();

            if (currentStageId === Nusuk1StageId) {
                NusukSubmitDecision_MentoringService_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_nusukdecisioncode)
                    .addOnChange(function () {
                        NusukSubmitDecision_MentoringService_OnChange(executionContext);
                    });
            }
            if (currentStageId === CompanyStageId) {
                NusukMonitoring_CompanyDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_companiesservicedecisioncode)
                    .addOnChange(function () {
                        NusukMonitoring_CompanyDecision_OnChange(executionContext);
                    });
            }
            if (currentStageId === nusukStageId) {
                NusukMonitoring_NusukResolveDecision_OnChange(executionContext);
                formContext
                    .getAttribute(caseFields.ldv_nusukclosedecisioncode)
                    .addOnChange(function () {
                        NusukMonitoring_NusukResolveDecision_OnChange(executionContext);
                    });
            }
            break;
    }
}

function GetDecisionsBasedOnService_OnLoad(formContext) {
    debugger;
    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
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
        case ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialComplainInternalPilgrimspostHajj.serviceDefinitionId.toLowerCase():
        case ServiceType.FinancialCompensationComplain.serviceDefinitionId.toLowerCase():
            FC_General_DepartmentComplain_OnLoad(formContext);
            HideOriginOnSubmitStage(formContext);
            break;

        // To be asked about
        //case ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase():
        //case ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase():
        //    TechnicalComplainNotMomentaryHijjAndUmarah_OnLoad(formContext);
        //    HideFieldsOnSubmitTechnicalComplainNonMomentaryHajjAndUmrah(formContext);
        //    HideOriginOnSubmitStage(formContext);
        //    break;

        case ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase():
            TechnicalComplainMomentaryUmrahForCompanies_OnLoad(formContext);
            HideOriginOnSubmitStage(formContext);
            break;

        case ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase():
            TechnicalComplainMomentaryUmrah_OnLoad(formContext);
            //HideFieldsOnSubmitStageTechnicalComplainMomentaryUmrah(formContext);
            HideOriginOnSubmitStage(formContext);

            break;
        case ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase():
            TechnicalComplainMomentaryUmrah_OnLoad(formContext);
            //HideFieldsOnSubmitStageTechnicalComplainMomentaryUmrah(formContext);
            HideOriginOnSubmitStage(formContext);

            break;

        case ServiceType.Inquiry.serviceDefinitionId.toLowerCase():
            Inquiry_QualityStageDecision_OnLoad(formContext);
            // socialMediaDecision_Inquiry_OnLoad(formContext);
            // RemoveTransferOptionFromInquirySocialMediaDecision(formContext);

            HideFieldsOnSubmitStageInquiry(formContext);
            break;

        case ServiceType.Suggestions.serviceDefinitionId.toLowerCase():
        case ServiceType.Testimonials.serviceDefinitionId.toLowerCase():
            Suggestions_OnLoad(formContext);
            HideOriginOnSubmitStage(formContext);
            break;
        case ServiceType.BusinessSectorComplainSD.serviceDefinitionId.toLowerCase():
        case ServiceType.BusinessSectorComplainKadana.serviceDefinitionId.toLowerCase():
        case ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase():
        case ServiceType.PilgrimsComplaintsDirectHajj.serviceDefinitionId.toLowerCase():
            //case ServiceType.FinancialComplainExternalPilgrims.serviceDefinitionId.toLowerCase():
            //case ServiceType.FinancialComplainNusukServices.serviceDefinitionId.toLowerCase():
            IntegrationServices_OnLoad(formContext);
            //HideFieldsOnSubmitStageRequestComplainsIntegration(formContext);
            HideOriginOnSubmitStage(formContext);
            break;

        case ServiceType.MentoringServiceNUSUKEnaya.serviceDefinitionId.toLowerCase():
        case ServiceType.MissingPersonsServiceNUSUKEnaya.serviceDefinitionId.toLowerCase():
            NusukMonitoring__OnLoad(formContext);
            HideFieldsOnCompanyStageNusukServices(formContext);
            RemoveTransferOptionFromCompanyDecision(formContext);
            break;
    }
}

// #region FC Internal Pilgrims post Hajj service & Financial Compensation Complain & technical Non Commentary for Hajj and Umrah
function FC_General_DepartmentAdministrationStageDecision_OnChange(
    executionContext
) {
    var formContext = executionContext.getFormContext();
    var departmentNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_departmentneededinformation;
    var departmentClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_departmentclosurereason;

    //DepartmentDecision_OnLoad(
    //    formContext,
    //    caseFields.ldv_departmentdecisioncode,
    //    departmentNeedInfoOnBpf,
    //    departmentClosureReasonOnBpf
    //);
    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_departmentdecisioncode,
        departmentNeedInfoOnBpf,
        departmentClosureReasonOnBpf
    );
}

// Department 2 show next and previos buttons
function FC_General_Department2AdministrationStageDecision_OnChange(
    executionContext
) {
    var formContext = executionContext.getFormContext();
    var departmentNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_departmentneededinformation + "_1";
    var departmentClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_departmentclosurereason + "_1";

    Department2Decision_OnChange(
        executionContext,
        caseFields.ldv_departmentdecisioncode,
        departmentNeedInfoOnBpf,
        departmentClosureReasonOnBpf
    );
}

function FC_General_DepartmentComplain_OnLoad(formContext) {
    var departmentNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_departmentneededinformation;
    var departmentClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_departmentclosurereason;

    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_departmentdecisioncode,
        departmentNeedInfoOnBpf,
        departmentClosureReasonOnBpf
    );

    GeneralQualityStageDecisionR2_OnLoad(formContext);
    GeneralSocialMediaDecision_OnLoad(formContext);
    GeneralSupervisorDecision_OnLoad(formContext);
}

// #region Financial compensation complain
//function FinancialCompensationComplain_MinistrySHajjAgencyStageDecision_OnChange(
//    executionContext
//) {
//    var ministryHajjNeedInfoOnBpf =
//        "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
//    var ministryHajjClosureReasonOnBpf =
//        "header_process_" + caseFields.ldv_closurereasons;

//    DepartmentDecision_OnChange(
//        executionContext,
//        caseFields.ldv_ministryshajjagencydecisisoncode,
//        ministryHajjNeedInfoOnBpf,
//        ministryHajjClosureReasonOnBpf
//    );
//}

//function FinancialCompensationComplain_OnLoad(formContext) {
//    var departmentNeedInfoOnBpf =
//        "header_process_" + caseFields.ldv_departmentneededinformation;
//    var departmentClosureReasonOnBpf =
//        "header_process_" + caseFields.ldv_departmentclosurereason;

//    DepartmentDecision_OnLoad(
//        formContext,
//        caseFields.ldv_departmentdecisioncode,
//        departmentNeedInfoOnBpf,
//        departmentClosureReasonOnBpf
//    );

//    GeneralQualityStageDecisionR2_OnLoad(formContext);
//    GeneralSocialMediaDecision_OnLoad(formContext);
//    GeneralSupervisorDecision_OnLoad(formContext);
//}

//#endregion

// #region Technical Complain Momentary Umrah For Companies
function TechnicalComplainMomentaryUmrahForCompanies_UmrahsCompanyServiceStageDecision_OnChange(
    executionContext
) {
    var umrahsCompanyNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_umrahscompanyserviceneededinformation;
    var umrahsCompanyClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_umrahscompanyservicedecisioncode,
        umrahsCompanyNeedInfoOnBpf,
        umrahsCompanyClosureReasonOnBpf
    );
}

function TechnicalComplainMomentaryUmrahForCompanies_OnLoad(formContext) {
    debugger;
    var umrahsCompanyNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_umrahscompanyserviceneededinformation;
    var umrahsCompanyClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_umrahscompanyservicedecisioncode,
        umrahsCompanyNeedInfoOnBpf,
        umrahsCompanyClosureReasonOnBpf
    );
    GeneralQualityStageDecisionR2_OnLoad(formContext);
    GeneralSocialMediaDecision_OnLoad(formContext);
    GeneralSupervisorDecision_OnLoad(formContext);
}

//#endregion

// #region Technical Complain Momentary Umrah
function TechnicalComplainMomentaryUmrah_CustomerApprovalStage_OnChange(
    executionContext
) {
    var MakkahyNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_companiesadministrationneededinformation;
    var customerApprovalIspassedCustomerApproval =
        "header_process_" + caseFields.ldv_Ispassedcustomerapproval;

    CustomerApprovalDecision_OnChange(
        executionContext,
        customerApprovalIspassedCustomerApproval
    );
}

//Company Decision OnCHange
function TechnicalComplainMomentaryUmrah_CompanyDecision_OnChange(
    executionContext
) {
    debugger;
    var companyDecision = caseFields.ldv_departmentdecisioncode;
    var companyNeedMoreInformation =
        "header_process_" + caseFields.ldv_needsmoredetails;
    var companyClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;
    console.log(
        companyDecision,
        companyNeedMoreInformation,
        companyClosureReasonOnBpf
    );
    DepartmentDecision_OnChange(
        executionContext,
        companyDecision,
        companyNeedMoreInformation,
        companyClosureReasonOnBpf
    );
}

function CompanyDecisionSupervisorSendBack_OnChange(executionContext) {
    debugger;
    var companyDecision = caseFields.ldv_departmentdecisioncode;
    var companyNeedMoreInformation =
        "header_process_" + caseFields.ldv_needsmoredetails + "_1";
    var companyClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons + "_1";
    console.log(
        companyDecision,
        companyNeedMoreInformation,
        companyClosureReasonOnBpf
    );
    DepartmentQualityDecisionForR2_OnChange(
        executionContext,
        companyDecision,
        companyNeedMoreInformation,
        companyClosureReasonOnBpf
    );
}

function TechnicalComplainMomentaryUmrah_JadaStageDecision_OnChange(
    executionContext
) {
    var JadaNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_agentemployeeneededinformation;
    var JadaClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons + "_1";

    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_agentemployeedecisioncode,
        JadaNeedInfoOnBpf,
        JadaClosureReasonOnBpf
    );
}

function TechnicalComplainMomentaryUmrah_MadinaStageDecision_OnChange(
    executionContext
) {
    var MadinaNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_castingofficerneededinformation;
    var MadinaClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_castingofficerdecisioncode,
        MadinaNeedInfoOnBpf,
        MadinaClosureReasonOnBpf
    );
}

function TechnicalComplainMomentaryUmrah_Quality2StageDecision_OnChange(
    executionContext
) {
    var quality2ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_1";
    var quality2NeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_1";
    DepartmentQualityDecisionForR2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        quality2NeededInformationOnBpf,
        quality2ClosureReasonOnBpf
    );
}

function TechnicalComplainMomentaryUmrah_Quality3StageDecision_OnChange(
    executionContext
) {
    var quality3ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_2";
    var quality3NeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_2";
    DepartmentQualityDecisionForR2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        quality3NeededInformationOnBpf,
        quality3ClosureReasonOnBpf
    );
}
function TechnicalComplainMomentaryHajj_Quality_StageDecision_OnChange(
    executionContext,
    number
) {
    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + number;
    var qualityNeededInformationOnBpf =
        "header_process_" +
        caseFields.ldv_qualityofficerneededinformations +
        number;
    DepartmentQualityDecisionForR2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        qualityClosureReasonOnBpf
    );
}
function TechnicalComplainMomentryHajj_Supervisor_Stage_Onchange(
    executionContext,
    number
) {
    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + number;
    SupervisorDecision_OnChange(
        executionContext,
        caseFields.ldv_supervisordecisioncode,
        supervisorCommentOnBpf
    );
}
function TechnicalComplainMomentryUmrah_Supervisor2Stage_Onchange(
    executionContext
) {
    var supervisor2CommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_1";
    SupervisorDecision_OnChange(
        executionContext,
        caseFields.ldv_supervisordecisioncode,
        supervisor2CommentOnBpf
    );
}
function TechnicalComplainMomentryUmrah_Supervisor3Stage_Onchange(
    executionContext
) {
    var supervisor3CommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_2";
    SupervisorDecision_OnChange(
        executionContext,
        caseFields.ldv_supervisordecisioncode,
        supervisor3CommentOnBpf
    );
}
function TechnicalComplainMomentaryUmrah_OnLoad(formContext) {
    var companyNeedInfoOnBpf =
        "header_process_" + caseFields.ldv_needsmoredetails;

    var companyClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;

    var company2NeedInfoOnBpf =
        "header_process_" + caseFields.ldv_needsmoredetails + "_1";

    var company2ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereasons + "_1";

    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment;

    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason;

    var qualityNeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformation2;

    //var customerApprovalIsPassedOnBPF = "header_process_" + caseFields.ldv_Ispassedcustomerapproval;

    //Company Stage
    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_departmentdecisioncode,
        companyNeedInfoOnBpf,
        companyClosureReasonOnBpf
    );
    // Customer Approval
    //CustomerApprovalDecision_OnLoad(formContext,customerApprovalIsPassedOnBPF);

    GeneralQualityStageDecisionR2_OnLoad(formContext);
    GeneralSocialMediaDecision_OnLoad(formContext);
    GeneralSuperVisorDecisionNext_OnLoad(formContext);
    ConcernedDepartmentDecisionR2_OnLoad(
        formContext,
        caseFields.ldv_departmentdecisioncode,
        company2NeedInfoOnBpf,
        company2ClosureReasonOnBpf
    );
    //SupervisorDecisionNext_OnLoad(
    //    formContext,
    //    caseFields.ldv_supervisorDesision2,
    //    supervisorCommentOnBpf
    //);

    DepartmentQualityDecisionForR2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        qualityClosureReasonOnBpf
    );
}

//#endregion

// #region Inquiry
//Quality 1
function Inquiry_QualityStageDecision_OnChange(executionContext) {
    var ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    var transferReasonOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations;

    QualityDecision_R2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        ClosureReasonOnBpf,
        transferReasonOnBpf
    );
}

function Inquiry_QualityStageDecision_OnLoad(formContext) {
    var ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereason;
    var transferReasonOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations;

    QualityDecision_R2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        ClosureReasonOnBpf,
        transferReasonOnBpf
    );
}
//Quality 2
function Inquiry_QualityStageDecision2_OnChange(executionContext) {
    var ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_1";
    var transferReasonOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_1";

    QualityDecision_R2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        ClosureReasonOnBpf,
        transferReasonOnBpf
    );
}

function Inquiry_QualityStageDecision2_OnLoad(formContext) {
    var ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_1";
    var transferReasonOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_1";

    QualityDecision_R2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        ClosureReasonOnBpf,
        transferReasonOnBpf
    );
}
function unLockCategoryFieldsOnInquiry(formContext) {
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    var currentStage = formContext.data.process.getActiveStage();

    if (!service || !currentStage) {
        console.error("Service or current stage is null or undefined.");
        return;
    }

    if (service !== null && service !== undefined) {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();
        var currentStageId = currentStage.getId().toLowerCase();
        var qualityStageId = BPFs.Inquiry.stages.quality.id.toLowerCase();
        var qualityStageId2 = BPFs.Inquiry.stages.quality2.id.toLowerCase();
        var socialMediaStageId = BPFs.Inquiry.stages.socialMedia.id.toLowerCase();

        if (serviceId === ServiceType.Inquiry.serviceDefinitionId.toLowerCase()) {
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_maincategoryid,
                caseFields.ldv_serviceid
            );
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_subcategoryid,
                caseFields.ldv_maincategoryid
            );
            CommonGeneric.LockUnlock_field1_BasedOn_field2_Emptiness(
                formContext,
                caseFields.ldv_secondarysubcategoryid,
                caseFields.ldv_subcategoryid
            );
            if (
                currentStageId === qualityStageId ||
                currentStageId === qualityStageId2
            ) {
                //Set Main Category Required in stage Quality
                CommonGeneric.SetReqLevel(
                    formContext,
                    caseFields.ldv_maincategoryid,
                    true
                );
                CommonGeneric.SetReqLevel(
                    formContext,
                    caseFields.ldv_subcategoryid,
                    true
                );
                CommonGeneric.SetReqLevel(
                    formContext,
                    caseFields.ldv_secondarysubcategoryid,
                    true
                );
            }
        }
    }
}

//
function socialMediaDecision_Inquiry_OnLoad(formContext) {
    RemoveTransferOptionFromInquirySocialMediaDecision(formContext);

    var socialMediaCommentOnBpf =
        "header_process_" + caseFields.ldv_socialMediaComment;

    inquirysocialmediadecisioncode_OnLoad(
        formContext,
        caseFields.ldv_inquirysocialmediadecisioncode,
        socialMediaCommentOnBpf
    );
}

function socialMediaDecision_Inquiry_OnChange(executionContext) {
    var socialMediaCommentOnBpf =
        "header_process_" + caseFields.ldv_socialMediaComment;
    //to do   ---  add clouser reason field
    inquirysocialmediadecisioncode_OnChange(
        executionContext,
        caseFields.ldv_inquirysocialmediadecisioncode,
        socialMediaCommentOnBpf
    );
}
//Supervisor1
function supervisorDecision_Inquiry_OnChange(executionContext) {
    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment;
    InquirySupervisorDecision_R2_OnChange(
        executionContext,
        caseFields.ldv_supervisorDesision2,
        supervisorCommentOnBpf
    );
}
//Supervisor2
function supervisorDecision2_Inquiry_OnChange(executionContext) {
    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_1";
    InquirySupervisorDecision_R2_OnChange(
        executionContext,
        caseFields.ldv_supervisorDesision2,
        supervisorCommentOnBpf
    );
}
//#endregion

// #region Suggestions
// function Suggestions_ConcernedDepartmentDecision_OnChange(executionContext) {
//     var suggestionsCDNeededInfoOnBpf = 'header_process_' + caseFields.ldv_departmentneededinformation;
//     var suggestionCDClosureReason = 'header_process_' + caseFields.ldv_departmentclosurereason;

//     ConcernedDepartmentDecisionR2_OnChange(
//         executionContext,
//         caseFields.ldv_departmentdecisioncode,
//         suggestionsCDNeededInfoOnBpf,
//         suggestionCDClosureReason
//     );
// }

function SocialMediaDecision_Suggestion_OnLoad(formContext) {
    var socialMediaCommentOnBpf =
        "header_process_" + caseFields.ldv_socialMediaComment;

    SocialMediaDecision_OnLoad(
        formContext,
        caseFields.ldv_socialMediaDecisioncode,
        socialMediaCommentOnBpf
    );
}

function SocialMediaDecision_Suggestion_OnChange(executionContext) {
    var socialMediaCommentOnBpf =
        "header_process_" + caseFields.ldv_socialMediaComment;

    SocialMediaDecision_OnChange(
        executionContext,
        caseFields.ldv_socialMediaDecisioncode,
        socialMediaCommentOnBpf
    );
}
// Cx Decision on Suggestions
function CxDecision_Suggestion_OnLoad(formContext) {
    var CxDescisionCommentOnBpf =
        "header_process_" + caseFields.ldv_closurereasoncx;

    CxDecision_OnLoad(
        formContext,
        caseFields.ldv_cxdecisioncode,
        CxDescisionCommentOnBpf
    );
}

function CxDecision_Suggestion_OnChange(formContext) {
    var CxDescisionCommentOnBpf =
        "header_process_" + caseFields.ldv_closurereasoncx;

    CxDecision_OnChange(
        formContext,
        caseFields.ldv_cxdecisioncode,
        CxDescisionCommentOnBpf
    );
}

// nusuk team submit Decision on case Mentoring Service
function NusukSubmitDecision_MentoringService_OnLoad(formContext) {
    var nusukSubmitDescisionCommentOnBpf =
        "header_process_" + caseFields.ldv_nusukcarecomment;

    NusukSubmitDecision_OnLoad(
        formContext,
        caseFields.ldv_nusukdecisioncode,
        nusukSubmitDescisionCommentOnBpf
    );
}

function NusukSubmitDecision_MentoringService_OnChange(formContext) {
    var NusukSubmitDescisionCommentOnBpf =
        "header_process_" + caseFields.ldv_nusukcarecomment;

    NusukSubmitDecision_OnChange(
        formContext,
        caseFields.ldv_nusukdecisioncode,
        NusukSubmitDescisionCommentOnBpf
    );
}

function NusukMonitoring_CompanyDecision_OnChange(executionContext) {
    //   var NusukSubmitDescisionCommentOnBpf =
    var needMoreDetailsOnBpf =
        "header_process_" + caseFields.ldv_companiesserviceneededinformation;

    var ClosureReasonOnBpfOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;
    //       caseFields.ldv_nusukdecisioncode,
    //       NusukSubmitDescisionCommentOnBpf
    //   );
    // }

    concernedCompanyDecision_OnChange(
        executionContext,
        caseFields.ldv_companiesservicedecisioncode,
        needMoreDetailsOnBpf
        // ClosureReasonOnBpfOnBpf
    );
}

function NusukMonitoring_CompanyDecision_OnLoad(formContext) {
    debugger;
    var needMoreDetailsOnBpf =
        "header_process_" + caseFields.ldv_companiesserviceneededinformation;
    var ClosureReasonOnBpfOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;
    concernedCompanyDecision__OnLoad(
        formContext,
        caseFields.ldv_companiesservicedecisioncode,
        needMoreDetailsOnBpf
        //ClosureReasonOnBpfOnBpf
    );
}

function NusukMonitoring_NusukResolveDecision_OnLoad(formContext) {
    var ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    var needMoreDetailsOnBpf =
        "header_process_" + caseFields.ldv_needsmoredetails;

    NusukResolveDecision_OnLoad(
        formContext,
        caseFields.ldv_nusukclosedecisioncode,
        ClosureReasonOnBpf,
        needMoreDetailsOnBpf
    );
}

function NusukMonitoring_NusukResolveDecision_OnChange(executionContext) {
    var ClosureReasonOnBpf = "header_process_" + caseFields.ldv_closurereasons;
    var needMoreDetailsOnBpf =
        "header_process_" + caseFields.ldv_needsmoredetails;

    NusukResolveDecision_OnChange(
        executionContext,
        caseFields.ldv_nusukclosedecisioncode,
        ClosureReasonOnBpf,
        needMoreDetailsOnBpf
    );
}

function Suggestions_OnLoad(formContext) {
    CxDecision_Suggestion_OnLoad(formContext);
}
//#endregion

// #region TC-Momentary Hajj
function TechnicalComplainMomentaryHajj_OnLoad(formContext) {
    GeneralQualityStageDecisionR2_OnLoad(formContext);
    GeneralSocialMediaDecision_OnLoad(formContext);
    GeneralSupervisorDecision_OnLoad(formContext);

    var quality2ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_1";
    var quality2NeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_1";

    var quality3ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_2";
    var quality3NeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_2";

    var quality4ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_3";
    var quality4NeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_3";

    var supervisor2CommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_1";
    var supervisor3CommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_2";
    var supervisor4CommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_3";

    DepartmentQualityDecisionForR2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        quality2NeededInformationOnBpf,
        quality2ClosureReasonOnBpf
    );
    DepartmentQualityDecisionForR2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        quality3NeededInformationOnBpf,
        quality3ClosureReasonOnBpf
    );
    DepartmentQualityDecisionForR2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        quality4NeededInformationOnBpf,
        quality4ClosureReasonOnBpf
    );
    SupervisorDecision_OnLoad(
        formContext,
        caseFields.ldv_supervisordecisioncode,
        supervisor2CommentOnBpf
    );
    SupervisorDecision_OnLoad(
        formContext,
        caseFields.ldv_supervisordecisioncode,
        supervisor3CommentOnBpf
    );
    SupervisorDecision_OnLoad(
        formContext,
        caseFields.ldv_supervisordecisioncode,
        supervisor4CommentOnBpf
    );

    var needInfoOnBpfMadina =
        "header_process_" + caseFields.ldv_castingofficerneededinformation;
    var closureReasonOnBpfMadina =
        "header_process_" + caseFields.ldv_closurereasons + "_2";
    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_castingofficerdecisioncode,
        needInfoOnBpfMadina,
        closureReasonOnBpfMadina
    );
    var needInfoOnBpfMakkah =
        "header_process_" + caseFields.ldv_companiesserviceneededinformation;
    var closureReasonOnBpfMakkah =
        "header_process_" + caseFields.ldv_closurereasons + "_3";
    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_companiesservicedecisioncode,
        needInfoOnBpfMakkah,
        closureReasonOnBpfMakkah
    );
    var needInfoOnBpfBorderCrossing =
        "header_process_" + caseFields.ldv_agentemployeeneededinformation;
    var closureReasonOnBpfBorderCrossing =
        "header_process_" + caseFields.ldv_closurereasons + "_1";
    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_agentemployeedecisioncode,
        needInfoOnBpfBorderCrossing,
        closureReasonOnBpfBorderCrossing
    );
    var needInfoOnBpfCoordinationCouncil =
        "header_process_" + caseFields.ldv_coordinationcouncilneededinformation;
    var closureReasonOnBpfCoordinationCouncil =
        "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_coordinationcouncildecisioncode,
        needInfoOnBpfCoordinationCouncil,
        closureReasonOnBpfCoordinationCouncil
    );
}
function TechnicalComplainMomentaryHajj_MadinaDecision_OnChange(
    executionContext
) {
    //ldv_supervisorid_2
    var needInfoOnBpfMadina =
        "header_process_" + caseFields.ldv_castingofficerneededinformation;
    var closureReasonOnBpfMadina =
        "header_process_" + caseFields.ldv_closurereasons + "_2";

    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_castingofficerdecisioncode,
        needInfoOnBpfMadina,
        closureReasonOnBpfMadina
    );
}
function TechnicalComplainMomentaryHajj_MakkahDecision_OnChange(
    executionContext
) {
    //ldv_supervisorid_3
    var needInfoOnBpfMakkah =
        "header_process_" + caseFields.ldv_companiesserviceneededinformation;
    var closureReasonOnBpfMakkah =
        "header_process_" + caseFields.ldv_closurereasons + "_3";

    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_companiesservicedecisioncode,
        needInfoOnBpfMakkah,
        closureReasonOnBpfMakkah
    );
}
function TechnicalComplainMomentaryHajj_BorderCrossingDecision_OnChange(
    executionContext
) {
    // ldv_supervisorid_1
    var needInfoOnBpfBorderCrossing =
        "header_process_" + caseFields.ldv_agentemployeeneededinformation;
    var closureReasonOnBpfBorderCrossing =
        "header_process_" + caseFields.ldv_closurereasons + "_1";

    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_agentemployeedecisioncode,
        needInfoOnBpfBorderCrossing,
        closureReasonOnBpfBorderCrossing
    );
}
function TechnicalComplainMomentaryHajj_CoordinationCouncilDecision_OnChange(
    executionContext
) {
    // ldv_supervisorid
    var needInfoOnBpfCoordinationCouncil =
        "header_process_" + caseFields.ldv_coordinationcouncilneededinformation;
    var closureReasonOnBpfCoordinationCouncil =
        "header_process_" + caseFields.ldv_closurereasons;

    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_coordinationcouncildecisioncode,
        needInfoOnBpfCoordinationCouncil,
        closureReasonOnBpfCoordinationCouncil
    );
}

//#endregion

// #region  TechnicalComplainNonMomentaryHajj&Umrah
function TechnicalComplainNotMomentaryHijjAndUmarah_OnLoad(formContext) {
    GeneralQualityStageDecisionR2_OnLoad(formContext);

    var needInfoOnBpfHajj =
        "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var closureReasonOnBpfHajj =
        "header_process_" + caseFields.ldv_closurereasons;
    var supervisor2CommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_1";
    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_1";
    var qualityNeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_1";
    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_ministryshajjagencydecisisoncode,
        needInfoOnBpfHajj,
        closureReasonOnBpfHajj
    );

    var needInfoOnBpfUmarah =
        "header_process_" + caseFields.ldv_ministrysumarahagencyneededinformation;
    var closureReasonOnBpfUmarah =
        "header_process_" + caseFields.ldv_closurereasons + "_1";
    DepartmentDecision_OnLoad(
        formContext,
        caseFields.ldv_ministrysumarahagencydecisisoncode,
        needInfoOnBpfUmarah,
        closureReasonOnBpfUmarah
    );
    GeneralSocialMediaDecision_OnLoad(formContext);
    GeneralSupervisorDecision_OnLoad(formContext);
    SupervisorDecision_OnLoad(
        formContext,
        caseFields.ldv_supervisordecisioncode,
        supervisor2CommentOnBpf
    );
    DepartmentQualityDecisionForR2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        qualityClosureReasonOnBpf
    );
}

function TechnicalComplainNotMomentaryHijjAndUmarah_HajjDecision_OnChange(
    executionContext
) {
    var needInfoOnBpfHajj =
        "header_process_" + caseFields.ldv_ministryshajjagencyneededinformation;
    var closureReasonOnBpfHajj =
        "header_process_" + caseFields.ldv_closurereasons;
    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_ministryshajjagencydecisisoncode,
        needInfoOnBpfHajj,
        closureReasonOnBpfHajj
    );
}
function TechnicalComplainNotMomentaryHijjAndUmarah_UmarahDecision_OnChange(
    executionContext
) {
    var needInfoOnBpfUmarah =
        "header_process_" + caseFields.ldv_ministrysumarahagencyneededinformation;
    var closureReasonOnBpfUmarah =
        "header_process_" + caseFields.ldv_closurereasons + "_1";
    DepartmentDecision_OnChange(
        executionContext,
        caseFields.ldv_ministrysumarahagencydecisisoncode,
        needInfoOnBpfUmarah,
        closureReasonOnBpfUmarah
    );
}

function TechnicalComplainMomentryUmrah_Supervisor2Stage_Onchange(
    executionContext
) {
    var supervisor2CommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment + "_1";
    SupervisorDecision_OnChange(
        executionContext,
        caseFields.ldv_supervisordecisioncode,
        supervisor2CommentOnBpf
    );
}

function TechnicalComplainMomentryUmrah_Quality2Stage_OnChange(
    executionContext
) {
    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_1";
    var qualityNeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_1";
    DepartmentQualityDecisionForR2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        qualityClosureReasonOnBpf
    );
}

//#endregion

//#region Integration Services

//using the general quality method directly
function IntegrationServices_Integration_OnLoad(formContext) {
    var sdFlag = "header_process_" + caseFields.ldv_isservicedeskupdated;
    var kadanaFlag = "header_process_" + caseFields.ldv_iskadanaupdated;
    var tashirFlag = "header_process_" + caseFields.ldv_istashirupdated;

    ShowAndHideIntegrationFlags(formContext, sdFlag, tashirFlag, kadanaFlag);
}

function IntegrationServices_Integration2_OnLoad(formContext) {
    var sdFlag = "header_process_" + caseFields.ldv_isservicedeskupdated + "_1";
    var kadanaFlag = "header_process_" + caseFields.ldv_iskadanaupdated + "_1";
    var tashirFlag = "header_process_" + caseFields.ldv_istashirupdated + "_1";

    ShowAndHideIntegrationFlags(formContext, sdFlag, tashirFlag, kadanaFlag);
}
function IntegrationServices_QualityDecision_OnChange(executionContext) {
    var quality2ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_2";
    var qualityNeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_2";

    DepartmentQualityDecisionForR2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        quality2ClosureReasonOnBpf
    );
}

function IntegrationServices_Quality3Decision_OnChange(executionContext) {
    var quality3ClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason + "_1";
    var qualityNeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations + "_1";
    DepartmentQualityDecisionForR2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        quality3ClosureReasonOnBpf
    );
}

function IntegrationServices_IsFCR_OnChange(executionContext) {
    var formContext = executionContext.getFormContext();
    var isFCR = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_isfcr);
    var fcrDecision = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_fcrdecisioncode
    );
    var fcrCommentSchemaNameOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;

    if (isFCR === null || isFCR === undefined) {
        HideUCIButtons(executionContext);
        return;
    }
    if (fcrDecision !== null && fcrDecision !== undefined) {
        ShowNextStage(executionContext);
        CommonGeneric.ShowField(
            formContext,
            fcrCommentSchemaNameOnBpf,
            true,
            false
        );
    } else {
        HideUCIButtons(executionContext);
        CommonGeneric.ShowField(
            formContext,
            fcrCommentSchemaNameOnBpf,
            false,
            false
        );
    }
}

function IntegrationServices_IsFCR_OnLoad(formContext) {
    var fcrDecision = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_fcrdecisioncode
    );
    var fcrCommentSchemaNameOnBpf =
        "header_process_" + caseFields.ldv_closurereasons;

    if (fcrDecision === null || fcrDecision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            fcrCommentSchemaNameOnBpf,
            false,
            false
        );
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            fcrCommentSchemaNameOnBpf,
            true,
            false
        );
    }
}

function IntegrationServices_OnLoad(formContext) {
    GeneralQualityStageDecisionR2_OnLoad(formContext);
    GeneralSocialMediaDecision_OnLoad(formContext);
    IntegrationServices_Integration_OnLoad(formContext);
    IntegrationServices_Integration2_OnLoad(formContext);
    GeneralSuperVisorDecisionNext_OnLoad(formContext);
    IntegrationServices_IsFCR_OnLoad(formContext);
    HideTransferOptionInQualityStageInFCR(formContext);
}

function NusukMonitoring__OnLoad(formContext) {
    NusukSubmitDecision_MentoringService_OnLoad(formContext);
    NusukMonitoring_CompanyDecision_OnLoad(formContext);
    NusukMonitoring_NusukResolveDecision_OnLoad(formContext);
}

//#endregion

// General Quality method
function QualityStageDecisionR1_OnChange(executionContext) {
    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason;
    DepartmentQualityDecision_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        qualityClosureReasonOnBpf
    );
}

function QualityStageDecisionR1_OnLoad(formContext) {
    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason;
    DepartmentQualityDecision_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        qualityClosureReasonOnBpf
    );
}

///R2 Quality
function GeneralQualityStageDecisionR2_OnChange(executionContext) {
    debugger;
    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason;
    var qualityNeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations;
    DepartmentDecisionForR2_OnChange(
        executionContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        qualityClosureReasonOnBpf
    );
}

function GeneralQualityStageDecisionR2_OnLoad(formContext) {
    var qualityClosureReasonOnBpf =
        "header_process_" + caseFields.ldv_closurereason;
    var qualityNeededInformationOnBpf =
        "header_process_" + caseFields.ldv_qualityofficerneededinformations;
    DepartmentQualityDecisionForR2_OnLoad(
        formContext,
        caseFields.ldv_qualitydecisioncode,
        qualityNeededInformationOnBpf,
        qualityClosureReasonOnBpf
    );
}

// General Customer Approval Stage

function GeneralCustomerApproval(executionContext) {
    let formContext = executionContext.getFormContext();
    let customerSatisfactionOnBPF =
        "header_process_" + caseFields.ldv_customersatisfycode;
    CommonGeneric.DisableField(formContext, customerSatisfactionOnBPF, true);
}

// General Social Media method
function GeneralSocialMediaDecision_OnChange(executionContext) {
    var socialMediaCommentOnBpf =
        "header_process_" + caseFields.ldv_socialMediaComment;
    SocialMediaDecision_OnChange(
        executionContext,
        caseFields.ldv_socialMediaDecisioncode,
        socialMediaCommentOnBpf
    );
}

function GeneralSocialMediaDecision_OnLoad(formContext) {
    var socialMediaCommentOnBpf =
        "header_process_" + caseFields.ldv_socialMediaComment;
    SocialMediaDecision_OnLoad(
        formContext,
        caseFields.ldv_socialMediaDecisioncode,
        socialMediaCommentOnBpf
    );
}

// General Supervisor Method
function GeneralSuperVisorDecision_OnChange(executionContext) {
    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment;
    SupervisorDecision_OnChange(
        executionContext,
        caseFields.ldv_supervisordecisioncode,
        supervisorCommentOnBpf
    );
}

function GeneralSupervisorDecision_OnLoad(formContext) {
    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment;
    SupervisorDecision_OnLoad(
        formContext,
        caseFields.ldv_supervisordecisioncode,
        supervisorCommentOnBpf
    );
}
//Next Supervisor method
function GeneralSuperVisorDecisionNext_OnChange(executionContext) {
    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment;
    SupervisorDecisionNext_OnChange(
        executionContext,
        caseFields.ldv_supervisordecisioncode,
        supervisorCommentOnBpf
    );
}

function GeneralSuperVisorDecisionNext_OnLoad(formContext) {
    debugger;
    var supervisorCommentOnBpf =
        "header_process_" + caseFields.ldv_supervisorcomment;
    var supervisorDecision =
        "header_process_" + caseFields.ldv_supervisordecisioncode;
    CommonGeneric.ShowAndReuiredField(
        formContext,
        supervisorDecision,
        true,
        true
    );
    SupervisorDecisionNext_OnLoad(
        formContext,
        caseFields.ldv_supervisordecisioncode,
        supervisorCommentOnBpf
    );
}
///////////////////////////////////////////////////////////////////////Common Methods////////////////////

// #region Common Functions For Decisions

//show next button in all cases
function DepartmentDecision_OnChange(
    executionContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf,
    supervisorSchemaNameOnBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();

    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    // Clear the value of the needMoreDetails field if the decision is not "needMoreDetails"
    if (decision !== caseFields.Enums.departmentDecision.needMoreDetails) {
        CommonGeneric.EmptyField(formContext, caseFields.ldv_needsmoredetails);
    }

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );

        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                false,
                false
            );
        }
    }

    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                false,
                false
            );
            var supervisorSchemaNameOnForm = GetLogicalFieldName(
                formContext,
                supervisorSchemaNameOnBpf
            );
            if (supervisorSchemaNameOnForm) {
                CommonGeneric.EmptyField(formContext, supervisorSchemaNameOnForm);
            }
        }
        var needInfoSchemaNameOnForm = GetLogicalFieldName(
            formContext,
            needInformationSchemaNameOnBpf
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
        }
        ShowNextStage(executionContext);
    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                true,
                true
            );
        }
        var closureReasonSchemaNameOnForm = GetLogicalFieldName(
            formContext,
            closureReasonSchemaNameOnBpf
        );
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        //HideUCIButtons(executionContext);
        ShowNextStage(executionContext);

        var needInformationSchemaNameOnForm = GetLogicalFieldName(
            formContext,
            needInformationSchemaNameOnBpf
        );
        if (needInformationSchemaNameOnForm) {
            formContext
                .getAttribute(needInformationSchemaNameOnForm)
                .addOnChange(function () {
                    // Set the value of the other field based on the changed value of needInformationSchemaNameOnBpf
                    var needInformationValue = CommonGeneric.GetFieldValue(
                        formContext,
                        needInformationSchemaNameOnForm
                    );

                    CommonGeneric.SetFieldValue(
                        formContext,
                        caseFields.ldv_needsmoredetails,
                        needInformationValue
                    );
                });
        }
    }
}

//SHOW next and previos button based on department decision for department2 stage
function Department2Decision_OnChange(
    executionContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf,
    supervisorSchemaNameOnBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();

    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    // Clear the value of the needMoreDetails field if the decision is not "needMoreDetails"
    if (decision !== caseFields.Enums.departmentDecision.needMoreDetails) {
        CommonGeneric.EmptyField(formContext, caseFields.ldv_needsmoredetails);
    }

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );

        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                false,
                false
            );
        }
    }

    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                false,
                false
            );
            var supervisorSchemaNameOnForm = GetLogicalFieldName(
                formContext,
                supervisorSchemaNameOnBpf
            );
            if (supervisorSchemaNameOnForm) {
                CommonGeneric.EmptyField(formContext, supervisorSchemaNameOnForm);
            }
        }
        var needInfoSchemaNameOnForm = GetLogicalFieldName(
            formContext,
            needInformationSchemaNameOnBpf
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
        }
        ShowNextStage(executionContext);
    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                true,
                true
            );
        }
        var closureReasonSchemaNameOnForm = GetLogicalFieldName(
            formContext,
            closureReasonSchemaNameOnBpf
        );
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }

        var needInformationSchemaNameOnForm = GetLogicalFieldName(
            formContext,
            needInformationSchemaNameOnBpf
        );
        if (needInformationSchemaNameOnForm) {
            formContext
                .getAttribute(needInformationSchemaNameOnForm)
                .addOnChange(function () {
                    // Set the value of the other field based on the changed value of needInformationSchemaNameOnBpf
                    var needInformationValue = CommonGeneric.GetFieldValue(
                        formContext,
                        needInformationSchemaNameOnForm
                    );

                    CommonGeneric.SetFieldValue(
                        formContext,
                        caseFields.ldv_needsmoredetails,
                        needInformationValue
                    );
                });
        }
        ShowPreviousStage(executionContext);
    }
}
function DepartmentDecision_OnLoad(
    formContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf,
    supervisorSchemaNameOnBpf
) {
    debugger;
    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );

        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                false,
                false
            );
        }
    }

    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                false,
                false
            );
        }
    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (supervisorSchemaNameOnBpf) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                supervisorSchemaNameOnBpf,
                true,
                true
            );
        }
    }
}
//Concerned Department Decision >>> Show Back if Need more Details and next if Resolve
function ConcernedDepartmentDecisionR2_OnChange(
    executionContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf
) {
    var formContext = executionContext.getFormContext();
    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    var needInfoSchemaNameOnForm = GetLogicalFieldName(
        formContext,
        needInformationSchemaNameOnBpf
    );
    var closureReasonSchemaNameOnForm = GetLogicalFieldName(
        formContext,
        closureReasonSchemaNameOnBpf
    );

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                false,
                false
            );
        }
    }

    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                false,
                false
            );
        }
        ShowNextStage(executionContext);
    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                true,
                true
            );
        }

        ShowPreviousStage(executionContext);
        //HideUCIButtons();
    }
}
function ConcernedDepartmentDecisionR2_OnLoad(
    formContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf
) {
    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    }

    if (decision === caseFields.Enums.departmentDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
    } else if (decision === caseFields.Enums.departmentDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    }
}
//in all choises >> show next button to go to the next stage
function DepartmentQualityDecision_OnChange(
    executionContext,
    decisionSchemaName,
    closureReasonSchemaNameOnBpf
) {
    var formContext = executionContext.getFormContext();

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
        return;
    }

    // Show and set required status for closure reason field based on decision
    if (
        decision === caseFields.Enums.qualityDecisR1.CloseTheTicket ||
        decision === caseFields.Enums.qualityDecisR1.NotResolved
    ) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        ShowNextStage(executionContext);
    }
}
function DepartmentQualityDecision_OnLoad(
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

function SupervisorDecisionNext_OnChange(
    executionContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, false, false);
        return;
    } else {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, true, true);
        ShowNextStage(executionContext);
    }
}

function SupervisorDecisionNext_OnLoad(
    formContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, false, false);
    } else {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, true, true);
    }
}

//Social Media

function SocialMediaDecision_OnChange(
    executionContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
        return;
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            true
        );
    }

    ShowNextStage(executionContext);
}

function SocialMediaDecision_OnLoad(
    formContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            true
        );
    }
}

///// inquiry social media decision

function inquirysocialmediadecisioncode_OnChange(
    executionContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
        return;
    } else if (
        decision === caseFields.Enums.socialmediadecision2options.Resolved
    ) {
        CommonGeneric.SetReqLevel(formContext, caseFields.ldv_maincategoryid, true);

        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            true,
            true
        );
    } else if (
        decision === caseFields.Enums.socialmediadecision2options.Transfer
    ) {
        //SetReqForMainCategoryForInquiry(formContext);

        CommonGeneric.SetReqLevel(
            formContext,
            caseFields.ldv_maincategoryid,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            true
        );
    }

    ShowNextStage(executionContext);
}

function inquirysocialmediadecisioncode_OnLoad(
    formContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
    } else if (
        decision === caseFields.Enums.socialmediadecision2options.Resolved
    ) {
        CommonGeneric.SetReqLevel(formContext, caseFields.ldv_maincategoryid, true);

        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            true,
            true
        );
    } else if (
        decision === caseFields.Enums.socialmediadecision2options.Transfer
    ) {
        CommonGeneric.SetReqLevel(
            formContext,
            caseFields.ldv_maincategoryid,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            true
        );
    }
}

//if Transfer >> hide next and back buttons and the agent assign it manually
function QualityOfficerDecision2_OnChange(
    executionContext,
    decisionSchemaName,
    closureReasonSchemaNameOnBpf
) {
    var formContext = executionContext.getFormContext();
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
    }

    if (decision === caseFields.Enums.qualityDecision2.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );

        ShowNextStage(executionContext);
    } else if (decision === caseFields.Enums.qualityDecision2.transferTheTicket) {
        //Trnasfer

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        var closureReasonSchemaNameOnForm = GetLogicalFieldName(
            formContext,
            closureReasonSchemaNameOnBpf
        );
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        HideUCIButtons(executionContext);
    }
}
function QualityOfficerDecision2_OnLoad(
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
    }

    if (decision === caseFields.Enums.qualityDecision2.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
    } else if (decision === caseFields.Enums.qualityDecision2.transferTheTicket) {
        //Trnasfer

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    }
}
//
//in all choises >> show next button to go to the next stage [if transfer or resolve] go next  //inquiry
function QualityDecision_R2_OnChange(
    executionContext,
    decisionSchemaName,
    closureReasonSchemaNameOnBpf,
    transferReasonSchemaNameonBpf
) {
    var formContext = executionContext.getFormContext();

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
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
        return;
    }

    // Show and set required status for Closure Reason field based on decision [Resolve]
    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
        ShowNextStage(executionContext);
    } //Transfer Reason
    else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        ShowNextStage(executionContext);
    }
}
function QualityDecision_R2_OnLoad(
    formContext,
    decisionSchemaName,
    closureReasonSchemaNameOnBpf,
    transferReasonSchemaNameonBpf
) {
    if (!decisionSchemaName || !closureReasonSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    }

    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) {
        //Resolved

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
    } else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) {
        //Transfer

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    }
}
///////////////////////////FOR R2////////if need more information >> hide next and Show back button to go to the previous stage
function DepartmentQualityDecisionForR2_OnChange(
    executionContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf
) {
    var formContext = executionContext.getFormContext();
    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    var needInfoSchemaNameOnForm = GetLogicalFieldName(
        formContext,
        needInformationSchemaNameOnBpf
    );
    var closureReasonSchemaNameOnForm = GetLogicalFieldName(
        formContext,
        closureReasonSchemaNameOnBpf
    );

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                false,
                false
            );
        }
    }

    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                false,
                false
            );
        }

        // OS => Transfer ,
        ShowNextStage(executionContext);
    } else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                true,
                true
            );
        }

        ShowPreviousStage(executionContext);
        //HideUCIButtons();
    }
}
function DepartmentDecisionForR2_OnChange(
    executionContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf
) {
    var formContext = executionContext.getFormContext();
    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    var needInfoSchemaNameOnForm = GetLogicalFieldName(
        formContext,
        needInformationSchemaNameOnBpf
    );
    var closureReasonSchemaNameOnForm = GetLogicalFieldName(
        formContext,
        closureReasonSchemaNameOnBpf
    );

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                false,
                false
            );
        }
    }

    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, needInfoSchemaNameOnForm);
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                false,
                false
            );
        }

        // OS => Transfer , Close
        ShowNextStage(executionContext);
    } else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        if (closureReasonSchemaNameOnForm) {
            CommonGeneric.EmptyField(formContext, closureReasonSchemaNameOnForm);
        }
        if (needInfoSchemaNameOnForm) {
            CommonGeneric.ShowAndReuiredField(
                formContext,
                needInfoSchemaNameOnForm,
                true,
                true
            );
        }

        ShowNextStage(executionContext);
        //HideUCIButtons();
    }
}
function DepartmentQualityDecisionForR2_OnLoad(
    formContext,
    decisionSchemaName,
    needInformationSchemaNameOnBpf,
    closureReasonSchemaNameOnBpf
) {
    if (
        !decisionSchemaName ||
        !needInformationSchemaNameOnBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    }

    if (decision === caseFields.Enums.qualityDecision.closeTheTicket) {
        //Close

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            false,
            false
        );
    } else if (decision === caseFields.Enums.qualityDecision.needMoreDetails) {
        //more Information

        //In Stage
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needInformationSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
    }
}

//// supervicor >> show only next button
function SupervisorDecision_OnChange(
    executionContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, false, false);
        return;
    } else {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, true, true);
        ShowNextStage(executionContext);
    }
}

function SupervisorDecision_OnLoad(
    formContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, false, false);
    } else {
        CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, true, true);
    }
}

// supervisor >> show Next button [Close Ticket]
function InquirySupervisorDecision_R2_OnChange(
    executionContext,
    decisionSchemaName,
    commentSchemaNameonBpf
) {
    var formContext = executionContext.getFormContext();

    if (!decisionSchemaName || !commentSchemaNameonBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }

    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameonBpf,
            false,
            false
        );
        return;
    }

    if (decision === caseFields.Enums.supervisorDecision2.CloseTicket) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameonBpf,
            true,
            true
        );
        ShowNextStage(executionContext);
    }
}
function InquirySupervisorDecision_R2_OnLoad(
    formContext,
    decisionSchemaName,
    commentSchemaNameonBpf
) {
    if (!decisionSchemaName || !commentSchemaNameonBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);

    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameonBpf,
            false,
            false
        );
    }
    //Close Ticket
    if (decision === caseFields.Enums.supervisorDecision2.CloseTicket) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameonBpf,
            true,
            true
        );
    }
}
//// customer approval (onChange)
function CustomerApprovalDecision_OnChange(executionContext, flagSchemaName) {
    var formContext = executionContext.getFormContext();
    var isPassedCustomerApproval = CommonGeneric.GetFieldValue(
        formContext,
        flagSchemaName
    );
    if (isPassedCustomerApproval === true) {
        // CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, false, false);
        ShowNextStage(executionContext);
    } else {
        // CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, true, true);
        return;
    }
}
// customer approval (onLoad)
function CustomerApprovalDecision_OnLoad(executionContext, flagSchemaName) {
    // var formContext = executionContext.getFormContext();
    // var isPassedCustomerApproval = CommonGeneric.GetFieldValue(formContext, flagSchemaName);
    // if (isPassedCustomerApproval === true) {
    //     // CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, false, false);
    //     ShowNextStage(executionContext);
    // } else {
    //     // CommonGeneric.ShowField(formContext, commentSchemaNameOnBpf, true, true);
    //     return;
    // }
}

/////// CX decision (onload | onChange)

function CxDecision_OnLoad(
    formContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            true
        );
    }
}

function CxDecision_OnChange(
    executionContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
        return;
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            true,
            true
        );
    }

    ShowNextStage(executionContext);
}

///////////////nusuk team decision

function NusukSubmitDecision_OnLoad(
    formContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            true,
            true
        );
    }
}

function NusukSubmitDecision_OnChange(
    executionContext,
    decisionSchemaName,
    commentSchemaNameOnBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !commentSchemaNameOnBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            false,
            false
        );
        return;
    } else {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            commentSchemaNameOnBpf,
            true,
            true
        );
    }

    ShowNextStage(executionContext);
}

// nusuk resolve decision

function NusukResolveDecision_OnLoad(
    formContext,
    decisionSchemaName,
    closureReasonSchemaNameOnBpf,
    transferReasonSchemaNameonBpf
) {
    debugger;
    if (
        !decisionSchemaName ||
        !transferReasonSchemaNameonBpf ||
        !closureReasonSchemaNameOnBpf
    ) {
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
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
        return;
    }

    // Show and set required status for Closure Reason field based on decision [Resolve]
    if (decision === caseFields.Enums.nusukTeamDecision.Resolved) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
        // ShowNextStage(executionContext);
    } //Transfer Reason
    else if (decision === caseFields.Enums.nusukTeamDecision.SendBack) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        // ShowNextStage(executionContext);
    }
}

function NusukResolveDecision_OnChange(
    executionContext,
    decisionSchemaName,
    closureReasonSchemaNameOnBpf,
    transferReasonSchemaNameonBpf
) {
    debugger;
    var formContext = executionContext.getFormContext();

    if (
        !decisionSchemaName ||
        !closureReasonSchemaNameOnBpf ||
        !transferReasonSchemaNameonBpf
    ) {
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
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
        return;
    }

    // Show and set required status for Closure Reason field based on decision [Resolve]
    else if (decision === caseFields.Enums.nusukTeamDecision.Resolved) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            false,
            false
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            true,
            true
        );
        ShowNextStage(executionContext);
    } //Transfer Reason
    else if (decision === caseFields.Enums.nusukTeamDecision.SendBack) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            transferReasonSchemaNameonBpf,
            true,
            true
        );
        CommonGeneric.ShowAndReuiredField(
            formContext,
            closureReasonSchemaNameOnBpf,
            false,
            false
        );
        ShowPreviousStage(executionContext);
    }
}
function concernedCompanyDecision__OnLoad(
    formContext,
    decisionSchemaName,
    needMoreDetailsShemaNameonBpf
    //ClosureReasonOnBpfOnBpf
) {
    debugger;
    if (!decisionSchemaName || !needMoreDetailsShemaNameonBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    if (decision === null || decision === undefined) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needMoreDetailsShemaNameonBpf,
            false,
            false
        );
        // CommonGeneric.ShowAndReuiredField(
        //   formContext,
        //   ClosureReasonOnBpfOnBpf,
        //   false,
        //   false
        // );
        return;
    }
    // Show and set required status for Closure Reason field based on decision [Resolve]
    if (decision === caseFields.Enums.companyDecision.Resolved) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needMoreDetailsShemaNameonBpf,
            false,
            false
        );
        // CommonGeneric.ShowAndReuiredField(
        //   formContext,
        //   ClosureReasonOnBpfOnBpf,
        //   true,
        //   true
        // );
    } //Transfer Reason
    else if (decision === caseFields.Enums.companyDecision.NeedMoreInformation) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needMoreDetailsShemaNameonBpf,
            true,
            true
        );
    }
}

function concernedCompanyDecision_OnChange(
    executionContext,
    decisionSchemaName,
    needMoreDetailsShemaNameonBpf
) {
    var formContext = executionContext.getFormContext();
    if (!decisionSchemaName || !needMoreDetailsShemaNameonBpf) {
        console.error("One or more parameters are null or undefined.");
        return;
    }
    var decision = CommonGeneric.GetFieldValue(formContext, decisionSchemaName);
    CommonGeneric.ShowAndReuiredField(
        formContext,
        needMoreDetailsShemaNameonBpf,
        false,
        false
    );
    //return;
    if (decision === caseFields.Enums.companyDecision.Resolved) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needMoreDetailsShemaNameonBpf,
            true,
            true
        );
        ShowNextStage(executionContext);
    } else if (
        decision === caseFields.Enums.companyDecision.NeedMoreInformation
    ) {
        CommonGeneric.ShowAndReuiredField(
            formContext,
            needMoreDetailsShemaNameonBpf,
            true,
            true
        );
        ShowNextStage(executionContext);
    }
}

//
// #endregion

// #endregion

// #region Helpers
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

function GetLookUpRecordWithExpandedValues(
    formContext,
    lookupField,
    expandField
) {
    var lookupRecord = CommonGeneric.GetLookUpRecord(formContext, lookupField);
    if (lookupRecord !== null) {
        var entityId = lookupRecord.id;
        var lookupLogicalName = lookupRecord.entityType;

        entityId = entityId.replace("{", "").replace("}", "");

        // Construct URL with expand parameter and entityId
        var entityName = lookupLogicalName.toLowerCase();
        if (entityName.endsWith("y")) {
            entityName = entityName.slice(0, -1) + "ies";
        } else {
            entityName += "s";
        }
        var url =
            Xrm.Utility.getGlobalContext().getClientUrl() +
            "/api/data/v9.0/" +
            entityName +
            "(" +
            entityId +
            ")";
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
        req.setRequestHeader("Prefer", 'odata.include-annotations="*"');
        req.send(null);

        if (req.status === 200) {
            var result = JSON.parse(req.responseText);
            return result;
        } else {
            console.log(
                "Error occurred while retrieving expanded lookup value: " +
                req.statusText
            );
            return null;
        }
    }
    return null;
}

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
}

function IsFieldVisible(formContext, fieldName) {
    var control = formContext.getControl(fieldName);
    if (control) {
        return control.getVisible();
    } else {
        console.error("Control for field " + fieldName + " not found.");
        return false;
    }
}

function ChangeFieldLabel(formContext, fieldName, newLabel) {
    var control = formContext.getControl(fieldName);

    if (control) {
        control.setLabel(newLabel);
    } else {
        console.error("Field control not found:", fieldName);
    }
}

function RemoveOptionSetValues(formContext, optionSetName, values) {
    var optionSet = formContext.ui.controls.get(optionSetName);

    if (optionSet != null) {
        for (var i = 0; i < values.length; i++) {
            optionSet.removeOption(values[i]);
        }
    }
}

function AddOptionSetValues(formContext, optionSetName, values) {
    debugger;
    var optionSet = formContext.getControl(optionSetName);
    var optionSetField = formContext.getAttribute(optionSetName);

    if (optionSet != null) {
        for (var i = 0; i < values.length; i++) {
            var optionToAdd = optionSetField
                .getOptions()
                .find((option) => option.value === values[i]);
            if (optionToAdd) optionSet.addOption(optionToAdd);
        }
    }
}

//function AddOptionSetValues(formContext, optionSetName, values, isClearFirst) {
//    /// <summary>
//    ///     Add the values given to the option-set. The values should be in the format
//    ///     '[{value: value, text: text}].
//    /// </summary>
//    var optionSet = formContext.ui.controls.get(optionSetName);

//    if (optionSet != null) {
//        if (isClearFirst) {
//            ClearOptionSetValues(optionSetName);
//        }

//        for (var i = 0; i < values.length; i++) {
//            optionSet.addOption(values[i]);
//        }
//    }
//}

function ClearOptionSetValues(formContext, optionSetName) {
    var optionSet = formContext.ui.controls.get(optionSetName);

    if (optionSet != null) {
        optionSet.clearOptions();
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
//ShowPreviusButtonInUCI = function (isVisible = true) {
//    try {
//        setTimeout(function () {
//            $(window.parent.document)
//                .find("[data-id = 'MscrmControls.Containers.ProcessStageControl-previousButtonContainer']")
//                .find('button')
//                .each(function (index) {
//                    if (this.ariaLabel == 'Back') {
//                        isVisible ? $(this).show() : $(this).hide();
//                        return;
//                    }
//                });
//        }, 100);

//        var intervalForBackButton = setInterval(function () {
//            var processStageFooter = parent.document.getElementById(
//                'MscrmControls.Containers.ProcessStageControl-previousButtonContainer'
//            );
//            if (processStageFooter != null) {
//                var previousButtonElement = parent.document.getElementById(
//                    'MscrmControls.Containers.ProcessStageControl-previousButtonContainer'
//                );
//                if (previousButtonElement != null) {
//                    previousButtonElement.style.visibility = isVisible ? 'visible' : 'hidden';
//                    previousButtonElement.style.display = isVisible ? '' : 'none';
//                    clearInterval(intervalForBackButton);
//                }
//            }
//        }, 150);
//    } catch (e) {
//        console.log('ShowPreviusButtonInUCI' + e.errorMessage);
//    }
//};

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
                                    "<label id='MOHUAddLabelPreviousStage' style='padding-right: 0.5em;cursor: inherit;'>المرحله السابقه</label>";
                                previousButtonElement.style.marginLeft = "50px";
                            }
                        }

                        var newLookButton = Array.from(
                            parent.document.querySelectorAll("button")
                        ).find(
                            (button) =>
                                button.getAttribute("aria-label") === "New look" ||
                                button.getAttribute("aria-label") === "المنظور الجديد"
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

/////
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

// #endregion

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
            Xrm.Utility.alertDialog("لا يمكنك استخدام تنشيط المرحله");
            return;
        } else {
            Xrm.Utility.alertDialog("You cannot use the Set Active button.");
            return;
        }
    }
    //dont allow to go back using the Previous button

    //    if (eventArgs.getDirection() === 'Previous') {
    //        eventArgs.preventDefault();
    //        if (userLanguage === 1025) {
    //          Xrm.Utility.alertDialog('ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â­ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â±ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã¢â‚¬ ÃƒÂ¢Ã¢â€šÂ¬Ã¢â€žÂ¢ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â© ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â§ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€¦Ã‚Â¾ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€šÃ‚Â¦ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â±ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â­ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€¦Ã‚Â¾ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â© ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â§ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€¦Ã‚Â¾ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â®ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€¦Ã‚Â¾ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚ÂÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã¢â‚¬Â¦ ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â© ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚ÂºÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã¢â‚¬Â¦ ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â± ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€šÃ‚Â¦ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â³ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€šÃ‚Â¦ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã¢â‚¬Â¹ÃƒÂ¢Ã¢â€šÂ¬ ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â­ ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â¨ÃƒÆ’Ã†â€™ÃƒÂ¢Ã¢â‚¬Å¾Ã‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â‚¬Å¡Ã‚Â¬Ãƒâ€šÃ‚Â¡ÃƒÆ’Ã†â€™Ãƒâ€¹Ã…â€œÃƒÆ’Ã¢â‚¬Å¡Ãƒâ€šÃ‚Â§.');
    //        } else {
    //            Xrm.Utility.alertDialog('Back stage movement is not allowed.');
    //        }
    //    }
    if (eventArgs.getDirection() === "Previous") {
        var previousButtonLabel = parent.document.getElementById(
            "MOHUAddLabelPreviousStage"
        );
        if (previousButtonLabel == null) {
            eventArgs.preventDefault();
            if (userLanguage === 1025) {
                Xrm.Utility.alertDialog("لا يمكنك الرجول الى المرحله السابقه");
            } else {
                Xrm.Utility.alertDialog("Back stage movement is not allowed.");
            }
        }
    }
}

//#region On Load Extensions
var globalCompanyLookupControl;
var globalFieldServiceOfficeControl;
var globalBeneficiaryTypeControl;
var serviceControl;
var globalFormContext;

function onLoadExtension(formContext) {
    debugger;
    globalFormContext = formContext;

    globalCompanyLookupControl = getControl(formContext, caseFields.ldv_company);
    globalFieldServiceOfficeControl = getControl(
        formContext,
        caseFields.ldv_fieldserviceofficeid
    );
    globalBeneficiaryTypeControl = getControl(
        formContext,
        caseFields.ldv_beneficiarytypecode
    );
    serviceControl = getControl(formContext, "ldv_serviceid");

    addPreSearchOnLoad();
    addOnChangeEvents();
    HandleFormUIOnLoad();
}

//#region Events
function addOnChangeEvents() {
    // addOnChangeEvent(serviceControl, handleServiceChange);
    // addOnChangeEvent(globalBeneficiaryTypeControl, handleBeneificiaryTypeChange);
    addOnChangeEvent(globalCompanyLookupControl, handleCompanyChange);
}

function HandleFormUIOnLoad() {
    handleServiceChange(true);
    handleBeneificiaryTypeChange(true);
    handleCompanyChange(true);
}

function handleBeneificiaryTypeChange(isManuallyTriggered) {
    debugger;
    var serviceId = getIdWithoutCurlyBraces(getLookupFieldValue(serviceControl));
    var beneficiaryTypeValue = getControlValue(globalBeneficiaryTypeControl);

    if (!IsHajj(serviceId)) return;

    if (isManuallyTriggered !== true) {
        clearLookupValue(globalCompanyLookupControl);
    }

    if (beneficiaryTypeValue) {
        setControlToVisibileAndRequired(globalCompanyLookupControl);
    } else {
        setControlToHiddenAndOptional(globalCompanyLookupControl);
    }
}

function handleServiceChange(isManuallyTriggered) {
    var serviceId = getIdWithoutCurlyBraces(getLookupFieldValue(serviceControl));
    var beniferciaryTypeValue = getControlValue(globalBeneficiaryTypeControl);

    if (isManuallyTriggered !== true) {
        clearLookupValue(globalCompanyLookupControl);
    }

    if (!serviceId || (IsHajj(serviceId) && !beniferciaryTypeValue)) {
        setControlToHiddenAndOptional(globalCompanyLookupControl);
    } else if (
        IsOmrah(serviceId) ||
        IsHajj(serviceId) ||
        IsNusukCare(serviceId)
    ) {
        setControlToVisibileAndRequired(globalCompanyLookupControl);
    }
}

function handleCompanyChange(isManuallyTriggered) {
    debugger;
    var companyId = getIdWithoutCurlyBraces(
        getLookupFieldValue(globalCompanyLookupControl)
    );

    if (isManuallyTriggered !== true) {
        clearLookupValue(globalFieldServiceOfficeControl);
    }

    if (companyId) {
        setControlToVisibileAndOptional(globalFieldServiceOfficeControl);
    } else {
        setControlToHiddenAndOptional(globalFieldServiceOfficeControl);
    }
}
//#endregion

//#region PreSearchFilters
function addPreSearchOnLoad() {
    addPreSearch(
        globalCompanyLookupControl,
        globalCompanyPresearchPresearchFilter
    );
    addPreSearch(globalFieldServiceOfficeControl, fieldServiceOfficePresearch);
}

function globalCompanyPresearchPresearchFilter() {
    debugger;
    let control = globalFieldServiceOfficeControl,
        serviceId = getIdWithoutCurlyBraces(getLookupFieldValue(serviceControl)),
        filter = null;

    if (IsOmrah(serviceId)) {
        filter = getOmrahFilter();
    } else if (IsHajj(serviceId)) {
        filter = getHajjFilter();
    }

    if (filter) addControlCustomFilter(control, filter, "account");
}

function getOmrahFilter() {
    return `
<filter type="and">
<condition attribute="ldv_servicetypecode" operator="eq" value="2" />
</filter>
`;
}

function getHajjFilter() {
    var beneficiaryTypeValue = getControlValue(globalBeneficiaryTypeControl);
    return `
<filter type="and">
<condition attribute="ldv_beneficiarytypecode" operator="eq" value="${beneficiaryTypeValue}" />
</filter>`;
}

function fieldServiceOfficePresearch() {
    debugger;
    let control = globalCompanyLookupControl,
        companyId = getIdWithoutCurlyBraces(
            getLookupFieldValue(globalCompanyLookupControl)
        ),
        filter = `
<filter type="and">
<condition attribute="ldv_companyid" operator="eq" value="${companyId}" />
</filter>`;

    if (filter)
        addControlCustomFilter(control, filter, "ldv_fieldserviceofficeid");
}

//#endregion

//#region Helpers

function IsOmrah(serviceId) {
    return (
        serviceId ===
        ServiceType.TechnicalComplainCompanyService.serviceDefinitionId.toLowerCase() ||
        serviceId ===
        ServiceType.TechnicalComplainNonMomentaryUmrah.serviceDefinitionId.toLowerCase() ||
        serviceId ===
        ServiceType.TechnicalComplainMomentaryUmrahForCompanies.serviceDefinitionId.toLowerCase()
    );
}

function IsHajj(serviceId) {
    return (
        serviceId ===
        ServiceType.TechnicalComplainMomentaryHajj.serviceDefinitionId.toLowerCase() ||
        serviceId ===
        ServiceType.TechnicalComplainNonMomentaryHajj.serviceDefinitionId.toLowerCase()
    );
}

function IsNusukCare(serviceId) {
    return (
        serviceId ===
        ServiceType.MentoringServiceNUSUKEnaya.serviceDefinitionId.toLowerCase() ||
        serviceId ===
        ServiceType.MissingPersonsServiceNUSUKEnaya.serviceDefinitionId.toLowerCase()
    );
}

//#endregion

//#endregion

function testRenameStage(formContext) {
    //new v2
    // Retrieve the lookup record
    var service = CommonGeneric.GetLookUpRecord(
        formContext,
        caseFields.ldv_serviceid
    );
    if (service === null || service === undefined) {
        return;
    } else {
        var serviceId = service.id.replace("{", "").replace("}", "").toLowerCase();

        // Check for specific service ID condition
        if (
            serviceId ===
            ServiceType.FinancialComplainInternalPilgrimspostHajj.serviceDefinitionId.toLowerCase()
        ) {
            var descriptionField = formContext
                .getAttribute("ldv_description")
                .getValue();
            if (descriptionField === "test") {
                var bpfControl = formContext.data.process;

                // Ensure the BPF is loaded
                if (bpfControl) {
                    var activeStage = bpfControl.getActiveStage();

                    if (activeStage) {
                        var activeStageId = activeStage.getId();
                        var companiesStageId =
                            BPFs.DepartmentComplain.stages.companiesAdminstration.id.toLowerCase();

                        if (activeStageId === companiesStageId) {
                            // Get the element by the known data ID and update the stage label
                            var stageElementId =
                                "MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageName_a37f5442-2123-4ef6-a63c-f4875085e8ca";
                            //var stageElementId = "MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageName_" + activeStageId.replace(/-/g, '_');
                            var stageElement = parent.document.getElementById(stageElementId);

                            if (stageElement) {
                                stageElement.innerText = "Test";
                            }
                        }
                    }
                }
            }
        }
    }
}

function HideQualityAndResolvedStages(formContext) {
    debugger;

    var processControl = formContext.data.process;

    if (processControl) {
        var currentStage = formContext.data.process.getActiveStage();

        if (!currentStage) {
            console.error("current stage is null or undefined.");
            return;
        }

        var currentStageId = currentStage.getId().toLowerCase();
        var supervisorStageId =
            BPFs.TechnicalComplainCompanyService.stages.Supervisor.id.toLowerCase();
        var supervisor2StageId =
            BPFs.TechnicalComplainCompanyService.stages.Supervisor2.id.toLowerCase();
        var supervisor3StageId =
            BPFs.TechnicalComplainCompanyService.stages.Supervisor3.id.toLowerCase();

        if (
            currentStageId === supervisorStageId ||
            currentStageId === supervisor2StageId ||
            currentStageId === supervisor3StageId
        ) {
            //var stageElementId = 'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton_0cad470e-d631-407e-8a5d-dba3cb633630';
            //var stageElement = parent.document.getElementById(stageElementId);

            var qualityStageElementId =
                "MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton_f43fe841-1303-4159-851f-a353ce7964b7";
            var qualityStageElement = parent.document.getElementById(
                qualityStageElementId
            );

            var resolvedStageElementId =
                "MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton_48050991-24dc-4d21-a459-d5679351219c";
            var resolvedStageElement = parent.document.getElementById(
                resolvedStageElementId
            );

            var nextElementId =
                "MscrmControls.Containers.ProcessBreadCrumb-headerNavigationButtoncontainertrue";
            var nextElement = parent.document.getElementById(nextElementId);

            //if (stageElement) {
            //    stageElement.style.display = 'none';
            //}
            if (qualityStageElement) {
                qualityStageElement.style.display = "none";
            }
            if (resolvedStageElement) {
                resolvedStageElement.style.display = "none";
            }
            if (nextElement) {
                nextElement.style.display = "none";
            }
        }
    }
}

function OnChange_ParentRequestType(formContext) {
    var fieldValue = CommonGeneric.GetFieldValue(
        formContext,
        caseFields.ldv_requesttypeid
    );

    if (fieldValue) {
        CommonGeneric.ShowField(formContext, caseFields.ldv_serviceid, true);
        const selectedEntity = CommonGeneric.GetLookUpRecord(
            formContext,
            caseFields.ldv_requesttypeid
        );
        if (selectedEntity != null) {
            const parentId = selectedEntity.id
                .replace("{", "")
                .replace("}", "")
                .toLowerCase();
            //GetChildService(formContext, parentId);
        }
    } else {
        CommonGeneric.ShowField(formContext, caseFields.ldv_serviceid, false);
    }
}

function GetChildService(formContext, parentId) {
    Xrm.WebApi.retrieveMultipleRecords(
        "ldv_service",
        `?$select=${caseFields.ldv_serviceid},${caseFields.ldv_name}&$filter=_ldv_serviceparentid_value eq ${parentId}`
    ).then(
        function success(results) {
            console.log(results);
            if (results.entities.length === 1) {
                console.log("services count " + results.entities.length);
                for (var i = 0; i < 1; i++) {
                    var result = results.entities[i];
                    // Columns
                    var childServiceId = result["ldv_serviceid"]; // Guid
                    var childRecordName = result["ldv_name"]; // Text

                    CommonGeneric.SetLookupRecord(
                        formContext,
                        caseFields.ldv_serviceid,
                        childServiceId,
                        "ldv_service",
                        childRecordName
                    );
                    formContext.getAttribute(caseFields.ldv_serviceid).fireOnChange();

                    CommonGeneric.DisableField(
                        formContext,
                        caseFields.ldv_serviceid,
                        true
                    );
                    // ShowAndHideBeneficiaryType_OnLoad(formContext);

                    //  OnChange_RequestType(formContext);
                }
            } else {
                console.log("services count " + results.entities.length);

                CommonGeneric.DisableField(
                    formContext,
                    caseFields.ldv_serviceid,
                    false
                );
            }
            // FilterOriginsDependOnRequestType(formContext);
        },
        function (error) {
            console.log(error.message);
        }
    );
}

function TechnicalComplainCompanyService_OnChange_Case(
    executionContext,
    currentStageId,
    formContext
) {
    var qualityStageId =
        BPFs.TechnicalComplainCompanyService.stages.quality.id.toLowerCase();
    var supervisorStageId =
        BPFs.TechnicalComplainCompanyService.stages.supervisor.id.toLowerCase();
    var companyStageId =
        BPFs.TechnicalComplainCompanyService.stages.company.id.toLowerCase();
    var company2StageId =
        BPFs.TechnicalComplainCompanyService.stages.company2.id.toLowerCase();
    var socialMediaStageId =
        BPFs.TechnicalComplainCompanyService.stages.socialMedia.id.toLowerCase();
    //var customerApprovalStageId = BPFs.TechnicalComplainCompanyService.stages.customerApproval.id.toLowerCase();

    if (currentStageId === socialMediaStageId) {
        GeneralSocialMediaDecision_OnChange(executionContext);
        formContext
            .getAttribute(caseFields.ldv_socialMediaDecisioncode)
            .addOnChange(function () {
                GeneralSocialMediaDecision_OnChange(executionContext);
            });
    }

    //if (currentStageId === customerApprovalStageId) {
    //    TechnicalComplainMomentaryUmrah_CustomerApprovalStage_OnChange(executionContext);
    //    formContext.getAttribute(caseFields.ldv_Ispassedcustomerapproval).addOnChange(function () {
    //        TechnicalComplainMomentaryUmrah_CustomerApprovalStage_OnChange(executionContext);
    //    });
    //}

    if (currentStageId === companyStageId) {
        TechnicalComplainMomentaryUmrah_CompanyDecision_OnChange(executionContext);
        formContext
            .getAttribute(caseFields.ldv_departmentdecisioncode)
            .addOnChange(function () {
                TechnicalComplainMomentaryUmrah_CompanyDecision_OnChange(
                    executionContext
                );
            });
    }
    if (currentStageId === company2StageId) {
        CompanyDecisionSupervisorSendBack_OnChange(executionContext);
        formContext
            .getAttribute(caseFields.ldv_departmentdecisioncode)
            .addOnChange(function () {
                CompanyDecisionSupervisorSendBack_OnChange(executionContext);
            });
    }

    if (currentStageId === qualityStageId) {
        GeneralQualityStageDecisionR2_OnChange(executionContext);
        formContext
            .getAttribute(caseFields.ldv_qualitydecisioncode)
            .addOnChange(function () {
                GeneralQualityStageDecisionR2_OnChange(executionContext);
            });
    }

    if (currentStageId === supervisorStageId) {
        GeneralSuperVisorDecisionNext_OnChange(executionContext);
        formContext
            .getAttribute(caseFields.ldv_supervisordecisioncode)
            .addOnChange(function () {
                GeneralSuperVisorDecisionNext_OnChange(executionContext);
            });
    }
}

function HideTransferOptionInQualityStageInFCR(formContext) {
    var service = formContext.getAttribute(caseFields.ldv_serviceid).getValue();
    if (!service) {
        console.error("Service or current stage is null or undefined.");
        return;
    }
    var serviceId = service[0].id.replace("{", "").replace("}", "").toLowerCase();
    if (!serviceId) {
        console.error("Service ID is null or undefined.");
        return;
    }
    var removedValues = [caseFields.Enums.qualityDecision2.transferTheTicket];
    var isFCR = CommonGeneric.GetFieldValue(formContext, caseFields.ldv_isfcr);

    if (
        serviceId ===
        ServiceType.TechnologicalComplainSD.serviceDefinitionId.toLowerCase() &&
        isFCR === true
    ) {
        RemoveOptionSetValues(
            formContext,
            caseFields.ldv_qualitydecisioncode,
            removedValues
        );
    }
}
