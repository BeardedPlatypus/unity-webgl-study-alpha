using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using Zenject;

namespace Camera
{
    public class Bindings : MonoBehaviour
    {
        private CameraInput _cameraInput;

        [Inject]
        private void Init(CameraInput cameraInput)
        {
            _cameraInput = cameraInput;
        }

        private void Awake()
        {
            ConfigureObservables();
        }

        private void Start()
        {
            _cameraInput.Enable();
        }

        private void ConfigureObservables()
        {
            IObservable<Vector2> dragStream = _cameraInput.World.Drag.ActionAsObservable()
                                                                     .Select(InterpretAs<Vector2>);

            var isOrbitingStream = _cameraInput.World.OrbitActive.ActionAsObservable()
                                                                               .Select(InterpretAsBool)
                                                                               .DistinctUntilChanged();

            Orbit = isOrbitingStream.CombineLatest(dragStream, (isActive, direction) => (isActive, direction))
                                    .Where(x => x.isActive)
                                    .Select(x => x.direction);
        }
        
        private static T InterpretAs<T>(InputAction.CallbackContext context) where T : struct =>
            context.ReadValue<T>();
        
        private static bool InterpretAsBool(InputAction.CallbackContext context) =>
            context.ReadValue<float>() != 0F;
        
        public IObservable<Vector2> Orbit { get; private set; }
    }
}
