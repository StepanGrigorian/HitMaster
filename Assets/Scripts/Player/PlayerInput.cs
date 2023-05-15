using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void CallbackPosition(Vector3 position);

    public CallbackPosition PositionCallback;

    [SerializeField] private LayerMask Mask;
    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Application.isFocused && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, Mask))
            {
                PositionCallback?.Invoke(hit.point);
            }
        }
    }
}