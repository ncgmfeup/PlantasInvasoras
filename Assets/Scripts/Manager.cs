using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour {

    public int weaponEquippedIndex = 0;
    public uint maxNumGroundCovered;
    public List<GameObject> possiblePlantSpots;

    public void ChangeWeapon(int newWeaponIndex) { weaponEquippedIndex = newWeaponIndex; }

    protected float GetInvadingPlantSpots()
    {
        uint invadingPlantCount = 0;
        foreach(GameObject plantSpot in possiblePlantSpots)
            if(plantSpot.GetComponent<GameInvadingPlant>())
                invadingPlantCount++;
        return invadingPlantCount;
    }

    protected abstract bool GameOverCheck();

}
