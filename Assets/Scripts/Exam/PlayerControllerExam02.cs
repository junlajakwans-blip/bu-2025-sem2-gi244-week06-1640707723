using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam02 : MonoBehaviour
{
    public float speed;

    // [6] set the range of the player's movement in x-axis
    //public float xRange = 10;
    public float zRange = 10f;

    // [8] declare Projectile prefab variable
    public GameObject projectilePrefab;

    public Transform model;

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
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);

        // [5] keep the player inbounds
        // if (transform.position.x < -10)
        // {
        //     transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        // }
        float z = Mathf.Clamp(transform.position.z, -zRange, zRange);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        if (model != null)
        {
            model.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }


        // [7] keep the player inbounds using xRange variable
        //if (transform.position.x < -xRange)
        //{
        //    transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        //}
        //if (transform.position.x > xRange)
        //{
        //    transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        //}
        if (model != null)
        {
            model.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        }
        // [12] check if the player is shooting
        if (shootAction.triggered)
        {
            // [13] spawn a projectile
            Instantiate(projectilePrefab, transform.position, Quaternion.LookRotation(Vector3.right, Vector3.up));
        }
    }
}
