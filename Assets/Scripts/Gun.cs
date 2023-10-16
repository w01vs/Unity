using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform barrel;
    [SerializeField] private int velocity = 1;
    [SerializeField] private float maxDistance = 50;
    [SerializeField] private GameObject bullet;
    private Animator animator;

    private Ray ray;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            Shoot();
        }

        //ray = new Ray(transform.parent.parent.position, transform.parent.parent.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);

    }

    public void Inspect()
    {
        animator.ResetTrigger("reload");
        animator.SetTrigger("inspect");
    }

    public void Reload()
    {
        animator.ResetTrigger("inspect");
        animator.SetTrigger("reload");
    }

    public void Shoot()
    {
        
        ray = new Ray(transform.parent.parent.position, transform.parent.parent.forward);
        GameObject bullet = Instantiate(this.bullet, barrel.position, barrel.rotation);
        RaycastHit hit;
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        if(Physics.Raycast(ray, out hit))
        {
            bullet.transform.LookAt(hit.point);
            rigidbody.velocity = (hit.point - barrel.position).normalized * velocity;
        }
        else
        {
            bullet.transform.LookAt(ray.GetPoint(maxDistance));
            rigidbody.velocity = (ray.GetPoint(maxDistance) - barrel.position).normalized * velocity;
        }
        
    }


}
