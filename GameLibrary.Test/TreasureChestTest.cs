using Xunit.Abstractions;

namespace GameLibrary.Test
{
    public class TreasureChestTest : IDisposable
    {
        private readonly Stack<TreasureChest> chests;
        private readonly ITestOutputHelper output;
        

        public TreasureChestTest(ITestOutputHelper output)
        {
            chests = new();
            this.output = output;

            output.WriteLine($"Initial chest count :{chests.Count}");
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, true)]
        [InlineData(false, false, true)]
        public void CanOpen_WhenCalled_ReturnsExpectedOutCome(bool isLocked, bool hasKey, bool expected)
        {
            var chest = new TreasureChest(isLocked);

            chests.Push(chest);
            output.WriteLine($"Chest count: {chests.Count}");

            var actual = chest.CanOpen(hasKey);

            Assert.Equal(expected, actual);
            Assert.Single(chests);
        }

        [Fact(Skip = "This test is not ready yet")]
        public void CanOpen_ChestIsLockedAndHasKey_ReturnsTrue()
        {
            var chest = new TreasureChest(true);

            chests.Push(chest);
            output.WriteLine($"Chest count: {chests.Count}");

            var result = chest.CanOpen(true);

            Assert.True(result);
            Assert.Single(chests);
        }

        [Fact]
        public void CanOpen_ChestIsLockedAndHasNoKey_ReturnsFalse()
        {
            var chest = new TreasureChest(true);
            chests.Push(chest);
            output.WriteLine($"Chest count: {chests.Count}");

            var result = chest.CanOpen(false);

            Assert.False(result);
            Assert.Single(chests);
        }

        [Fact]
        public void CanOpen_ChestIsUnlockedAndHasKey_ReturnsTrue()
        {
            var chest = new TreasureChest(false);
            chests.Push(chest);
            output.WriteLine($"Chest count: {chests.Count}");

            var result = chest.CanOpen(true);

            Assert.True(result);
            Assert.Single(chests);
        }

        [Fact]
        public void CanOpen_ChestIsUnlockedAndHasNoKey_ReturnsTrue()
        {
            var chest = new TreasureChest(false);
            chests.Push(chest);
            output.WriteLine($"Chest count: {chests.Count}");

            var result = chest.CanOpen(false);

            Assert.True(result);
            Assert.Single(chests);
        }

        public void Dispose()
        {
            chests.Pop();
            Assert.Empty(chests);
            output.WriteLine($"Final chest count: {chests.Count}");
        }
    }
}