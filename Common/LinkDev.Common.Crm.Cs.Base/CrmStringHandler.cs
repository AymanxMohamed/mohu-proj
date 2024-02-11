using Microsoft.CSharp;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Maan.Core.Helper
{
    public class CrmStringHandler
    {

        public enum NodeType { PureCrm, PureCSharp };
        #region Variables

        char starting_pattern;

        char ending_pattern;

        char variables_separator;

        char csharp_starting_pattern;

        char csharp_ending_pattern;
        Node parent;

        IOrganizationService OrganizationService { get; set; }

        #endregion

        #region Constructors
        public CrmStringHandler(EntityReference contextEntity, IOrganizationService organizationService)
        {
            parent = new Node();
            parent.attribute = contextEntity;
            starting_pattern = '$';
            ending_pattern = '$';
            csharp_starting_pattern = '@';
            csharp_ending_pattern = '@';
            variables_separator = ':';
            OrganizationService = organizationService;
        }


        #endregion

        #region public

        public static string Substitute(EntityReference context_entity, string input, IOrganizationService organizationService)
        {
            var parameters_to_eveluate = new Dictionary<Tuple<string, Guid>, Entity>();
            var manger = new CrmStringHandler(context_entity, organizationService);
            string output = string.Empty;

            var match_valid_special_parameters =
                manger.ExtractVariables(input);

            foreach (var item in match_valid_special_parameters)
            {
                manger.Insert(item);
            }

            manger.ExecuteRetrieve();

            var i = 0;
            foreach (var item in match_valid_special_parameters)
            {
                var remove_special_char = item.Item2.Replace("$", "");

                var orginal_pattern = @"$" + remove_special_char + @"$";
                var converted_node = manger.Search(remove_special_char);
                var result = "";
                if (converted_node != null)
                {
                    result = converted_node.ToString();

                }

                while (true)
                {
                    if (i < item.Item1)
                    {
                        if (input[i] == '\\' && i + 1 < input.Length && input[i + 1] == '$')
                            i++;
                        else
                        {
                            output += input[i];
                            i++;
                        }
                    }
                    else
                    {
                        output += result;
                        i = item.Item1 + orginal_pattern.Length;
                        break;
                    }
                }
            }

            output = i < input.Length ? output + input.Substring(i, input.Length - i) : output + "";
            return output;
        }


        public static object SubstituteToAttribute(EntityReference context_entity, string input, IOrganizationService organizationService)
        {
            var parameters_to_eveluate = new Dictionary<Tuple<string, Guid>, Entity>();
            var manger = new CrmStringHandler(context_entity, organizationService);
            object output = null;
            input = input.StartsWith(manger.starting_pattern.ToString()) ? input : manger.starting_pattern + input;
            input = input.EndsWith(manger.ending_pattern.ToString()) ? input : input + manger.ending_pattern;

            var match_valid_special_parameters =
                manger.ExtractVariables(input);

            if (match_valid_special_parameters.Count > 1)
                throw new ArgumentOutOfRangeException("Found more than replacement");


            foreach (var item in match_valid_special_parameters)
            {
                manger.Insert(item);
            }

            manger.ExecuteRetrieve();

            var i = 0;
            foreach (var item in match_valid_special_parameters)
            {
                var remove_special_char = item.Item2.Replace("$", "");
                var orginal_pattern = @"$" + remove_special_char + @"$";
                var converted_node = manger.Search(remove_special_char);
                if (converted_node != null)
                {
                    output = converted_node.attribute;

                }

            }
            return output;
        }

        public static string ExtractString(string sentence, string startingPattern, string endingPattern)
        {
            var startIndx = -1;
            var endingIndx = -1;
            var output = string.Empty;
            if (!string.IsNullOrEmpty(sentence))
            {
                startIndx = sentence.IndexOf(startingPattern);
                if (startIndx >= 0)
                    endingIndx = sentence.IndexOf(endingPattern, startIndx);

                if (startIndx >= 0 && endingIndx >= 0)
                {
                    var startingPatternLengh = startingPattern.Length;

                    var sentenceStartIndx = startIndx + startingPatternLengh;
                    var sentenceLength = sentence.Length - (startIndx + startingPatternLengh) - (sentence.Length - endingIndx);

                    output = sentence.Substring(sentenceStartIndx, sentenceLength);
                }
            }
            return output;
        }

        public static string SubstituteWithCustomPattern(EntityReference context_entity, string input, string startingPattern, string endingPattern, IOrganizationService organizationService)
        {
            string output = input;
            while (true)
            {
                string extractedSentence = ExtractString(input, startingPattern, endingPattern);
                if (string.IsNullOrEmpty(extractedSentence))
                {
                    output = input;
                    break;
                }

                var substitutedSentence = Substitute(context_entity, extractedSentence, organizationService);
                if (!string.IsNullOrEmpty(substitutedSentence))
                    input = input.Replace(startingPattern + extractedSentence + endingPattern, substitutedSentence);
            }
            return output;
        }
        #endregion

        #region Private
        private Node Search(string key)
        {
            return Search(parent, key);
        }

        private Node Search(Node node_to_search_with, string pattern_to_be_evaluted)
        {
            if (pattern_to_be_evaluted == string.Empty)
            {
                return node_to_search_with;
            }

            var key = string.Empty;
            var variable = string.Empty;
            var rest_of_pattern_to_be_evaluted = string.Empty;

            if (!HasItCSharpCode(pattern_to_be_evaluted))
                ExtarctVariable(pattern_to_be_evaluted, out key, out variable, out rest_of_pattern_to_be_evaluted);
            else
            {
                key = pattern_to_be_evaluted.Replace("@", "");
            }



            foreach (var item in node_to_search_with.nodes)
            {
                if (item.Key == key)
                {
                    return Search(item.Value, rest_of_pattern_to_be_evaluted);
                }
            }

            return null;
        }

        private void Insert(Tuple<int, string, NodeType> pattern_to_be_evaluted)
        {
            var item2 = pattern_to_be_evaluted.Item2.Replace(starting_pattern, ' ').Replace(ending_pattern, ' ');
            item2 = item2.Replace(csharp_starting_pattern, ' ').Replace(csharp_ending_pattern, ' ');
            Insert(parent, pattern_to_be_evaluted.Item1, item2, pattern_to_be_evaluted.Item3);
        }

        private void Insert(Node node_to_search_with, int index, string pattern_to_be_evaluted, NodeType? nodeType)
        {
            if (pattern_to_be_evaluted == string.Empty)
            {
                return;
            }

            var variable = string.Empty;
            var key = string.Empty;
            var rest_of_pattern_to_be_evaluted = string.Empty;

            if (nodeType == NodeType.PureCrm)
                ExtarctVariable(pattern_to_be_evaluted, out key, out variable, out rest_of_pattern_to_be_evaluted);
            else
            {
                key = variable = pattern_to_be_evaluted.TrimEnd(' ').TrimStart(' ');
            }

            foreach (var item in node_to_search_with.nodes)
            {
                if (item.Key == variable)
                {
                    Insert(item.Value, index, rest_of_pattern_to_be_evaluted, nodeType);
                }

            }

            // if come here, then it search all child of the given node
            // but no node found with same key, then crate the node and continue
            // with rest_of_pattern_to_be_evaluted
            var nodeToCreate = new Node(index, variable, rest_of_pattern_to_be_evaluted, nodeType.Value);

            if (nodeType == NodeType.PureCSharp)
                nodeToCreate.attribute = variable;

            node_to_search_with.LinkTo(nodeToCreate);
            Insert(nodeToCreate, index, rest_of_pattern_to_be_evaluted, NodeType.PureCrm);

        }

        private void ExecuteRetrieve()
        {
            ExecuteRetrieve(parent);
        }

        private void ExecuteRetrieve(Node parent)
        {
            var entity_refrence_that_hold_attribute = parent.attribute as EntityReference;
            var column = new ColumnSet();
            foreach (var item in parent.nodes)
            {
                if (item.Value.nodeType == NodeType.PureCrm)
                    column.AddColumn(item.Key);
            }

            if (column.Columns.Count <= 0)
                return;

            if (!(entity_refrence_that_hold_attribute is EntityReference))
            {
                //"'{0}' this field is not entity to retreive from it"
                parent.nodes.Clear();
                return;
            }
            var retrived_entity =
                OrganizationService.Retrieve(entity_refrence_that_hold_attribute.LogicalName, entity_refrence_that_hold_attribute.Id, column);

            var unevaluated_nodes = new List<string>();
            foreach (var node in parent.nodes)
            {
                if (node.Value.nodeType == NodeType.PureCrm)
                {

                    if (retrived_entity.Contains(node.Key))
                    {
                        node.Value.attribute = retrived_entity[node.Key];
                    }
                    else
                        unevaluated_nodes.Add(node.Key);
                }
            }

            // Remove
            foreach (var item in unevaluated_nodes)
            {
                parent.nodes.Remove(item);
            }

            foreach (var item in parent.nodes)
            {
                ExecuteRetrieve(item.Value);
            }

        }

        /// <summary>
        /// Extract any word between the starting_pattern and ending_pattern to evaluate them later
        /// Ex: 
        /// if the starting_pattern and the ending_pattern are $ then below will be applied
        /// 1) someTextBefore$regardingobjectid:ldv_contact:lastname$someTextAfter
        /// 2) someTextBefore$regardingobjectid:ldv_contact:lastname.ToLower()$someTextAfter
        /// </summary>
        /// <param name="input">Full text that need to extract the special sentences</param>
        /// <returns></returns>
        private List<Tuple<int, string, NodeType>> ExtractVariables(string input)
        {
            int? stack_starting_index = null;


            var matches_list = new List<Tuple<int, string, NodeType>>();
            for (int i = 0; i < input.Length; i++)
            {
                // escaping?
                if (input[i] == '\r' || input[i] == '\n')
                {
                    i++;

                    if (stack_starting_index != null)
                        //$"Syntax error, missing '{ending_pattern}' at index {i} \r\n"

                        stack_starting_index = null;
                    continue;
                }
                if (input[i].Equals('\\'))
                {
                    i++;
                    continue;
                }
                else if (stack_starting_index == null && input[i].Equals(starting_pattern))
                {
                    stack_starting_index = i;
                }

                else if (stack_starting_index != null && (input[i].Equals(ending_pattern)))
                {
                    // ex: $ plaplapla $
                    var subString = input.Substring(stack_starting_index.Value, i - stack_starting_index.Value + 1);

                    if (HasItCSharpCode(subString))
                    {
                        matches_list.Add(new Tuple<int, string, NodeType>(stack_starting_index.Value, subString, NodeType.PureCSharp));
                    }
                    else
                    {
                        matches_list.Add(new Tuple<int, string, NodeType>(stack_starting_index.Value, subString, NodeType.PureCrm));
                    }


                    // to get another
                    stack_starting_index = null;
                }
            }

            return matches_list;
        }

        private bool HasItCSharpCode(string input)
        {
            int? csharp_starting_index = null;
            var isCSharpCode = false;
            // Look for somthing like this @DateTime.Now.Year - 1@
            for (int i = 0; i < input.Length; i++)
            {

                if (csharp_starting_index == null && input[i].Equals(csharp_starting_pattern))
                {
                    csharp_starting_index = i;
                }
                else if (csharp_starting_index != null && input[i].Equals(csharp_ending_pattern))
                {
                    isCSharpCode = true;
                }
            }

            return isCSharpCode;
        }
        private void ExtarctVariable(string input, out string key, out string variable, out string rest_of_input)
        {
            var matches_list = new List<string>();
            key = string.Empty;
            variable = string.Empty;
            rest_of_input = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                // escaping?
                if (input[i].Equals('\\'))
                {
                    i++;
                    continue;
                }
                else if (input[i].Equals(variables_separator))
                {
                    // Get the word before the separator
                    //
                    variable = input.Substring(0, i);
                    variable = variable.TrimEnd(' ').TrimStart(' ');
                    rest_of_input = input.Substring(i + 1, input.Length - i - 1);
                    break;
                }
                else if (i == input.Length - 1)
                {
                    variable = input;
                    variable = variable.TrimEnd(' ').TrimStart(' ');
                    break;
                }
            }

            key = variable.Split('.')[0];
        }

        public string GetValueFromEntity(string entityLogicalName, string fieldLogicalName, string value, string returnedFieldlogicalname)
        {
            var returnedValue = string.Empty;
            var fetchXml =
                @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                      <entity name='" + entityLogicalName + @"'>
                        <attribute name='" + returnedFieldlogicalname + @"' />
                        <filter type='and'>
                          <condition attribute='" + fieldLogicalName + @"'  operator='eq' value='" + value + @"' />
                        </filter>
                      </entity>
                    </fetch>";

            var retrievedEntities =
                OrganizationService.RetrieveMultiple(new FetchExpression(fetchXml));

            if (retrievedEntities.Entities.Count <= 0)
                throw new InvalidFilterCriteriaException($"Didn't find record with following criteria entity logical name '{entityLogicalName}' field logical name to search on '{fieldLogicalName}' with value '{value}'");
            else if (retrievedEntities.Entities.Count > 1)
                throw new ArgumentOutOfRangeException($"Found more than one record with following criteria entity logical name '{entityLogicalName}' field logical name to search on '{fieldLogicalName}' with value '{value}' it should be only one unique record to evaluate it in fetchXml");

            if (!retrievedEntities.Entities[0].Contains(returnedFieldlogicalname))
                throw new KeyNotFoundException($"Record with following criteria entity logical name '{entityLogicalName}' field logical name to search on '{fieldLogicalName}' with value '{value}' has no attribute with logical name {returnedFieldlogicalname} or it seems to be null");

            var node = new Node(retrievedEntities.Entities[0][returnedFieldlogicalname]);
            returnedValue = node.ToString();
            return returnedValue;
        }
        #endregion

        public class Node
        {
            public Dictionary<string, Node> nodes;

            public string key { get { return nodeType == NodeType.PureCrm ? variable.Split('.')[0] : variable; } }
            public string variable_without_escape_char { get { return variable.Replace("\\", ""); } }
            public string variable;
            public string pattern_to_be_evaluted;
            public object attribute;
            public int index;
            public NodeType nodeType;

            public Node()
                : this(0, null, null, new object(), NodeType.PureCrm)
            {

            }

            public Node(int index, string Key, string Pattern_to_be_evaluted, NodeType NodeType)
                : this(index, Key, Pattern_to_be_evaluted, new object(), NodeType)
            {
            }

            public Node(int Index, string Key, string Pattern_to_be_evaluted, object Attribute, NodeType NodeType)
            {
                index = Index;
                variable = Key;
                pattern_to_be_evaluted = Pattern_to_be_evaluted;
                attribute = Attribute;
                nodeType = NodeType;
                nodes = new Dictionary<string, Node>();
            }

            public Node(object Attribute)
            {
                attribute = Attribute;
                index = -1;
                variable = string.Empty;
                nodes = new Dictionary<string, Node>();
            }

            public void LinkTo(Node node)
            {
                if (!nodes.ContainsKey(node.key))
                {
                    nodes.Add(node.key, node);
                }
            }

            public override string ToString()
            {
                var value = "";
                if (attribute is String) //String
                {
                    if (nodeType == NodeType.PureCrm)
                    {
                        if (variable_without_escape_char.Contains("."))
                        {
                            var splitted = variable_without_escape_char.Split('.');
                            if (splitted.Length > 1)
                                value = EvaluteRuntimeCode(attribute, splitted[1]);
                        }
                        else
                            value = attribute.ToString();
                    }
                    else if (nodeType == NodeType.PureCSharp)
                    {
                        value = EvaluteRuntimeCSharpCode((string)attribute);
                    }
                }

                else if (attribute is OptionSetValue) //OptionSet
                    value = (attribute as OptionSetValue).Value.ToString();

                else if (attribute is DateTime) //DateTime
                {
                    if (variable_without_escape_char.Contains("."))
                    {
                        var splitted = variable_without_escape_char.Split('.');
                        if (splitted.Length > 1)
                            value = EvaluteRuntimeCode(attribute, splitted[1]);
                    }
                    else
                        value = ((DateTime)attribute).ToLocalTime().ToString();
                }

                else if (attribute is BooleanManagedProperty) //Boolean
                    value = ((BooleanManagedProperty)attribute).Value.ToString();


                else if (attribute is float) //float
                    value = ((float)attribute).ToString();

                else if (attribute is int) //Integer
                    value = ((int)attribute).ToString();

                else if (attribute is EntityReference) //Lookup
                    value = ((EntityReference)attribute).Name;

                else if (attribute is Money) //Crm Money
                    value = ((Money)attribute).Value.ToString();

                else if (attribute is decimal) //Decimal
                    value = ((decimal)attribute).ToString();

                else if (attribute is Guid) //Decimal
                    value = ((Guid)attribute).ToString();

                else
                {
                    if (variable_without_escape_char.Contains("."))
                    {
                        var splitted = variable_without_escape_char.Split('.');
                        if (splitted.Length > 1)
                            value = EvaluteRuntimeCode(attribute.ToString(), splitted[1]);
                    }
                    else
                        value = attribute.ToString();
                }

                return value;
            }

            private MethodInfo CreateRuntimeFunction(object objectType, string pattern)
            {
                string code = @"
                using System;
            
                namespace UserFunctions
                {                
                    public class BinaryFunction
                    {                
                        public static string Function(object x)
                        {
                            return pattern;
                        }
                    }
                }
            ";
                code = code.Replace("object", objectType.GetType().ToString());
                code = code.Replace("pattern", "x" + pattern);
                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), code);

                Type binaryFunction = results.CompiledAssembly.GetType("UserFunctions.BinaryFunction");
                return binaryFunction.GetMethod("Function");
            }

            private string EvaluteRuntimeCode(object attribute, string codeToEvalute)
            {
                codeToEvalute =
                    codeToEvalute.StartsWith(".") ? codeToEvalute : codeToEvalute.Insert(0, ".");

                var function = CreateRuntimeFunction(
                    attribute, codeToEvalute);


                var result = "";

                if (attribute is String) //String
                {
                    var betterFunction =
                      (Func<string, string>)Delegate.CreateDelegate(typeof(Func<string, string>), function);

                    result = betterFunction(attribute as string);
                }

                else if (attribute is OptionSetValue) //OptionSet
                {
                    var betterFunction =
                      (Func<OptionSetValue, string>)Delegate.CreateDelegate(typeof(Func<OptionSetValue, string>), function);

                    result = betterFunction(attribute as OptionSetValue);
                }

                else if (attribute is DateTime) //DateTime
                {
                    var betterFunction =
                      (Func<DateTime, string>)Delegate.CreateDelegate(typeof(Func<DateTime, string>), function);

                    result = betterFunction((DateTime)attribute);
                }

                else if (attribute is BooleanManagedProperty) //Boolean
                {
                    var betterFunction =
                      (Func<BooleanManagedProperty, string>)Delegate.CreateDelegate(typeof(Func<BooleanManagedProperty, string>), function);

                    result = betterFunction((BooleanManagedProperty)attribute);
                }

                else if (attribute is float) //float
                {
                    var betterFunction =
                      (Func<float, string>)Delegate.CreateDelegate(typeof(Func<float, string>), function);

                    result = betterFunction((float)attribute);
                }

                else if (attribute is int) //Integer
                {
                    var betterFunction =
                      (Func<int, string>)Delegate.CreateDelegate(typeof(Func<int, string>), function);

                    result = betterFunction((int)attribute);
                }

                else if (attribute is EntityReference) //Lookup
                {
                    var betterFunction =
                      (Func<EntityReference, string>)Delegate.CreateDelegate(typeof(Func<EntityReference, string>), function);

                    result = betterFunction((EntityReference)attribute);
                }

                else if (attribute is Money) //Crm Money
                {
                    var betterFunction =
                      (Func<Money, string>)Delegate.CreateDelegate(typeof(Func<Money, string>), function);

                    result = betterFunction((Money)attribute);
                }

                else if (attribute is decimal) //Decimal
                {
                    var betterFunction =
                      (Func<decimal, string>)Delegate.CreateDelegate(typeof(Func<decimal, string>), function);

                    result = betterFunction((decimal)attribute);
                }

                else if (attribute is Guid) //Decimal
                {
                    var betterFunction =
                      (Func<Guid, string>)Delegate.CreateDelegate(typeof(Func<Guid, string>), function);

                    result = betterFunction((Guid)attribute);
                }



                return result;

            }

            private MethodInfo EvaluateCSharpCode(string pattern)
            {

                string code = @"
                using System;
            
                namespace UserFunctions
                {                
                    public class BinaryFunction
                    {                
                        public static string Function()
                        {
                            try
                            {
                                return (pattern).ToString();
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }
                           
                        }
                    }
                }";

                code = code.Replace("pattern", pattern);
                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), code);

                Type binaryFunction = results.CompiledAssembly.GetType("UserFunctions.BinaryFunction");
                return binaryFunction.GetMethod("Function");
            }

            private string EvaluteRuntimeCSharpCode(string codeToEvalute)
            {
                var function = EvaluateCSharpCode(codeToEvalute);

                var betterFunction =
                       (Func<string>)Delegate.CreateDelegate(typeof(Func<string>), function);

                return betterFunction();
            }

        }

    }
}
