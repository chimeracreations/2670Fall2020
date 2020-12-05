using UnityEngine;

public class AttachOnTrigger : MonoBehaviour
{
    public PlayerData player;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Platform")
        {
            player.onPlatform = true;
            transform.parent = other.transform;
        }
        else if (other.tag == "River")
        {
            player.onPlatform = false;
            transform.parent = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent = null;
        player.onPlatform = false;
    }

}