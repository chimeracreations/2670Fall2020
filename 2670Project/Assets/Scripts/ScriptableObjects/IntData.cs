using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class IntData : ScriptableObject
{
    public int value;
    [SerializeField] private UnityEvent startScene;


    public void AddToValue()
    {
        value++;
    }


}