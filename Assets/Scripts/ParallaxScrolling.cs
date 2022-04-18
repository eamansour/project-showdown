using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField]
    private Transform[] backgrounds;

    private float[] parallaxScales;

    [SerializeField]
    private float smoothing = 1f;

    private Transform cameraTransform;

    private Vector3 previousCamPos;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = cameraTransform.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cameraTransform.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fade between current position and the target position
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cameraTransform.position;
    }
}