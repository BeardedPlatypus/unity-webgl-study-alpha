using System;
using Cinemachine;
using UniRx;
using Unity.Mathematics;
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
            Vector2 rotation = inputTranslation * orbitFactor;

            var orbitCenter = Vector3.zero;
            var worldX = _virtualCameraTransform.TransformVector(Vector3.left);

            float rotationAroundX = ClampRotation(rotation.y);
            
            _virtualCameraTransform.RotateAround(orbitCenter, worldX, rotationAroundX);
            _virtualCameraTransform.RotateAround(orbitCenter, Vector3.up, rotation.x);
        }

        private float ClampRotation(float newRotation)
        {
            // The rotation will be applied negatively due to rotating around the local x axis.
            // As such we need to invert the rotation during this calculation.
            float rotation = -newRotation * Mathf.Deg2Rad;
            float currentRotation = CameraRotationAroundX();
            
            float clampedRotation = 
                Mathf.Clamp(rotation, -currentRotation + 5F * Mathf.Deg2Rad, 0.5f * Mathf.PI - currentRotation);

            return -clampedRotation * Mathf.Rad2Deg;
        }

        private float CameraRotationAroundX()
        {
            var position = _virtualCameraTransform.position;
            
            var distance = math.distance(position, Vector3.zero);
            return Mathf.Asin(position.y / distance);
        }


    }
}
