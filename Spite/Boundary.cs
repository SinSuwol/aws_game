using UnityEngine;

public class Boundary : MonoBehaviour
{
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -5f;
    public float maxY = 5f;

    void LateUpdate()
    {
        // 플레이어의 위치를 화면 경계 내로 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }
}
