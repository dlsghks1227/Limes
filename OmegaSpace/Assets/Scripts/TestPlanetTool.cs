using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlanetTool : MonoBehaviour
{
    [SerializeField]
    private GameObject    imagePlanet = null;
    [SerializeField]
    private GameObject    imageAsteroidBelt = null;
    [SerializeField]
    private GameObject    planetTooltip = null;
    [SerializeField]
    private Vector3       offset;

    private GameObject    target = null;
    private GameObject    currObject = null;

    private PlanetTooltip planetTool = null;
    private Planet        currPlanet = null;


    void Start()
    {
        planetTooltip.SetActive(false);
        offset = new Vector3(200, 0, 0);
        planetTool = planetTooltip.GetComponent<PlanetTooltip>();
        imageAsteroidBelt.SetActive(false);
        imagePlanet.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.collider.gameObject;
          
            if (hit.collider.gameObject.GetComponent<Planet>() != null)
            {
                Planet planet = target.GetComponent<Planet>();

                currPlanet = planet;
                planetTool.SetTextPlanet(planet.planetName);
                planetTool.GetDescItem(0).SetDesc("크기 : " + planet.landSize.ToString());
                planetTool.GetDescItem(1).SetDesc("감지 수 : " + planet.numCivilization.ToString());
                planetTool.GetDescItem(2).SetDesc("매장량 : " + planet.iron.resAmount.ToString());
                planetTool.GetDescItem(3).SetDesc("매장량 : " + planet.gold.resAmount.ToString());

                currObject = imagePlanet;
                currObject.SetActive(true);

                Vector3 UIposition = Camera.main.WorldToScreenPoint(target.transform.position);
                planetTooltip.transform.position = UIposition + offset;
                planetTooltip.SetActive(true);

            }
            else if(hit.collider.gameObject.GetComponent<AsteroidBelt>() != null)
            {
                //currObject = imageAsteroidBelt;
                //currObject.SetActive(true);
                AsteroidBelt asteroidBelt = target.GetComponent<AsteroidBelt>();
            }
        }
        else
        {
            planetTooltip.SetActive(false);
            currPlanet = null;
        }
    }

}
