using FluentAssertions;
using NSubstitute;

namespace GameLibrary.Test
{
    public class GameWorldTest
    {
        [Fact]
        public void GetPlayerReport_PlayerExists_ReturnsExpectedReport()
        {
            // Arrange
            var player = new Player("Alice", 10, new DateTime(2020, 1, 1));

            var stats = new PlayerStatistics
            {
                PlayerName = player.Name,
                GamesPlayed = 10,
                TotalScore = 100
            };

            var statisticsServiceStub = Substitute.For<IPlayerStatisticsService>();
            statisticsServiceStub.GetPlayerStatistics(player.Name).Returns(stats);

            var expected = new PlayerReportDto(
                player.Name,
                player.Level,
                player.JoinDate,
                stats.GamesPlayed,
                stats.TotalScore,
                stats.TotalScore / stats.GamesPlayed
            );

            var sut = new GameWorld(statisticsServiceStub);

            // Act
            var actual = sut.GetPlayerReport(player);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void RecordPlayerGameWin_ValidPlayerAndScore_UpdatesPlayerStatistics()
        {
            // Arrange
            var player = new Player("Alice", 10, new DateTime(2020, 1, 1));

            var stats = new PlayerStatistics
            {
                PlayerName = player.Name,
                GamesPlayed = 10,
                TotalScore = 1000
            };

            var statisticsServiceMock = Substitute.For<IPlayerStatisticsService>();
            statisticsServiceMock.GetPlayerStatistics(player.Name)
                                 .Returns(stats);

            var sut = new GameWorld(statisticsServiceMock);

            // Act
            sut.RecordPlayerGameWin(player, 20);

            // Assert
            statisticsServiceMock.Received().UpdatePlayerStatistics(Arg.Any<PlayerStatistics>());
        }
    }
}