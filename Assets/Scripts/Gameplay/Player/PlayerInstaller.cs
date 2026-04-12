using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerInputMain>().AsSingle().NonLazy();

        Container.Bind<SmellController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SmellVisualController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerMovementController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerAnimationController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameLoop>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IDamageable>().To<PlayerHealthController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IDamageFX>().FromComponentInHierarchy().AsSingle();
    }
}