using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using Mirror;
public class Bomb : NetworkBehaviour
{
    public int damage = 50;
    public float explosionRadius = 2f;
    public float explosionDelay = 2f;
    public float destroyDelay = 1f;

    public GameObject explosionPrefab;
    public GameObject linePrefab;

    private Animation anim;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);    
    }
    void Awake()
    {
        anim = GetComponent<Animation>();
    }
    void Start()
    {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);

        Explode(transform.position, explosionRadius);
        CmdExplode(transform.position, explosionRadius, damage);

        anim.Play("Shrink");

        yield return new WaitForSeconds(destroyDelay);

        NetworkServer.Destroy(gameObject);
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject clone = Instantiate(linePrefab, transform);
        LineRenderer line = clone.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    [Command]
    void CmdExplode(Vector3 position, float radius, int damage)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        foreach (var hit in hits)
        {
            Health health = hit.GetComponent<Health>();
            if (health)
            {
                health.TakeDamage(damage);
            }
        }
    }

    void Explode(Vector3 position, float radius)
    {
        // Spawn Explosion Effect
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(position, radius);
        foreach (var hit in hits)
        {
            NetworkIdentity networkId = hit.GetComponent<NetworkIdentity>();
            if (networkId && hit.name.Contains("Enemy"))
            {
                CreateLine(transform.position, hit.transform.position);
            }
        }
    }

    
}
