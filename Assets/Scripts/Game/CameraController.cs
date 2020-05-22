using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")] public bool updateCamera;
    [Range(0,1f)] public float speedScale = 0.5f;
    [Range(-4f, 4f)] public float deviationFromY = 0.5f;
    [Range(0f, 4f)] public float deviationFromX = 0.5f;

    [Header("Components")] public PlayerController PlayerController;

    private float HorizontalSpeed = 0f;

    void Start()
    {
        PlayerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (updateCamera)
        {
            Vector3 rigidbodySpeed = PlayerController.rigidbody.velocity;
            HorizontalSpeed += rigidbodySpeed.x * Time.deltaTime * speedScale;
            HorizontalSpeed = Mathf.Clamp(HorizontalSpeed,-deviationFromX, deviationFromX);
            Vector3 tempPos = PlayerController.transform.position;
            Transform thisCameraTransform = gameObject.transform;
            
            thisCameraTransform.position =
                new Vector3(
                    tempPos.x + HorizontalSpeed,
                    tempPos.y + deviationFromY,
                    thisCameraTransform.position.z);


        }
    }
}