using UnityEngine;

public class RacketManager : MonoBehaviour
{
    private float[] armForce = {6, 10, 14};
    public enum currentForce
    {
        Weak = 0,
        Middle = 1,
        Strong = 2
    }
    public currentForce CurrentForce;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 swingForce = transform.forward * armForce[((int)CurrentForce)];
            ballRigidbody.AddForce(swingForce, ForceMode.Impulse);
        }
    }

    public void Swing(Collision collision, int forceIndex)
    {
           
    }
}
