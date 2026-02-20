using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;

    // [6] set the range of the player's movement in x-axis
    public float xRange = 10;

    // [8] declare Projectile prefab variable
    public GameObject projectilePrefab;

    //private float horizontalInput;
    private float verticalInput;

    // [1] declare a private InputAction variable
    private InputAction moveAction;
    // [10] declare a private InputAction variable for shooting
    private InputAction shootAction;

    private void Awake()
    {
        // [2] find the action by name
        // this is to optimize the search for the action
        moveAction = InputSystem.actions.FindAction("Move");

        // [11] find the action by name
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    private void OnEnable()
    {
        // [9] enable the actions
        moveAction?.Enable();
        shootAction?.Enable();
    }

    private void OnDisable()
    {
        // [14] disable the actions
        moveAction?.Disable();
        shootAction?.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // [3] use input system to get horizontal input
        //horizontalInput = moveAction.ReadValue<Vector2>().x;
        verticalInput = moveAction.ReadValue<Vector2>().y;

        // [4] move the player up and down
        //transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);
        transform.Translate(verticalInput * speed * Time.deltaTime * Vector3.up);

        // [5] keep the player inbounds
        // if (transform.position.x < -10)
        // {
        //     transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        // }

        // [7] keep the player inbounds using xRange variable
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -xRange)
        {
            transform.position = new Vector3(transform.position.x, -xRange, transform.position.z);
        }

        // [12] check if the player is shooting
        if (shootAction.triggered)
        {
            // [13] spawn a projectile
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
