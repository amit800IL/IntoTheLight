public interface IGhostPlayerMediator
{
    public void OnGhostAwake();
    public void OnGhostSleep();
    public void RegisterPlayerHealing(IPlayerHealing playerHealing);
    public void UnregisterPlayerHealing(IPlayerHealing playerHealing);

    public bool IsGhostAwake();
}
