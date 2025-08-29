using System;
using UnityEngine;

public class DoorInteractable : SimpleHingeInteractable
{
    public event Action isOpen;
    [SerializeField] private Transform doorGameobject;
    [SerializeField] private CombinationLock combinationLock;
    [SerializeField] private Collider closedCollider;
    [SerializeField] private Collider openCollider;
    [SerializeField] private GameObject roomGO;

    private Transform doorOpenPosition;
    private Transform doorClosedPosition;
    private float doorClosedX;
    private float startAngleX;
    private float doorLocalX;
    private float doorLocalY;

    protected override void Start()
    {
        base.Start();

        combinationLock.isUnlocked += UnlockedDoors;
        isOpen += ShowRoom;

        doorClosedPosition = transform;
        doorClosedX = transform.localEulerAngles.x;

        if (doorClosedX >= 180f)
        {
            doorClosedX -= 360f;
        }
    }

    protected override void Update()
    {
        base.Update();

        ConvertEulerAngles();

        if (doorGameobject != null && grabHandle != null)
        {
            doorGameobject.localEulerAngles = new Vector3(doorGameobject.localEulerAngles.x, transform.localEulerAngles.y, doorGameobject.localEulerAngles.z);

            CheckLimits();
        }
    }

    private void CheckLimits()
    {
        if (doorLocalX <= doorClosedX - handPositionLimits.x || doorLocalX > doorClosedX + handPositionLimits.x)
        {
            Debug.Log("x: " + doorLocalX + " " + doorClosedX);
            SetLayerMask(defaultLayerMask);
        }
    }

    protected override void ResetHinge()
    {
        Debug.Log("called");
        transform.localEulerAngles = new Vector3(
                doorClosedX,
                transform.localEulerAngles.y,
                transform.localEulerAngles.z);
        ;
    }

    private void ConvertEulerAngles()
    {
        doorLocalX = transform.localEulerAngles.x;

        if (doorLocalX >= 180f)
        {
            doorLocalX -= 360f;
        }
    }

    private void ShowRoom()
    {
        if (roomGO != null)
        {
            roomGO.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == closedCollider)
        {
            SetLayerMask(defaultLayerMask);
            Debug.Log("Closed");
        }
        else if (other == openCollider)
        {
            SetLayerMask(defaultLayerMask);
            isOpen?.Invoke();
            Debug.Log("Opened");
        }
    }
}
