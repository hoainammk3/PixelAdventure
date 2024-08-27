using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float left;
    [SerializeField] private float right;

    [SerializeField] private float top;

    [SerializeField] private float bot;
    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = player.position;
        Vector2 camPos = transform.position;

        if (playerPos.x > left && playerPos.x < right)
        {
            camPos.x = playerPos.x;
        }
        else
        {
            if (playerPos.x < left) camPos.x = left;
            if (playerPos.x > right) camPos.x = right;
        }

        if (playerPos.y > bot && playerPos.y < top)
        {
            camPos.y = playerPos.y;
        }
        else
        {
            if (playerPos.y < bot) camPos.y = bot;
            if (playerPos.y > top) camPos.y = top;
        }

        var transform1 = transform;
        var position = transform1.position;
        position = new Vector3(camPos.x, camPos.y, position.z);
        transform1.position = position;
    }
}
