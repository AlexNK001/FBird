using UnityEngine;

public class Screen : MonoBehaviour 
{
    public Vector3 StartPosition { get; private set; }

    private void Start()
    {
        StartPosition = transform.position;
    }
}
