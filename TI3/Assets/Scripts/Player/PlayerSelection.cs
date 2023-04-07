﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [Header("Selected during playtime")]
    [SerializeField] private Worker selectedWorker;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //TrySelectWorker();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            TrySendWorker();
        }
    }
    private void TrySelectWorker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, Player.Instance.workerLayerMask);
        if (raycastHit.transform.TryGetComponent<Worker>(out Worker worker) == true)
        {
            selectedWorker = worker;
        }
    }
    private void TrySendWorker()
    {
        if (selectedWorker == null) { return; }
        Vector3 worldPosition = MouseSystem.Instance.GetWorldPosition();
        GridPosition localGridPosition = GridSystem.Instance.GetGridGroundPositionRelative(worldPosition);
        GridTile gridTile = GridSystem.Instance.TryGetGridTile(localGridPosition);
        if (gridTile == null) { return; }
        Vector3 localPosition = GridSystem.Instance.GetWorldPositionWithoutOffset(localGridPosition);
        selectedWorker.SetDestination(gridTile, localPosition); return;
    }
}