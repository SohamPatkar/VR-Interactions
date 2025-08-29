using UnityEngine;


public class NavmeshJoystick : SimpleHingeInteractable
{
    [SerializeField] private Transform trackingObject;
    [SerializeField] private Transform trackedObject;
    [SerializeField] private RobotNavigation robotNavigation;

    private Transform startPos;
    private float startPosX;

    protected override void Start()
    {
        base.Start();

        startPos = transform;

        startPosX = startPos.localEulerAngles.x;

        if (startPosX >= 180)
        {
            startPosX -= 360;
        }
    }

    protected override void ResetHinge()
    {
        Debug.Log(startPosX);

        transform.localEulerAngles = new Vector3(startPosX, 0, 0);
    }

    protected override void Update()
    {
        base.Update();

        trackingObject.position = new Vector3(trackedObject.position.x, trackingObject.position.y, trackedObject.position.z);

        if (robotNavigation != null && isSelected)
        {
            robotNavigation.Move(trackingObject.localPosition);
        }
    }
}
