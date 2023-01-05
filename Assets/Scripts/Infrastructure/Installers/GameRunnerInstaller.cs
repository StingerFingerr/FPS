using Game_runner;
using Zenject;

public class GameRunnerInstaller: MonoInstaller
{
    public GameRunner gameRunner;

    public override void InstallBindings() => 
        Container.BindInstance(gameRunner);
}