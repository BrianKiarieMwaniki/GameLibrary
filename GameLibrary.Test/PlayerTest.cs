using FluentAssertions;

namespace GameLibrary.Test
{
    public class PlayerTest
    {
        [Fact]
        public void IncreaseLevel_WhenCalled_HasExpectedLevel()
        {
            // Arrange
            var player = new Player("Alice", 1, DateTime.Now);

            // Act
            player.IncreaseLevel();

            // Assert
            player.Level.Should().Be(2);
            player.Level.Should().BeGreaterThan(1);
        }

        [Fact]
        public void Greet_ValidGreeting_ReturnsGreetingWithName()
        {
            // Arrange
            var player = new Player("Alice", 1, DateTime.Now);

            // Act 
            var result = player.Greet("Hello");

            // Assert
           result.Should().Be("Hello, Alice!");
           result.Should().Contain("Alice");
           result.Should().EndWith("Alice!");
           result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Constructor_OnNewInstance_SetsJoinDate()
        {
            // Arrange 
            var currentDate = DateTime.Now;

            // Act
            var player = new Player("Alice", 1, currentDate);

            // Assert
            player.JoinDate.Should().Be(currentDate);
            player.JoinDate.Should().BeCloseTo(currentDate, TimeSpan.FromMilliseconds(500));
        }

        [Fact]
        public void AddItemToInventory_WithValidItem_AddsTheItem()
        {
            // Arrange
            var player = new Player("Alice", 1, DateTime.Now);
            var item = new InventoryItem(101, "Sword", "A sharp blade.");

            // Act
            player.AddItemToInventory(item);

            // Assert
            player.InventoryItems.Should().HaveCount(1);
            player.InventoryItems.Should().NotBeEmpty();
            player.InventoryItems.Should().Contain(item);
            player.InventoryItems.Should().ContainSingle(item => item.Id == 101);
        }

        [Fact]
        public void Greet_NullOrEmptyGreeting_ThrowsArgumentException()
        {
            // Arrange
            var player = new Player("Alice", 1, DateTime.Now);

            // Act
            Action act = () => player.Greet("");

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void IncreaseLevel_WhenCalled_RaisesLevelUpEvent()
        {
            // Arrange
            var player = new Player("Alice", 1, DateTime.Now);
            using var monitorPlayer = player.Monitor();

            // Act
            player.IncreaseLevel();

            // Assert
            monitorPlayer.Should().Raise(nameof(player.LevelUp));
        }
    }
}