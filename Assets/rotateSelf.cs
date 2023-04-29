using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSelf : MonoBehaviour
{
    public float rotationSpeed;
    public Vector3 direction;
    [Range(0f, 50f)] public float speed;
    [SerializeField] Vector3 startDestroy;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        movement();
        destroyMe();
    }
    void destroyMe()
    {
        if (transform.position.z < startDestroy.z)
            Destroy(this.gameObject);
    }

    void movement()
    {
        //transform.Translate(direction * Time.deltaTime * speed);
        //new Vector3(transform.position.x, transform.position.y, 0f),

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -10f), speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //coins ++ 
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //coins ++ 
            GameManager.coins++;
            Destroy(this.gameObject);
        }
    }

}
