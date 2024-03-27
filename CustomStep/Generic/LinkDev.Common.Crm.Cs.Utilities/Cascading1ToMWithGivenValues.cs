using LinkDev.Common.Crm.Cs.Base;
using LinkDev.Common.Crm.Logger;
using LinkDev.Common.Crm.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Common.Crm.Cs.Utilities
{//UpdateRecordsFrom1ToMWithGivenValues
    public class Cascading1ToMWithGivenValues : CustomStepBase
    {
        #region "Input Parameters"
        [RequiredArgument]
        [Input("Entity Logical Name")]
        public InArgument<string> EntityLogicalName { get; set; }
        [RequiredArgument]
        [Input("Entity ID")]
        public InArgument<string> EntityId { get; set; }

        [RequiredArgument]
        [Input("Related 1 To M Entity Schema Name")]
        public InArgument<string> Related1ToMEntitySchemaName { get; set; }
        ////////
        #region Lookup
        [Default("false")]
        [Input("Is Lookup")]
        public InArgument<bool> IsLookup { get; set; }
        [Input("Lookup SchemaName")]
        public InArgument<string> LookupSchemaName { get; set; }
        [Input("Entity Id Value")]
        public InArgument<string> EntityIdValue { get; set; } 
        [Input("Entity Schema Name")]
        public InArgument<string> EntitySchemaName { get; set; }
        //lookup schemaname , entity schemaname ,entity id
        ////////
        #endregion

        #region OptionSet
        [Default("false")]
        [Input("Is OptionSet")]
        public InArgument<bool> IsOptionSet { get; set; }
        [Input("OptionSet SchemaName")]
        public InArgument<string> OptionSetSchemaName { get; set; }
        //[RequiredArgument]
        [Input("OptionSet Code Value")]
        public InArgument<string> OptionSetCodeValue { get; set; }
        ////////
        #endregion

        #region TwoOption
        [Default("false")]
        [Input("Is TwoOption")]
        public InArgument<bool> IsTwoOption { get; set; }
        [Input("TwoOption SchemaName")]
        public InArgument<string> TwoOptionSchemaName { get; set; }
        [Input("OptionSet Code Value")]
        public InArgument<string> TwoOptionCodeValue { get; set; }
        ////////
        #endregion

        #region String
        [Default("false")]
        [Input("Is String")]
        public InArgument<bool> IsString { get; set; }
        [Input("String SchemaName")]
        public InArgument<string> StringSchemaName { get; set; }
        [Input("String Code Value")]
        public InArgument<string> StringCodeValue { get; set; }
        ////////
        #endregion

        #region Date
        [Default("false")]
        [Input("Is Date")]
        public InArgument<bool> IsDate { get; set; }
        [Input("Date SchemaName")]
        public InArgument<string> DateSchemaName { get; set; }
        [Input("Date Code Value")]
        public InArgument<DateTime> DateValue { get; set; }
        /////////////
        #endregion

        #region Currency
        [Default("false")]
        [Input("Is Currency")]
        public InArgument<bool> IsCurrency { get; set; }
        [Input("Currency SchemaName")]
        public InArgument<string> CurrencySchemaName { get; set; }
        [Input("Date Code Value")]
        public InArgument<Money> CurrencyValue { get; set; }
        /////////////

        #endregion



        #endregion

        public override void ExtendedExecute()
        {
            #region check if input paramaters are null
   
            if (IsLookup.Get<bool>(ExecutionContext) == true && (EntityIdValue.Get<string>(ExecutionContext) == null ||
                                                                 LookupSchemaName.Get<string>(ExecutionContext) == null ||
                                                                 EntitySchemaName.Get<string>(ExecutionContext) == null
                                                                 ))
                throw new Exception(string.Format($"EntityIdValue ,LookupSchemaName, EntitySchemaName   shouldn't be null "));
            if (IsOptionSet.Get<bool>(ExecutionContext) == true && (OptionSetCodeValue.Get<string>(ExecutionContext) == null || OptionSetSchemaName.Get<string>(ExecutionContext) == null))
                throw new Exception(string.Format($"OptionSetCodeValue{ OptionSetCodeValue} is null "));
            if (IsTwoOption.Get<bool>(ExecutionContext) == true && TwoOptionCodeValue.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format($"TwoOptionCodeValue{ TwoOptionCodeValue} is null "));
            if (IsString.Get<bool>(ExecutionContext) == true && StringCodeValue.Get<string>(ExecutionContext) == null)
                throw new Exception(string.Format($"StringCodeValue{ StringCodeValue} is null "));
            if (IsDate.Get<bool>(ExecutionContext) == true && DateValue.Get<DateTime>(ExecutionContext) == null)
                throw new Exception(string.Format($"Date Value{ DateValue} is null "));
            if (IsCurrency.Get<bool>(ExecutionContext) == true && CurrencyValue.Get<Money>(ExecutionContext) == null)
                throw new Exception(string.Format($"Currency Value{ CurrencyValue} is null "));
            #endregion


            #region map input paramaters 
            string entityLogicalName = EntityLogicalName.Get<string>(ExecutionContext);
            string entityId = EntityId.Get<string>(ExecutionContext);
            string related1ToMEntitySchemaName = Related1ToMEntitySchemaName.Get<string>(ExecutionContext);

            bool isLookup = IsLookup.Get<bool>(ExecutionContext);
            string lookupSchemaName = LookupSchemaName.Get<string>(ExecutionContext);
            string entityIdValue = EntityIdValue.Get<string>(ExecutionContext);
            string entitySchemaName = EntitySchemaName.Get<string>(ExecutionContext);

            bool isOptionSet = IsOptionSet.Get<bool>(ExecutionContext);
            string optionSetCodeValue = OptionSetCodeValue.Get<string>(ExecutionContext);
            string optionSetSchemaName= OptionSetSchemaName.Get<string>(ExecutionContext);

            bool isTwoOption = IsTwoOption.Get<bool>(ExecutionContext);
            string twoOptionCodeValue = TwoOptionCodeValue.Get<string>(ExecutionContext);

            bool isString = IsString.Get<bool>(ExecutionContext);
            string stringCodeValue = StringCodeValue.Get<string>(ExecutionContext);

            bool isDate = IsDate.Get<bool>(ExecutionContext);
            DateTime dateCodeValue = DateValue.Get<DateTime>(ExecutionContext);

            bool isCurrency = IsCurrency.Get<bool>(ExecutionContext);
            Money currencyValue = CurrencyValue.Get<Money>(ExecutionContext);

            #endregion

            #region call BLL method UpdateRecordsFrom1ToMWithGivenValues

            Tools.UpdateRecordsFrom1ToMWithGivenValues(OrganizationService, entityLogicalName, entityId , related1ToMEntitySchemaName, 
                isLookup , lookupSchemaName, entityIdValue, entitySchemaName,
                isOptionSet, optionSetCodeValue, optionSetSchemaName,
                isTwoOption , twoOptionCodeValue, 
                isString, stringCodeValue,
                isDate,  dateCodeValue, 
                isCurrency, currencyValue 
                , Tracer);
            
           
            #endregion
        }

       
    }
}
