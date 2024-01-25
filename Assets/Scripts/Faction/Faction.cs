using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Nation
{
    neutral = 0,
    Britain,
    Pirates,
    France,
    Spain,
    Portuguese,
    Dutch
}
public class Faction : MonoBehaviour
{
    // Start is called before the first frame update
        [SerializeField] private Nation nation;
        public Nation Nation { get { return nation; } }

        [Header("Resources")]
        [SerializeField]  int food;
        public int Food { get { return food; } set { food = value; } }
        [SerializeField] private int wood;
        public int Wood { get { return wood; } set {  wood = value; } }
        [SerializeField] private int gold;
        public int Gold { get { return gold; } set {  gold = value; } }
        [SerializeField] private int stone;
        public int Stone { get {  return stone; } set {  stone = value; } }
    
   
}
