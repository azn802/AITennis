using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5f;
    public Animator animator;

    private Vector3 movement;


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);

        if (movement.magnitude > 0)
        {
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Forehannd");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("Backhand");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Smash");
        }
    }

    public void ServeBall(Rigidbody ball)
    {
        Vector3 serveDirection = new Vector3(0, 1, 1).normalized;
        float serveForce = 20f;
        ball.AddForce(serveDirection * serveForce, ForceMode.Impulse);
        ball.AddTorque(new Vector3(0, 10f, 0), ForceMode.Impulse);
    }


}
