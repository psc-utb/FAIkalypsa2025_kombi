using UnityEngine;
using UnityEngine.Events;

public class GhoulEyes : MonoBehaviour
{
    bool collision;

    [SerializeField]
    GameObject player;

    public UnityEvent<bool> PlayerIsVisible;

    // Update is called once per frame
    void Update()
    {
        if (collision)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Ray ray = new Ray(transform.position, direction);
            Physics.Raycast(ray, out RaycastHit hitInfo, 3, ~LayerMask.GetMask("Enemy"));
            Debug.DrawRay(ray.origin, ray.direction * 3, Color.chocolate);
            if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == player.layer)
            {
                PlayerIsVisible?.Invoke(true);
                return;
            }
        }
        PlayerIsVisible?.Invoke(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == player.layer)
        {
            collision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == player.layer)
        {
            collision = false;
        }
    }
}
