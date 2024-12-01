using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField, Min(1)] int _maxWeight;
    [SerializeField, Min(1)] int _minWeight;

    public int Weight { get; private set; }

    private void Start()
    {
        Weight = _maxWeight;
    }

    public void TakeWeight()
    {
        Weight--;

        if (Weight < _minWeight)
        {
            Weight = _maxWeight;
        }
    }
}
