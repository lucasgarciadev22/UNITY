using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform _previousRoom;

    [SerializeField]
    private Transform _nextRoom;

    [SerializeField]
    private CameraController _camera;

    // Start is called before the first frame update
    private void Start() { }

    // Update is called once per frame
    private void Update() { }

    /// <summary>
    /// Verify is player triggered the door and move the camera to the new room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
                _camera.MoveToNewRoom(_nextRoom);
            else
                _camera.MoveToNewRoom(_previousRoom);
        }
    }
}
