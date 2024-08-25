using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlanet : MonoBehaviour
{
    public float ShootDistance = 100f;
    public LayerMask SpawnLayer;
    public CreatePlanetUI UI;
    Vector3 hitPosition;
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, ShootDistance, SpawnLayer))
        {
            hitPosition = hit.point;
            if(hit.point == null)
            {

            }
            else
            {

                UI.OpenCreateCustomization();
            }
        }
    }
}
