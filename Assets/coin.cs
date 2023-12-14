using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left;
    }
}
