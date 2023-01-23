using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private List<GameObject> CollectionsAircraft = new List<GameObject>();
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cleaner;

    [Header("Gameplay settings")]
    [SerializeField] private float startingPositionXPlanes = 10.0f;
    [SerializeField] private float attackBreakTime = 2.0f;
    [Range(0, 5)]
    [SerializeField] private int minimumNumberOfAircraft = 2;

    private readonly int _numberOfTracks = 5;
    private List<Vector2> flightTrack = new List<Vector2>();
    private List<GameObject> aircraft = new List<GameObject>();

    private bool playGame = false;


    public void Start()
    {
        float maxFlightAltitude = player.GetComponent<Player>().GetMaxFlightAltitude();
        float minFlightAltitude = player.GetComponent<Player>().GetMinFlightAltitude();
        float step = (Mathf.Abs(minFlightAltitude) + Mathf.Abs(maxFlightAltitude)) / _numberOfTracks;

        for (float y = maxFlightAltitude; y > minFlightAltitude; y -= step )
        {
            Vector2 newTrack = new Vector2(startingPositionXPlanes, y);
            flightTrack.Add(newTrack);
        }
    }

    public void newGame()
    {
        playGame = true;
        StartCoroutine(GameCoroutine());
    }
    public void endGame()
    {
        cleaner.GetComponent<Cleaner>().clearSky();
        playGame = false;
        
    }

    IEnumerator GameCoroutine()
    {
        while (playGame)
        {
            createAttackOfPlanes();
            yield return new WaitForSeconds(attackBreakTime);
        }      
    }

    private GameObject randAirPlane()
    {
        int airPlaneNumber = Random.Range(0, CollectionsAircraft.Count);
        return CollectionsAircraft[airPlaneNumber];
    }

    private int randTheNumberOfPlanes()
    {
        int numberOfPlanes = Random.Range(minimumNumberOfAircraft, _numberOfTracks);
        return numberOfPlanes;
    }

    private Vector2 randTrack()
    {
        int trackNumber = Random.Range(0, _numberOfTracks);
        Vector2 position = flightTrack[trackNumber];
        return position;
    }
    

    private void createAttackOfPlanes()
    {
        for(int i = 0; i < randTheNumberOfPlanes(); i++)
        {
            aircraft.Add(Instantiate(randAirPlane(), randTrack(), transform.rotation));
        }
    }

}
