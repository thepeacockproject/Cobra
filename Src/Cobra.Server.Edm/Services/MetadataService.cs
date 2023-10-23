using System.Reflection;
using Cobra.Server.Edm.Attributes;
using Cobra.Server.Edm.Enums;
using Cobra.Server.Edm.Interfaces;
using Cobra.Server.Edm.Models;
using Cobra.Server.Edm.Models.Base;

namespace Cobra.Server.Edm.Services
{
    public abstract class MetadataService : IMetadataService
    {
        private OSMetadata _metadata;

        protected void BuildMetadata(string schemaNamespace)
        {
            var entityTypes = BuildEdmEntityTypes(GetEdmEntityTypes());
            var importFunctions = BuildEdmImportFunctions(GetEdmFunctionImports());

            _metadata = new OSMetadata
            {
                ClientConfiguration = new EdmClientConfiguration
                {
                    UsageTrackingEnabled = false
                    //UsageTrackingSamplingInterval = 60_000,
                    //UsageTrackingMetricsInterval = 60_000,
                    //MetricsThreshold = 0,
                    //MetricsPriorityThreshold = 0
                },
                Schemas = new List<OSMetadata.Schema>
                {
                    new()
                    {
                        Namespace = schemaNamespace,
                        EntityTypes = entityTypes,
                        //ComplexTypes = null,
                        //Associations = null,
                        EntityContainers = new List<OSMetadata.EntityContainer>
                        {
                            new()
                            {
                                FunctionImports = importFunctions
                            }
                        }
                    }
                }
            };
        }

        private static List<EdmEntityType> BuildEdmEntityTypes(List<Type> edmEntityTypes)
        {
            return edmEntityTypes
                .Where(x =>
                    typeof(IEdmEntity).IsAssignableFrom(x) &&
                    x.IsDefined(typeof(EdmEntityAttribute))
                )
                .Select(entityType =>
                {
                    var entityTypeAttribute = entityType.GetCustomAttribute<EdmEntityAttribute>()!;

                    var propertyAttributes = entityType.GetProperties()
                        .Where(x => x.IsDefined(typeof(EdmPropertyAttribute)))
                        .Select(x => x.GetCustomAttribute<EdmPropertyAttribute>())
                        .ToList();

                    return new EdmEntityType
                    {
                        Name = entityTypeAttribute.Name,
                        Properties = propertyAttributes.Select(x => new EdmProperty
                        {
                            Name = x.Name,
                            Type = x.Type,
                            Nullable = x.Nullable
                        }).ToList()
                    };
                })
                .ToList();
        }

        private static List<EdmFunctionImport> BuildEdmImportFunctions(List<Type> edmImportFunctions)
        {
            //NOTE: Most likely unused
            var functionNames = new List<string>
            {
                "DecreaseConsumables",
                "IncreaseConsumables",
                "consumables", //EntitySet
                "transactions" //EntitySet
            };

            var functions = functionNames.Select(x => new EdmFunctionImport
            {
                Name = x,
                HttpMethod = HttpMethods.POST
            }).ToList();

            var generatedFunctions = edmImportFunctions
                .Where(x =>
                    typeof(IEdmFunctionImport).IsAssignableFrom(x) &&
                    x.IsDefined(typeof(EdmFunctionImportAttribute))
                )
                .Select(entityType =>
                {
                    var functionImportAttribute = entityType.GetCustomAttribute<EdmFunctionImportAttribute>()!;

                    var functionParameterAttributes = entityType.GetProperties()
                        .Where(x => x.IsDefined(typeof(SFunctionParameterAttribute)))
                        .Select(x => x.GetCustomAttribute<SFunctionParameterAttribute>())
                        .ToList();

                    return new EdmFunctionImport
                    {
                        Name = functionImportAttribute.Name,
                        HttpMethod = functionImportAttribute.HttpMethod,
                        ReturnType = functionImportAttribute.ReturnType,
                        Parameters = functionParameterAttributes.Select(x => new SFunctionParameter
                        {
                            Name = x.Name,
                            Type = x.Type
                        }).ToList()
                    };
                })
                .ToList();

            functions.AddRange(generatedFunctions);

            return functions;
        }

        protected abstract List<Type> GetEdmEntityTypes();
        protected abstract List<Type> GetEdmFunctionImports();

        public OSMetadata GetMetadata()
        {
            return _metadata;
        }
    }
}
