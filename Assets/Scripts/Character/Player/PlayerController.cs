using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度")]
    private float speed = 5.0f;

    [SerializeField, Tooltip("地面判定用の距離")]
    private float groundCheckDistance = 0.1f;

    [SerializeField, Tooltip("地面のレイヤー")]
    private LayerMask groundLayer;

    Rigidbody2D myRigidbody2D;
    Vector2 inputMove = Vector2.zero;
    float lateralVelocity = 0f;
    private bool isGrounded = true;
    private Vector2 Vector2_dowm = Vector2.down;

    void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    #region InputAction
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        // 移動
        if(inputMove.x > 0)
        {
            lateralVelocity = 1;
        }
        else if(inputMove.x < 0)
        {
            lateralVelocity = -1;
        }
        else
        {
            lateralVelocity = 0;
        }
        myRigidbody2D.linearVelocity = new Vector2(lateralVelocity * speed, myRigidbody2D.linearVelocity.y);

        // 設置判定
        isGrounded = Physics2D.Raycast(myRigidbody2D.position, Vector2_dowm, groundCheckDistance, groundLayer); ;
        Debug.DrawRay(myRigidbody2D.position, Vector2_dowm * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}
