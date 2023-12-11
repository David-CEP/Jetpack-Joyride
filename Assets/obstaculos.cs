using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculos : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, Random.Range(3f, 7f), gameObject.transform.localScale.z);
    }

    private void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 4f;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            gameObject.SetActive(false);
            Quaternion resetRot = gameObject.transform.localRotation;
            resetRot.z = 0f;
            gameObject.transform.localRotation = resetRot;
            gameObject.transform.position = new Vector3(10f, Random.Range(-3.86f, 4.5f), 0f);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, Random.Range(3f, 7f), gameObject.transform.localScale.z);
            Quaternion tempRot = gameObject.transform.localRotation;
            tempRot.z = Random.Range(-50f, -50f);
            gameObject.transform.localRotation = tempRot;
            gameObject.SetActive(true);
        }
    }
}
