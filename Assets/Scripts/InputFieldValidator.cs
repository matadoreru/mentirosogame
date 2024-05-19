using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TMPro;
public class InputFieldValidator : MonoBehaviour
{
    public List<TMP_InputField> inputFields;

    private void Start()
    {
        foreach (TMP_InputField inputField in inputFields)
        {
            inputField.onValueChanged.AddListener(delegate { ValidateInput(inputField); });
        }
    }

    private void ValidateInput(TMP_InputField inputField)
    {
        string validInput = Regex.Replace(inputField.text, "[^a-zA-Z0-9]", "");

        if (validInput.Length > 10)
        {
            validInput = validInput.Substring(0, 10);
        }

        if (inputField.text != validInput)
        {
            inputField.text = validInput;
        }
    }
}
