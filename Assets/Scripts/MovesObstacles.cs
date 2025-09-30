using UnityEngine;

public class CenterPingPong : MonoBehaviour
{
    public float distance = 5f;      // base distance
    public float minSpeed = 1f;      // minimum random speed
    public float maxSpeed = 3f;      // maximum random speed

    private float startX;
    private float speed;
    private float cycleStartTime;

    void Start()
    {
        startX = transform.position.x;
        PickNewSpeed();
    }

    void Update()
    {
        // Time since last cycle began, but at half speed
        float elapsed = (Time.time - cycleStartTime) * (speed * 0.5f);

        // PingPong between 0 and 2 (right->center->left->center)
        float t = Mathf.PingPong(elapsed, 2f);

        // Double distance
        float maxDist = distance * 2f;

        float x = 0f;
        if (t <= 1f)
        {
            // center -> right -> center
            x = Mathf.Lerp(0f, maxDist, t);
        }
        else
        {
            // center -> left -> center
            x = Mathf.Lerp(maxDist, -maxDist, t - 1f);
        }

        transform.position = new Vector3(startX + x, transform.position.y, transform.position.z);

        // When a full cycle ends, randomize speed again
        if (Mathf.Approximately(t, 0f))
        {
            PickNewSpeed();
        }
    }

    void PickNewSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        cycleStartTime = Time.time;
    }
}
