using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<string>().FromInstance("Inject");//создали в контейнере тип стринг и привязали значение
        //Container.Bind<TestZenject>().AsSingle().NonLazy();//создали шаблон экземпляра, AsSingle() - он один, NonLazy() - запуск сразу по старту

        //Container.Bind<IData>().To<TestZenject>().AsSingle().NonLazy();

        Container.Bind<IData>().To<ExecutorZenject>().AsSingle().NonLazy();//инициализируем точку входа исполнителя ExecutorZenject
    }
}
