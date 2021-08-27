using System;
using System.IO;

namespace CodePruner.Examples.TypeScriptCodeGenerators.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start: CodePruner.Examples.TypeScriptCodeGenerators");
            var typeScriptModelGenerator = new TypeScriptModelGenerator();
            var content = typeScriptModelGenerator.GenerateTypeScriptModel(typeof(AccountDto));
            File.WriteAllText("AccountDto.generated.ts", content);
        }
    }

    public class AccountDto
    {
        public int AccountId { get; set; }
        public string DisplayName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
