using Cinemachine;
using UniRx;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float orbitFactor = 0.05F;

        private Transform _virtualCameraTransform;
        private Bindings _bindings;

        [Inject]
        private void Init(Bindings bindings)
        {
            _bindings = bindings;
        }

        private void Awake()
        {
            _virtualCameraTransform = GetComponent<CinemachineVirtualCamera>().transform;
        }

        private void Start()
        {
            ConfigureSubscriptions();
        }

        private void ConfigureSubscriptions()
        {
            _bindings.Orbit.Subscribe(OnOrbit).AddTo(this);
        }

        private void OnOrbit(Vector2 inputTranslation)
        {
            Vector2 translation = inputTranslation * orbitFactor;

            var orbitCenter = Vector3.zero;
            var worldX = _virtualCameraTransform.TransformVector(Vector3.left);
            
            _virtualCameraTransform.RotateAround(orbitCenter, worldX, translation.y);
            _virtualCameraTransform.RotateAround(orbitCenter, Vector3.up, translation.x);
        }
    }
}
