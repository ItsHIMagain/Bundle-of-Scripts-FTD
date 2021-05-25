using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject basePlane;
    public GameObject baseBoat;
    public GameObject advancedBoat;
    public GameObject advancedPlane;
    public GameObject laserSpot;
    public GameObject laserEffect;
    public GameObject largeBombPlane;
    public GameObject helicopter;

    public float spawnDelay;
    public float spawnDelayMinus;
    private bool spawningSomething;
    public int whatToSpawn;
    //0 = nothing
    //20 = Boat
    //45 = Plane
    //65 = Advance Boat
    //77 = Helicopter
    //88 = Advanced Plane
    //105 = Laser
    //120 = LargeBombPlane

    private int SpawnSpotRandom;
    public Transform planeSpawnPos1;
    public Transform planeSpawnPos2;
    public Transform planeSpawnPos3;
    public Transform planeSpawnPos4;
    public Transform planeSpawnPos5;
    public Transform planeSpawnPos6;
    public Transform planeSpawnPos7;
    public Transform planeSpawnPos8;
    public Transform planeSpawnPos9;
    public Transform planeSpawnPos10;

    public Transform boatSpawnPos1;
    public Transform boatSpawnPos2;

    public Transform laserSpawn1;
    public Transform laserSpawn2;
    public Transform laserSpawn3;

    public int maxDifficulty;

    private float delayToRespawn = 0;

    private GameObject thingToSpawn;

    private void Update()
    {
        whatToSpawn = 0;

        if (spawnDelay > 0)
        {
            spawnDelay -= 1 * Time.deltaTime;
        }
        else
        {
            if (spawnDelayMinus <= 1.4f)
            {
                spawnDelayMinus += 0.0032f;
            }
            if (maxDifficulty <= 120)
            {
                maxDifficulty += 1;
            }
            spawnDelay = Random.Range(0.4f - spawnDelayMinus, 2f - spawnDelayMinus);
            whatToSpawn = Random.Range(0, maxDifficulty);
        }
        
        if (whatToSpawn > 0 && whatToSpawn <= 20)
        {
            thingToSpawn = baseBoat;
        }
        else if (whatToSpawn >= 21 && whatToSpawn < 45)
        {
            thingToSpawn = basePlane;
        }
        else if (whatToSpawn <= 45 && whatToSpawn >= 65)
        {
            thingToSpawn = helicopter;
        }
        else if (whatToSpawn >= 66 && whatToSpawn <= 77)
        {
            thingToSpawn = advancedBoat;
        }
        else if (whatToSpawn >= 78 && whatToSpawn <= 88)
        {
            thingToSpawn = advancedPlane;
        }
        else if (whatToSpawn >= 89 && whatToSpawn <= 105)
        {
            if (GameObject.FindGameObjectsWithTag("Laser").Length == 0 && GameObject.FindGameObjectsWithTag("LaserTarget").Length == 0)
            {
                thingToSpawn = laserSpot;
            } else
            {
                whatToSpawn = Random.Range(0, maxDifficulty);
            }
        }
        else if (whatToSpawn >= 106 && whatToSpawn <= 120)
        {
            thingToSpawn = largeBombPlane;
        }

        if(thingToSpawn != null)
        {
            if(thingToSpawn != baseBoat && thingToSpawn != advancedBoat && thingToSpawn != laserSpot)
            {
                SpawnSpotRandom = Random.Range(0, 2); 
                switch (SpawnSpotRandom)
                {
                    case 0:
                        Instantiate(thingToSpawn, new Vector3(boatSpawnPos2.position.x, Random.Range(1.5f, 3), 0), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(thingToSpawn, new Vector3(boatSpawnPos1.position.x, Random.Range(1.5f, 3), 0), Quaternion.identity);
                        break;
                }
            } else if(thingToSpawn != laserSpot && thingToSpawn == baseBoat || thingToSpawn != laserSpot && thingToSpawn == advancedBoat)
            {
                SpawnSpotRandom = Random.Range(0, 2);
                switch (SpawnSpotRandom)
                {
                    case 0:
                        Instantiate(thingToSpawn, boatSpawnPos1.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(thingToSpawn, boatSpawnPos2.position, Quaternion.identity);
                        break;
                }
            } else if (delayToRespawn <= 0 && thingToSpawn == laserSpot)
            {
                SpawnSpotRandom = Random.Range(0, 3);
                switch (SpawnSpotRandom)
                {
                    case 0:
                        delayToRespawn = 13;
                        Instantiate(thingToSpawn, laserSpawn1.position, Quaternion.identity);
                        Instantiate(laserEffect, laserSpawn1.position, Quaternion.identity);
                        break;
                    case 1:
                        delayToRespawn = 13;
                        Instantiate(thingToSpawn, laserSpawn2.position, Quaternion.identity);
                        Instantiate(laserEffect, laserSpawn2.position, Quaternion.identity);
                        break;
                    case 2:
                        delayToRespawn = 13;
                        Instantiate(thingToSpawn, laserSpawn3.position, Quaternion.identity);
                        Instantiate(laserEffect, laserSpawn3.position, Quaternion.identity);
                        break;
                }
            }
            thingToSpawn = null;
        }
        SpawnSpotRandom = 0;
        if (delayToRespawn >= 0)
        {
            delayToRespawn -= 1 * Time.deltaTime;
        }
    }
        /*
        void Start() {
            spawnDelay = Random.Range(1, 2);
        }
        private void Update() {
            whatToSpawn = 0;

            if (spawnDelay > 0) {
                spawnDelay -= 1 * Time.deltaTime;
            } else {
                if(spawnDelayMinus <= 1f)
                {
                    spawnDelayMinus += 0.0016f;
                }
                if (maxDifficulty <= 120)
                {
                    maxDifficulty += 2;
                }
                spawnDelay = Random.Range(0.4f - spawnDelayMinus, 2f - spawnDelayMinus);
                whatToSpawn = Random.Range(0, maxDifficulty);
            }

            //Boat
            if (whatToSpawn > 0 && whatToSpawn <= 20) {
                SpawnSpotRandom = Random.Range(1, 3);
                if (SpawnSpotRandom == 1) {
                    Instantiate(baseBoat, boatSpawnPos1.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 2) {
                    Instantiate(baseBoat, boatSpawnPos2.position, Quaternion.identity);
                }
            }
            //Plane
            if (whatToSpawn >= 21 && whatToSpawn < 45) {
                SpawnSpotRandom = Random.Range(1, 11);

                if (SpawnSpotRandom == 1) {
                    Instantiate(basePlane, planeSpawnPos1.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 2) {
                    Instantiate(basePlane, planeSpawnPos2.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 3) {
                    Instantiate(basePlane, planeSpawnPos3.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 4) {
                    Instantiate(basePlane, planeSpawnPos4.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 5) {
                    Instantiate(basePlane, planeSpawnPos5.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 6) {
                    Instantiate(basePlane, planeSpawnPos6.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 7) {
                    Instantiate(basePlane, planeSpawnPos7.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 8) {
                    Instantiate(basePlane, planeSpawnPos8.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 9) {
                    Instantiate(basePlane, planeSpawnPos9.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 10) {
                    Instantiate(basePlane, planeSpawnPos1.position, Quaternion.identity);
                }
            }
            // Advance boat
            if (whatToSpawn <= 45 && whatToSpawn >= 65) {
                SpawnSpotRandom = Random.Range(1, 3);

                if (SpawnSpotRandom == 1) {
                    Instantiate(advancedBoat, boatSpawnPos1.position, Quaternion.identity);
                } else if (SpawnSpotRandom == 2) {
                    Instantiate(advancedBoat, boatSpawnPos2.position, Quaternion.identity);
                }
            }

            // Advanced Plane
            if (whatToSpawn >= 66 && whatToSpawn <= 77)
            {
                SpawnSpotRandom = Random.Range(1, 11);

                if (SpawnSpotRandom == 1)
                {
                    Instantiate(advancedPlane, planeSpawnPos1.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 2)
                {
                    Instantiate(advancedPlane, planeSpawnPos2.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 3)
                {
                    Instantiate(advancedPlane, planeSpawnPos3.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 4)
                {
                    Instantiate(advancedPlane, planeSpawnPos4.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 5)
                {
                    Instantiate(advancedPlane, planeSpawnPos5.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 6)
                {
                    Instantiate(advancedPlane, planeSpawnPos6.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 7)
                {
                    Instantiate(advancedPlane, planeSpawnPos7.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 8)
                {
                    Instantiate(advancedPlane, planeSpawnPos8.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 9)
                {
                    Instantiate(advancedPlane, planeSpawnPos9.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 10)
                {
                    Instantiate(advancedPlane, planeSpawnPos1.position, Quaternion.identity);
                }
            }
            // Laser
                if (whatToSpawn >= 78 && whatToSpawn <= 88)
                {
                    SpawnSpotRandom = Random.Range(1, 4);

                        if (SpawnSpotRandom == 1 && delayToRespawn <= 0)
                        {
                            delayToRespawn = 13;
                        Instantiate(laserSpot, laserSpawn1.position, Quaternion.identity);
                        Instantiate(laserEffect, laserSpawn1.position, Quaternion.identity);
                    }
                    else if (SpawnSpotRandom == 2 && delayToRespawn <= 0)
                        {
                            delayToRespawn = 13;
                        Instantiate(laserSpot, laserSpawn2.position, Quaternion.identity);
                        Instantiate(laserEffect, laserSpawn2.position, Quaternion.identity);
                    }
                        else if (SpawnSpotRandom == 3 && delayToRespawn <= 0)
                        {
                            delayToRespawn = 13;
                        Instantiate(laserSpot, laserSpawn3.position, Quaternion.identity);
                        Instantiate(laserEffect, laserSpawn3.position, Quaternion.identity);
                    }
                }
                if(delayToRespawn >= 0)
                {
                    delayToRespawn -= 1 * Time.deltaTime;
                }

            if (whatToSpawn >= 89 && whatToSpawn <= 105)
            {
                SpawnSpotRandom = Random.Range(1, 11);

                if (SpawnSpotRandom == 1)
                {
                    Instantiate(largeBombPlane, planeSpawnPos1.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 2)
                {
                    Instantiate(largeBombPlane, planeSpawnPos2.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 3)
                {
                    Instantiate(largeBombPlane, planeSpawnPos3.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 4)
                {
                    Instantiate(largeBombPlane, planeSpawnPos4.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 5)
                {
                    Instantiate(largeBombPlane, planeSpawnPos5.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 6)
                {
                    Instantiate(largeBombPlane, planeSpawnPos6.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 7)
                {
                    Instantiate(largeBombPlane, planeSpawnPos7.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 8)
                {
                    Instantiate(largeBombPlane, planeSpawnPos8.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 9)
                {
                    Instantiate(largeBombPlane, planeSpawnPos9.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 10)
                {
                    Instantiate(largeBombPlane, planeSpawnPos1.position, Quaternion.identity);
                }
            }

            if (whatToSpawn >= 106 && whatToSpawn <= 120)
            {
                SpawnSpotRandom = Random.Range(1, 11);

                if (SpawnSpotRandom == 1)
                {
                    Instantiate(helicopter, planeSpawnPos1.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 2)
                {
                    Instantiate(helicopter, planeSpawnPos2.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 3)
                {
                    Instantiate(helicopter, planeSpawnPos3.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 4)
                {
                    Instantiate(helicopter, planeSpawnPos4.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 5)
                {
                    Instantiate(helicopter, planeSpawnPos5.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 6)
                {
                    Instantiate(helicopter, planeSpawnPos6.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 7)
                {
                    Instantiate(helicopter, planeSpawnPos7.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 8)
                {
                    Instantiate(helicopter, planeSpawnPos8.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 9)
                {
                    Instantiate(helicopter, planeSpawnPos9.position, Quaternion.identity);
                }
                else if (SpawnSpotRandom == 10)
                {
                    Instantiate(helicopter, planeSpawnPos1.position, Quaternion.identity);
                }
            }

        }
        */
}

