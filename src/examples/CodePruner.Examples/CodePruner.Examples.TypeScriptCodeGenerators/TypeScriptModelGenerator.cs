using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePruner.Examples.TypeScriptCodeGenerators
{
    public class TypeScriptModelGenerator
    {
        private BackendFieldGetter backendFieldGetter;
        private TypeScriptContentGenerator typeScriptContentGenerator;

        public TypeScriptModelGenerator()
        {
            backendFieldGetter = new BackendFieldGetter();
            typeScriptContentGenerator = new TypeScriptContentGenerator();
        }

        public string GenerateTypeScriptModel(Type type)
        {
            string typeName = type.Name;
            var backendFields = backendFieldGetter.GetBackendField(type);
            var content = typeScriptContentGenerator.GenerateModel(typeName, backendFields);
            return content;
        }
    }
}
