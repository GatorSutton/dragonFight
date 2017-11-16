using UnityEngine;

public class SplineWalker : MonoBehaviour
{
    public bool lookForward;
    public BezierSpline spline;

    private Position position;
    public float duration;
    private float progress;
    private float target;

    private void Update()
    {
        positionControls();
        setTarget(position);
        moveProgressTowardsTarget();
        transform.localPosition = spline.GetPoint(progress);
    }

    public enum Position
    {
        Left,
        Front,
        Right
    }

    private void positionControls()
    {
        if(Input.GetKeyDown(KeyCode.P) && position != Position.Right)
        {
            position++;
            print(position);
        }

        if (Input.GetKeyDown(KeyCode.O) && position != Position.Left)
        {
            position--;
            print(position);
        }
    }

    private void setTarget(Position position)
    {
        switch (position)
        {
            case Position.Left:
                target = 0f;
                break;
            case Position.Front:
                target = .5f;
                break;
            case Position.Right:
                target = 1f;
                break;
            default:
                break;
        }
    }

    private void moveProgressTowardsTarget()
    {
        if(progress < target)
        {
            progress += Time.deltaTime / duration;
        }

        if (progress > target)
        {
            progress -= Time.deltaTime / duration;
        }
    }
}