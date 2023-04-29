using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveStopTraffic : MonoBehaviour
{


    public Vector3 direction;
    [Range(0f, 50f)] public float speed;
    [SerializeField] Vector3 startDestroy;

    void Update()
    {
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
        transform.Translate(direction * Time.deltaTime * speed);

    }
}
