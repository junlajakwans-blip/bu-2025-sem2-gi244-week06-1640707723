using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam05 : MonoBehaviour
{
    public float speed;
    public float xRange = 10;
    public GameObject projectilePrefab;


    // Exam 05 ...
    public int maxBulletCount = 10;
    public float bulletRegenerateCooldown = 1f;
    // ...

    private int currentBullets;
    private bool isCoolingDown = false;
    private float cooldownEndTime = 0f;


    private float horizontalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    private void OnEnable()
    {
        moveAction?.Enable();
        shootAction?.Enable();

        //Full ammo ตอนเริ่มเกม
        currentBullets = Mathf.Max(0, maxBulletCount);
        isCoolingDown = false;
        cooldownEndTime = 0f;
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

        // Cooldown complete -> refill
        if (isCoolingDown && Time.time >= cooldownEndTime)
        {
            currentBullets = Mathf.Max(0, maxBulletCount);
            isCoolingDown = false;
        }

        if (shootAction.triggered)
        {
            // ถ้าอยู่ใน cooldown อยู่ ให้ข้ามการยิง
            if (isCoolingDown) return;

            // ถ้าไม่มีลูกกระสุนเหลืออยู่ ให้เริ่ม cooldown
            if (currentBullets <= 0)
            {
                isCoolingDown = true;
                cooldownEndTime = Time.time + Mathf.Max(0f, bulletRegenerateCooldown);
                return;
            }

            // ยิงปกติ
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            currentBullets--;

            // ถ้ายิงแล้วลูกกระสุนหมด ให้เริ่ม cooldown
            if (currentBullets <= 0)
            {
                isCoolingDown = true;
                cooldownEndTime = Time.time + Mathf.Max(0f, bulletRegenerateCooldown);
            }
        }
    }
}
