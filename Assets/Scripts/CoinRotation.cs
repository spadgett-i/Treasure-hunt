using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 120f;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}