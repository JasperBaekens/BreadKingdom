using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float movementspeed = 1f;
    private InputSystem_Actions inputActions;

    public float rotationspeed = 1f;


    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void Update()
    {
        Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();
        transform.position = new Vector3(transform.position.x + move.x * Time.deltaTime * movementspeed, transform.position.y + 0, transform.position.z + move.y * Time.deltaTime * movementspeed);

        Vector2 rotate = inputActions.Player.Rotate.ReadValue<Vector2>();
        transform.Rotate((rotate.y * Time.deltaTime * rotationspeed),0, (-rotate.x * Time.deltaTime * rotationspeed));

    }
}
