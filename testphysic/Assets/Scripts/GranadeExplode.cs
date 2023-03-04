using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeExplode : MonoBehaviour
{
    public GameObject explodePrefab;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explodePrefab, transform.position, transform.rotation);   
        Destroy(gameObject);
    }
}
