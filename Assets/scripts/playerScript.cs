using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float speed;
    public float curPos;
    public float horizontal;

    public CapsuleCollider coll; 
    public BoxCollider rollingColl; 
    public LayerMask groundlayer;
    public float checkdistance=0.1f;
    public Animator animator;
    public bool isGrounded;
    public bool isFalling;
    public Rigidbody rb;
    public float jumpforce;
    //public Animation rollAnimation;
    public GameObject restartButton;

    private void Start()
    {
        rollingColl.enabled = false;
        curPos = 0;
    }
    void Update()
    {
        checkFalling();
        animator.SetBool("isGrounded", isGrounded);
        jumping();
        roll();
        movement();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (coll.height / 2 + checkdistance));
    }


    private Vector3 previousPosition;
    void checkFalling()//check the position betwenn this frame and the previous frame and check if it's going up or down
    {
        Vector3 currentPosition = transform.position;
        Vector3 movementDelta = currentPosition - previousPosition;
        previousPosition = currentPosition;
        isFalling = movementDelta.y < 0;
        animator.SetBool("isFalling", isFalling);
        //return movementDelta.y < 0;
    }

    bool checkGrounded()//returns if grounded by sending one ray down 
    {
        //bool hit;
        RaycastHit hit;
        //Debug.Log(coll.height/ 2);        
        //Debug.Log(isGrounded);
        if(Physics.Raycast(transform.position, Vector3.down,out hit,checkdistance, groundlayer) && hit.transform.gameObject.layer==groundlayer)
        {
            return true;
        }
        return false;
    }

    void roll()
    {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                rb.AddForce(Vector3.down * 100, ForceMode.Impulse);
                coll.enabled = false;
                rollingColl.enabled = true;
                animator.SetTrigger("rolling");
                StartCoroutine(PlayAndWaitForAnimationToEnd());
            }
    }

    IEnumerator PlayAndWaitForAnimationToEnd()//(Animation animation, string clipName)//, Action action)
    {
        //animation.Play(clipName);
        yield return new WaitForSeconds(1);
        coll.enabled = true;
        rollingColl.enabled = false;
        //action.Invoke();
    }


    void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            animator.SetTrigger("jump");
        }
    }


    void movement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            horizontal = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontal = 1;
        }
        else horizontal = 0;

        if (curPos != 0 && horizontal != 0 && math.sign(curPos)!=math.sign(horizontal))
        {
            curPos = 0;
        }
        else if (curPos == 0 && horizontal != 0)
        {
            curPos = horizontal * 4;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(curPos, transform.position.y, 0f), speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Time.timeScale = 0f;
            restartButton.SetActive(true);
            //Debug.Log("gameover");
        }
        if(collision.gameObject.layer==3)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }
}
