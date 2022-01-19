
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPush : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform _cam;
    [SerializeField]
    private float pushForce;
    [SerializeField]
    private bool canPush;
    [SerializeField]
    private GameObject obj;

    public RaycastHit Hit;

    private Rigidbody pushTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0) && canPush == true)
        {
            CmdRaycastPush();
        }
    }

    [Command(requiresAuthority = false)]
    void CmdRaycastPush()
    {
        if (Physics.Raycast(_cam.position, _cam.forward, out Hit, 2.0f))
        {
            if (Hit.rigidbody != null)
            {
                Hit.rigidbody.AddForce(_cam.forward * pushForce, ForceMode.Impulse);
            }
            canPush = false;
            StartCoroutine(PushTimer());
            Debug.DrawRay(_cam.position, _cam.forward * 2.0f, Color.yellow);
            Debug.Log("Fuck");
        }
        Debug.Log("Not worse");
    }

    IEnumerator PushTimer()
    {
        yield return new WaitForSeconds(1f);
        canPush = true;
    }

}
