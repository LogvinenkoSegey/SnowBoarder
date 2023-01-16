using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float TorqueAmount = 2f;
    [SerializeField] float SpeedUpModifierInTouch = 5f;
    [SerializeField] float SpeedDownModifierInTouch = 3f;
    [SerializeField] float SpeedModifier = 1f;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] GameObject LeftSpot;
    [SerializeField] GameObject MidSpot;
    [SerializeField] GameObject RightSpot;

    private SurfaceEffector2D SurfaceEffector2D;
    private Rigidbody2D rigidbody;
    private bool inGame;

    private void Start()
    {
        inGame = true;
        SurfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!inGame)
            return;

        RotatePlayer();
        RespondToBoost();
    }

    public void DisableControls() => 
        inGame = false;

    private void RespondToBoost()
    {
        if (gameObject.transform.rotation.z < 0 && InTouch())
            SurfaceEffector2D.speed += SpeedUpModifierInTouch * Time.deltaTime;

        if (gameObject.transform.rotation.z > 0 && InTouch())
            SurfaceEffector2D.speed -= SpeedDownModifierInTouch * Time.deltaTime;

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && InTouch())
            SurfaceEffector2D.speed += SpeedModifier * Time.deltaTime;

        TorqueAmount = SurfaceEffector2D.speed;
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rigidbody.AddTorque(TorqueAmount);
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rigidbody.AddTorque(-TorqueAmount);       
    }

    private bool InTouch()
    {
        Vector2 positionMid = MidSpot.transform.position;
        Vector2 positionLeft = LeftSpot.transform.position;
        Vector2 positionRight = RightSpot.transform.position;
        Vector2 direction = transform.TransformDirection(Vector2.down);
        float distance = 0.3f;

        RaycastHit2D hitMid = Physics2D.Raycast(positionMid, direction, distance, GroundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(positionLeft, direction, distance, GroundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(positionRight, direction, distance, GroundLayer);

        Debug.DrawRay(positionMid, direction * distance, Color.green);
        Debug.DrawRay(positionLeft, direction * distance, Color.green);
        Debug.DrawRay(positionRight, direction * distance, Color.green);

        if (hitMid.collider != null || hitLeft.collider != null || hitRight.collider != null)
            return true;
        
        return false;
    }
}
