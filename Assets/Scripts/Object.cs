using UnityEngine;

public class Object : MonoBehaviour
{
    private Quaternion startRotation;
    private bool isRotating = false;
    private float totalRotation = 0f;
    private float startAngle;
    public float rotationSpeed = 100f;
    public GameObject vfxCompleted;

    void Start()
    {
        // Store the initial rotation of the object
        startRotation = transform.rotation;
        startAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        if (isRotating)
        {
            // Rotate the object around the Z-axis
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            // Calculate the total rotation
            totalRotation += Mathf.Abs(transform.eulerAngles.z - startAngle);
            // Check if the total rotation is approximately 360 degrees
            if (totalRotation >= 26000f)
            {
                GameObject vfx = Instantiate(vfxCompleted, transform.position, Quaternion.identity) as GameObject;
                Destroy(vfx, 1f);
                GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
                GameManager.Instance.CheckLevelUp();
                gameObject.SetActive(false);
                isRotating = false;
            }
        }
    }

    private void OnMouseDown()
    {
        isRotating = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Wrench"))
        {
            Reset();
        }
    }

    private void Reset()
    {
        totalRotation = 0f;
        transform.rotation = Quaternion.Euler(0f, 0f, startAngle);
        isRotating = false;
    }
}
