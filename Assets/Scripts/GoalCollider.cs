using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    public Player player;

    // beendet die Zeitmessung wenn Spieler im Ziel
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TimeManager.EndTimer();
            player.speed = 0f;
        }
    }
}
