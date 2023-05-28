using Coroutine_runner;
using Game_state_machine;
using LoadingScreen;
using Scene_service;
using UnityEngine;
using Zenject;

public class ProjectInstaller: MonoInstaller, ICoroutineRunner
{
    public PrefabService prefabs;

    public override void InstallBindings()
    {
        BindGameStateMachine();
        BindStatesFactory();
        BindGameStates();
        
        BindProgressService();
        BindCoroutineRunner();
        BindSceneLoaderService();
        BindLoadingScreen();

        BindFPSCounter();
    }

    private void BindFPSCounter() => 
        Container.Bind<FPSCounter>().FromComponentInNewPrefab(prefabs.fpsCounter).AsSingle();

    private void BindGameStateMachine() => 
        Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

    private void BindStatesFactory() => 
        Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();

    private void BindGameStates()
    {
        Container.Bind<InitialState>().AsSingle();
        Container.Bind<MenuBuilderState>().AsSingle();
        Container.Bind<LoadLevelState>().AsSingle();
        Container.Bind<GameLoopState>().AsSingle();
        Container.Bind<LoadMainMenuState>().AsSingle();
        Container.Bind<ExitState>().AsSingle();
    }

    private void BindProgressService() => 
        Container.Bind<IProgressService>().To<ProgressService>().AsSingle().NonLazy();

    private void BindCoroutineRunner() => 
        Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

    private void BindSceneLoaderService() => 
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

    private void BindLoadingScreen() =>
        Container.Bind<ILoadingScreen>().To<LoadingScreen.LoadingScreen>()
            .FromComponentInNewPrefab(prefabs.loadingScreenPrefab).AsSingle();
}