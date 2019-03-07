using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Music_Button : MusicControls
{
    protected Button button;
    public KeyCode[] keys;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
    }

    protected virtual void Update()
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                DoButtonClick();
            }
        }
    }

    protected void DoButtonClick()
    {
        ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }
}
