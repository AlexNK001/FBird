using System;
using UnityEngine;

public interface IPositioner
{
    public event Action<Transform> SpawnPointAppeared;
}
