using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace CodePruner.Examples.AI.ExploreSemanticKernel.Plugins;

public class BikeApiPlugin
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
    
    [SKFunction, Description("Get list of bikes for me based on passed criteria")]
    public string BikeForMe(
        [Description("The size of the bike that is fine for me")] string bikeSize,
        [Description("The type o bike I want")] string bikeType)
    {
        
        return $"You should get a {bikeSize} {bikeType} bike";
    }
}
