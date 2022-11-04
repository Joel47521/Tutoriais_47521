using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    float rotSpeed = 2;
    // Start is called before the first frame update
    void CreateBullet()
    {
        Instantiate(bullet, turret.transform.position, turret.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateBullet();
        }
    }
}
