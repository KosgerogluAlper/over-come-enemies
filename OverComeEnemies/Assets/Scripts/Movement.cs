using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f; 
    [SerializeField] float justPower = 13f;
    [SerializeField] float turnSpeed = 15f;  
    [SerializeField] Transform[] rayStartPoint;  
    new Rigidbody rigidbody;
    GameManangement gamemanangement;
    public void Awake() 
    {
        rigidbody = GetComponent<Rigidbody>();
        gamemanangement=FindObjectOfType<GameManangement>();
    }
    void Update()
    {
        if (gamemanangement.levelFinish != true)
        TakeInput();  // update icerisinde hareket methodu calýsýyor sadece
    }
    bool a = false;
    bool d = false;
    bool space = false;
    private void TakeInput()
    {
        if (Input.GetKey(KeyCode.A))
        { 
         a= true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            d= true;
        }
        else
        {
        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
       space= true;
        }
    }
    private void FixedUpdate()
    {
        if (a)
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((speed * 100), 0f, 15f), rigidbody.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 179.99f, 0f), turnSpeed);
            a= false;
        }
        else if (d)
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((-speed * 100), -15f, 0f), rigidbody.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f),turnSpeed);
            d= false;
        }
        else
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
        }
        if(space&& OnGroundCheck())
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp((justPower * 100), 0f, 30f), 0f);
            space= false;
        }
    }
    private bool OnGroundCheck()
    {
        bool hit = false; 
        for (int i = 0; i < rayStartPoint.Length; i++)  
        {
            hit = Physics.Raycast(rayStartPoint[i].position, -rayStartPoint[i].transform.up, 0.3f); 
            if (hit) break;
        }
        return hit;
    }
}
