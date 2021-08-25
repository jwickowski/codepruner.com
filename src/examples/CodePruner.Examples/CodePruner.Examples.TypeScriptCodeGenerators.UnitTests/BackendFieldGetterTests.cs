using System;
using Xunit;
using Shouldly;
using System.Linq;

namespace CodePruner.Examples.TypeScriptCodeGenerators.UnitTests
{
    public class BackendFieldGetterTests
    {
        [Fact]
        public void it_should_return_all_fields()
        {
            var backendFieldGetter = new BackendFieldGetter();
            var result = backendFieldGetter.GetBackendField(typeof(SampleClass)).ToList();

            result.Count.ShouldBe(3);
            result.Select(x => x.Name).ShouldBe(new[] { nameof(SampleClass.AnInt), nameof(SampleClass.AFloat), nameof(SampleClass.AString) }, ignoreOrder: true);
        }
    }

    internal class SampleClass
    {
        public int AnInt { get; set; }
        public string AString{ get; set; }
        public float AFloat{ get; set; }
    }
}
