using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    // Tốc độ di chuyển
    public float maxSpeed = 50f;       // Tốc độ tối đa
    public float minSpeed = -10f;      // Tốc độ lùi tối đa
    public float acceleration = 10f;    // Tốc độ tăng giảm
    public float deceleration = 7f;    // Tốc độ giảm về 0 khi không bấm
    public float turnSpeed = 100f;     // Tốc độ quay xe

    private Rigidbody rb;
    private float currentSpeed = 0f;   // Tốc độ hiện tại

    public float health = 0f;
    public Image healthBar;
    public TextMeshProUGUI healthText;

    public float maxHealth = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
    }

    void FixedUpdate()
    {
        // Nhận đầu vào từ phím W/S
        float moveInput = Input.GetAxis("Vertical");   // W/S
        float turnInput = Input.GetAxis("Horizontal"); // A/D

        // Điều chỉnh tốc độ dựa trên input
        if (moveInput > 0) // Bấm W
        {
            currentSpeed += moveInput * acceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed); // Giới hạn max
        }
        else if (moveInput < 0) // Bấm S
        {
            currentSpeed += moveInput * acceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, 0f); // Giới hạn min
        }
        else
        {
            // Tự động giảm tốc về 0 khi không bấm phím
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0f); // Không giảm quá 0
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += deceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Min(currentSpeed, 0f); // Không tăng quá 0
            }
        }

        // Di chuyển xe về phía trước theo tốc độ hiện tại
        Vector3 movement = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Quay xe trái/phải dựa trên input
        if (currentSpeed != 0)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }

    private void OnCollisionEnter(Collision other) {
        
        currentSpeed = 0;
        health -= 10;
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        healthBar.fillAmount = health/maxHealth;
        Debug.Log("Health = " + health.ToString());
    }

}
