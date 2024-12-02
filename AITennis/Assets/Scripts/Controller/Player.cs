using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5f;
    public Animator animator;

    private Vector3 movement;

    [SerializeField] private RacketManager racketManager;


    [SerializeField] private Transform leftFootIKTarget;
    [SerializeField] private Transform rightFootIKTarget;
    [SerializeField] private LayerMask groundLayer;
    private float footOffset = 0.1f;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);

        if (movement.magnitude > 0)
        {
            //transform.Translate(movement * speed * Time.deltaTime, Space.World);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            animator.SetTrigger("Forehand");
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            animator.SetTrigger("Backhand");
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            animator.SetTrigger("Smash");
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            racketManager.CurrentForce = RacketManager.currentForce.Weak;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            racketManager.CurrentForce = RacketManager.currentForce.Middle;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            racketManager.CurrentForce = RacketManager.currentForce.Strong;
        }
    }

    public void ServeBall(Rigidbody ball)
    {
        Vector3 serveDirection = new Vector3(0, 1, 1).normalized;
        float serveForce = 20f;
        ball.AddForce(serveDirection * serveForce, ForceMode.Impulse);
        ball.AddTorque(new Vector3(0, 10f, 0), ForceMode.Impulse);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator == null) return;

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        AdjustFootIK(AvatarIKGoal.LeftFoot, leftFootIKTarget);
        AdjustFootIK(AvatarIKGoal.RightFoot, rightFootIKTarget);

    }

    private void AdjustFootIK(AvatarIKGoal foot, Transform ikTarget)
    {
        Ray ray = new Ray(ikTarget.position + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, groundLayer))
        {
            Vector3 footPosition = hit.point + Vector3.up * footOffset;
            Quaternion footRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            animator.SetIKPosition(foot, footPosition);
            animator.SetIKRotation(foot, footRotation);
        }
    }
}
