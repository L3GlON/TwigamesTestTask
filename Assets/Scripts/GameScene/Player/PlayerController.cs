using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Ball ball;

    public float playerSpeed;

    float _playerHorizontalOffset;

    bool _pressedLeft;
    bool _pressedRight;

    private void Start()
    {
        _playerHorizontalOffset = GetComponent<Collider2D>().bounds.extents.x;
    }

    //Due to RigidBody interactions, use FixedUpdate for movement
    private void FixedUpdate()
    {
        //if A button is pressed (or left arrow)
        if(Input.GetKey(KeyCode.A) || _pressedLeft)
        {
            //Move Player left
            MovePlayer(Vector2.left);
        }
        //if D button is pressed (or right arrow)
        if(Input.GetKey(KeyCode.D) || _pressedRight)
        {
            MovePlayer(Vector2.right);
        }
        //if spaceBar is pressed
        if(Input.GetKey(KeyCode.Space))
        {
            ball.ReleaseBall();
        }
    }


    void MovePlayer(Vector2 movementVector)
    {
        transform.Translate(movementVector * Time.deltaTime * playerSpeed);

        float _clampedPlayerXPosition = Mathf.Clamp(transform.position.x, ScreenBounds.instance.MinXCoordinate + _playerHorizontalOffset, ScreenBounds.instance.MaxXCoordinate - _playerHorizontalOffset);

        transform.position = new Vector3(_clampedPlayerXPosition, transform.position.y, transform.position.z);
    }

    //for screen buttons input

    /// <summary>
    /// Player pressed Right Arrow
    /// </summary>
    public void RightButtonDown()
    {
        _pressedRight = true;
    }

    /// <summary>
    /// Player released Right Arrow
    /// </summary>
    public void RightButtonUp()
    {
        _pressedRight = false;
    }

    /// <summary>
    /// Player pressed Left Arrow 
    /// </summary>
    public void LeftButtonDown()
    {
        _pressedLeft = true;
    } 

    /// <summary>
    /// Player released Right Arrow
    /// </summary>
    public void LeftButtonUp()
    {
        _pressedLeft = false;
    }

    /// <summary>
    /// Player pressed Release Button
    /// </summary>
    public void ReleaseButtonDown()
    {
        ball.ReleaseBall();
    }
}
