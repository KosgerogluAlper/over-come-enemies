using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f; 
    [SerializeField] float jumpPower = 13f;
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
    bool inputA = false;
    bool inputD = false;
    bool inputSpace = false;
    private void TakeInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            inputA = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputD = true;
        }
        else
        {
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGroundCheck())
        {
            inputSpace = true;
        }
    }
    private void FixedUpdate()
    {
        if (inputA)
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((speed * 100), 0f, 15f), rigidbody.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 179.99f, 0f), turnSpeed);
            inputA = false;
        }
        else if (inputD)
        {
            rigidbody.velocity = new Vector3(Mathf.Clamp((-speed * 100), -15f, 0f), rigidbody.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f),turnSpeed);
            inputD = false;
        }
        else
        {
            rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, 0f);
        }
        if (inputSpace)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, Mathf.Clamp((jumpPower * 100), 0f, 30f), 0f);
                inputSpace = false;
            }
    }

    private bool isGroundCheck()
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
