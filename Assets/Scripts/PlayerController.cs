using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public enum ActionType { Jump, Blow , OpenMenu}
    public Dictionary<ActionType, InputAction.CallbackContext> inputDict = new Dictionary<ActionType, InputAction.CallbackContext>();

    [Header("References")] [Space(4)]
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator playerAnimator;
    public Collider2D coll;
    public GameObject weakWindProjectile;
    public GameObject strongWindProjectile;
    public WindMeter windMeter;
    public Transform raycastedTransform;
    public Transform rotatedTransform;

    [Header("Combat Variables")] [Space(4)]
    [Tooltip("distance from the player the attack is fired")]
    public float fireDist = 1.5f;
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
    public float titlAngle = 5f;
    public float rotateSpeed;
    public PlayerInput input;
    public EventSystem eventSystem;
    // private variables
    private Vector2 moveDirection;
    private float baseGravityScale;
    private float baseLinearDrag;
    private bool jumped=false;
    private bool charging=false;
    private Vector2 fireDirection;
    [SerializeField]private bool canMove;

    Quaternion smoothTilt;

    void Awake() {
        
        rb = rb ? rb : Global.FindComponent<Rigidbody2D>(gameObject);
        coll = coll ? coll : Global.FindComponent<Collider2D>(gameObject);
    }
    void Start()
    {
        baseLinearDrag = rb.drag;
        baseGravityScale = rb.gravityScale;
        if (!canMove)
        {
            rb.gravityScale = 0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        if (!canMove) return;
        //AlignRampPlayer();
        UpdatePhysics();
        UpdateFireDirection();
        ApplyMovement();
    }
    void ApplyMovement() {
        if (Mathf.Abs(rb.velocity.x) > movementSpeedCap) return;
        rb.AddForce(Time.deltaTime*moveAcceleration*moveDirection);
    }

    void AlignRampPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastedTransform.position, -Vector2.up, 3f);
        Quaternion playerTilt = Quaternion.FromToRotation(Vector2.up, hit.normal);
        smoothTilt = Quaternion.Slerp(smoothTilt,playerTilt, Time.deltaTime * rotateSpeed);
        Quaternion newRot = new Quaternion();
        Vector3 vec = new Vector3
        {
            x = smoothTilt.eulerAngles.x,
            y = rotatedTransform.rotation.eulerAngles.y,
            z = smoothTilt.eulerAngles.z
        };
        newRot.eulerAngles = vec;
        rotatedTransform.rotation = newRot;
    }
    public void ToggleMovement(bool state)
    {
        canMove = state;
        rb.gravityScale = baseGravityScale;
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

    public void FireDirection(InputAction.CallbackContext context) {
        if (!canMove) { return; }
        if (gameObject.IsDestroyed()) return;
        if (input.currentControlScheme == "Mouse&Keyboard") {
            //fireDirection = (Vector2)transform.position-context.ReadValue<Vector2>();
            fireDirection = -Global.GetRelativeMousePosition(transform.position);
        }
        else {
            fireDirection = context.ReadValue<Vector2>();
        }
        fireDirection.Normalize();
    }

    public void WeakBlow(InputAction.CallbackContext context) {
        if (!ManageAction(ActionType.Blow, context) || !canMove || windMeter.GetCurrentMeter() <= 0) {
            return;
        }
        Blow(false);
    }

    public void StrongBlow(InputAction.CallbackContext context) {
        if (!ManageAction(ActionType.Blow, context) || !canMove || windMeter.GetCurrentMeter() <= 0) {
            return;
        }
        Blow(true);
    }

    void Blow(bool strong) {
        //Vector2 fireDirection = Global.GetRelativeMousePosition(transform.position);
        // can cancel all momentum from other direction if not blow stunned
        if (blowStunTimer <= 0) {
            rb.velocity = new Vector2(
                rb.velocity.x * -fireDirection.x < 0 ? 0 : rb.velocity.x,
                rb.velocity.y * -fireDirection.y < 0 ? 0 : rb.velocity.y
            );
        }
        if (fireDirection.y < 0) {
            jumped=false;
        }
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg, Vector3.forward);
        WindProjectile proj;
        if (strong) {
            proj = Instantiate(strongWindProjectile, (Vector2)transform.position+fireDirection*fireDist, rotation, GameManager.Instance.instanceManager).GetComponent<WindProjectile>();
        }
        else {
            proj = Instantiate(weakWindProjectile, (Vector2)transform.position+fireDirection*fireDist, rotation, GameManager.Instance.instanceManager).GetComponent<WindProjectile>();
        }
        //WindProjectile proj = Instantiate(windProjectile, (Vector2)transform.position+fireDirection*fireDist, rotation, GameManager.Instance.instanceManager).GetComponent<WindProjectile>();
        proj.InitDamageSource(Global.FindComponent<PlayerCombatant>(gameObject), fireDirection);
        rb.AddForce(-fireDirection*proj.knockbackForce*selfKnockbackMultiplier, ForceMode2D.Impulse);
        StartCoroutine(windMeter.DepleteMeter(proj.meterCost));
    }

    public void OpenPauseMenu()
    {
        Debug.Log("Open Menu");

        UIManager.Instance.pauseMenuPanel.gameObject.SetActive(true);

    }
    public void BlowStun() {
        blowStunTimer = blowStunDuration;
    }
    #endregion

    #region Helper Functions
    void UpdateTimers() {
        jumpBufferTimer = Mathf.Max(jumpBufferTimer-Time.deltaTime, 0f);
        coyoteTimeTimer = Mathf.Max(coyoteTimeTimer-Time.deltaTime, 0f);
        blowStunTimer = Mathf.Max(blowStunTimer-Time.deltaTime, 0f);
    }
    void UpdateFireDirection() {
        sprite.flipX = fireDirection.x < 0;
    }
    void UpdatePhysics() {
        rb.velocity = velocityCap > 0 ? Vector2.ClampMagnitude(rb.velocity, velocityCap) : rb.velocity;
        playerAnimator.SetFloat("yVelocity", rb.velocity.y);
        if (IsGrounded()) {
            UpdateGrounded();
        }
        else {
            UpdateAirTime();
        }
    }
    void UpdateGrounded() {
        playerAnimator.Play("playerIdle");
        // if still able to jump
        if (jumpBufferTimer <= 0f) {
            playerAnimator.SetBool("grounded", true);
            //playerAnimator.SetBool("falling", false);
            rb.gravityScale = baseGravityScale;
            rb.drag = baseLinearDrag;
            coyoteTimeTimer = coyoteTime;
            jumped = false;
        }
    }
    void UpdateAirTime() {
        playerAnimator.SetBool("grounded", false);
        rb.drag = baseLinearDrag * 2f;
        rb.gravityScale = baseGravityScale;
        //rb.drag = baseLinearDrag * 0.15f;
        if (rb.velocity.y < fallYThreshold) {
            //playerAnimator.SetBool("falling", true);
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
