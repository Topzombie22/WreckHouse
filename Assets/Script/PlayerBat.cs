using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBat : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform _cam;
    [SerializeField]
    private float pushForce;
    [SerializeField]
    private bool canHit;
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private AudioSource batNoise;

    public RaycastHit Hit;

    private Rigidbody pushTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0) && canHit == true)
        {
            CmdRaycastBat();
        }
    }

    [Command(requiresAuthority = false)]
    void CmdRaycastBat()
    {
        if (Physics.Raycast(_cam.position, _cam.forward, out Hit, 2.0f))
        {
            if (Hit.rigidbody != null)
            {
                batNoise.Play();
                Hit.rigidbody.AddForce(_cam.forward * pushForce, ForceMode.Impulse);
            }
            canHit = false;
            StartCoroutine(PushTimer());
            Debug.DrawRay(_cam.position, _cam.forward * 2.0f, Color.yellow);
            Debug.Log("Fuck");
        }
        Debug.Log("Not worse");
    }

    IEnumerator PushTimer()
    {
        yield return new WaitForSeconds(2f);
        canHit = true;
    }
}
