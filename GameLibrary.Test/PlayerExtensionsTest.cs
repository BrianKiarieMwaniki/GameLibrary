using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;

namespace GameLibrary.Test
{
    public class PlayerExtensionsTest
    {
        [Fact]
        public void ToDto_WhenCalled_MapsProperties()
        {
            // Arrange
            var player = new Player("Alice", 1, DateTime.Now);
            var item = new InventoryItem(101, "Sword", "A sharp blade.");
            player.AddItemToInventory(item);

            // Act
            var dto = player.ToDto();

            // Assert
            dto.Should().BeEquivalentTo(player, options=> options
                                                            .Excluding(s => s.InventoryItems)
                                                            .Excluding(s => s.ExperiencePoints));     
        }
    }
}