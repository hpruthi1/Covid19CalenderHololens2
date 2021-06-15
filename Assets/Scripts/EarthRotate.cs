using UnityEngine;

public class EarthRotate : MonoBehaviour
{
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((Vector3.up * RotationSpeed) * Time.deltaTime);
    }
}
