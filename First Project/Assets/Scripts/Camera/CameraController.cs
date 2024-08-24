using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Cam
    [SerializeField]
    private float _speed;

    private float _currentPosX;
    private Vector3 _velocity = Vector3.zero;

    //Player Cam
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _aheadDistance;

    [SerializeField]
    private float _playerCamSpeed;

    private float _lookAhead;

    // Start is called before the first frame update
    private void Start() { }

    // Update is called once per frame
    private void Update()
    {
        MoveRoomCam();
        //MovePlayerCam();
    }

    private void MovePlayerCam()
    {
        transform.position = new Vector3(
            _player.position.x,
            _player.position.y,
            _player.position.z
        );

        //Lerp make camera movement more smooth interpolating between two values
        _lookAhead = Mathf.Lerp(
            _lookAhead,
            Mathf.Sign(_player.localScale.x) * _aheadDistance,
            _playerCamSpeed * Time.deltaTime
        );
    }

    private void MoveRoomCam()
    {
        //SmoothDamp make camera movement more smooth
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(_currentPosX, transform.position.y, transform.position.z),
            ref _velocity,
            _speed
        );
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        _currentPosX = newRoom.position.x;
    }
}
