using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    public enum ActionType { Jump, Blow }
    public Dictionary<ActionType, InputAction.CallbackContext> inputDict = new Dictionary<ActionType, InputAction.CallbackContext>();

    [Header("References")] [Space(4)]
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Collider2D coll;
    public GameObject windProjectile;
    public WindMeter windMeter;
    
    [Header("Combat Variables")] [Space(4)]
    [Tooltip("distance from the player the attack is fired")]
    public float fireDist = 1.5f;
    [Tooltip("Time it takes to reach max charge")]
    public float maxChargeTime = 1f;
    protected float chargeTimeTimer;
    [Tooltip("Self knockback multiplier the user receives from blow attacks")]
    public float selfKnockbackMultiplier = 0.7f;
    [Tooltip("How long the player gets stunned out of being able to change momentum with wind after being hit")]
    public float blowStunDuration = 3f;
    private float blowStunTimer;

    [Header("Platformer Feel Variables")] [Space(4)]
    [Tooltip("How fast the player accelerates")]
    public float moveAcceleration;
    [Tooltip("Force Applied from Jumping")]
    public float jumpForce;
    [Tooltip("Max speed from moving")]
    public float movementSpeedCap;
    [Tooltip("Time before you can jump after jumping")]
    public float fallMultiplier = 2f;
    [Tooltip("Max possible velocity")]
    public float velocityCap;
    [Tooltip("Time you can jump while not grounded")]
    public float coyoteTime = 0.2f;
    protected float coyoteTimeTimer = 0f;
    [Tooltip("Time before you can jump after jumping")]
    public float jumpBuffer = 0.1f;
    protected float jumpBufferTimer = 0f;
    [Tooltip("Minimym Y velocity before your fall accelerates")]
    public float fallYThreshold = 2f;
    [Tooltip("Distance from the ground that the player is considered grounded")]
    public float minJumpDist = 0.7f;
    
    // private variables
    private Vector2 moveDirection;
    private float baseGravityScale;
    private float baseLinearDrag;
    private bool jumped=false;
    private bool charging=false;

    void Awake() {
        rb = rb ? rb : Global.FindComponent<Rigidbody2D>(gameObject);
        coll = coll ? coll : Global.FindComponent<Collider2D>(gameObject);
    }
    void Start()
    {
        baseLinearDrag = rb.drag;
        baseGravityScale = rb.gravityScale;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        UpdatePhysics();
        ApplyMovement();
    }
    void ApplyMovement() {
        if (Mathf.Abs(rb.velocity.x) > movementSpeedCap) return;
        rb.AddForce(Time.deltaTime*moveAcceleration*moveDirection);
    }

    #region Input Callbacks
    public void Move(InputAction.CallbackContext context) {
        moveDirection = new Vector2(context.ReadValue<Vector2>().x, 0);
    }
    public void Jump(InputAction.CallbackContext context) {
        if (!ManageAction(ActionType.Jump, context)) return;
        if (!CanJump()) return;
        jumped = true;
        coyoteTimeTimer = 0f;
        jumpBufferTimer = jumpBuffer;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
    }

    public void Blow(InputAction.CallbackContext context) {
        ManageAction(ActionType.Blow, context);
        Debug.Log("click");
        if (windMeter.GetCurrentMeter() <= 0) {
            return;
        }
        if (context.started) {
            chargeTimeTimer = maxChargeTime;
            charging = true;
        }
        else if (context.canceled && charging) {
            charging = false;
            Vector2 fireDir = Global.GetRelativeMousePosition(transform.position);
            // can cancel all momentum from other direction if not blow stunned
            if (blowStunTimer <= 0) {
                rb.velocity = new Vector2(
                    rb.velocity.x * -fireDir.x < 0 ? 0 : rb.velocity.x,
                    rb.velocity.y * -fireDir.y < 0 ? 0 : rb.velocity.y
                );
            }
            if (fireDir.y < 0) {
                jumped=false;
            }
            Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg, Vector3.forward);
            WindProjectile proj = Instantiate(windProjectile, (Vector2)transform.position+fireDir*fireDist, rotation, GameManager.Instance.instanceManager).GetComponent<WindProjectile>();
            proj.InitDamageSource(Global.FindComponent<PlayerCombatant>(gameObject), fireDir);
            proj.InitBlowProjectile(GetChargeRatio());
            rb.AddForce(-fireDir*proj.knockbackForce*selfKnockbackMultiplier, ForceMode2D.Impulse);
            StartCoroutine(windMeter.DepleteMeter(proj.meterCost));
        }
    }
    public void BlowStun() {
        blowStunTimer = blowStunDuration;
    }
    #endregion

    #region Helper Functions
    void UpdateTimers() {
        jumpBufferTimer = Mathf.Max(jumpBufferTimer-Time.deltaTime, 0f);
        coyoteTimeTimer = Mathf.Max(coyoteTimeTimer-Time.deltaTime, 0f);
        chargeTimeTimer = Mathf.Max(chargeTimeTimer-Time.deltaTime, 0f);
        blowStunTimer = Mathf.Max(blowStunTimer-Time.deltaTime, 0f);
    }
    void UpdatePhysics() {
        rb.velocity = velocityCap > 0 ? Vector2.ClampMagnitude(rb.velocity, velocityCap) : rb.velocity;
        UpdateGrounded();
        UpdateAirTime();
    }
    public float GetChargeRatio() {
        return 1f-(chargeTimeTimer/maxChargeTime);
    }
    void UpdateGrounded() {
        if (!IsGrounded()) return;
        // if still able to jump
        if (jumpBufferTimer <= 0f) {
            rb.gravityScale = baseGravityScale;
            rb.drag = baseLinearDrag;
            coyoteTimeTimer = coyoteTime;
            jumped = false;
        }
    }
    void UpdateAirTime() {
        if (IsGrounded()) return;
        rb.drag = baseLinearDrag * 2f;
        rb.gravityScale = baseGravityScale;
        //rb.drag = baseLinearDrag * 0.15f;
        if (rb.velocity.y < fallYThreshold) {
            rb.gravityScale = baseGravityScale*fallMultiplier;
        }
        else if (rb.velocity.y > 0f && jumped && !inputDict.ContainsKey(ActionType.Jump)) {
            rb.gravityScale = baseGravityScale*fallMultiplier;
        }
    }
    bool CanJump() {
        return coyoteTimeTimer > 0f;
    }
    bool IsGrounded() {
        return Physics2D.OverlapCircle(GetBottomPoint(), minJumpDist, 1 << LayerMask.NameToLayer("Environment"));
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the bottom point
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetBottomPoint(), minJumpDist);
    }
    bool ManageAction(ActionType action, InputAction.CallbackContext context) {
        if (context.canceled) {
            inputDict.Remove(action);
            return false;
        }
        inputDict[action] = context;
        if (context.started) return true;
        return false;
    }
    private Vector2 GetBottomPoint() {
        return new Vector2(coll.transform.position.x+coll.offset.x, coll.transform.position.y+coll.offset.y-coll.bounds.extents.y);
    }
    #endregion
}
