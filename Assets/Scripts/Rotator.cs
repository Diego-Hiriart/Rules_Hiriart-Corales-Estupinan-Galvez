//Diego Hiriart Leon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //No se aplicaran fuerzas para rotar el cubo, entonces no se necesita FixedUpdate
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);//Rotate rota el objeto en su transform
        //deltaTime es un float que representa el tiempo entre frames, esto hace que sea un movimiento smooth
    }
}
