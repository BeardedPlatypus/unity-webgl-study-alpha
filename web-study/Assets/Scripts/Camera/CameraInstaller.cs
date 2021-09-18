using Zenject;

namespace Camera
{
    public class CameraInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            CameraInput cameraInput = new CameraInput();
            Container.Bind<CameraInput>()
                     .FromInstance(cameraInput)
                     .AsSingle();
            
            Container.Bind<Bindings>()
                     .FromNewComponentOnNewGameObject()
                     .AsSingle();
        }
    }
}
