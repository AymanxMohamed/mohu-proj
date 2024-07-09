var BPFsIntegrationStageConfig = {

    stages: {

        ServiceDesk: {
            name: "Service Desk",
            id: "0A585524-E3CF-EE11-9078-6045BD895C76"
        },
        Kadana: {
            name: "Kadana",
            id: "49E3D0B7-B4D1-EE11-9079-6045BD895E74"
        },
        Tashir: {
            name: "Tashir",
            id: "E8A5BC48-32DB-EE11-904B-6045BD895C76"
        }

    },

};


function ResolveCaseVisibilty(executionContext) {
    return false;
    debugger;
    var formContext = executionContext;
    var isFCR = formContext.getControl("ldv_isfcr").getAttribute().getValue();
    return isFCR;
};

function RetryIntegration_Action(executionContext) {
    debugger;
    var formContext = executionContext;
    var IntegrationStagelookupValue = formContext.getAttribute("ldv_stageconfigurationid").getValue();
    var recordId = formContext.data.entity.getId().replace('{', '').replace('}', '');

    // Check if the lookup value is not null
    if (IntegrationStagelookupValue != null) {
        // Get the name of the selected record
        var lookupid = IntegrationStagelookupValue[0].id; // Assuming it's a single-value lookup
        var lookupIdWithoutBrackets = lookupid.replace('{', '').replace('}', '');

        // Check if the name contains a specific value
        if (lookupIdWithoutBrackets.toLowerCase() == BPFsIntegrationStageConfig.stages.Kadana.id.toLowerCase()) {
            var senttokidana = formContext.getControl("ldv_senttokidana").getAttribute().getValue();
            if (!senttokidana) {
                //UpdateCase_IsSentField_tobeTrue(formContext, "ldv_senttokidana");
                //showTemporaryProgressIndicator("try to Integrate with Kidana");
                CallAction("38F2A709-F40B-4EAA-95AF-879C4D95130A", recordId, "Kidana")
            }
        }
        else if (lookupIdWithoutBrackets.toLowerCase() == BPFsIntegrationStageConfig.stages.ServiceDesk.id.toLowerCase()) {
            var senttoServiceDesk = formContext.getControl("ldv_senttoservicedesk").getAttribute().getValue();
            if (!senttoServiceDesk) {
                //UpdateCase_IsSentField_tobeTrue(formContext, "ldv_senttoservicedesk");
                //showTemporaryProgressIndicator("try to Integrate with Service Desk");
                CallAction("8DC79E7C-E23E-4D62-89E8-3273CD7E6095", recordId, "Service Desk")
            }
        }
        else if (lookupIdWithoutBrackets.toLowerCase() == BPFsIntegrationStageConfig.stages.Tashir.id.toLowerCase()) {
            var senttoTashir = formContext.getControl("ldv_issenttotasher").getAttribute().getValue();
            if (!senttoTashir) {
                //UpdateCase_IsSentField_tobeTrue(formContext, "ldv_issenttotasher");
                //showTemporaryProgressIndicator("try to Integrate with Tashir");
                CallAction("5007089F-40D2-4EBF-BC42-213E6577B56F", recordId, "Tasher")
            }
        }
    }
}

function RetryIntegrationHome_Action(formContext, data) {
    debugger;
    var SelectedRecordID = data;
    var selectedRows = formContext.getGrid().getSelectedRows();

    selectedRows.forEach(function (row) {
        var rowData = row.getData();
        var entity = rowData.getEntity();
        var recordId = entity.getId();


        // Call Web API to retrieve the record data
        retrieveRecord(recordId)
            .then(function (record) {
                var StageConfigId = record["_ldv_stageconfigurationid_value"];

                if (StageConfigId != null) {

                    // Check if the name contains a specific value
                    if (StageConfigId.toLowerCase() == BPFsIntegrationStageConfig.stages.Kadana.id.toLowerCase()) {
                        var senttokidana = record["ldv_senttokidana"];
                        if (!senttokidana) {
                            CallAction("38F2A709-F40B-4EAA-95AF-879C4D95130A", recordId, "Kidana")
                        }
                    }
                    else if (StageConfigId.toLowerCase() == BPFsIntegrationStageConfig.stages.ServiceDesk.id.toLowerCase()) {
                        var senttoServiceDesk = record["ldv_senttoservicedesk"];
                        if (!senttoServiceDesk) {
                            CallAction("8DC79E7C-E23E-4D62-89E8-3273CD7E6095", recordId, "Service Desk")
                        }
                    }
                    else if (StageConfigId.toLowerCase() == BPFsIntegrationStageConfig.stages.Tashir.id.toLowerCase()) {
                        var senttoTashir = record["ldv_issenttotasher"];
                        if (!senttoTashir) {
                            CallAction("5007089F-40D2-4EBF-BC42-213E6577B56F", recordId, "Tasher")
                        }
                    }
                }

            })
            .catch(function (error) {
                console.error("Error retrieving record:", error);
            });
    });

}
function retrieveRecord(recordId) {
    debugger;
    var entityName = "incidents";
    var recordIdwithoutBrackets = recordId.replace("{", "").replace("}", "");
    var query = entityName + "(" + recordIdwithoutBrackets + ")";


    return new Promise(function (resolve, reject) {
        var req = new XMLHttpRequest();
        var url = Xrm.Utility.getGlobalContext().getClientUrl() + "/api/data/v9.0/" + query;
        req.open("GET", url, true);
        req.setRequestHeader("OData-MaxVersion", "4.0");
        req.setRequestHeader("OData-Version", "4.0");
        req.setRequestHeader("Accept", "application/json");
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        req.setRequestHeader("Prefer", "odata.include-annotations=*");

        req.onreadystatechange = function () {
            if (this.readyState === 4) {
                req.onreadystatechange = null;
                if (this.status === 200) {
                    var result = JSON.parse(this.response);
                    resolve(result); // Resolve the promise with the retrieved record
                } else {
                    reject(new Error("Error retrieving record: " + this.statusText));
                }
            }
        };
        req.send();
    });
}
function CallAction(workFlowId, recordId, IntegrationName) {
    //Aya : check language and depend on language appear the msg 
    Xrm.Utility.showProgressIndicator("Integrating with " + IntegrationName + " may take a few seconds ..")

    var executeWorkflowRequest = {
        entity: { entityType: "workflow", id: workFlowId },
        EntityId: { guid: recordId },

        getMetadata: function () {
            return {
                boundParameter: "entity",
                parameterTypes: {
                    entity: { typeName: "mscrm.workflow", structuralProperty: 5 },
                    EntityId: { typeName: "Edm.Guid", structuralProperty: 1 }
                },
                operationType: 0, operationName: "ExecuteWorkflow"
            };
        }
    };

    Xrm.WebApi.online.execute(executeWorkflowRequest).then(
        function success(response) {
            if (response.ok) { return response.json(); }
        }
    ).then(function (responseBody) {
        var result = responseBody;
        console.log(result);
        Xrm.Utility.closeProgressIndicator();
    }).catch(function (error) {

        console.log(error.message);
        Xrm.Utility.closeProgressIndicator();
    });
}

