using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    public static ScreenBounds instance;

    BoxCollider2D _boxCollider;

    public float MaxXCoordinate { get; private set; }
    public float MinXCoordinate { get; private set; }


    private void Start()
    {
        //Get static singleton as this ScreenBounds
        instance = GetComponent<ScreenBounds>();

        //Get connected boxCollider2D
        _boxCollider = GetComponent<BoxCollider2D>();

        //Find max and min X coordinates as max and min of collider bounds
        MaxXCoordinate = _boxCollider.bounds.max.x;
        MinXCoordinate = _boxCollider.bounds.min.x;
    }


}
