//Diego Hiriart Leon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;//Velocidad de movimiento de la esfera
    public TextMeshProUGUI countText;//Componente de texto del contador de la UI
    public GameObject winTextObject;

    private Rigidbody rb;//Referencia al rigidbody de la esfera
    private int count;//Contador de pickups
    private float movementX;
    private float movementY;    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();//Poner el rigidbody de la esfera en la referncia
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();//Modificar el texto de la UI
        if (count >= 12)//Mostrar mensaje de fin de juego
        {
            winTextObject.SetActive(true);          
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);//Aniade una fuerza al rigidbody, o sea lo mueve, multiplicado por la velocidad de movimiento
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))//Si lo que se toco es un pickup, deshabilitar
        {
            other.gameObject.SetActive(false);//Deshabilitar el pickup que se toco al entrar al trigger
            count = count + 1;
            SetCountText();
        }      
    }
}
