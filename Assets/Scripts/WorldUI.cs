using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldUI : MonoBehaviour
{
    public Transform cam;

    public void Start()
    {
        DisableChildren();
    }

    // Update is called once per frame
    public void Update()
    {
        PlayerRange();
    }

    public void LateUpdate()
    {
        gameObject.transform.LookAt(cam.position);
    }

    private void PlayerRange()
    {
        float distance = Vector3.Distance(transform.position, cam.position);
        if(distance < 10)
        {
            EnableChildren();
        }
        else
        {
            DisableChildren();
        }
    }

    private void EnableChildren()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    private void DisableChildren()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }
}
