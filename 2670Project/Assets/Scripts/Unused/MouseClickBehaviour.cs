using UnityEngine;
using UnityEngine.Events;

public class MouseClickBehaviour : MonoBehaviour
{
    public UnityEvent mouseDownEvent;
    private void OnMouseDown()
    {
        mouseDownEvent.Invoke();
    }
}