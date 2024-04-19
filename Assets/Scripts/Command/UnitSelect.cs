using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UnitSelect : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Unit curUnit; //current selected single unit

    [SerializeField]
    private Building curBuilding; //current selected single building
    public Building CurBuilding { get { return curBuilding; } }
    public Unit CurUnit { get { return curUnit; } }
    private Camera cam;
    private Faction faction;
    public static UnitSelect instance;
    private void Awake()
    {
        faction = GetComponent<Faction>();
    }
    private void SelectUnit(RaycastHit hit)
    {
        curUnit = hit.collider.GetComponent<Unit>();
        curUnit.ToggleSelectionVisual(true);
        Debug.Log("Selected Unit");

        if (GameManager.instance.MyFaction.IsMyUnit(curUnit))
        {
            ShowUnit(curUnit);
        }
    }
    private void TrySelect(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Unit":
                    SelectUnit(hit);
                    break;
                case "Building":
                    BuildingSelect(hit);
                    break;
            }
        }
    }
    private void ClearAllSelectionVisual()
    {
        if (curUnit != null)
            curUnit.ToggleSelectionVisual(false);
        if (curBuilding != null)
            curBuilding.ToggleSelectionVisual(false);

    }

    private void ClearEverything()
    {
        ClearAllSelectionVisual();
        curUnit = null;
        curBuilding = null;

        InfoManager.instance.ClearAllInfo();
    }
    private void ShowUnit(Unit u)
    {
        InfoManager.instance.ShowAllInfo(u);
    }
    private void ShowBuilding(Building b)
    {
        InfoManager.instance.ShowAllInfo(b);
        ActionManager.instance.ShowCreateUnitMode(b);
    }
    private void BuildingSelect(RaycastHit hit)
    {
        curBuilding = hit.collider.GetComponent<Building>();
        curBuilding.ToggleSelectionVisual(true);
        Debug.Log("Selected");
        if (GameManager.instance.MyFaction.IsMyBuilding(curBuilding))
        {
            Debug.Log("my building");
            ShowBuilding(curBuilding);//Show building info
        }
    }

    void Start()
    {
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Unit", "Building", "Resource", "Ground");
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            ClearEverything();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("1");
            TrySelect(Input.mousePosition);
        }
    }
}

