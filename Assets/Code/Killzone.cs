using UnityEngine;

public class KillZone : MonoBehaviour
{
    private Vector3 spawnPoint = new Vector3(0.38f, 1f, 4.2f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            other.transform.position = spawnPoint;
        }
    }
}
