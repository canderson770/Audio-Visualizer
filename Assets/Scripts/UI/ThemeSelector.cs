using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ThemeSelector : MonoBehaviour
{
    private Dropdown dropdown;
    public List<UnityEvent> events;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
    }

    public void DROPDOWN_DoEvents(int value)
    {
        if (events.Count < value) return;
        events[value].Invoke();
        StartCoroutine(Deselect());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            dropdown.value = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            dropdown.value = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            dropdown.value = 2;
        }
    }

    private IEnumerator Deselect()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
