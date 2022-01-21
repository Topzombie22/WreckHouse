using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BombTimer : NetworkBehaviour
{
    [SerializeField]
    public float explosionForce;
    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private float timer;

    private bool bombIgnited;

    private Collider[] hitCollliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        timer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
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
                hitCollider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position, explosionRadius, 1, ForceMode.Impulse);
            }
        }
        DestroyBomb();
        bombIgnited = false;
    }

    void Timer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Debug.Log("Ago");
            Explosion();
        }
    }

    void DestroyBomb()
    {
        DestroyImmediate(this.gameObject, true);
    }
}
