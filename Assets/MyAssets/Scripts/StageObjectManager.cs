﻿using UnityEngine;
using System.Collections;

public class StageObjectManager : MonoBehaviour
{

    public GameObject[] CreateObjects;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 30; ++i)
        {
            int index = Random.RandomRange(0, CreateObjects.Length);
            float xPos = (float)Random.RandomRange(-10, 10);
            float zPos = (float)Random.RandomRange(8, 15);
            CreateObjects[index].transform.position = new Vector3(xPos, 1.0f, zPos);
            CreateObjects[index].tag = "SuctionObject";
            Instantiate(CreateObjects[index]);
        }

        //GameObject.Find("StageObjectManager").SendMessage("Start");
    }

    // Update is called once per frame
    void Update()
    {

    }


}
