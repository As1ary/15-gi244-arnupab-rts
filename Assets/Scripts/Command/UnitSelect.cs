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
    private List<Unit> curUnits = new List<Unit>(); //current selected single unit
    public List<Unit> CurUnits {get { return curUnits;}}

    [SerializeField]
    private Building curBuilding; //current selected single building
    public Building CurBuilding { get { return curBuilding; } }
    
    private Camera cam;
    private Faction faction;
    [SerializeField]
    private ResourceSource curResource; //current selected resource
    public static UnitSelect instance;
    private void Awake()
    {
        faction = GetComponent<Faction>();
    }
    private void SelectUnit(RaycastHit hit)
    {
        Unit unit = hit.collider.GetComponent<Unit>();
        
        Debug.Log("Selected Unit");

        if (GameManager.instance.MyFaction.IsMyUnit(unit))
        {
            ShowUnit(unit);
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
                case "Resource":
                    ResourceSelect(hit);
                    break;
            }
        }
    }
    private void ClearAllSelectionVisual()
    {
        foreach (Unit u in curUnits)
            u.ToggleSelectionVisual(false);
        if (curBuilding != null)
            curBuilding.ToggleSelectionVisual(false);
        if (curResource != null)
            curResource.ToggleSelectionVisual(false);

    }

    private void ClearEverything()
    {
        ClearAllSelectionVisual();
        curUnits = null;
        curBuilding = null;

        InfoManager.instance.ClearAllInfo();
    }
    private void ShowUnit(Unit u)
    {
        InfoManager.instance.ShowAllInfo(u);
        if(u.IsBuilder)
            ActionManager.instance.ShowBuilderMode(u);
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

     private void ShowResource()
    {
        InfoManager.instance.ShowAllInfo(curResource);//Show resource info in Info Panel

    }

     private void ResourceSelect(RaycastHit hit)
    {
        curResource = hit.collider.GetComponent<ResourceSource>();
        if (curResource == null)
            return;

        curResource.ToggleSelectionVisual(true);
        ShowResource();//Show resource info
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

