using UnityEngine;

public class RacketManager : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 swingForce = transform.forward * 10f;
            ballRigidbody.AddForce(swingForce, ForceMode.Impulse);
        }
    }
}
