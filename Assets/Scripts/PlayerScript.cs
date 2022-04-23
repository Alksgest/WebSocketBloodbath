using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public SceneManagerScript sceneManager;
    public bool isMainPlayer;

    // use for testing if you want to connect multiple players to the server and
    // see them moving
    public bool autopilotOn = false;

    [SerializeField]
    private float moveSpeed = 10f;

    // autopilot movement for testing
    private readonly List<Vector3> _moveDirections = new()
    {
        Vector3.up,
        Vector3.right,
        Vector3.down,
        Vector3.left
    };
    private int currMoveDirIndex = 0;
    
    void Start()
    {
        if (!isMainPlayer)
        {
            // gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void Update()
    {
        if (isMainPlayer)
        {
            HandleMovement();
        }
    }
    

    private void HandleMovement()
    {
        var targetPos = transform.position;
        if (Input.anyKey)
        {
            // left
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-moveSpeed * Time.deltaTime,0,0);
            }
            // right
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveSpeed * Time.deltaTime,0,0);
            }
            // up
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0,0,moveSpeed * Time.deltaTime);
            }
            // down
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0,0,-moveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(new Vector3(0, -1, 0));
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(new Vector3(0, 1, 0));
            }
        }

        // if (targetPos != transform.position)
        // {
        //     transform.position = Vector3.MoveTowards(
        //         transform.position,
        //         targetPos,
        //         Time.deltaTime * moveSpeed
        //     );
        //     sceneManager.SyncPlayerState(gameObject);
        // }
    }
}
