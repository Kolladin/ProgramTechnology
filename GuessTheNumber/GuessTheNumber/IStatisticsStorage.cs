
namespace GuessTheNumber;

internal interface IStatisticsStorage
{
    void Save(GameStatistics stats);

    GameStatistics Load();
}
