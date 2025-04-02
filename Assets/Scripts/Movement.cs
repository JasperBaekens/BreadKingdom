using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Movement : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    public float movementspeed = 1f;
    private Vector3 movement;


    public float rotationspeed = 5f;


    private bool isJump = false;
    private float Jumptime = 0.4f;
    private float currentJumpTime = 0f;

    private Rigidbody Rb;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        Rb = GetComponent<Rigidbody>();
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
    }

    private void FixedUpdate()
    {
        Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();
        //later with rigidbody
        //Rb.MovePosition(new Vector3(transform.position.x + move.x * Time.deltaTime * movementspeed, transform.position.y + 0, transform.position.z + move.y * Time.deltaTime * movementspeed));
        movement = new Vector3(move.x, 0, move.y).normalized;
        //Rb.linearVelocity = movement * Time.fixedDeltaTime * movementspeed;
        //Rb.MovePosition(transform.position + movement * Time.fixedDeltaTime * movementspeed);
        //transform.position = new Vector3(transform.position.x + move.x * Time.deltaTime * movementspeed, transform.position.y + 0, transform.position.z + move.y * Time.deltaTime * movementspeed);
        transform.position = transform.position + movement * Time.fixedDeltaTime * movementspeed;


        Vector2 rotate = inputActions.Player.Rotate.ReadValue<Vector2>();        
        //later with rigidbody
        transform.Rotate((rotate.y * Time.deltaTime * rotationspeed),0, (-rotate.x * Time.fixedDeltaTime * rotationspeed));



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
