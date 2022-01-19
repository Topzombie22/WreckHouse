using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        GameObject bulletClone = Instantiate(cube, new Vector3(1, 1, 1), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