function RetryIntegration_visibilty(executionContext) {
    debugger;
    var formContext = executionContext;
    var IntegrationStagelookupValue = formContext.getAttribute("ldv_stageconfigurationid").getValue();

    // Check if the lookup value is not null
    if (IntegrationStagelookupValue != null) {
        // Get the name of the selected record
        var lookupid = IntegrationStagelookupValue[0].id; // Assuming it's a single-value lookup
        var lookupIdWithoutBrackets = lookupid.replace('{', '').replace('}', '');

        // Check if the name contains a specific value

        if (lookupIdWithoutBrackets.toLowerCase() == BPFsIntegrationStageConfig.stages.Kadana.id.toLowerCase()) {
            return true;
        }
        else if (lookupIdWithoutBrackets.toLowerCase() == BPFsIntegrationStageConfig.stages.ServiceDesk.id.toLowerCase()) {
            return true;
        }
        else if (lookupIdWithoutBrackets.toLowerCase() == BPFsIntegrationStageConfig.stages.Tashir.id.toLowerCase()) {
            return true;
        }
        else {
            return false;
        }
    } else {

        return false;
    }

}


function UpdateCase_IsSentField_tobeTrue(formContext, fieldName) {

    // Define the entity name
    var entityName = "incident"; // "incident" is the logical name for the Incident (Case) entity

    // Get the current record ID from the form context
    var recordId = formContext.data.entity.getId();

    // Set the value to update the Two Option Set field
    var newValue = true; // This sets the Two Option Set field to "Yes"

    // Create a JSON object with the field name and value
    var data = {};
    data[fieldName] = newValue;

    // Use the Xrm.WebApi SDK to update the record
    Xrm.WebApi.updateRecord(entityName, recordId, data).then(
        function success(result) {
            console.log("Record updated successfully");
            // Handle success
        },
        function (error) {
            console.error("Error occurred while updating record:", error.message);
            // Handle error
        }
    );

}

function showMessageDialog(message) {

    Xrm.Navigation.openAlertDialog(message, { height: 100, width: 300 }).then(function () {
        // Wait for 5 seconds and then close the dialog
        window.setTimeout(function () {
            Xrm.Navigation.closeDialog();
        }, 5000);
    });
}

function showTemporaryProgressIndicator(message) {
    Xrm.Utility.showProgressIndicator(message);

    // Wait for 3 seconds and then close the indicator
    window.setTimeout(function () {
        Xrm.Utility.closeProgressIndicator();
        refreshForm(formContext);
    }, 5000);
}


function refreshForm(formContext) {
    Xrm.Navigation.openForm(formContext.data.entity.getEntityName(), formContext.data.entity.getId());
}

function callFlow() {
    var flowEndpoint = 'https://prod-226.westeurope.logic.azure.com:443/workflows/9e48fda9e4674542bbeaa095d9ec7819/triggers/manual/paths/invoke?api-version=2016-06-01';

    // Make a POST request to trigger the flow
    fetch(flowEndpoint, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
            // Add any other headers as needed
        },
        body: JSON.stringify({
            // Add any data you want to pass to your flow
        })
    })
        .then(response => {
            if (response.ok) {
                console.log('Flow triggered successfully');
                // Optionally, perform any additional actions after the flow is triggered
            } else {
                console.error('Error triggering flow');
            }
        })
        .catch(error => {
            console.error('Error triggering flow:', error);
        });
}

function ExecuteIncuationFlow(flowURL, params, buttonSchemaName) {
    debugger;
    (async () => {
        const rawResponse = await fetch(flowURL, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(params)
        }).then(response => response.text())
            .then(data => {
                if (buttonSchemaName != null && buttonSchemaName != "" && buttonSchemaName != undefined) {
                    /////save button here
                    Xrm.Page.getAttribute(buttonSchemaName).setValue(true);
                    Xrm.Page.data.save().then(function () {
                        console.log("Updated  " + buttonSchemaName);
                        Xrm.Utility.closeProgressIndicator();
                    }, function (err) {
                    });
                }
                else {
                    Xrm.Utility.closeProgressIndicator();
                }
            }).catch(Error => {
                Xrm.Page.ui.setFormNotification("An Error occurred , please contact support if the issue persists,code: " + err.message, "ERROR", "Error");
                Xrm.Utility.closeProgressIndicator();
            });
    })();

}