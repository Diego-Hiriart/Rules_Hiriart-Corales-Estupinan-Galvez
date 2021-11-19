//Diego Hiriart Leon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoController : MonoBehaviour
{
    public GameObject camino;//Camino que sigue el objeto
    public Transform[] nodos;//Puntos del camino a seguir
    private int nodoActual = 0;//Inicia en el nodo 0
    private Transform nodoSiguiente;
    public float velocidad = 0;

    void Update()
    {
        //Rotacion constante para empujar
        transform.Rotate(new Vector3(0, 110, 0) * Time.deltaTime * velocidad/4);//velocidad dividido para 4 sino gira demasiado rapido     

        //Segun en donde este el obstaculo, seguir un camino distinto
        if (CompareTag("Rampa"))
        {
            transform.RotateAround(camino.transform.position, new Vector3(0,1,0), 140f *  Time.deltaTime);
            /*Rotate around indica que debe rotar alrededor del primer parametro, alrededor del  eje que se indique 
             * en el segundo (eje y en este caso), con una cierta velocidad (3er parametro)*/
        }
        else if (CompareTag("Suelo"))
        {
            if (nodoActual < (nodos.Length - 1))//Recorrer normalmente
            {
                
                nodoSiguiente = nodos[(nodoActual + 1)];                
                RecorrerCamino();//Ir de la posicion actual al destino (nodoSiguiente)
                if (transform.position.Equals(nodoSiguiente.position))//Cambiar el nodo actual solo cuando se llegue al destino
                {
                    nodoActual++;
                }               
            }
            else//Reiniciar el recorrido
            {
                nodoSiguiente = nodos[0];
                RecorrerCamino();
                if (transform.position.Equals(nodos[0].position))//Reiniciar nodo solo si ya se esta en el inicio de nuevo
                {
                    nodoActual = 0;
                }              
            }
        }       
    }

    void RecorrerCamino()//Funcion para ir de un nodo (punto) a otro
    {
        //Mover el obstaculo a donde debe estar (siguiente nodo)
        transform.position = Vector3.MoveTowards(transform.position, nodoSiguiente.position, Time.deltaTime * velocidad);
    }
}
