using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBomb : NetworkBehaviour
{
    [SerializeField]
    private Transform hand;

    [SerializeField]
    private GameObject bomb;

    private GameObject insantiation;

    [SerializeField]
    private bool bombIgnited;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BombDrop();
        }
    }

    [Command]
    void BombDrop()
    {
        if (bombIgnited == false)
        {
            bombIgnited = true;
            NetworkServer.Spawn(insantiation = Instantiate(bomb, hand.transform.position, Quaternion.identity));
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        bombIgnited = false;
    }
}
