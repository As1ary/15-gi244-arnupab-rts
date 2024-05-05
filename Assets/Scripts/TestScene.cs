using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    [SerializeField]
    private Unit[] units;

    public void SetIdle()
    {
        for(int i =0; i < units.Length; i++)
        {
            units[i].State = UnitState.Idle;        }
    }
    public void SetMove()
    {
        for (int i = 0; i < units.Length; i++)
        {
            units[i].State = UnitState.Move;
        }
    }
    public void SetAttack()
    {
        for (int i = 0;i< units.Length; i++)
        {
            units[i].State = UnitState.AttackUnit;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
