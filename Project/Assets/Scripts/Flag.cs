using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Flag : NetworkBehaviour {
    private World _world;
    
    void Start() {
        _world = GameObject.Find("World").GetComponent<World>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Player>() != null) {
            Debug.Log("Flag collision with player " + NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().NetworkObjectId);
            _world.OnFlagPickUpServerRpc(NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().NetworkObjectId);
        }
    }
}
