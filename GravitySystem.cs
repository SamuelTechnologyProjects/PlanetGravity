using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    public float G = 1f;
    public List<Rigidbody> CelestialObjects = new List<Rigidbody>();
    public void Start()
    {
        GameObject[] game = GameObject.FindGameObjectsWithTag("celestialObject");
        foreach (GameObject gameObj in game)
        {
            CelestialObjects.Add(gameObj.GetComponent<Rigidbody>());
        }
        Orbit(1,0,true);
    }
    public void FixedUpdate()
    {
        Gravity();

    }
    public void Gravity()
    {
        for (int a = 0; a < CelestialObjects.Count; a++)
        {
            for (int b = 0; b < CelestialObjects.Count; b++)
            {
                if (a != b)
                {
                    Rigidbody rbA = CelestialObjects[a];
                    Rigidbody rbB = CelestialObjects[b];

                    float m1 = rbA.mass;
                    float m2 = rbB.mass;

                    Vector3 obj1 = rbA.transform.position;
                    Vector3 obj2 = rbB.transform.position;
                    float r = Vector3.Distance(obj1, obj2);

                    // Prevent division by zero
                    if (r == 0f)
                    {
                        continue;
                    }

                    // Calculate the gravitational force
                    Vector3 force = (obj2 - obj1).normalized * ((G * (m1 * m2)) / (r * r));

                    // Apply the force to both objects
                    rbA.AddForce(force);
                    rbB.AddForce(-force); // Apply an equal and opposite force to the other object
                }
            }
        }
    }

    public void Orbit(float orbitspeed, int index,bool all)
    {
        for (int a = 0; a < CelestialObjects.Count; a++)
        {
            for (int b = 0; b < CelestialObjects.Count; b++)
            {
                if (a != b)
                {
                    Rigidbody rbA = CelestialObjects[a];
                    Rigidbody rbB = CelestialObjects[b];

                    float m2 = rbB.mass;
                    Vector3 obj1 = rbA.transform.position;
                    Vector3 obj2 = rbB.transform.position;
                    float r = Vector3.Distance(obj1, obj2);

                    // Prevent division by zero
                    if (r == 0f)
                    {
                        continue;
                    }

                    // Calculate the velocity to set an initial orbit
                    Vector3 direction = (obj2 - obj1).normalized;
                    rbA.transform.LookAt(rbB.transform);
                    if (a == index || b == index)
                    {
                        rbA.velocity += Vector3.Cross(direction, Vector3.up)  * orbitspeed;
                    }
                    else if(!all)
                    {
                        rbA.velocity += Vector3.Cross(direction, Vector3.up) * Mathf.Sqrt((G * m2) / r)*0f;
                    }
                    else if(all)
                    {
                        rbA.velocity += Vector3.Cross(direction, Vector3.up) * Mathf.Sqrt((G * m2) / r);
                    }
                }
            }
        }
    }

}
