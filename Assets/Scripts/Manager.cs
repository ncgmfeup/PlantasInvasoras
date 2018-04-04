using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        //InitialCountdown,
        Playing,
        Won,
        Lost,
        Paused
    }

    public GameState currGameState;
    public int weaponEquippedIndex = 0;
    public uint minStartingInvadingPlants = 10, maxStartingInvadingPlants, maxInvadingPlants, invadingPlantCount = 0,
           minStartingNativePlants = 5, maxStartingNativePlants, nativePlantCount = 0;
    public float yInvadingPlantPos, yNativePlantPos, initialXInvadingPlantPos, xPosIncrement;
    public List<GameObject> possiblePlantSpots;
    public GameObject emptyTreePrefab, invadingPlantPrefab, nativePlantPrefab;

    void Start()
    {
        GeneratePlants();
        currGameState = GameState.Playing;
    }

    protected void GeneratePlants()
    {
        do
        {
            float currXPos = initialXInvadingPlantPos;
            for (int i = 0; i < possiblePlantSpots.Count; i++)
            {
                if(possiblePlantSpots[i] != emptyTreePrefab)
                    continue;
                float ranChance = Random.Range(0.0f, 1.0f);
                //Populate random spots.
                if(ranChance <= 0.35f && nativePlantCount < maxStartingNativePlants && nativePlantPrefab)
                {
                    nativePlantCount++;
                    possiblePlantSpots[i] = Instantiate(nativePlantPrefab,
                                            new Vector3(currXPos, yNativePlantPos),
                                            Quaternion.identity);
                }
                else if(invadingPlantPrefab && ranChance <= 0.5f && invadingPlantCount < maxStartingInvadingPlants)
                {
                    invadingPlantCount++;
                    possiblePlantSpots[i] = Instantiate(invadingPlantPrefab,
                                            new Vector3(currXPos, yNativePlantPos),
                                            Quaternion.identity);
                }
                else
                {
                    possiblePlantSpots[i] = Instantiate(emptyTreePrefab);
                }
                currXPos += xPosIncrement;
            }
        } while (nativePlantCount < minStartingNativePlants || invadingPlantCount < minStartingInvadingPlants);
    }

    public virtual void RemoveInvadingPlant(GameObject removedInvadingPlant)
    {
        invadingPlantCount--;
    }

    public void ChangeWeapon(int newWeaponIndex)
    {
        weaponEquippedIndex = newWeaponIndex;
    }

    protected virtual bool GameOverCheck()
    {
        return invadingPlantCount >= maxInvadingPlants;
    }
}
