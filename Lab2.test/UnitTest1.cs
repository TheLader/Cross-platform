namespace Lab2.test;

public class UnitTest1
{
    [Fact]
    public void ConvertStringToAgent_ValidInput_ReturnsSortedAgents()
    {
        // Arrange
        string[] input = new[]
        {
            "3",
            "5000 4 5500 3 6000 2"
        };

        // Act
        var result = Program.ConvertStringToAgent(input);

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Equal(5000, result[0].Age);
        Assert.Equal(4, result[0].Risk);
        Assert.Equal(5500, result[1].Age);
        Assert.Equal(3, result[1].Risk);
        Assert.Equal(6000, result[2].Age);
        Assert.Equal(2, result[2].Risk);
    }

    [Fact]
    public void CalculateRisk_ValidInput_CalculatesTotalRiskCorrectly()
    {
        // Arrange
        var agents = new Program.Agent[]
        {
            new Program.Agent { Age = 5000, Risk = 4 },
            new Program.Agent { Age = 5500, Risk = 3 },
            new Program.Agent { Age = 6000, Risk = 2 }
        };

        // Act
        int totalRisk = Program.CalculateRisk(agents.Length, agents);

        // Assert
        Assert.Equal(5, totalRisk); // 3 + 2 = 5
    }

    [Fact]
    public void CalculateRisk_SingleAgent_ReturnsZeroRisk()
    {
        // Arrange
        var agents = new Program.Agent[]
        {
            new Program.Agent { Age = 5000, Risk = 4 }
        };

        // Act
        int totalRisk = Program.CalculateRisk(agents.Length, agents);

        // Assert
        Assert.Equal(0, totalRisk); // Для одного агента ризик дорівнює 0
    }
}