using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodePruner.Examples.TypeScriptCodeGenerators
{
    public class TypeScriptContentGenerator
    {
        public string GenerateModel(string className, IEnumerable<BackendField> backendFields)
        {
            var sb = new StringBuilder();
            sb.Append($"export type {className} {{");
            foreach (var backendField in backendFields)
            {
                var frontendType = GetFrontendType(backendField);
                sb.Append($"  {backendField.Name}: {frontendType}");
            }

            sb.Append($"  ");
            sb.Append("}");

            return sb.ToString();
        }

        private string GetFrontendType(BackendField backendField)
        {
            switch (backendField.Type)
            {
                case "Int32":
                    return "number";
                case "String":
                    return "string";
            }

            return backendField.Type;
        }
    }
}
