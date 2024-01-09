namespace GameLibrary.Test
{
    public class TreasureChestTest
    {
        [Fact]
        public void CanOpenTest()
        {
            var chest = new TreasureChest(true);

            var result = chest.CanOpen(true);

            Assert.True(result);
        }
    }
}