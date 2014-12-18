using UnityEngine;
using System.Collections;

public class CollisionEventManager : MonoBehaviour
{
    public delegate void CollisionEventHandler();
    public static event CollisionEventHandler onGameOver;

    void OnTriggerEnter2D()
    {
        onGameOver();
    }
}
