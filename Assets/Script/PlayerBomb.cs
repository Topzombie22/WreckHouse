using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBomb : NetworkBehaviour
{
    [SerializeField]
    public float explosionForce;
    [SerializeField]
    private float explosionRadius;

    private float bombTimer = 5.0f;

    [SerializeField]
    private Transform hand;

    [SerializeField]
    private GameObject bomb;

    [SerializeField]
    private float timer;

    private GameObject insantiation;

    private bool bombIgnited;

    private Collider[] hitCollliders;
    // Start is called before the first frame update
    void Start()
    {
        bombIgnited = false;
        timer = 5.0f;
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bombIgnited == false && Input.GetKeyDown(KeyCode.Mouse0))
        {
            BombDrop();
        }
        Timer();
    }

    [Command]
    void BombDrop()
    {
        bombIgnited = true;
        NetworkServer.Spawn(insantiation = Instantiate(bomb, hand.transform.position, Quaternion.identity));
        Timer();
    }

    void Explosion()
    {
        timer = 5.0f;
        hitCollliders = (Physics.OverlapSphere(gameObject.transform.position, explosionRadius));
        foreach (var hitCollider in hitCollliders)
        {
            Debug.Log("Agony");
            if (hitCollider.GetComponent<Rigidbody>() != null)
            {
                hitCollider.GetComponent<Rigidbody>().isKinematic = false;
                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, insantiation.transform.position, explosionRadius, 1, ForceMode.Impulse);
            }
        }
        DestroyBomb();
        bombIgnited = false;
    }

    void Timer()
    {
        if (bombIgnited == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            Debug.Log("Ago");
            Explosion();
        }
    }

    [Command]
    void DestroyBomb()
    {
        DestroyImmediate(insantiation.gameObject, true);
    }
}
