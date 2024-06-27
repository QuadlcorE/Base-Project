using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpDefinitions 
{
    Jaugernaut,
    Cannon,
    Turret,
    DropBox,
    Regen,
}

public class PowerUps : MonoBehaviour
{
    public static GameObject jaugernautPrefab;
    public static GameObject cannonPrefab;
    public static GameObject turretPrefab;
    public static GameObject dropBoxPrefab;
    public static GameObject regenPrefab;

    public static void activate(PowerUpDefinitions poweruptype, Transform DropLocation)
    {
        switch (poweruptype)
        {
            case PowerUpDefinitions.Jaugernaut:
                Instantiate(jaugernautPrefab, DropLocation.position, DropLocation.rotation);
                break;
            case PowerUpDefinitions.Cannon:
                Instantiate(cannonPrefab, DropLocation.position, DropLocation.rotation);
                break;
            case PowerUpDefinitions.Turret:
                Instantiate(turretPrefab, DropLocation.position, DropLocation.rotation);
                break;
            case PowerUpDefinitions.DropBox:
                Instantiate(dropBoxPrefab, DropLocation.position, DropLocation.rotation);
                break;
            case PowerUpDefinitions.Regen:
                Instantiate(regenPrefab, DropLocation.position, DropLocation.rotation);
                break;
        }

    }


}
