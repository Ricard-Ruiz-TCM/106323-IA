﻿
using System.Collections.Generic;
using UnityEngine;

public class ROOMBA_Blackboard : MonoBehaviour {

    public float dustDetectionRadius = 60;  // radius for dust detection   
    public float pooDetectionRadius = 150;   // radius for poo detection    

    public float dustReachedRadius = 5; // reachability radius
    public float pooReachedRadius = 5;  // reachability radius

    public float chargingStationDetectionRadius = 999.0f; // radius for charging station detection   
    public float chargingStationReachedRadius = 2;  // reachability radius

    public float energyConsumptionPerSecond = 1;
    public float energyRechargePerSecond = 15;
    public float minCharge = 15;    // min threshold. If currentCharge is below this figure go to recharging station    
    public float maxCharge = 99;    // max threshold. Leave charging station if currentCharge reaches this level

    public float currentCharge = 100;

    private TextMesh energyLine;

    public List<GameObject> memory; // list of detected dust units not picked due to presence of poo
                                    // or other events

    void Start() {
        memory = new List<GameObject>();
        energyLine = GameObject.Find("EnergyLine").GetComponent<TextMesh>();
    }

    void Update() {
        Discharge(Time.deltaTime);
        energyLine.text = "Charge: " + Mathf.RoundToInt(currentCharge);
    }

    // invoke this method while in charging station
    public void Recharge(float deltaTime) {
        currentCharge = currentCharge + deltaTime * energyRechargePerSecond;
        if (currentCharge > 100) currentCharge = 100;
    }

    // invoked by Update to subtract energy. 
    private void Discharge(float deltaTime) {
        currentCharge = currentCharge - deltaTime * energyConsumptionPerSecond;
        if (currentCharge < 0) currentCharge = 0;
    }

    // invoke to memorize detected but unattended dust particles
    public void AddToMemory(GameObject gm) {
        if (!memory.Contains(gm)) {
            memory.Add(gm);
            gm.tag = "MEMO";
        }
    }

    // checks wether there's any dust particle momorized
    public bool somethingInMemory() {

        // Retrieve the lost memory items when i get on "CHARGE"
        GameObject[] lostMemo = GameObject.FindGameObjectsWithTag("MEMO");
        foreach (GameObject memo in lostMemo) {
            if (!memory.Contains(memo)) { memo.tag = "Untagged"; memory.Add(memo); }
        }

        return memory.Count > 0;
    }

    // get a dust particle from memory (and remove it). Retrieval follows a FIFO policy
    public GameObject RetrieveFromMemory() {
        if (memory.Count == 0) return null;
        else {
            GameObject result = memory[0];
            memory.RemoveAt(0);
            return result;
        }
    }

    // get a dust particle from memory (and remove it). Retrieval finds for the Nearest object
    public GameObject RetrieveNearestFromMemory(GameObject roomba) {
        if (memory.Count == 0) return null;
        else {
            int nearestIndex = 0;
            float distance = 999.0f;
            for (int i = 0; i < memory.Count; i++) {
                float tmp = SensingUtils.DistanceToTarget(roomba, memory[i]);
                if (tmp < distance) {
                    nearestIndex = i;
                    distance = tmp;
                }
            }
            GameObject result = memory[nearestIndex];
            memory.RemoveAt(nearestIndex);
            return result;
        }
    }

}
