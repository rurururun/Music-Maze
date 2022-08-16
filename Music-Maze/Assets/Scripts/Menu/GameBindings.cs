using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBindings : MonoBehaviour
{
    public TextMeshProUGUI binding;

    public void ChangeUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(keybinding());
        }
    }

    public void ChangeDown()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(keybinding());
        }
    }

    public void ChangeLeft()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(keybinding());
        }
    }

    public void ChangeRight()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(keybinding());
        }
    }

    IEnumerator keybinding()
    {
        yield return new WaitWhile(() => !Input.anyKeyDown);
        binding.text = Input.inputString.ToUpper();
    }
}
