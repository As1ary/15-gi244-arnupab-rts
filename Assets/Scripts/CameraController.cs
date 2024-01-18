using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Move")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform corner1;
    [SerializeField] private Transform corner2;

    [SerializeField] private float xInput;
    [SerializeField] private float zInput;

    public static CameraController instance;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        MoveByKb();
    }
    

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }
    private void MoveByKb()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        Vector3 dir = (transform.forward * zInput) + (transform.right * xInput);
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.position = Clamp(corner1.position , corner2.position);
    }

    private Vector3 Clamp(Vector3 lowerLeft ,  Vector3 topRight)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x),
                                                                    transform.position.y,
                                                                    Mathf.Clamp(transform.position.z,
                                  lowerLeft.z, topRight.z));
        return pos;
    }
}