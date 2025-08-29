using UnityEngine;


public class NavmeshJoystick : SimpleHingeInteractable
{
    [SerializeField] private Transform trackingObject;
    [SerializeField] private Transform trackedObject;
    [SerializeField] private RobotNavigation robotNavigation;

    protected override void ResetHinge()
    {

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
