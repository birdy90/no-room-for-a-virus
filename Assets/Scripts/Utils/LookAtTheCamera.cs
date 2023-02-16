using UnityEngine;

namespace Utils
{
    public class LookAtTheCamera : MonoBehaviour
    {
        private Camera _mainCamera;
        private bool _hasCamera;
        private Vector3 _cameraDirection;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _hasCamera = _mainCamera != null;

            if (_hasCamera)
            {
                _cameraDirection = _mainCamera.transform.rotation * Vector3.forward;
            }
        }

        private void LateUpdate()
        {
            if (!_hasCamera) return;
        
            transform.LookAt(transform.position + _cameraDirection);
        }
    }
}