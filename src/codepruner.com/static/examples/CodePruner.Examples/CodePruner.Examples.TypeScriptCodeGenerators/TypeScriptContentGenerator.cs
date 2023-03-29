using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodePruner.Examples.TypeScriptCodeGenerators
{
    internal class TypeScriptContentGenerator
    {
        internal string GenerateModel(string className, IEnumerable<BackendField> backendFields)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"export type {className} {{");
            foreach (var backendField in backendFields)
            {
                var frontendType = GetFrontendType(backendField);
                sb.AppendLine($"  {backendField.Name}: {frontendType};");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string GetFrontendType(BackendField backendField)
        {
            switch (backendField.Type)
            {
                case "Int32":
                case "Single":
                    return "number";
                case "String":
                    return "string";
                case "DateTime":
                    return "Date";
            }

            return backendField.Type;
        }
    }
}
