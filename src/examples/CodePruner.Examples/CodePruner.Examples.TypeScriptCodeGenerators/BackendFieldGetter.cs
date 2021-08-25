using System;
using System.Collections.Generic;

namespace CodePruner.Examples.TypeScriptCodeGenerators
{
    public class BackendFieldGetter
    {
        public IEnumerable<BackendField> GetBackendField(Type sourceType)
        {
            var properties = sourceType.GetProperties();
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType.Name;
                var propertyName = property.Name;
                var backendField = new BackendField
                {
                    Name = propertyName,
                    Type = propertyType
                };

                yield return backendField;
            }
        }
    }
}
