using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider BladeCollider;
    private bool slcing;
    public float minSliceVelocity = 0.01f;
    private TrailRenderer sliceTrail;
    public float sliceForce = 5f;
 

    public Vector3 direction { get; private set; }
    private void Awake()
    {
        mainCamera = Camera.main;
        BladeCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StartSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slcing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;
        slcing = true;
        BladeCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
    }

    private void StopSlicing()
    {
        slcing = false;
        BladeCollider.enabled = false;
        sliceTrail.enabled = false;
    }
    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;


        direction = newPosition - transform.position;
        float velocity = direction.magnitude / Time.deltaTime;
        BladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }


}
