using UnityEngine;

public class CoinFloat : MonoBehaviour
{
    public float floatHeight = 0.15f;
    public float floatSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = startPosition + new Vector3(0f, y, 0f);
    }
}