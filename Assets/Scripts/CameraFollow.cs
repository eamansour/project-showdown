using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float offset;
    
    [SerializeField]
    private float offsetSmoothing;
    
    private float xMax = 23.7f;
    private float xMin = -23.7f;

    private Vector3 playerPosition;

    private Transform target;

	void Start ()
    {
        target = GameObject.Find("Player").transform;
	}
	
	void LateUpdate ()
    {
        if (!target) return;

        float adjustedOffset = target.transform.localScale.x < 0f ? -offset : offset;

        playerPosition = new Vector3(
            Mathf.Clamp(target.transform.position.x + adjustedOffset, xMin, xMax),
            transform.position.y,
            transform.position.z
        );
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
	}
}
