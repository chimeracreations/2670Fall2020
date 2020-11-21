using UnityEngine;

public class AttachOnTrigger : MonoBehaviour
{
    public PlayerData player;

    private void OnTriggerStay(Collider other)
    {
        var otherTag = other.CompareTag("Platform");
        if (otherTag)
        {
            player.onPlatform = true;
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent = null;
        player.onPlatform = false;
    }
}