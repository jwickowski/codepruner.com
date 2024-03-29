using System;
using Xunit;
using Shouldly;
using System.Linq;

namespace CodePruner.Examples.TypeScriptCodeGenerators.UnitTests
{
    public class TypeScriptModelGeneratorTests
    {
        [Fact]
        public void it_should_generate_SampleClass_in_ts()
        {
            var typeScriptModelGenerator = new TypeScriptModelGenerator();
            var result = typeScriptModelGenerator.GenerateTypeScriptModel(typeof(SampleClass));

            var expectedClass =
@"export type SampleClass {
  AnInt: number;
  AString: string;
  AFloat: number;
  ADateTime: Date;
}
";

            result.ShouldBe(expectedClass);
        }
    }

    internal class SampleClass
    {
        public int AnInt { get; set; }
        public string AString{ get; set; }
        public float AFloat{ get; set; }
        public DateTime ADateTime { get; set; }
    }
}
