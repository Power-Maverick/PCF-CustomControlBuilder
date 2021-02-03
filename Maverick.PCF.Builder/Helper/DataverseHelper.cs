using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Maverick.PCF.Builder.Helper
{
    public class DataverseHelper
    {
        /// <summary>
        /// Retrieve list of unmanaged and visible solutions
        /// </summary>
        /// <param name="orgService"></param>
        /// <returns></returns>
        public static EntityCollection RetrieveSolutions(IOrganizationService orgService)
        {
            QueryExpression querySolution = new QueryExpression
            {
                EntityName = "solution",
                ColumnSet = new ColumnSet(new string[] { "publisherid", "installedon", "version", "friendlyname", "uniquename", "description" }),
                Criteria = new FilterExpression()
            };

            querySolution.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
            querySolution.Criteria.AddCondition("isvisible", ConditionOperator.Equal, true);
            LinkEntity linkPublisher = querySolution.AddLink("publisher", "publisherid", "publisherid");
            linkPublisher.Columns = new ColumnSet("customizationprefix", "uniquename", "friendlyname");
            linkPublisher.EntityAlias = "pub";

            return orgService.RetrieveMultiple(querySolution);
        }

        /// <summary>
        /// Exports solution as unmanaged
        /// </summary>
        /// <param name="orgService"></param>
        /// <returns></returns>
        public static byte[] ExportSolution(IOrganizationService orgService, string solutionName)
        {
            ExportSolutionRequest exportSolutionRequest = new ExportSolutionRequest();
            exportSolutionRequest.Managed = false;
            exportSolutionRequest.SolutionName = solutionName;

            ExportSolutionResponse exportSolutionResponse = (ExportSolutionResponse)orgService.Execute(exportSolutionRequest);

            byte[] exportXml = exportSolutionResponse.ExportSolutionFile;

            return exportXml;
        }
    }
}
