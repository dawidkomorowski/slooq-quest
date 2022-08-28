namespace SlooqQuest.Core
{
    public interface IModeInfo
    {
        Mode Mode { get; }
    }

    public enum Mode
    {
        Edit,
        Game
    }
}