using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float movementspeed = 1f;
    private InputSystem_Actions inputActions;


    public float rotationspeed = 5f;


    private bool isJump = false;
    private float Jumptime = 0.4f;
    private float currentJumpTime = 0f;

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
        //later with rigidbody
        transform.position = new Vector3(transform.position.x + move.x * Time.deltaTime * movementspeed, transform.position.y + 0, transform.position.z + move.y * Time.deltaTime * movementspeed);

        Vector2 rotate = inputActions.Player.Rotate.ReadValue<Vector2>();        
        //later with rigidbody
        transform.Rotate((rotate.y * Time.deltaTime * rotationspeed),0, (-rotate.x * Time.deltaTime * rotationspeed));



        if (inputActions.Player.Hop.triggered)
        {
            if (!isJump)
            {
                isJump = true;
                //later with rigidbody
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);

                Debug.Log("Hop");
            }

        }

        if (isJump == true)
        {
            if (currentJumpTime < Jumptime)
            {
                currentJumpTime += Time.deltaTime;
            }
            else
            {
                currentJumpTime = 0;
                isJump = false;
                //later with rigidbody
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);

                Debug.Log("DeHop");

            }
        }
    }
}
