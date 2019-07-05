using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Ball : MonoBehaviour
{
    public float speed;

    public bool IsReleased { get; private set; }

    Vector2 _movementVector;

    private void Start()
    {
        //default movement Vector will be straight up
        _movementVector = Vector2.up;
    }

    //Due to RigidBody, use FixedUpdate for movement
    private void FixedUpdate()
    {
        //Ball can move only if it is released from player platform
        if (IsReleased)
        {
            //Move ball, according to movementVector and speed
            transform.Translate(_movementVector * Time.deltaTime * speed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if ball is hitting Player, change movement vector according to position of collision point on platform
        if (collision.collider.CompareTag("Player"))
        {
            // y will always be 1 (up)
            //find x of new movement Vector
            float movementX = PlayerHitResult(transform.position, collision.transform.position, collision.collider.bounds.size.x);

            //set new movement vector
            _movementVector = new Vector2(movementX, 1);
        }
        //if ball is hitting something else 
        else
        {
            //just reflect movement vector using collision normal 
            _movementVector = Vector2.Reflect(_movementVector, collision.GetContact(0).normal);
        }

        //Finally, if ball hit Brick - destroy it
        if(collision.transform.CompareTag("Brick"))
        {
            Destroy(collision.transform.gameObject);

            //And check win condition in GameManager
            GameManager.instance.CheckWinCondition();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //if ball is out of bounds
        if(collider.CompareTag("Bounds"))
        {
            //Lose the game
            GameManager.instance.GameOver();
        }
    }

    /// <summary>
    /// Allow ball to move
    /// </summary>
    public void ReleaseBall()
    {
        //protection from multiple calls
        if (!IsReleased)
        {
            IsReleased = true;
            //Ball is no longer platform child
            transform.parent.DetachChildren();
        }
    }

    /// <summary>
    /// Return X of new movement Vector, according on ball and platform positions (Closer to platform centre => X closer to 0)
    /// </summary>
    /// <param name="ballPosition"></param>
    /// <param name="playerPlatformPosition"></param>
    /// <param name="playerPlatformWidth"></param>
    /// <returns></returns>
    float PlayerHitResult(Vector2 ballPosition, Vector2 playerPlatformPosition, float playerPlatformWidth)
    {
        //compare difference in positions and divide by player width
        return (ballPosition.x - playerPlatformPosition.x) / playerPlatformWidth;
    }
}
