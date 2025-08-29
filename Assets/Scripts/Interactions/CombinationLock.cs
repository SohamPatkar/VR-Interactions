using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CombinationLock : MonoBehaviour
{
    [SerializeField] private XRButtonScript[] comboButtons;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private int[] comboValues = new int[3];
    [SerializeField] private int[] inputValues;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color unlockedColor;
    [SerializeField] private TextMeshProUGUI lockedText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private bool isResetable;

    public event Action isUnlocked;

    private const string unlockedString = "Unlocked";
    private const string lockedString = "Locked";
    private const string resetString = "Enter 3 digit numbers";

    private int maxButtonPress;
    private int buttonPresses;
    private int matches;
    private bool isLocked;

    private bool isReseting;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnSelectButton);
        }

        InitializeValues();
    }

    private void InitializeValues()
    {
        lockedText.text = lockedString;
        lockedText.color = lockedColor;
        isLocked = true;
        isReseting = false;
        matches = 0;
        comboText.text = string.Empty;
        infoText.text = string.Empty;
        maxButtonPress = comboValues.Length;
        inputValues = new int[maxButtonPress];
        buttonPresses = 0;
    }

    private void OnSelectButton(SelectEnterEventArgs arg0)
    {
        if (buttonPresses > maxButtonPress)
        {
            return;
        }
        else
        {
            for (int i = 0; i < comboButtons.Length; i++)
            {
                if (arg0.interactableObject.transform.name == comboButtons[i].transform.name)
                {
                    comboText.text += i.ToString();
                    inputValues[buttonPresses] = i;

                    Debug.Log(inputValues[buttonPresses]);
                }
                else
                {
                    comboButtons[i].ResetColor();
                }
            }

            buttonPresses++;

            if (maxButtonPress == buttonPresses)
            {
                if (isReseting)
                {
                    for (int i = 0; i < maxButtonPress; i++)
                    {
                        comboValues[i] = inputValues[i];
                    }

                    InitializeValues();
                    isReseting = false;
                }
                else
                {
                    CheckCombo();
                }
            }
        }
    }

    private void CheckCombo()
    {
        for (int i = 0; i < maxButtonPress; i++)
        {
            if (inputValues[i] == comboValues[i])
            {
                matches++;
            }
        }

        if (matches == maxButtonPress)
        {
            lockedText.text = unlockedString;
            lockedText.color = unlockedColor;
            isLocked = false;

            SoundManager.Instance.PlayAudio(SoundType.COMPLETED, false);

            isUnlocked?.Invoke();
        }
        else
        {
            InitializeValues();
        }
    }

    public void Reset()
    {
        if (isResetable)
        {
            InitializeValues();
            infoText.text = resetString;
            isReseting = true;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
