using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using System.Drawing;

public class CreatePlanetUI : MonoBehaviour
{
    private Vector3 PlanetPosition;
    public GameObject CreateMenu;
    public GravitySystem GravitySystem;
    public ColorPickerTriangle ColorPicker;
    public TMP_InputField PlanetName;
    public TMP_InputField OrbitVelocity;
    public TMP_InputField Radius;
    public TMP_InputField Mass;
    public GameObject DefaultPlanet;
   
    public void OpenCreateCustomization()
    {
        CreateMenu.SetActive(true);
    }
    public void CloseCreateCustomization()
    {
        CreateMenu.SetActive(false);
    }
    public void CreatePlanet()
    {
        float Velocity = float.Parse(OrbitVelocity.text);
        float PlanetRadius = float.Parse(Radius.text);
        float PlanetMass = float.Parse(Mass.text);
        GameObject Spawn = Instantiate(DefaultPlanet, PlanetPosition, Quaternion.identity);
        TextMeshPro text = Spawn.transform.GetChild(0).GetComponent<TextMeshPro>();
        Rigidbody rigidbody = Spawn.GetComponent<Rigidbody>();
        rigidbody.mass = PlanetMass;
        Spawn.transform.localScale = new Vector3(PlanetRadius, PlanetRadius,PlanetRadius);
        GravitySystem.CelestialObjects.Add(Spawn.GetComponent<Rigidbody>());
        GravitySystem.Orbit(Velocity, GravitySystem.CelestialObjects.Count - 1, false);
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.SetColor("_BaseColor", ColorPicker.TheColor);
        Spawn.GetComponent<MeshRenderer>().material = material;

        text.text = PlanetName.text;

    }
}
