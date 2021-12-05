using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours.Triggers;   //For PickupTrigger type

public class UpdateProgress : MonoBehaviour
{
    private PickupTrigger winTrigger;
    private Collider[] totalMinionColliders;
    private const int collidersPerMinion = 21;
    private int currentMinionsSaved = 0;
    private const Vector3 safeRoomCenter = new Vector3(0, 5, -72.8f);
    private const Vector3 safeRoomHalfExtents = new Vector3(11.2f, 5, 11.2f);

    private void Start()
    {
        winTrigger = GetComponent<PickupTrigger>();
    }

    void Update()
    {
        totalMinionColliders = Physics.OverlapBox(safeRoomCenter, safeRoomHalfExtents, Quaternion.identity, 1 << LayerMask.NameToLayer("Minion"));
        currentMinionsSaved = totalMinionColliders.Length / collidersPerMinion;

        winTrigger.Progress = currentMinionsSaved;
    }
}
