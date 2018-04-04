using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Manager : MonoBehaviour {

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
    public uint maxInvadingPlants, invadingPlantCount = 0, minStartingInvadingPlants = 10;
    public List<GameObject> possiblePlantSpots;
    public GameObject invadingPlantPrefab;

    protected virtual void StartGame()
    {
        currGameState = GameState.Playing;
    }

    protected void GeneratePlants()
    {
        //TODO: possibly rethink this part, as it can generate inumerous plants. (add min range, add random safeguards e.g. increasing probability, to do it all in one go)
        //Populate random spots.
        for (int i = 0; i < possiblePlantSpots.Count; i++)
            if (Random.Range(0.0f, 1.0f) > 0.5f && !possiblePlantSpots[i].GetComponent<GameInvadingPlant>())
            {
                possiblePlantSpots[i] = Instantiate(invadingPlantPrefab, possiblePlantSpots[i].transform.position, Quaternion.identity);
                invadingPlantCount++;
            }
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
