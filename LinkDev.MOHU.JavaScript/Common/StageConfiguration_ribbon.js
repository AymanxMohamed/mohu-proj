function Clone_Visibility(primaryControl) {
	debugger;
	return true;
};

function Clone_Action(primaryControl) {
    debugger;
	var id = Xrm.Page.data.entity.getId().replace('{', '').replace('}', '');
	var params = {
		"StageConfigurationId": id
	}
	var req = new XMLHttpRequest();
	req.open("GET", Xrm.Utility.getGlobalContext().getClientUrl() + "/api/data/v9.2/ldv_configurations(eb66dd6c-ed23-ef11-840a-7c1e52132400)?$select=ldv_value", false);
	req.setRequestHeader("OData-MaxVersion", "4.0");
	req.setRequestHeader("OData-Version", "4.0");
	req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
	req.setRequestHeader("Accept", "application/json");
	req.setRequestHeader("Prefer", "odata.include-annotations=*");
	req.onreadystatechange = function () {
		if (this.readyState === 4) {
			req.onreadystatechange = null;
			if (this.status === 200) {
				var result = JSON.parse(this.response);
				console.log(result);
				Xrm.Utility.showProgressIndicator("Creating record, It may take a few seconds ..")
				ExecuteFlow(result["ldv_value"], params);

			} else {
				console.log(this.responseText);
			}
		}
	};
	req.send();


}


function ExecuteFlow(flowURL, params) {
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
				Xrm.Utility.closeProgressIndicator();
			}).catch(Error => {
				Xrm.Page.ui.setFormNotification("An Error occurred , please contact support if the issue persists,code: " + err.message, "ERROR", "Error");
				Xrm.Utility.closeProgressIndicator();
			});
	})();

}


function CallFlow(formContext, recordId) {
	var flowUrl;
	var id = Xrm.Page.data.entity.getId().replace('{', '').replace('}', '');

	var params = {
		"StageConfigurationId": id
	}
	Xrm.Utility.showProgressIndicator("Creating record, it may take a few seconds ..")
    Xrm.WebApi.online.retrieveRecord("ldv_configuration", recordId, "?$select=ldv_value").then(
        function success(result) {

            flowUrl = result["ldv_value"];
            if (flowUrl != null) {
                var req = new XMLHttpRequest();
                req.open("POST", flowUrl, true);
                req.setRequestHeader('Content-Type', 'application/json');
                req.send(JSON.stringify({
                    "ldv_stageconfiguration": entity
                }));
            }
        },
        function (error) {
            console.log(error.message);
        }
    );

    //Read Value From The Field.
   /* var entity = formContext.getAttribute(fieldSchemaName).getValue();*/
  

}
function GetFieldValueFromConfigurationEntity(recordId) {

    
}
