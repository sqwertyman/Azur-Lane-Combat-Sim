using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject gameController, playSpace;
    public Vector2 margin = new Vector2(1, 1);
    public Vector2 smoothing = new Vector2(3, 3);

    private BoxCollider2D cameraBounds;
    private GameObject player;
    private Vector2 min, max;
    private Camera camera;

    void Awake()
    {
        
    }

    private void Start()
    {
        player = gameController.GetComponent<GameController>().GetLeadShip();
        cameraBounds = playSpace.GetComponent<BoxCollider2D>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        var x = transform.position.x;
        var y = transform.position.y;

        if (Mathf.Abs(x - player.transform.position.x) > margin.x)
        {
            x = Mathf.Lerp(x, player.transform.position.x, smoothing.x * Time.deltaTime);
        }
        if (Mathf.Abs(y - player.transform.position.y) > margin.y)
        {
            y = Mathf.Lerp(y, player.transform.position.y, smoothing.y * Time.deltaTime);
        }

        var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);
        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
    {
        float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) *unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
        return adjustedUnityUnits;
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position;
        Vector3 roundPos = new Vector3(RoundToNearestPixel(newPos.x, camera), RoundToNearestPixel(newPos.y, camera), newPos.z);
        transform.position = roundPos;
    }

    public void UpdateBounds()
    {
        min = cameraBounds.bounds.min;
        max = cameraBounds.bounds.max;
    }
}
