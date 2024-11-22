public class ParticleProcessorTests
{
    [Theory]
    [MemberData(nameof(GetTestData))]
    public void ProcessParticles_ShouldReturnCorrectFinalStates(int n, List<int> particles, bool[,] destructionMatrix, List<List<int>> expectedFinalStates)
    {
        // Act
        var finalStates = ParticleProcessor.ProcessParticles(n, particles, destructionMatrix);

        // Assert
        Assert.Equal(expectedFinalStates.Count, finalStates.Count);
        foreach (var expectedState in expectedFinalStates)
        {
            Assert.Contains(expectedState, finalStates);
        }
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[]
        {
                3,
                new List<int> { 1, 1, 2 },
                new bool[,] {
                    { false, false, true },
                    { true, false, false },
                    { true, true, true }
                },
                new List<List<int>>
                {
                    new List<int> { 0, 1, 0 },
                    new List<int> { 0, 0, 1 },
                    new List<int> { 1, 0, 0 }
                }
        };

        yield return new object[]
        {
                1,
                new List<int> { 2 },
                new bool[,] { { false } },
                new List<List<int>>
                {
                    new List<int> { 2 }
                }
        };
    }
}