using Units;
using Unity.VisualScripting;
using UnityEngine;

public class DesktopInput : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;

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
        if (GameFlow.IsPaused) return;
        
        GetKeyboardInput();
        PlayerController.Move(_forwardInput, _sideInput);

        GetMouseInput();
        PlayerController.LookAt(_mouseWorldPoint);
    }

    void GetKeyboardInput()
    {
        _forwardInput = Input.GetAxisRaw(Constants.VerticalAxis);
        _sideInput = Input.GetAxisRaw(Constants.HorizontalAxis);
    }

    void GetMouseInput()
    {
        if (!_mainCamera) return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        _mouseWorldPoint = _plane.Raycast(ray, out float distance) ? ray.GetPoint(distance) : Vector3.zero;

        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            PlayerController.Fire();
        }
    }
}
