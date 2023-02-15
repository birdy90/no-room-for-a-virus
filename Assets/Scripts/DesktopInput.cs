using Units;
using Unity.VisualScripting;
using UnityEngine;

public class DesktopInput : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private float _forwardInput;
    private float _sideInput;
    private Vector3 _mouseWorldPoint;

    private Camera _mainCamera;
    private Plane _plane = new Plane(Vector3.up, new Vector3(0, Constants.WeaponVecticalPosition, 0));

    void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        GetKeyboardInput();
        _playerController.Move(_forwardInput, _sideInput);

        GetMouseInput();
        _playerController.LookAt(_mouseWorldPoint);
    }

    void GetKeyboardInput()
    {
        _forwardInput = Input.GetAxisRaw(Constants.VerticalAxis);
        _sideInput = Input.GetAxisRaw(Constants.HorizontalAxis);
    }

    void GetMouseInput()
    {
        if (!_mainCamera)
        {
            _mouseWorldPoint = Vector3.zero;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        _mouseWorldPoint = _plane.Raycast(ray, out float distance) ? ray.GetPoint(distance) : Vector3.zero;

        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            _playerController.Fire();
        }
    }
}
