using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    void Update()
    {
        if (playerTransform.position.y > transform.position.y)
        {
            transform.position = new Vector3(playerTransform.position.x,playerTransform.position.y,-10f);
        }
    }
}
