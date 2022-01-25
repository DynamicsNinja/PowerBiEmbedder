using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;

namespace Fic.XTB.PowerBiEmbedder.Helper
{
    public static class MetadataHelper
    {
        public static String[] attributeProperties = { "DisplayName", "Description", "AttributeType", "IsManaged", "IsCustomizable", "IsCustomAttribute", "IsValidForCreate", "IsPrimaryName", "SchemaName", "AutoNumberFormat", "MaxLength" };
        public static String[] entityDetails = { "Attributes" };
        public static String[] entityProperties = { "LogicalName", "DisplayName", "DisplayCollectionName", "PrimaryNameAttribute", "ObjectTypeCode", "IsManaged", "IsCustomizable", "IsCustomEntity", "IsIntersect", "IsValidForAdvancedFind" };

        public static RetrieveMetadataChangesResponse LoadEntities(IOrganizationService service)
        {
            if (service == null)
            {
                return null;
            }
            var eqe = new EntityQueryExpression();
            eqe.Properties = new MetadataPropertiesExpression(entityProperties);
            var req = new RetrieveMetadataChangesRequest()
            {
                Query = eqe,
                ClientVersionStamp = null
            };
            return service.Execute(req) as RetrieveMetadataChangesResponse;
        }

        public static RetrieveMetadataChangesResponse LoadEntityDetails(IOrganizationService service, string entityName)
        {
            if (service == null)
            {
                return null;
            }
            var eqe = new EntityQueryExpression();
            eqe.Properties = new MetadataPropertiesExpression(entityProperties);
            eqe.Properties.PropertyNames.AddRange(entityDetails);
            eqe.Criteria.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityName));
            var aqe = new AttributeQueryExpression();
            aqe.Properties = new MetadataPropertiesExpression(attributeProperties);
            eqe.AttributeQuery = aqe;
            var req = new RetrieveMetadataChangesRequest()
            {
                Query = eqe,
                ClientVersionStamp = null
            };
            return service.Execute(req) as RetrieveMetadataChangesResponse;
        }
    }
}
