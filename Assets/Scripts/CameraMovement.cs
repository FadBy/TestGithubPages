using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float smoothness = 5f;
    [SerializeField] private Transform LeftLevelBound;
    [SerializeField] private Transform RightLevelBound;
    [SerializeField] private float leftBoundX;
    [SerializeField] private float rightBoundX;

    private Transform target;
    
    private Camera mainCamera;
    private Transform playerTransform;


    private float cameraWidthHalf; 

    private void Awake()
    {
        playerTransform = GameManager.Instance.PlayerController.transform;
        mainCamera = Camera.main;
        cameraWidthHalf = mainCamera.orthographicSize * mainCamera.aspect;
        target = playerTransform;
    }

    private void Start()
    {
        var cameraPosition = transform.position;
        cameraPosition.x = playerTransform.position.x;
        transform.position = cameraPosition;
    }

    private void FixedUpdate()
    {
        FollowTarget(Time.fixedDeltaTime);
    }

    private void FollowTarget(float time)
    {
        Vector3 cameraPosition = transform.position;
        Vector3 targetPosition = target.position;
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, targetPosition.x, time * smoothness);
        cameraPosition.x = Clamp(cameraPosition.x);
        transform.position = cameraPosition;
    }

    public float Clamp(float positionX)
    {
        return Mathf.Clamp(positionX, leftBoundX + cameraWidthHalf, rightBoundX - cameraWidthHalf);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3((leftBoundX + rightBoundX) / 2f, Camera.main.transform.position.y, 0f), new Vector3(rightBoundX - leftBoundX, Camera.main.orthographicSize * 2f, 0f));
    }
}
