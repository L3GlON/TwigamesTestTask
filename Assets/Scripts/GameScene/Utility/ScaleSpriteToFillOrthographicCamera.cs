using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ScaleSpriteToFillOrthographicCamera : MonoBehaviour
{
    [Header("Set in Inspector")]
    [Tooltip("If the camToMatch is not orthographic, this code will do nothing.")]
    public Camera   camToMatch;

    void Awake()
    {
        // The camera must be Orthographic for this to work.
        if (camToMatch == null || !camToMatch.orthographic)
        {
            return;
        }

        //Set local scale as (1,1,1)
        transform.localScale = Vector3.one;
        //Get SpriteRenderer on this object 
        Renderer rend = GetComponent<Renderer>();

        //Get base size and size of rendered bounds
        Vector3 baseSize = rend.bounds.size;
        Vector3 camSize = baseSize;

        //camSize y should be double orthographic size
        camSize.y = camToMatch.orthographicSize * 2;
        //camSize x should be camSize.y multiplied by aspect ratio
        camSize.x = camSize.y * camToMatch.aspect;

        // Divide coordinates from camSize and baseSize
        Vector3 scale = Vector3Divide(camSize, baseSize);

        //Returned result will be our target Sprite scale 
        transform.localScale = scale;
    }

    /// <summary>
    /// Divides 2 Vectors x, y and z coordinates and returns divided Vector 
    /// </summary>
    /// <param name="firstVector"></param>
    /// <param name="secondVector"></param>
    /// <returns></returns>
    Vector3 Vector3Divide(Vector3 firstVector, Vector3 secondVector)
    {
        return new Vector3(firstVector.x / secondVector.x, firstVector.y / secondVector.y, firstVector.z / secondVector.z);
    }
}
