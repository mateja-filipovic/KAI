using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public CheckpointManager CheckpointManager { private get; set; }

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<CarController>(out CarController carController))
            CheckpointManager.OnCheckpointReached(this, other.transform);
    }
}
