using UnityEngine;

public class GrenadeShooting : MonoBehaviour
{
    public float speed = 10f;
    public GameObject bullet;
    public Transform firePoint;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

            newBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * speed;
        }
    }
}
