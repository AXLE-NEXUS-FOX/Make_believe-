using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private float InputX;


    private void FixedUpdate()
    {
        InputX = Input.GetAxisRaw("Horizontal");
       
    }



}
