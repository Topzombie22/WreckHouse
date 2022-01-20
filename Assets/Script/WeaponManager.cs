using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour
{
    private PlayerPush push;
    private PlayerBat bat;
    [SerializeField]
    private GameObject batModel;
    [SerializeField]
    private bool wep1;
    [SerializeField]
    private bool wep2;
    [SerializeField]
    private bool wep3;

    // Start is called before the first frame update
    void Start()
    {
        push = GetComponent<PlayerPush>();
        bat = GetComponent<PlayerBat>();
        push.enabled = true;
        bat.enabled = false;
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKey(KeyCode.Alpha1))
        {
            wep1 = true;
            wep2 = false;
            weaponManager();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            wep1 = false;
            wep2 = true;
            weaponManager();
        }
    }

    void weaponManager()
    {
        if (wep1 == true)
        {
            push.enabled = true;
            bat.enabled = false;
            batModel.SetActive(false);
        }
        if (wep2 == true)
        {
            bat.enabled = true;
            push.enabled = false;
            batModel.SetActive(true);
        }
        if (wep3 == true)
        {

        }
    }
}
