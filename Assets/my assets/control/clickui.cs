using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickui : MonoBehaviour
{
    public LayerMask menuLayer;
    private RaycastHit result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           Physics.Raycast(gameObject.transform.position, transform.forward,out result,50);
            Debug.Log("racast has hit" + result.collider.gameObject.layer);
            if(result.collider.gameObject.layer == 9)
            {
                SceneManager.LoadScene("main menu");
            }
         
        }
    }
}
