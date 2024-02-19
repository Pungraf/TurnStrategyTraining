using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDice : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float ExplosionForce;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddExplosionForce(ExplosionForce, transform.position + Vector3.down * 30, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
