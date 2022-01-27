using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public List<Transform> pickupLocations;
    public GameObject healthPickup;
    public GameObject bandaidPickup;
    public GameObject ammoPickup;
    public GameObject pistolPickup;
    public GameObject tranqPickup;

    public static PickupSpawner instance; 
    void Start()
    {
        instance = this;
        Invoke("SpawnPickups", 3f);
    }

    public void SpawnPickups()
    {
        foreach (Transform t in pickupLocations)
        {
            if (t.name.Contains("Health"))
            {
                print("health pickup");
                Instantiate(healthPickup, t);
            }
            else if (t.name.Contains("Bandaid"))
            {
                print("bandaid pickup");
                Instantiate(bandaidPickup, t);
            }
            else if (t.name.Contains("Ammo"))
            {
                print("ammo pickup");
                Instantiate(ammoPickup, t);
            }
            else if (t.name.Contains("Pistol"))
            {
                print("pistol pickup");
                GameObject go = Instantiate(pistolPickup, t);
            }
            else if (t.name.Contains("Tranq"))
            {
                if(TimeScaler.instance.didCaraSequence)
                {
                    print("tranq pickup");
                    GameObject go = Instantiate(tranqPickup, t);
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
