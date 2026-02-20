using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam03 : MonoBehaviour
{
    public float speed;
    public float xRange = 10;
    public GameObject projectilePrefab;

    public bool enableAutoFireMode;
    public float autoFireInterval = 0.1f;

    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private float nextAutoFireTime = 0f;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    private void OnEnable()
    {
        moveAction?.Enable();
        shootAction?.Enable();
        nextAutoFireTime = Time.time; // reset timer ตอนเริ่ม
    }

    private void OnDisable()
    {
        moveAction?.Disable();
        shootAction?.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        horizontalInput = moveAction.ReadValue<Vector2>().x;
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (shootAction.triggered)
        {
            Fire();
        }

        if (enableAutoFireMode)
        {
            // กันค่า interval แปลก ๆ (เช่น 0 หรือติดลบ)
            float interval = Mathf.Max(0.01f, autoFireInterval);

            if (Time.time >= nextAutoFireTime)
            {
                Fire();
                nextAutoFireTime = Time.time + interval;
            }
        }
    }
    private void Fire()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}
