using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public int sheepCollected = 0;
    public int wolvesDefeated = 0;
    public int berriesCollected = 0;
    public float time = 0.0f;
    public int simplfiedTime = 0; //Time but as an int
    public int score = 0;

    int wolfPoints = 500; //Points per wolf killed
    int berryPoints = 1000; //Points per berry collected and not eaten
    int sheepPoints = 2000; //Points per sheep saved

    public TextMeshProUGUI Sheepers;
    public TextMeshProUGUI Berry;
    public TextMeshProUGUI Wolf;
    public TextMeshProUGUI TextTime;
    public TextMeshProUGUI ScoreText;

    public GameObject player;
    Inventory inventory;
    PlayerCollider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        playerCollider = player.GetComponent<PlayerCollider>();

        sheepCollected = inventory.sheepRescued;
        wolvesDefeated = inventory.wolvesDefeated;
        berriesCollected = inventory.berryCount;
        time = playerCollider.time;
        simplfiedTime = (int) time;

        //Once values have been collected, calculate the final score:
        score += wolfPoints * wolvesDefeated;
        score += berryPoints * berriesCollected;
        score += sheepPoints * sheepCollected;
        score += 10000 - (50 * simplfiedTime);

        //Add score to exp:
        //print(score);
        Manager.Instance.exp += score;
        Sheepers.text = "Sheep Collected: " + sheepCollected;
        Berry.text = "Berries Collected: " + berriesCollected;
        Wolf.text = "Wolves Defeated: " + wolvesDefeated;
        TextTime.text = "Time: " + time;
        ScoreText.text = "Score: " + score;
    }

}
