using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField, Min(1)] int _weight;

    public int Weight { get; private set; }

    private void Start()
    {
        Weight = _weight;
    }
}
