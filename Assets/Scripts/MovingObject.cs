using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private enum InitialState
    {
        Out,
        In,
        None
    }

    [SerializeField] private Transform objectToMove;
    [SerializeField] private Transform destinationPoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private InitialState initialState;
    [SerializeField] private float speed;

    private Vector2 moveTo;

    private void Start()
    {
        switch (initialState)
        {
            case InitialState.Out:
                objectToMove.position = destinationPoint.position;
                moveTo = destinationPoint.position;
                break;
            case InitialState.In:
                objectToMove.position = startPoint.position; 
                moveTo = startPoint.position;
                break;
        }
    }

    private void Update()
    {
        Vector3 pos = objectToMove.position;
        float z = pos.z;
        pos = Vector2.MoveTowards(objectToMove.position, moveTo, speed * Time.deltaTime);
        pos.z = z;
        objectToMove.position = pos;
    }

    public void MoveOut()
    {
        moveTo = destinationPoint.position;
    }

    public void MoveIn()
    {
        moveTo = startPoint.position;
    }

}
