using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace CodePruner.Examples.AI.ExploreSemanticKernel.Plugins;

public class BikeSizePlugin
{
    [SKFunction, Description("Calculate what size of bike will fit you")]
    public string CalculateBikeSize([Description("Person height in centimeters")] double height)
    {
        if (height < 160)
        {
            return "S";
        }
        else if (height < 180)
        {
            return "M";
        }
        else
        {
            return "L";
        }
    }
}
