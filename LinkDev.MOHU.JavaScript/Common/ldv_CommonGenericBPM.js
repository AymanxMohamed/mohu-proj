var CommonGeneric = CommonGeneric || {};

CommonGeneric = {
    isPublishedStatusEditable: true,
    //dev
    printFlowURL: 'https://prod-07.westeurope.logic.azure.com:443/workflows/db168b954681447ca85daee698ba5546/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=8sGtmlnlGH794Fix_v6O8PotO4imZqiiBtf2zk4kd7M',
    printFlowURLArabic: 'https://prod-56.westeurope.logic.azure.com:443/workflows/ecd830fe21b340d6964006dad8ceaa43/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=o3SV5Z6hOjjH_zvzVDMa5yGHTcPj_8DsS2bGKJ-yUfE',
    //qc
    //printFlowURL: 'https://prod-14.uaenorth.logic.azure.com:443/workflows/d204d50f6e924d82995976266bac7119/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=RiszVzGi_XsVsnqeNJFlcyaNIehnc2uVjB-Yr9lg25o',
    //printFlowURLArabic: 'https://prod-28.uaenorth.logic.azure.com:443/workflows/c7ad8f62ef6545e6b894b5edf3f3379e/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=4Z4bVuPdGmRQ0Yh0mPtYPFp74x-dhBNV2q2xYBrGaIM',
    //Stage
    //   printFlowURL: 'https://prod-24.uaecentral.logic.azure.com:443/workflows/58902fc9a4004e07a3818317bf795f85/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=OgLBpmYsWcJJmBi4DKyxuyAs2JwBgjz_-W_eL5Ac5f8',
    //   printFlowURLArabic: 'https://prod-15.uaecentral.logic.azure.com:443/workflows/c79025d3222b45b4b9f19cd04ea401f2/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=YCpor3r9ViV8WNdXnHQwX1JuH2fd7vct3V9b6TD4dLY',

    // function to get field value
    GetFieldValue: function (formContext, fieldName) {
        var fieldValue = null;

        if (fieldName !== null) {
            fieldAttribute = formContext.getAttribute(fieldName);

            if (fieldAttribute !== null && fieldAttribute !== "undefined") {

                fieldValue = fieldAttribute.getValue();
            }
        }

        return fieldValue;
    },
    // function to get values from many to many mutli selection component 
    CheckMultiselctionComponentBCFContainValue: function (formContext, valuetocheck, fieldSchemaname) {
        let fieldvalue = CommonGeneric.GetFieldValue(formContext, fieldSchemaname);
        const strvalues = fieldvalue;
        if (strvalues != null && strvalues != '') {
            const arrofstring = strvalues.split(',');
            if (arrofstring.includes(valuetocheck)) {
                return true;

            }
            else {
                return false;
            }
        }

    },

    //function to set value to field
    SetFieldValue: function (formContext, fieldName, value) {
        var fieldValue = null;

        if (fieldName !== null) {
            fieldAttribute = formContext.getAttribute(fieldName);

            if (fieldAttribute !== null && fieldAttribute !== "undefined") {

                fieldAttribute.setValue(value);
            }
        }

    },
    // function set field to required or optional 
    SetReqLevel: function (formContext, fieldName, isRequired) {

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
    },

    // function set fields to required or optional 
    SetReqLevelToFields: function (formContext, fieldsArray, isRequired) {
        for (var i = 0; i < fieldsArray.length; i++) {
            this.SetReqLevel(formContext, fieldsArray[i], isRequired);
        }
    },

    // function to make field read only or not
    DisableField: function (formContext, fieldName, isLocked) {
        if (IsNull(isLocked))
            isLocked = true;

        if (fieldName !== null) {
            var fieldControl = formContext.getControl(fieldName);
            if (fieldControl !== null && fieldControl !== "undefined") {
                fieldControl.setDisabled(isLocked);
            }
        }

    },
    validateIfMobileNumber: function (fieldValue) {
        let numRegex = /^\+?\d+$/;
        return numRegex.test(fieldValue);
    },
    // function to make fields read only or not
    DisableFields: function (formContext, fieldsArray, isLocked) {
        for (var i = 0; i < fieldsArray.length; i++) {
            this.DisableField(formContext, fieldsArray[i], isLocked);
        }
    },

    LockFormControls: function (formContext) {
        var formControls = formContext.ui.controls;
        if (!IsNull(formControls)) {
            formControls.forEach(function (control, index) {
                var controlType = control.getControlType();

                if (control.setDisabled) {
                    control.setDisabled(true);
                }

            });

        }

    },
    LockFormControlsBoolen: function (formContext, isLocked) {
        if (IsNull(isLocked))
            isLocked = false;
        var formControls = formContext.ui.controls;
        if (!IsNull(formControls)) {
            formControls.forEach(function (control, index) {
                var controlType = control.getControlType();
                if (control.setDisabled) {
                    control.setDisabled(isLocked);
                }
            });
        }
    },
    // function to make fields read only or not
    DisableBPFFields: function (formContext, fieldsArray, stageId) {
        debugger;
        var activeStage = formContext.data.process.getActiveStage();
        var activeStageId = activeStage.getId();

        for (var i = 0; i < fieldsArray.length; i++) {
            if (activeStageId === stageId)
                this.DisableField(formContext, fieldsArray[i], false);
            else
                this.DisableField(formContext, fieldsArray[i], true);
        }
    },
    LockFormControlsExecptBPF: function (formContext) {
        var formControls = formContext.ui.controls;
        if (!IsNull(formControls)) {
            formControls.forEach(function (control, index) {
                var controlType = control.getControlType();
                if (!control._controlName.includes("header")) {
                    if (control.setDisabled) {
                        control.setDisabled(true);
                    }
                }
            });
        }
    },
    LockFormControlsExecptBPFFlag: function (formContext, isLock) {
        var formControls = formContext.ui.controls;
        if (!IsNull(formControls)) {
            formControls.forEach(function (control, index) {
                var controlType = control.getControlType();
                if (!control._controlName.includes("header")) {
                    if (control.setDisabled) {
                        control.setDisabled(isLock);
                    }
                }
            });
        }
    },
    // function to set field visible or not
    ShowField: function (formContext, fieldName, isShown) {
        if (IsNull(isShown))
            isShown = true;

        if (fieldName !== null) {
            var fieldControl = formContext.getControl(fieldName);
            if (fieldControl !== null && fieldControl !== "undefined") {
                fieldControl.setVisible(isShown);
                if (isShown == false) { CommonGeneric.EmptyField(formContext, fieldName)}
            }
        }

    },

    // function to set array of fields visible or not
    ShowFields: function (formContext, fieldsArray, isShown) {
        for (var i = 0; i < fieldsArray.length; i++) {
            this.ShowField(formContext, fieldsArray[i], isShown);
        }
    },

    // function to set field value to null
    EmptyField: function (formContext, fieldName) {

        if (fieldName !== null) {
            var fieldAttribute = formContext.getAttribute(fieldName);

            if (fieldAttribute !== null && fieldAttribute !== "undefined") {
                if (fieldAttribute.getAttributeType && fieldAttribute.getAttributeType() == "multiselectoptionset")
                    fieldAttribute.setValue([0]);
                else
                    fieldAttribute.setValue(null);

            }
        }
    },

    // function to set fields values to null
    EmptyFields: function (formContext, fieldsArray) {
        for (var i = 0; i < fieldsArray.length; i++) {
            this.EmptyField(formContext, fieldsArray[i]);
        }
    },

    SetLookupRecord: function (formContext, fieldName, id, entityType, recordName) {

        if (fieldName != null) {
            var lookupValue = new Array();
            lookupValue[0] = new Object();
            lookupValue[0].id = id;
            lookupValue[0].name = recordName;
            lookupValue[0].entityType = entityType;
            if (lookupValue[0].id != null) {
                formContext.getAttribute(fieldName).setValue(lookupValue);
            }
        }
    },
    GetMultiSelectOptions: function (formContext, field) {
        try {
            var optionField = formContext.getAttribute(field);

            if (optionField !== null) {
                var options = optionField.getSelectedOption();

                return options;
            }
        }
        catch (e) {
            console.log("Error retrieving Multi-Select options for " + field);
        }
    },
    SearchValueInArr: function (valueToBeSearchedFor, arrToSearchIn) {
        if (arrToSearchIn !== null) {
            for (var i = 0; i < arrToSearchIn.length; i++) {
                if (arrToSearchIn[i] === valueToBeSearchedFor) {
                    return true;
                }
            }
        }
        return false;
    },
    SearchValueInOptionSetArr: function (valueToBeSearchedFor, arrToSearchIn) {
        if (arrToSearchIn !== null) {
            for (var i = 0; i < arrToSearchIn.length; i++) {
                if (arrToSearchIn[i].value === valueToBeSearchedFor) {
                    return true;
                }
            }
        }
        return false;
    },
    ShowAndReuiredField: function (formContext, fieldSchemaName, isRequired, isShown) {
        CommonGeneric.SetReqLevel(formContext, fieldSchemaName, isRequired);
        CommonGeneric.ShowField(formContext, fieldSchemaName, isShown);

    },
    ConvertNumberTowords: function (n) {
        if (n < 0)
            return false;
        single_digit = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine']
        double_digit = ['Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen']
        below_hundred = ['Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety']
        if (n === 0) return 'Zero'
        function translate(n) {
            word = ""
            if (n < 10) {
                word = single_digit[n] + ' '
            }
            else if (n < 20) {
                word = double_digit[n - 10] + ' '
            }
            else if (n < 100) {
                rem = translate(n % 10)
                word = below_hundred[(n - n % 10) / 10 - 2] + ' ' + rem
            }
            else if (n < 1000) {
                word = single_digit[Math.trunc(n / 100)] + ' Hundred ' + translate(n % 100)
            }
            else if (n < 1000000) {
                word = translate(parseInt(n / 1000)).trim() + ' Thousand ' + translate(n % 1000)
            }
            else if (n < 1000000000) {
                word = translate(parseInt(n / 1000000)).trim() + ' Million ' + translate(n % 1000000)
            }
            else {
                word = translate(parseInt(n / 1000000000)).trim() + ' Billion ' + translate(n % 1000000000)
            }
            return word
        }
        result = translate(n)
        return result.trim() + '.'
    }
    ,
    GetLookUpRecord: function (formContext, lookupField) {
        if (!CommonGeneric.IsNullOrEmpty(lookupField)) {
            var lookupAttribute = formContext.getAttribute(lookupField);
            if (!CommonGeneric.IsNullOrEmpty(lookupAttribute)) {
                var LookUpRecords = lookupAttribute.getValue();
                if (!CommonGeneric.IsNullOrEmpty(LookUpRecords) && LookUpRecords.length > 0) {
                    return LookUpRecords[0];
                }
            }
        }
        return null;
    },
    // function make field1 required if field2 has value
    SetReqLvl_field1_BasedOn_field2_HasValue: function (formContext, field1, field2) {

        var field2Value = CommonGeneric.GetFieldValue(formContext, field2);

        if (field2Value !== null) {
            CommonGeneric.SetReqLevel(formContext, field1, true);
        }
        else {
            CommonGeneric.SetReqLevel(formContext, field1, false);
        }
    },

    // function make field1 required if field2 value = option1
    SetReqLvl_field1_BasedOn_field2_Option: function (formContext, field1, field2, option1) {

        var field2Value = CommonGeneric.GetFieldValue(formContext, field2);

        if (field2Value === option1) {
            CommonGeneric.SetReqLevel(formContext, field1, true);
        }
        else {
            CommonGeneric.SetReqLevel(formContext, field1, false);
        }

    },

    // overload function make field1 required if field2 value = option1 or option2
    SetReqLvl_field1_BasedOn_field2_2Options: function (formContext, field1, field2, option1, option2) {

        var field2Value = CommonGeneric.GetFieldValue(formContext, field2);

        if (field2Value === option1 || field2Value === option2) {
            CommonGeneric.SetReqLevel(formContext, field1, true);
        }
        else {
            CommonGeneric.SetReqLevel(formContext, field1, false);
        }
    },

    // overload function make field1 required if field2 value = option1 or option2 or option3
    SetReqLvl_field1_BasedOn_field2_3Options: function (formContext, field1, field2, option1, option2, option3) {

        var field2Value = CommonGeneric.GetFieldValue(formContext, field2);

        if (field2Value === option1 || field2Value === option2 || field2Value === option3) {
            CommonGeneric.SetReqLevel(formContext, field1, true);
        }
        else {
            CommonGeneric.SetReqLevel(formContext, field1, false);
        }
    },

    // overload function make field1 required if field2 value = option1 or option2 or option3 or option4
    SetReqLvl_field1_BasedOn_field2_4Options: function (formContext, field1, field2, option1, option2, option3, option4) {

        var field2Value = CommonGeneric.GetFieldValue(formContext, field2);

        if (field2Value === option1 || field2Value === option2 || field2Value === option3 || field2Value === option4) {
            CommonGeneric.SetReqLevel(formContext, field1, true);
        }
        else {
            CommonGeneric.SetReqLevel(formContext, field1, false);
        }
    },

    // overload function make field1 required if field2 value = option1 or option2 or option3 or option4 or option5
    SetReqLvl_field1_BasedOn_field2_5Options: function (formContext, field1, field2, option1, option2, option3, option4, option5) {

        var field2Value = CommonGeneric.GetFieldValue(formContext, field2);

        if (field2Value === option1 || field2Value === option2 || field2Value === option3 || field2Value === option4 || field2Value === option5) {
            CommonGeneric.SetReqLevel(formContext, field1, true);
        }
        else {
            CommonGeneric.SetReqLevel(formContext, field1, false);
        }
    },

    // function lock filed1 if field2 is empty
    LockUnlock_field1_BasedOn_field2_Emptiness: function (formContext, field1, field2) {
        var field2Value = null;

        if (field2 !== null) {
            field2Value = CommonGeneric.GetFieldValue(formContext, field2);
        }
        if (field2Value !== null && field2Value !== "undefined") {
            CommonGeneric.DisableField(formContext, field1, false);
        }
        else {
            CommonGeneric.DisableField(formContext, field1);
        }

    },

    ShowHide_field1_BasedOn_Field2_Option: function (formContext, field1, field2, option) {

        var field2Value = CommonGeneric.GetFieldValue(formContext, field2);

        if (field2Value === option) {
            CommonGeneric.ShowField(formContext, field1);
        }
        else {
            CommonGeneric.ShowField(formContext, field1, false);
        }
    },

    // function empty field 1 if field 2 is empty
    Empty_field1_BasedOn_field2: function (formContext, field1, field2) {
        var field2Value = null;

        if (field2 !== null) {
            field2Value = CommonGeneric.GetFieldValue(formContext, field2);
        }
        if (field2Value === null || field2Value === "undefined") {

            CommonGeneric.EmptyField(formContext, field1);
        }
    },

    Empty_field1_BasedOn_BoolField2: function (formContext, field1, field2, value) {
        var field2Value = null;

        if (field2 !== null) {
            field2Value = CommonGeneric.GetFieldValue(formContext, field2);
        }
        if (field2Value === value) {

            CommonGeneric.EmptyField(formContext, field1);
        }
    },

    hideBusinessProcessFlow: function (formContext) {

        if (formContext.ui.process !== null && formContext.ui.process !== undefined) {
            formContext.ui.process.setVisible(false);
        }
    },

    ProcessSet: function () {
    },

    DrawBPF: function (formContext, processIdField_Custum, processIdField_OOB) {
        if (formContext.ui.getFormType() === 1) { // if form type is create
            this.hideBusinessProcessFlow(formContext);
        } else {

            var BPFAttribute = formContext.getAttribute(processIdField_Custum);
            if (BPFAttribute !== null && BPFAttribute !== undefined) {

                var BPFValue = BPFAttribute.getValue();
                if (BPFValue !== null) {

                    var BPF = BPFValue[0].id.replace('{', '').replace('}', '');
                    if (BPF !== null && BPF !== undefined) {
                        var proc = formContext.getAttribute(processIdField_OOB);
                        if (proc !== null && proc !== undefined) {
                            var processIdInTheForm = proc.getValue();
                            var processIdInConfig = BPF;
                            if (processIdInTheForm !== null && processIdInConfig !== null && processIdInTheForm.toLowerCase() !== processIdInConfig.toLowerCase() && formContext.data.process !== null && formContext.data.process !== undefined) {
                                formContext.data.process.setActiveProcess(processIdInConfig, this.ProcessSet);
                            }
                        }
                    }
                }
            }


        }
    },

    // method hide or show given section if given field value = given condition
    HideSection_BasedOn_FieldValue: function (formContext, tabName, sectionName, fieldName, conditionValue) {
        fieldValue = this.GetFieldValue(formContext, fieldName);
        var tabObj = formContext.ui.tabs.get(tabName);
        var sectionObj = tabObj.sections.get(sectionName);
        if (fieldValue === conditionValue) {
            // hide section
            sectionObj.setVisible(false);
        } else {
            // show section
            sectionObj.setVisible(true);
        }
    },

    TabVisability: function (formContext, tabName, Isvisable) {
        formContext.ui.tabs.get(tabName).setVisible(Isvisable);
    },
    TabsVisability: function (formContext, tabsNames, isVisible) {
        for (var i = 0; i < tabsNames.length; i++) {
            CommonGeneric.TabVisability(formContext, tabsNames[i], isVisible);
        }
    },
    HideSection: function (formContext, tabName, sectionName, visibility) {
        var generalTab = formContext.ui.tabs.get(tabName);
        if (generalTab !== null) {
            var section = generalTab.sections.get(sectionName);
            section.setVisible(visibility);
        }
    },

    SectionVisibility: function (formContext, tabName, sectionName, visibility) {
        var generalTab = formContext.ui.tabs.get(tabName);
        if (generalTab !== null) {
            var section = generalTab.sections.get(sectionName);
            section.setVisible(visibility);
        }
    },
    SectionsVisibility: function (formContext, tabName, sectionsNames, isVisible) {
        for (var i = 0; i < sectionsNames.length; i++) {
            CommonGeneric.SectionVisibility(formContext, tabName, sectionsNames[i], isVisible);
        }
    },
    GetMultiSelectOptions: function (formContext, field) {
        try {
            var optionField = formContext.getAttribute(field);

            if (optionField !== null) {
                var options = optionField.getSelectedOption();

                return options;
            }
        }
        catch (e) {
            console.log("Error retrieving Multi-Select options for " + field);
        }
    },
    SearchValueInArr: function (valueToBeSearchedFor, arrToSearchIn) {
        if (arrToSearchIn !== null) {
            for (var i = 0; i < arrToSearchIn.length; i++) {
                if (arrToSearchIn[i] === valueToBeSearchedFor) {
                    return true;
                }
            }
        }
        return false;
    },
    // Appear notification message at the top of form
    // Priorty: [INFO,ERROR,WARNING]
    SetNotification: function (msg, priorty, notificationId, executionContext) {
        var formContext = executionContext.getFormContext();
        formContext.ui.clearFormNotification(notificationId);
        formContext.ui.setFormNotification(msg, priorty, notificationId);
        window.setTimeout(function myfunction() {
            formContext.ui.clearFormNotification(notificationId);
        }, 60 * 1000);
    },

    showFormNotificationForXSeconds: async function (messageCode, notificationLevel, notificationId, formContext, numberOfSeconds) {
        var millisecondes = numberOfSeconds * 1000;
        await CommonGeneric.showFormNotification(messageCode, notificationLevel, notificationId, formContext);

        //Wait the designated time and then remove
        setTimeout(
            function () {
                formContext.ui.clearFormNotification(notificationId);
            },
            millisecondes
        );
    },
    IsDateInPast: function (dateToValidate) {
        if (dateToValidate) {
            var currentDate = new Date().setHours(0, 0, 0, 0);
            dateToValidate = dateToValidate.setHours(0, 0, 0, 0);
            if (dateToValidate !== null && dateToValidate !== undefined) {
                if (dateToValidate < currentDate) {
                    return true;
                }
            }
        }
        return false;
    },
    // checks if date in the bast and show error message on the field to enter valid date
    ValidateDateNotToBeInPast: function (formContext, attributeName) {

        // clear notification first    
        formContext.getControl(attributeName).clearNotification();

        var DateToValidate = this.GetFieldValue(formContext, attributeName);
        var isDateToValidateInPast = this.IsDateInPast(DateToValidate);
        if (isDateToValidateInPast === true) {
            var userLanguage = formContext.context.getUserLcid();
            var message = "";
            if (userLanguage === 1025) {
                message = "Wrong Date, please enter date not in the past";
            }
            else {
                message = "Invalid Date, please enter date not in the past";
            }
            formContext.getControl(attributeName).setNotification(message);
        }
    },

    ValidateFieldDateNotToBeInPast: function (formContext, attributeName, attributeName_Notiifcation) {

        // clear notification first    
        formContext.getControl(attributeName_Notiifcation).clearNotification();

        var DateToValidate = this.GetFieldValue(formContext, attributeName);
        var isDateToValidateInPast = this.IsDateInPast(DateToValidate);
        if (isDateToValidateInPast === true) {
            var userLanguage = formContext.context.getUserLcid();
            var message = "";
            if (userLanguage === 1025) {
                message = "Wrong Date, please enter date in the future";
            }
            else {
                message = "Invalid Date, please enter date in the future";
            }
            formContext.getControl(attributeName_Notiifcation).setNotification(message);
        }
    },

    ValidateFieldDateNotToBeInFuture: function (formContext, attributeName, attributeName_Notiifcation) {

        // clear notification first    
        formContext.getControl(attributeName_Notiifcation).clearNotification();

        var DateToValidate = this.GetFieldValue(formContext, attributeName);
        var isDateToValidateInPast = this.IsDateInPast(DateToValidate);
        if (isDateToValidateInPast === false) {
            var userLanguage = formContext.context.getUserLcid();
            var message = "";
            if (userLanguage === 1025) {
                message = "Wrong Date, please enter date not in the future";
            }
            else {
                message = "Invalid Date, please enter date not in the future";
            }
            formContext.getControl(attributeName_Notiifcation).setNotification(message);
        }
    },

   ValidateArabicCharacters: function(formContext, fieldName) {
    debugger;
    formContext.getControl(fieldName).clearNotification("ArabicLettersValidator");

    var fieldValue = formContext.getAttribute(fieldName).getValue();

    if (fieldValue) {
        fieldValue = fieldValue.replace(/\s/g, '').trim().replace(" ", "");

        for (var i = 0; i < fieldValue.length; i++) {
            var unicode = fieldValue.charCodeAt(i);

            if ((unicode < 0x0600 || unicode > 0x06FF) || (unicode >= 0xFE70 && unicode <= 0xFEFF)) {

                var ArabicField = formContext.getControl(fieldName);
                CommonGeneric.showNotification(ArabicField, "3", "ArabicLettersValidator");


                return false;
            }
            else {
                formContext.getControl(fieldName).clearNotification("ArabicLettersValidator");

            }
        }

        return true;
    }
},

ValidateEnglishCharacters: function(formContext, fieldName) {
    formContext.getControl(fieldName).clearNotification("EnglishLettersValidator");

    if (formContext.getAttribute(fieldName).getValue() == null) {
        return true;
    }
    else {
        var english = /^[A-Za-z0-9]*$/;
        var fieldValue = formContext.getAttribute(fieldName).getValue().replace(/\s/g, '');

        if (fieldValue != null && !english.test(fieldValue)) {
            var EnglishField = formContext.getControl(fieldName);
            //formContext.getControl(fieldname).setNotification(message, "9");
            CommonGeneric.showNotification(EnglishField, "4", "EnglishLettersValidator");
            return false;
        }
        else {
            formContext.getControl(fieldName).clearNotification("EnglishLettersValidator");

        }

        return true;
    }
},


    ShowBPFNextButton: function () {
        var backAction = Helper.SearchForElementByDom(parent.document, "stageAdvanceActionContainer");
        if (backAction != null) {
            backAction.setAttribute("style", "display:Inline");
        }
    },
    HideBPFNextButton: function () {
        var backAction = Helper.SearchForElementByDom(parent.document, "stageAdvanceActionContainer");
        if (backAction != null) {
            backAction.setAttribute("style", "display:none");
        }
    },

    hideNextButtonUCI: function () {

        var div = document.getElementById('MscrmControls.Containers.ProcessStageControl-businessProcessFlowFlyoutFooterContainer');
        div.parentNode.removeChild(div);

    },
    hidefinishButtonUCI: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-finishButtonContainer']")
                .find('button').each(function (index) {
                    if (this.id === "MscrmControls.Containers.ProcessStageControl-finishButtonContainer") {
                        $(this).hide();
                        return;
                    }
                });
        },
            50);
        var hide = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-finishButtonContainer");
            if (processStageFooter != null) {
                var previousButtonElement =
                    parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-finishButtonContainer");
                if (previousButtonElement != null) {
                    hide = true;
                    previousButtonElement.style.display = "none";
                    clearInterval(intervalForBackButton);
                }
            }
        }, 50);
    },
    showfinishButtonUCI: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-finishButtonContainer']")
                .find('button').each(function (index) {
                    if (this.id === "MscrmControls.Containers.ProcessStageControl-finishButtonContainer") {
                        $(this).show();
                        return;
                    }
                });
        },
            50);
        var hide = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-finishButtonContainer");
            if (processStageFooter != null) {
                var previousButtonElement =
                    parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-finishButtonContainer");
                if (previousButtonElement != null) {
                    hide = true;
                    previousButtonElement.style.display = "flex";
                    clearInterval(intervalForBackButton);
                }
            }
        }, 50);
    },

    hideDockModeButtonUCI: function () {
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
            50);
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
        }, 100);
    },
    hideDockModeButtonUCIGeneric: function () {
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
            50);
        var hide = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-stageDockModeButton");
            if (processStageFooter != null) {
                var previousButtonElement =
                    parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-stageDockModeButton");
                if (previousButtonElement != null) {
                    hide = true;
                    previousButtonElement.style.display = "none";
                    if (document.getElementById('MscrmControls.Containers.ProcessStageControl-setActiveButtonContainerbuttonInnerContainer').parentElement.parentElement.parentElement.id == "MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer_" + Xrm.Page.data.process.getSelectedStage()._stageStep.id)
                        clearInterval(intervalForBackButton);
                }
            }
        }, 100);
    },

    HideNextStageUCI: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-nextButtonContainer']")
                .find('button').each(function (index) {
                    $(this).hide();
                });
        },
            50);
        var hide = true;
        var interval = setInterval(function () {
            var element5 = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-nextButtonContainer");
            if (element5 != null && hide == true) {
                hide = false;
                element5.style.display = "none";
                clearInterval(interval);
            }
        }, 50);
    },
    HideNextStageUCIExpand: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-stageDockFooterContainer']")
                .find('button').each(function (index) {
                    $(this).hide();
                });
        },
            50);
        var hide = true;
        var interval = setInterval(function () {
            var element5 = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-stageDockFooterContainer");
            if (element5 != null && hide == true) {
                hide = false;
                element5.style.display = "none";
                clearInterval(interval);
            }
        }, 50);
    },
    HidePreviousButtonInUCI: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-previousButtonContainer']")
                .find('button').each(function (index) {
                    if (this.id === "MscrmControls.Containers.ProcessStageControl-previousButtonContainer") {
                        $(this).hide();
                        return;
                    }
                });
        },
            50);
        var hide = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-previousButtonContainer");
            if (processStageFooter != null) {
                var previousButtonElement =
                    parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-previousButtonContainer");
                if (previousButtonElement != null) {
                    hide = true;
                    previousButtonElement.style.display = "none";
                    if (document.getElementById('MscrmControls.Containers.ProcessStageControl-setActiveButtonContainerbuttonInnerContainer').parentElement.parentElement.parentElement.id == "MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer_" + Xrm.Page.data.process.getSelectedStage()._stageStep.id)
                        clearInterval(intervalForBackButton);
                }
            }
        }, 100);
    },

    HideSetActiveButtonInUCI: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer']")
                .find('button').each(function (index) {
                    if (this.id === "MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer") {
                        $(this).hide();
                        return;
                    }
                });
        },
            50);
        var hide = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer");
            if (processStageFooter != null) {
                var previousButtonElement =
                    parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer");
                if (previousButtonElement != null) {
                    hide = true;
                    previousButtonElement.style.display = "none";
                    clearInterval(intervalForBackButton);
                }
            }
        }, 100);
    },
    HideSetActiveButtonInUCIGeneric: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer']")
                .find('button').each(function (index) {
                    if (this.id === "MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer") {
                        $(this).hide();
                        return;
                    }
                });
        },
            50);
        var hide = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer");
            if (processStageFooter != null) {
                var previousButtonElement =
                    parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-setActiveButtonContainer");
                if (previousButtonElement != null) {
                    hide = true;
                    previousButtonElement.style.display = "none";
                    if (document.getElementById('MscrmControls.Containers.ProcessStageControl-setActiveButtonContainerbuttonInnerContainer').parentElement.parentElement.parentElement.id == "MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer_" + Xrm.Page.data.process.getSelectedStage()._stageStep.id)
                        clearInterval(intervalForBackButton);
                }
            }
        }, 100);
    },
    ShowPreviousButtonInUCI: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-previousButtonContainer']")
                .find('button').each(function (index) {
                    if (this.id === "MscrmControls.Containers.ProcessStageControl-previousButtonContainer") {
                        $(this).show();
                        return;
                    }
                });
        },
            50);
        var show = false;
        var intervalForBackButton = setInterval(function () {
            var processStageFooter = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-previousButtonContainer");
            if (processStageFooter != null) {
                var previousButtonElement =
                    parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-previousButtonContainer");
                if (previousButtonElement != null) {
                    show = true;
                    previousButtonElement.style.display = "flex";
                    clearInterval(intervalForBackButton);
                }
            }
        }, 50);
    },
    ShowNextStageUCI: function () {
        setTimeout(function () {
            $(window.parent.document)
                .find(
                    "[data-id = 'MscrmControls.Containers.ProcessStageControl-nextButtonContainer']")
                .find('button').each(function (index) {
                    $(this).show();
                });
        },
            50);
        var hide = true;
        var interval = setInterval(function () {
            var element5 = parent.document.getElementById("MscrmControls.Containers.ProcessStageControl-nextButtonContainer");
            if (element5 != null && hide == true) {
                hide = false;
                element5.style.display = "flex";
                clearInterval(interval);
            }
        }, 50);
    },

    //MODON
    IsNumber: function (value) {
        if (value != null) {
            for (var i = 0; i < value.length; i++) {
                if (value.charAt(i) % 1 != 0)
                    return false;
            }
            return true;
        }
    },
    IsNullOrEmpty: function (value) {
        if (value == null || value == "")
            return true;
        return false;
    },
    OptionValueIncludedInMultiSelection: function (formContext, optionValue, fieldname) {
        var multiSelectionAttribute = formContext.getAttribute(fieldname);
        if (multiSelectionAttribute !== null && multiSelectionAttribute !== "undefined") {
            var multiSelectionValues = multiSelectionAttribute.getValue();
            if (multiSelectionValues !== null && multiSelectionValues !== "undefined") {
                for (var i = 0; i < multiSelectionValues.length; i++) {

                    if (optionValue == multiSelectionValues[i]) {
                        return true;
                    }
                }
            }
        }

        return false;
    },

    FieldFireOnChange: function (formContext, fieldName) {
        var fieldAttribute = formContext.getAttribute(fieldName);
        fieldAttribute.fireOnChange();
    },

    //for SubGrid
    EnableOrDisableGridInClassic: function (gridName, disableFlag) {
        var itemId = "titleContainer_" + gridName;
        var addItem = window.parent.document.getElementById(itemId);
        if (addItem) {
            addItem.style.display = "none";
        } else {
            var tmr = setTimeout(function () { CommonGeneric.EnableOrDisableGridInClassic(gridName, disableFlag); }, 50);
        }
    },

    EnableOrDisableGridInUCI: function (gridName, disableFlag) {
        //debugger;
        var itemId = "dataSetRoot_" + gridName;
        var addItem = window.parent.document.getElementById(itemId);
        if (addItem && addItem.querySelector("ul[data-lp-id*='commandbar-SubGridStandard:']")) {
            addItem.querySelector("ul[data-lp-id*='commandbar-SubGridStandard:']").style.display = disableFlag ? "none" : "block";
            var tmr = setTimeout(function () { CommonGeneric.EnableOrDisableGridInUCI(gridName, disableFlag); }, 500);

        } else {
            var tmr = setTimeout(function () { CommonGeneric.EnableOrDisableGridInUCI(gridName, disableFlag); }, 500);
        }
    },

    EnableOrDisableAllGrids: function (formContext, disableFlag) {
        //debugger;
        formContext.ui.controls.forEach(function (currentControl) {
            //debugger;
            if (currentControl.getControlType() == "subgrid" || currentControl.getControlType() == "customsubgrid:MscrmControls.Grid.GridControl") {
                CommonGeneric.EnableOrDisableGridInClassic(currentControl.getName(), disableFlag);
                CommonGeneric.EnableOrDisableGridInUCI(currentControl.getName(), disableFlag);
            }
        });
    },

    EnableOrDisableGridInUCIWithCondition: function (gridName, disableFlag, FieldName, FieldValue, formContext) {
        //debugger;
        var itemId = "dataSetRoot_" + gridName;
        var addItem = window.parent.document.getElementById(itemId);
        var valueOfField = CommonGeneric.GetFieldValue(formContext, FieldName)
        if (addItem && addItem.querySelector("ul[data-lp-id*='commandbar-SubGridStandard:']")) {
            addItem.querySelector("ul[data-lp-id*='commandbar-SubGridStandard:']").style.display = disableFlag ? "none" : "block";

            var tmr = setTimeout(function () {
                if (valueOfField == FieldValue)
                    CommonGeneric.EnableOrDisableGridInUCIWithCondition(gridName, disableFlag, FieldName, FieldValue, formContext);
            }, 500);

        } else {
            var tmr = setTimeout(function () {
                if (valueOfField == FieldValue)
                    CommonGeneric.EnableOrDisableGridInUCIWithCondition(gridName, disableFlag, FieldName, FieldValue, formContext);
            }, 500);
        }
    },

    EnableOrDisableAllGridsWithCondition: function (formContext, disableFlag, FieldName, FieldValue) {
        //debugger;
        formContext.ui.controls.forEach(function (currentControl) {
            //debugger;
            if (currentControl.getControlType() == "subgrid" || currentControl.getControlType() == "customsubgrid:MscrmControls.Grid.GridControl") {
                CommonGeneric.EnableOrDisableGridInUCIWithCondition(currentControl.getName(), disableFlag, FieldName, FieldValue, formContext);
            }
        });
    },
    EnableGridInUCI: function (gridName) {
        //debugger;
        var itemId = "dataSetRoot_" + gridName;
        var addItem = window.parent.document.getElementById(itemId);
        if (addItem && addItem.querySelector("ul[data-lp-id*='commandbar-SubGridStandard:']")) {
            addItem.querySelector("ul[data-lp-id*='commandbar-SubGridStandard:']").style.display = "block";

        }
    },
    EnableGridsInUCI: function (formContext) {
        //debugger;
        formContext.ui.controls.forEach(function (currentControl) {
            //debugger;
            if (currentControl.getControlType() == "subgrid" || currentControl.getControlType() == "customsubgrid:MscrmControls.Grid.GridControl") {
                CommonGeneric.EnableGridInUCI(currentControl.getName());
            }
        });

    },
    validateIfNumber: function (fieldValue) {
        var numRegex = /^\d+$/;
        return numRegex.test(fieldValue);
    },
    showNotification: async function (controlName, messageCode, notificationId) {
        // call get message by code
        var userLang = this.getUserLanguageId();
        var msg = await this.getMessageByCode(messageCode, userLang);
        // var msg = "error";
        controlName?.setNotification(msg, notificationId);
    },
    getMessage: async function (messageCode) {
        // call get message by code
        var userLang = this.getUserLanguageId();
        var msg = await this.getMessageByCode(messageCode, userLang);

        return msg;
    },
    showFormNotification: async function (formContext, messageCode, notificationId, level) {
        // call get message by code
        var userLang = this.getUserLanguageId();
        var msg = await this.getMessageByCode(messageCode, userLang);
        // var msg = "error";
        formContext.ui.setFormNotification(msg, level, messageCode);

    },


    getUserLanguageId: function () {
        let userSettings = Xrm.Utility.getGlobalContext().userSettings;
        return userSettings.languageId;
    },
    getMessageByCode: async function (messageCode, langId) {
        let arabicmessage = "";
        let englishmessage = "";
        //todo: call odata for notification entity
        await Xrm.WebApi.online.retrieveMultipleRecords("ldv_message", "?$select=ldv_arabicmessage,ldv_englishmessage,ldv_name&$filter=ldv_code eq '" + messageCode + "'").then(
            function success(results) {
                for (var i = 0; i < results.entities.length; i++) {
                    arabicmessage = results.entities[i]["ldv_arabicmessage"];
                    englishmessage = results.entities[i]["ldv_englishmessage"];
                    var ldv_name = results.entities[i]["ldv_name"];
                }
            },
            function (error) {
                Xrm.Utility.alertDialog(error.message);
            }
        );
        if (langId == "1025") {
            return arabicmessage;
        } else {
            return englishmessage;
        }

    },
    validateIfEmirateId: function (fieldValue) {
        var numRegex = /^784/;
        return numRegex.test(fieldValue);
    },
    RetrieveEntityFieldsByWebApi: async function (entitySchemaName, fieldsSchemaName, filterFieldSchemaName, filterFieldValue) {
        var result = "";

        //todo: call odata for notification entity
        await Xrm.WebApi.online.retrieveMultipleRecords(entitySchemaName, "?$select=" + fieldsSchemaName + "&$filter=" + filterFieldSchemaName + " eq '" + filterFieldValue + "'").then(
            function success(results) {
                result = results;
            },
            function (error) {
                Xrm.Utility.alertDialog(error.message);
            }
        );
        return result;
    },

    validateIfWebsiteURL: function (fieldValue) {
        let specialUrlRegex = /(http|ftp|https):\/\/[\w-]+(\.[\w-]+)+([\w.,@?^=%&amp;:\/~+#-]*[\w@?^=%&amp;\/~+#-])?/
        return specialUrlRegex.test(fieldValue);
    },
    // check if string (english Charchters or number)
    validateIfEnglishCharacterOrNumber: function (fieldValue) {
        let enCharRegex = /^[a-zA-Z \n0-9]*$/;
        return enCharRegex.test(fieldValue);
    },
    // check if arabic Charchters or numbers or special charachters
    validateIfArabicOrSpechialCharacter: function (fieldValue) {
        let arCharRegex = /^([\u0600-\u06FF ]|[0-9]|[\!\@\#\$\%\^\&\*\)\(\+\=\.\,\<\>\[\]\:\;\'\|\"\/\""\â€™\~\`\”\’\â€œ\â€\_\-\â€“\n])+$/;
        return arCharRegex.test(fieldValue);
    },
    // check if english or numbers or special charachter
    validateIfEnglishOrSpechialCharacter: function (fieldValue) {
        let charRegex = /^(\w|[\!\@\#\$\%\^\&\*\)\(\+\=\.\,\<\>\[\]\:\;\'\"\""\|\~\”\`\/\â€™\_ \’\â€œ\â€\- \â€“\n])+$/;
        return charRegex.test(fieldValue);
    },
    // check if string (arabic Charchters Only)
    validateIfArabicCharacter: function (fieldValue) {
        let arCharRegex = /[^\u0600-\u06FF \n]/;
        return arCharRegex.test(fieldValue);
    },
    PreventEnglishValidation: function (fieldValue) {
        let charRegex = /[\u0600-\u06FF]/;
        return charRegex.test(fieldValue);
    },
    // function to get field Optionse
    GetFieldOptions: function (formContext, fieldName) {
        var fieldOptions = null;

        if (fieldName !== null) {
            fieldAttribute = formContext.getAttribute(fieldName);

            if (fieldAttribute !== null && fieldAttribute !== "undefined") {

                fieldOptions = fieldAttribute.getOptions();
            }
        }

        return fieldOptions;
    },
    // Remove Option in Optionse
    RemoveOptionInOptionse: function (formContext, fieldName, optionValueToRemove, isRemoved) {
        let options = this.GetFieldOptions(formContext, fieldName);
        let control = Xrm.Page.getControl(fieldName);
        for (var i = 0; i < options.length; i++) {
            if (control !== null && control !== "undefined") {
                if (options[i].value === optionValueToRemove && isRemoved === true)
                    control.removeOption(options[i].value);

            }
        }

    },
    SetOptionInOptionse: function (formContext, fieldName, option, index) {
        //        The option to add.The object contains the following: (Required)
        //        - text: String.The label for the option.
        //        - value: Number.The value for the option.
        //The index position to place the new option in. If not provided, the option will be added to the end. (optional)

        let control = null;

        if (fieldName !== null && option != null) {
            control = formContext.getControl(fieldName);;

            if (control !== null && control !== "undefined") {

                control.addOption(option, index);
            }
        }

    },
    ExecuteAction: function (recordId, actionSChemaName, entitySchemaNamePlural) {
        debugger;
        var req = new XMLHttpRequest();
        Xrm.Utility.showProgressIndicator("Loading...")
        req.open("POST", Xrm.Utility.getGlobalContext().getClientUrl() + "/api/data/v9.2/" + entitySchemaNamePlural + "(" + recordId + ")/Microsoft.Dynamics.CRM." + actionSChemaName, false);
        req.setRequestHeader("OData-MaxVersion", "4.0");
        req.setRequestHeader("OData-Version", "4.0");
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        req.setRequestHeader("Accept", "application/json");
        req.onreadystatechange = function () {
            if (this.readyState === 4) {
                req.onreadystatechange = null;
                if (this.status === 200 || this.status === 204) {
                    debugger;
                    Xrm.Utility.closeProgressIndicator();
                    console.log("Action " + actionSChemaName + " has been called");
                    //formContext.data.refresh(false);
                } else {
                    debugger;
                    Xrm.Utility.closeProgressIndicator();
                    console.log(this.responseText);
                }
            }
        };
        req.send();
    },
    GetFieldValueFromMaanGenericConfiguration: async function (fieldSchemaName) {

        await Xrm.WebApi.online.retrieveRecord("ldv_maangenericconfiguration", "56bda355-29b1-eb11-8236-000d3aa961e", "?$select=" + fieldSchemaName + "").then(
            function success(result) {

                return result[fieldSchemaName]; // Text	
            },
            function (error) {
                console.log(error.message);
            }
        );
    },

};

CommonGeneric.BusinessProcessFlowTools = CommonGeneric.BusinessProcessFlowTools || {};
CommonGeneric.BusinessProcessFlowTools.__inactiveBPFStagesCallBack = null;
CommonGeneric.BusinessProcessFlowTools.__ShowBPFBackActionBasedOnDecisionOldValue = null;
CommonGeneric.BusinessProcessFlowTools.Disable_InactiveBPFStages = function (executionContext, callback) {

    var formContext = executionContext.getFormContext();

    formContext.data.process.addOnStageChange(function () {
        CommonGeneric.BusinessProcessFlowTools.StageChanged(executionContext);
    });
    formContext.data.process.addOnStageSelected(function () {
        CommonGeneric.BusinessProcessFlowTools.StageChanged(executionContext);

    });

    CommonGeneric.BusinessProcessFlowTools.__inactiveBPFStagesCallBack = callback;


    CommonGeneric.BusinessProcessFlowTools.StageChanged(executionContext);
};
CommonGeneric.BusinessProcessFlowTools.StageChanged = function (executionContext) {
    if (IsNull(executionContext))
        return;

    var formContext = executionContext.getFormContext();

    if (!IsNull(formContext.getAttribute("statecode")) && formContext.getAttribute("statecode").getValue() !== 1) {
        var stageCollection = formContext.data.process.getActivePath();
        var activeStage = formContext.data.process.getActiveStage();

        if (stageCollection !== null && activeStage !== null) {
            for (var i = 0; i < stageCollection.getLength(); i++) {
                if (stageCollection.get(i).getStatus() === "inactive")
                    CommonGeneric.BusinessProcessFlowTools.DisableStageFields(formContext, stageCollection.get(i), true);
                else
                    CommonGeneric.BusinessProcessFlowTools.DisableStageFields(formContext, stageCollection.get(i), false);

            }
        }
    }

    if (typeof __inactiveBPFStagesCallBack === "function")
        CommonGeneric.BusinessProcessFlowTools.__inactiveBPFStagesCallBack(executionContext);
};
CommonGeneric.BusinessProcessFlowTools.DisableStageFields = function (formContext, stage, disable) {
    try {
        var attributes = stage.getSteps();

        for (var i = 0; i < attributes.getLength(); i++) {
            var attr = attributes.get(i);
            if (!IsNull(attr) && !IsNull(attr.getAttribute())) {
                CommonGeneric.BusinessProcessFlowTools.DisableStep(formContext, attr.getAttribute(), disable);
            }
        }
    } catch (e) {
        throw e;
    }
};
CommonGeneric.BusinessProcessFlowTools.DisableStep = function (formContext, fieldName, disable) {
    //debugger;
    if (IsNull(fieldName))
        return;
    var _BPFSteps = [];
    var fieldIndx = _BPFSteps.indexOf(fieldName);
    var field = null;
    if (fieldIndx >= 0)
        field = _BPFSteps[fieldIndx];
    else {
        var foundedField = CommonGeneric.BusinessProcessFlowTools.FindField(formContext, fieldName);
        if (!IsNull(foundedField)) {
            _BPFSteps.push(foundedField);
            field = foundedField;
        }
    }

    if (field !== null)
        CommonGeneric.BusinessProcessFlowTools.DisableFieldControls(formContext, field, disable);

};
CommonGeneric.BusinessProcessFlowTools.DisableFieldControls = function (formContext, fieldName, disable) {
    var controls = formContext.getAttribute(fieldName).controls;
    if (controls !== null && controls !== undefined) {
        for (var i = 0; i < controls.getLength(); i++)
            controls.get(i).setDisabled(disable);
    }
};
CommonGeneric.BusinessProcessFlowTools.FindField = function (formContext, fieldName) {
    var ctrlObj = null;
    var ctrlName = null;

    ctrlObj = formContext.getAttribute(fieldName);
    if (!IsNull(ctrlObj))
        ctrlName = fieldName;

    if (IsNull(ctrlObj)) {
        ctrlObj = CommonGeneric.BusinessProcessFlowTools.GetBPFField(formContext, fieldName);
        if (!IsNull(ctrlObj))
            ctrlName = ctrlObj.getAttribute().getName();
    }

    if (IsNull(ctrlObj)) {
        ctrlObj = CommonGeneric.BusinessProcessFlowTools.GetElementByDom(formContext, fieldName);
        if (!IsNull(ctrlObj))
            ctrlName = ctrlObj.getName();
    }

    if (!IsNull(ctrlName))
        return ctrlName;
    return null;
};
CommonGeneric.BusinessProcessFlowTools.GetBPFField = function (formContext, stepName) {
    var stepNameCntr = null;
    try {
        if (!IsNull(stepName)) {
            stepNameCntr = formContext.getControl("header_process_" + stepName);
        }
    } catch (e) {
        stepNameCntr = null;
    }
    return stepNameCntr;
};
CommonGeneric.BusinessProcessFlowTools.GetElementByDom = function (formContext, fieldTitle) {
    // IE 10
    return CommonGeneric.BusinessProcessFlowTools.SearchForFieldByDom(formContext, document, fieldTitle);
};
CommonGeneric.BusinessProcessFlowTools.SearchForFieldByDom = function (formContext, dom, id) {
    try {
        var foundedAttr = dom.getElementById("header_process_" + id);
        if (foundedAttr !== null) {
            var attr = foundedAttr.getAttribute("data-attributename");
            if (attr !== null)
                return formContext.getAttribute(attr);
            else return null;
        }

        var iframes = dom.querySelectorAll('iframe');
        for (var i = 0; i < iframes.length; i++) {
            var returnedField = CommonGeneric.BusinessProcessFlowTools.SearchForFieldByDom(iframes[i].contentDocument, id);
            if (!IsNull(returnedField))
                return returnedField;
        }
    }
    catch (e) {
        //
    }
    return null;
};
CommonGeneric.BusinessProcessFlowTools.ShowBPFBackAction = function (executionContext, show) {
    var visibility = 'visible';
    if (!show)
        visibility = 'hidden';
    parent.document.getElementById("stageBackActionContainer").style.visibility = visibility;
};
CommonGeneric.BusinessProcessFlowTools.ShowBPFNextAction = function (executionContext, show) {
    var visibility = 'visible';
    if (!show)
        visibility = 'hidden';
    parent.document.getElementById("stageAdvanceActionContainer").style.visibility = visibility;
};
CommonGeneric.BusinessProcessFlowTools.ShowBPFFinishAction = function (executionContext, show) {
    var visibility = 'visible';
    if (!show)
        visibility = 'hidden';
    parent.document.getElementById("stageFinishActionContainer").style.visibility = visibility;
};
CommonGeneric.BusinessProcessFlowTools.ShowBPFSetActive = function (executionContext, show) {
    var visibility = 'visible';
    if (!show)
        visibility = 'hidden';
    parent.document.getElementById("stageSetActiveActionContainer").style.visibility = visibility;
};
CommonGeneric.BusinessProcessFlowTools.ShowBPFBackActionBasedOnDecision = function (executionContext, valueToShowBackAction) {
    var show = null;
    var sessionKey = '__showBPFBackActionBasedOnDecision' + executionContext.getEventSource().getName();
    var oldValue = sessionStorage.getItem(sessionKey);

    // set attribute to null if its value will show back button and hide next or finish button
    if (!IsNull(oldValue) && oldValue !== "null" && (oldValue === valueToShowBackAction || executionContext.getEventSource().getValue() === valueToShowBackAction)) {
        show = false;
        sessionStorage.setItem(sessionKey, null);
        executionContext.getEventSource().setValue(null);
    }
    else {

        sessionStorage.setItem(sessionKey, executionContext.getEventSource().getValue());
        if (executionContext.getEventSource().getValue() === valueToShowBackAction) show = true;
        else show = false;
    }



    if (!IsNull(show)) {
        var activePath = executionContext.getFormContext().data.process.getActivePath();
        var activeStage = executionContext.getFormContext().data.process.getActiveStage();

        if (activeStage.getId() === activePath.get(activePath.getLength() - 1).getId())
            CommonGeneric.BusinessProcessFlowTools.ShowBPFFinishAction(executionContext, !show);
        else
            CommonGeneric.BusinessProcessFlowTools.ShowBPFNextAction(executionContext, !show);


        CommonGeneric.BusinessProcessFlowTools.ShowBPFBackAction(executionContext, show);
    }
};
CommonGeneric.BusinessProcessFlowTools.HideAllBPFButtons = function (executionContext) {
    CommonGeneric.BusinessProcessFlowTools.ShowBPFBackAction(executionContext, false);
    CommonGeneric.BusinessProcessFlowTools.ShowBPFSetActive(executionContext, false);
    CommonGeneric.BusinessProcessFlowTools.ShowBPFFinishAction(executionContext, false);
    CommonGeneric.BusinessProcessFlowTools.ShowBPFNextAction(executionContext, false);
};



var Helper = {
    SearchForElementByDom: function (dom, id) {
        try {
            var foundedAttr = dom.getElementById(id);

            if (foundedAttr != null)
                return foundedAttr;

            var iframes = dom.querySelectorAll('iframe');
            for (var i = 0; i < iframes.length; i++) {
                var returnedField = SearchForElementByDom(iframes[i].contentDocument, id);
                if (!IsNull(returnedField))
                    return returnedField;
            }
        }
        catch (e) {
        }

        return null;
    }
}



