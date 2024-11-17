using Chess;
using Xunit;

namespace Test
{
    public class Test
    {
        [Theory]
        [InlineData("a1", "b3", 1, true)]
        [InlineData("a1", "c5", 2, true)]
        [InlineData("b4", "c5", 2, true)]
        [InlineData("a1", "h1", 2, false)]
        public void ValidInput(string From, string To, int Steps, bool CanMove)
        {
            if (Steps == 1)
                Assert.True(Program.CanReachInOneMove(Program.StringToVector(From), Program.StringToVector(To)) == CanMove);
            else if (Steps == 2)
                Assert.True(Program.CanReachInTwoMove(Program.StringToVector(From), Program.StringToVector(To)) == CanMove);
            else
                Assert.True(Program.CanReachInOneMove(Program.StringToVector(From), Program.StringToVector(To)) || Program.CanReachInTwoMove(Program.StringToVector(From), Program.StringToVector(To)));
        }
    }
}
