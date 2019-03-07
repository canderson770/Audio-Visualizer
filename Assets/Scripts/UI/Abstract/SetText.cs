using UnityEngine;
using UnityEngine.UI;

public abstract class SetText : MonoBehaviour
{
    protected Text textComponent;

    protected virtual void Awake()
    {
        textComponent = GetComponent<Text>();
        textComponent.text = "";
    }
}
