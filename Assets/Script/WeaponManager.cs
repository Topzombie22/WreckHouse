using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour
{
    private PlayerPush push;
    private PlayerBat bat;
    private PlayerBomb bomb;
    [SerializeField]
    private GameObject batModel;
    [SerializeField]
    private GameObject bombModel;
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
        bomb = GetComponent<PlayerBomb>();
        wep1 = true;
        weaponManager();
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
            wep3 = false;
            weaponManager();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            wep1 = false;
            wep2 = true;
            wep3 = false;
            weaponManager();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            wep1 = false;
            wep2 = false;
            wep3 = true;
            weaponManager();
        }
    }

    void weaponManager()
    {
        if (wep1 == true)
        {
            push.enabled = true;
            bat.enabled = false;
            bomb.enabled = false;
            bombModel.SetActive(false);
            batModel.SetActive(false);
        }
        if (wep2 == true)
        {
            bat.enabled = true;
            push.enabled = false;
            bomb.enabled = false;
            bombModel.SetActive(false);
            batModel.SetActive(true);
        }
        if (wep3 == true)
        {
            bomb.enabled = true;
            bat.enabled = false;
            push.enabled = false;
            bombModel.SetActive(true);
            batModel.SetActive(false);
        }
    }
}
