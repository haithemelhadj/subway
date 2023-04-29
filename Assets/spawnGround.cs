using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnGround : MonoBehaviour
{
    public Transform nextPosition;
    public GameObject[] planes;
    public Vector3 direction;
    [Range(0f, 50f)] public float speed;
    [SerializeField] Vector3 startDestroy;

    private void Start()
    {
        
    }

    void Update()
    {
        if (nextPosition.position.z < -30f)
        {
            transform.position= new Vector3(transform.position.x, transform.position.y, 400f);
        }
        movement();
        //destroyMe();
    }
    void destroyMe()
    {
        if (transform.position.z < startDestroy.z)
            Destroy(this.gameObject);
    }

    void movement()
    {
        transform.Translate(direction * Time.deltaTime * speed);

    }
}
