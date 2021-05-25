using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<int> upgrades;
    public int upgrade1ID; //1: Extra Damage 2: Extra Hp 3: Screen clearer 4: Some missiles can bounce 5: Last chance 6: better regen 7: the end
    public int upgrade2ID;
    public int upgrade3ID;
    private int upgradeTaken = -1;

    public Text title1;
    public Text desc1;
    public Text title2;
    public Text desc2;
    public Text title3;
    public Text desc3;
    public Animator anim;

    private GameObject tentacleBase;
    private GameObject[] tentacleSpriterenderer;

    public static UpgradeManager instance;
    //This horribleness exists just to fix debugging
    public int isMidUpgrade = 0;

    [Header("Upgrade specific")]
    public Sprite armorSprite;
    public Sprite spikedSprite;
    public bool bouncy;
    public bool lastChance;

    [Header("The End")]
    public GameObject secondBg;
    public Sprite uncorrupted;
    public Sprite uncorruptedWater;
    public Sprite corruption1;
    public Sprite corruption2;
    public Sprite corruption3;
    public Sprite corruptedWater1;
    public Sprite corruptedWater2;
    public GameObject sky;
    public GameObject water;
    private string corruptedText = "We.";
    public int corruptionCounter = 0;
    public bool theEnd = false;


    public void Start()
    {
        //Reset values
        ScoreManager.score = 0;
        corruptionCounter = 0;

        instance = this;
        tentacleSpriterenderer = GameObject.FindGameObjectsWithTag("Tentacle");

        tentacleBase = GameObject.FindGameObjectWithTag("Base");
        for (int i = 1; i <= 7; ++i)
        {
            upgrades.Add(i);
        }
    }

    public void UpdateOptions()
    {
        if (isMidUpgrade == 0)
        {
            isMidUpgrade = 1;
            anim.SetBool("Slide", true);

            upgrade1ID = upgrades[Random.Range(0, upgrades.Count)];
            upgrade2ID = upgrades[Random.Range(0, upgrades.Count)];
            upgrade3ID = upgrades[Random.Range(0, upgrades.Count)];
        }
    }

    public void Evolve1()
    {
        AudioManager.instance.playSound("upgrade");
        upgradeTaken = upgrade1ID;
        if (upgrades.Count == 1)
        {
            upgrades.Remove(0);
        }
        else
        {
            if (upgrade1ID != 7 || corruptionCounter == 2)
            {
                upgrades.Remove(upgrade1ID);
            }
        }
    }

    public void Evolve2()
    {
        AudioManager.instance.playSound("upgrade");
        upgradeTaken = upgrade2ID;
        if (upgrades.Count == 1)
        {
            upgrades.Remove(0);
        }
        else
        {
            if (upgrade2ID != 7 || corruptionCounter == 2)
            {
                upgrades.Remove(upgrade2ID);
            }
        }
    }

    public void Evolve3()
    {
        AudioManager.instance.playSound("upgrade");
        upgradeTaken = upgrade3ID;
        if (upgrades.Count == 1)
        {
            upgrades.Remove(0);
        }
        else
        {
            if (upgrade3ID != 7 || corruptionCounter == 2)
            {
                upgrades.Remove(upgrade3ID);
            }
        }
    }

    private void Update()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isMidUpgrade == 2)
        {
            isMidUpgrade = 0;
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("UpgradesSD"))
        {
            //freeze enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies)
            {
                if (go.GetComponent<PlaneController>() != null)
                {
                    go.GetComponent<PlaneController>().enabled = false;
                    go.GetComponent<Rigidbody2D>().isKinematic = true;

                }
                else if (go.GetComponent<BoatController>() != null)
                {
                    go.GetComponent<BoatController>().enabled = false;
                    go.GetComponent<Rigidbody2D>().isKinematic = true;

                }
                else if (go.GetComponent<AdvancedBoatController>() != null)
                {
                    go.GetComponent<AdvancedBoatController>().enabled = false;
                    go.GetComponent<Rigidbody2D>().isKinematic = true;

                }
                else if (go.GetComponent<EnemySpawner>() != null)
                {
                    go.GetComponent<EnemySpawner>().enabled = false;
                }
                else if (go.GetComponent<HelicopterController>() != null)
                {
                    go.GetComponent<HelicopterController>().enabled = false;
                    go.GetComponent<Rigidbody2D>().isKinematic = true;
                }
                else if (go.GetComponent<submarineController>() != null)
                {
                    go.GetComponent<submarineController>().enabled = false;
                    go.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            }
            Destroy(GameObject.FindGameObjectWithTag("Laser"));

            //Proceed
        }
        else if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("UpgradeSU"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies)
            {
                go.SetActive(true);
            }
        }

        //Reactivate scripts
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("UpgradeSU"))
        {
            //freeze enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in enemies)
            {
                if (go.GetComponent<PlaneController>() != null)
                {
                    go.GetComponent<PlaneController>().enabled = true;
                    go.GetComponent<Rigidbody2D>().isKinematic = false;

                }
                else if (go.GetComponent<BoatController>() != null)
                {
                    go.GetComponent<BoatController>().enabled = true;
                    go.GetComponent<Rigidbody2D>().isKinematic = false;
                }
                else if (go.GetComponent<AdvancedBoatController>() != null)
                {
                    go.GetComponent<AdvancedBoatController>().enabled = true;
                    go.GetComponent<Rigidbody2D>().isKinematic = false;
                }
                else if (go.GetComponent<EnemySpawner>() != null)
                {
                    go.GetComponent<EnemySpawner>().enabled = true;
                }
                else if (go.GetComponent<SpawnLaser>() != null)
                {
                    go.GetComponent<SpawnLaser>().enabled = true;
                }
                else if (go.GetComponent<HelicopterController>() != null)
                {
                    go.GetComponent<HelicopterController>().enabled = true;
                    go.GetComponent<Rigidbody2D>().isKinematic = false;
                }
                else if (go.GetComponent<submarineController>() != null)
                {
                    go.GetComponent<submarineController>().enabled = true;
                    go.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }
            //Proceed with the upgrade descriptions
        }
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            switch (upgrade1ID)
            {
                case 1:
                    title1.text = "Spiked Carapace";
                    desc1.text = "Your carapace evolves sharp spikes, allowing you to deal more damage.";
                    break;
                case 2:
                    title1.text = "Armored Carapace";
                    desc1.text = "You evolve a thougher carapace, allowing you to soak up more damage.";
                    break;
                case 3:
                    title1.text = "Screams of anguish";
                    desc1.text = "If harmed too much you, you let out a scream of anguish clearing all enemies.";
                    break;
                case 4:
                    title1.text = "Rubbery Carapace";
                    desc1.text = "Missiles occasionally bounce off instead of detonating.";
                    break;
                case 5:
                    title1.text = "Deny Death";
                    desc1.text = "Refuse the next call of death.";
                    break;
                case 6:
                    title1.text = "Improved regeneration";
                    desc1.text = "You can utilize more of the consumed material, allowing faster regeneration.";
                    break;
                case 7:
                    title1.text = "Distort Reality";
                    desc1.text = corruptedText;
                    break;
                default:
                    title1.text = "The end";
                    desc1.text = "We've ran out of evolutions.";
                    break;
            }
            //2
            switch (upgrade2ID)
            {
                case 1:
                    title2.text = "Spiked Carapace";
                    desc2.text = "Your carapace evolves sharp spikes, allowing you to deal more damage.";
                    break;
                case 2:
                    title2.text = "Armored Carapace";
                    desc2.text = "You evolve a thougher carapace, allowing you to soak up more damage.";
                    break;
                case 3:
                    title2.text = "Screams of anguish";
                    desc2.text = "If harmed too much you, you let out a scream of anguish clearing all enemies.";
                    break;
                case 4:
                    title2.text = "Rubbery Carapace";
                    desc2.text = "Missiles occasionally bounce off instead of detonating.";
                    break;
                case 5:
                    title2.text = "Deny Death";
                    desc2.text = "Refuse the next call of death.";
                    break;
                case 6:
                    title2.text = "Improved regeneration";
                    desc2.text = "You can utilize more of the consumed material, allowing faster regeneration.";
                    break;
                case 7:
                    title2.text = "Distort Reality";
                    desc2.text = corruptedText;
                    break;
                default:
                    title2.text = "The end";
                    desc2.text = "We've ran out of evolutions.";
                    break;
            }
            //3
            switch (upgrade3ID)
            {
                case 1:
                    title3.text = "Spiked Carapace";
                    desc3.text = "Your carapace evolves sharp spikes, allowing you to deal more damage.";
                    break;
                case 2:
                    title3.text = "Armored Carapace";
                    desc3.text = "You evolve a thougher carapace, allowing you to soak up more damage.";
                    break;
                case 3:
                    title3.text = "Screams of anguish";
                    desc3.text = "If harmed too much you, you let out a scream of anguish clearing all enemies.";
                    break;
                case 4:
                    title3.text = "Rubbery Carapace";
                    desc3.text = "Missiles occasionally bounce off instead of detonating.";
                    break;
                case 5:
                    title3.text = "Deny Death";
                    desc3.text = "Refuse the next call of death.";
                    break;
                case 6:
                    title3.text = "Improved regeneration";
                    desc3.text = "You can utilize more of the consumed material, allowing faster regeneration.";
                    break;
                case 7:
                    title3.text = "Distort Reality";
                    desc3.text = corruptedText;
                    break;
                default:
                    title3.text = "The end";
                    desc3.text = "We've ran out of evolutions.";
                    break;
            }
        }

        //Upgrades

        switch (upgradeTaken)
        {
            case 1:
                //Change Appearance
                for (int i = 0; i < tentacleSpriterenderer.Length; i++)
                {
                    tentacleSpriterenderer[i].GetComponent<SpriteRenderer>().sprite = spikedSprite;
                }
                StatManager.dmg = StatManager.dmg * 2;
                break;
            case 2:
                StatManager.maxHP += 500;
                StatManager.hp += 500;
                //Change Appearance
                for (int i = 0; i < tentacleSpriterenderer.Length; i++)
                {
                    tentacleSpriterenderer[i].GetComponent<SpriteRenderer>().sprite = armorSprite;
                }
                break;
            case 3:
                GameObject.FindGameObjectWithTag("Deathbox").GetComponent<playerHitbox>().screech = true;
                break;
            case 4:
                bouncy = true;
                break;
            case 5:
                lastChance = true;
                break;
            case 6:
                GameObject.FindGameObjectWithTag("Deathbox").GetComponent<playerHitbox>().improvedRegen = true;
                break;
            case 7:
                corruptionCounter += 1;
                GameObject[] bg = GameObject.FindGameObjectsWithTag("Background");
                screenFlasher.instance.flashScreen();
                GameObject[] waters = GameObject.FindGameObjectsWithTag("Foreground Water");
                if (corruptionCounter == 1)
                {
                    corruptedText = "Are.";
                    for (int i = 0; i < bg.Length - 1; i++)
                    {
                        bg[i].GetComponent<SpriteRenderer>().sprite = corruption1;
                        waters[i].GetComponent<SpriteRenderer>().sprite = corruptedWater1;
                        sky.GetComponent<SpriteRenderer>().color = new Color32(202, 100, 100, 255);
                        water.GetComponent<SpriteRenderer>().color = new Color32(137, 93, 93, 255);
                    }
                    secondBg.GetComponent<SpriteRenderer>().sprite = corruption1;
                }
                if (corruptionCounter == 2)
                {
                    corruptedText = "One.";
                    for (int i = 0; i < bg.Length - 1; i++)
                    {
                        bg[i].GetComponent<SpriteRenderer>().sprite = corruption2;
                        waters[i].GetComponent<SpriteRenderer>().sprite = corruptedWater2;
                        sky.GetComponent<SpriteRenderer>().color = new Color32(131, 48, 48, 255);
                        water.GetComponent<SpriteRenderer>().color = new Color32(116, 54, 54, 255);
                    }
                    secondBg.GetComponent<SpriteRenderer>().sprite = corruption2;
                }
                if (corruptionCounter == 3)
                {
                    for (int i = 0; i < bg.Length - 1; i++)
                    {
                        bg[i].GetComponent<SpriteRenderer>().sprite = corruption3;
                        sky.GetComponent<SpriteRenderer>().color = new Color32(112, 37, 37, 255);
                    }
                    secondBg.GetComponent<SpriteRenderer>().sprite = corruption3;
                    theEnd = true;

                }
                break;
        }
        if (upgradeTaken != -1)
        {
            upgradeTaken = -1;
            isMidUpgrade = 2;
            anim.SetBool("Slide", false);
        }
    }

    public void normalizeBackdrop()
    {
        bool hasHappened = false;
        if(hasHappened == false)
        {
            GameObject[] bg = GameObject.FindGameObjectsWithTag("Background");
            screenFlasher.instance.flashScreen();
            GameObject[] waters = GameObject.FindGameObjectsWithTag("Foreground Water");
            screenFlasher.instance.flashScreen();

            for (int i = 0; i < bg.Length - 1; i++)
            {
                bg[i].GetComponent<SpriteRenderer>().sprite = uncorrupted;
                waters[i].GetComponent<SpriteRenderer>().sprite = uncorruptedWater;
                sky.GetComponent<SpriteRenderer>().color = new Color32(159, 181, 204, 255);
                water.GetComponent<SpriteRenderer>().color = new Color32(106, 125, 148, 255);
            }
            hasHappened = true;
            secondBg.GetComponent<SpriteRenderer>().sprite = uncorrupted;
        }
    }
}
