using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour {

    [SerializeField]
    GameObject TopSpawn;

    [SerializeField]
    GameObject BottomSpawn;

    [Header("Animation Properties")]
    [SerializeField]
    float speed;

    GameObject[] m_TopFood;
    GameObject[] m_BottomFood;

    Vector3 m_TopStartSpawnPosition;
    Vector3 m_BottomStartSpawnPosition;

    Vector3 m_TopDestination;
    Vector3 m_BottomDestination;

    #region Mono
    private void Awake()
    {
        FindGameObjectsWithTags();

        InitPointsOfInterest();
    }


    void FindGameObjectsWithTags()
    {
        m_TopFood = GameObject.FindGameObjectsWithTag("TopFood");
        m_BottomFood = GameObject.FindGameObjectsWithTag("BottomFood");
    }

    void InitPointsOfInterest()
    {
        m_TopDestination = TopSpawn.transform.position;
        m_BottomDestination = BottomSpawn.transform.position;
        m_TopStartSpawnPosition = new Vector3(m_BottomDestination.x, m_TopDestination.y, 0);
        m_BottomStartSpawnPosition = new Vector3(m_TopDestination.x, m_BottomDestination.y, 0);
    }

    void Update () {
        MoveListOfFood(m_TopFood, m_TopDestination, m_TopStartSpawnPosition);
        MoveListOfFood(m_BottomFood, m_BottomDestination,m_BottomStartSpawnPosition);
	}

    #endregion

    void MoveListOfFood(GameObject[] foods, Vector3 destination, Vector3 spawn)
    {
        foreach(GameObject food in foods)
        {
            food.transform.position = Vector3.MoveTowards(food.transform.position, destination, speed * Time.deltaTime);
            ResetPositionIfNeeded(food.transform, destination, spawn);
        }
    }

    void ResetPositionIfNeeded(Transform food, Vector3 destination, Vector3 spawn)
    {
        if (food.position == destination)
        {
            food.position = spawn;
        }
    }

}
