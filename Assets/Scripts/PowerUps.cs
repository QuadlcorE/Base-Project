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
    //public GameObject jaugernautPrefab;
    public CannonAI cannonPrefab;
    public TurretAI turretPrefab;
    //public GameObject dropBoxPrefab;
    //public GameObject regenPrefab;

    public static float cannonCoolDownTime = 5f;
    public static float turretCoolDownTime = 5f;
    public static float dropBoxCoolDownTime = 5f;
    public static float regenCoolDownTime = 5f;

    public void activate(PowerUpDefinitions poweruptype, Transform DropLocation, GameObject target)
    {
        switch (poweruptype)
        {
            case PowerUpDefinitions.Jaugernaut:
                // Instantiate(jaugernautPrefab, DropLocation.position, DropLocation.rotation);
                break;
            case PowerUpDefinitions.Cannon:
                CannonAI newCanon = Instantiate(cannonPrefab, DropLocation.position, DropLocation.rotation);
                newCanon.SetTracker(target);
                break;
            case PowerUpDefinitions.Turret:
                TurretAI newTurret = Instantiate(turretPrefab, DropLocation.position, DropLocation.rotation);
                newTurret.SetTracker(target);
                break;
            case PowerUpDefinitions.DropBox:
                //Instantiate(dropBoxPrefab, DropLocation.position, DropLocation.rotation);
                break;
            case PowerUpDefinitions.Regen:
                //Instantiate(regenPrefab, DropLocation.position, DropLocation.rotation);
                break;
        }

    }


}
