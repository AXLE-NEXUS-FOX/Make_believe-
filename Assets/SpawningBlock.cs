using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blockPrefab;
    public ThirdPersonController playerScript;
    public GameObject playerModel;
    //public Stack<GameObject> blocks = new Stack<GameObject>();
    public List<GameObject> Blocks = new List<GameObject>();
    public float BlockYPlacement = -0.5f;
    void Start()
    {
        
    }

    void DestroyLastBlock()
    {
        Destroy(Blocks[Blocks.Count - 1]);
        Blocks.Remove(Blocks[Blocks.Count - 1]);

    }

    void DestroyAllBlocks()
    {
        foreach (GameObject block in Blocks)
        {
            Destroy(block);
           
        }
        Blocks.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        


        if(Input.GetKeyDown(KeyCode.KeypadEnter) && !playerScript.Grounded)
        {
          GameObject Block = Instantiate(blockPrefab, playerModel.transform.position + new Vector3(0,BlockYPlacement,0), Quaternion.identity);
          Blocks.Add(Block);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            DestroyLastBlock();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            DestroyAllBlocks();
        }
    }
}
