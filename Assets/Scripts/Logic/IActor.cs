namespace Assets.Scripts.Logic
{
    public interface IActor
    {
        bool IsDead { get; }

        int Health { get; }
    }
}
