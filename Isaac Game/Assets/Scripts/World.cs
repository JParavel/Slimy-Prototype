using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World instance;
    [SerializeField] private GameObject[] entities;
    [SerializeField] private Player player;
    private Dictionary<string, GameObject> entityDictionary = new Dictionary<string, GameObject>();

    private void Awake() {

        instance = this;

        foreach (GameObject entity in entities)
        {
            entityDictionary.Add(entity.name, entity);
        }
    }

    public GameObject GetEntity(string name){
        return entityDictionary[name];
    }

    public Player GetPlayer()
    {
        return player;
    }
}
