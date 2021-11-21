//Diego Hiriart Leon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    [SerializeField]
    private GameObject controller;
    private GameController gameControl;
    [SerializeField]
    private Material originalMaterial;

    private Rigidbody rb;//Referencia al rigidbody de la esfera
    private int count;//Contador de pickups
    private float movementX;
    private float movementY;
    private Ball ball = new Ball();

    // Start is called before the first frame update
    void Start()
    {
        this.gameControl = this.controller.GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();//Poner el rigidbody de la esfera en la referncia
        count = 0;
    }

    private void Update()
    {
        if (this.ball.Health <= 0 && this.ball.Lives>=1)
        {
            this.Respawn();
        }
    }

    private void Respawn()//Reset values for the ball
    {
        this.ball.Respawn();
        this.transform.position = new Vector3(0, 0.5f, 0);//Reset position
        this.transform.rotation = new Quaternion(0, 0, 0, 1);//Reset rotation
        this.GetComponent<Renderer>().material = this.originalMaterial;//Return to original color
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;//Remove position changes (displacement)
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;//Remove angular movement (rotation)
    }    

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * this.ball.Speed);//Aniade una fuerza al rigidbody, o sea lo mueve, multiplicado por la velocidad de movimiento
    }

    public void LowHealth()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    public void SetSpeed(float value)
    {
        this.ball.Speed = value;
    }

    public float GetSpeed()
    {
        return this.ball.Speed;
    }

    public Ball GetBall()
    {
        return this.ball;
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("PickUp"))//Si lo que se toco es un pickup, deshabilitar
        {
            trigger.gameObject.SetActive(false);//Deshabilitar el pickup que se toco al entrar al trigger
            count = count + 1;
            this.ball.Points += 1;
            this.gameControl.UpdatePoints(this.count);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.ball.Health -= 20;
            if (this.ball.Health <= 0)
            {
                this.ball.Lives -= 1;               
            }
        }
    }   
}
