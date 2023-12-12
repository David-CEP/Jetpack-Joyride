using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public jugador jugadorObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "restartBullet")
        {
            Debug.Log("reseteo");
            gameObject.transform.position = new Vector3(-10f, gameObject.transform.position.y, gameObject.transform.position.z);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            jugadorObj.movingBullet = false;
        }
    }
}
