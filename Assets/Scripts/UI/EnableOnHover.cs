using UnityEngine;
using UnityEngine.EventSystems;

public class EnableOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Toggle(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Toggle(false);
    }

    private void Toggle(bool value)
    {
        Canvas[] canvases = GetComponentsInChildren<Canvas>();
        foreach (var canvas in canvases)
            canvas.enabled = value;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Toggle(true);
        }
    }
}
