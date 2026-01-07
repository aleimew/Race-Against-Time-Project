using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// How to Play Manager - Displays game instructions
/// </summary>
public class HowToPlayManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI instructionsText;
    [SerializeField] private Button backButton;

    private void Start()
    {
        // Set up instructions text
        if (instructionsText != null)
        {
            instructionsText.text = GetInstructionsText();
        }

        // Set up back button
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
    }

    /// <summary>
    /// Get formatted instructions text
    /// </summary>
    private string GetInstructionsText()
    {
        return @"<size=36><b>HOW TO PLAY</b></size>

<size=24><b>OBJECTIVE</b></size>
Reach the exit door before time runs out!

<size=24><b>CONTROLS</b></size>
• Use <b>WASD</b> or <b>Arrow Keys</b> to move
• Navigate through the level to find the exit
• Avoid touching the death walls (red lines)

<size=24><b>GAMEPLAY</b></size>
• Each level has a time limit
• If time runs out, you'll need to restart
• Touching walls will instantly restart the level
• Complete levels to unlock the next one

<size=24><b>LEVELS</b></size>
• <b>Tutorial:</b> Learn the basics
• <b>Easy:</b> Simple navigation challenge
• <b>Intermediate:</b> More complex paths
• <b>Hard:</b> Tight time limits and tricky routes
• <b>Extra Hard:</b> Ultimate challenge

<size=24><b>TIPS</b></size>
• Plan your route before starting
• Watch the timer - it turns red when time is low
• Practice makes perfect!
• Collectibles may appear in some levels";
    }

    /// <summary>
    /// Back button handler
    /// </summary>
    private void OnBackButtonClicked()
    {
        MainMenu mainMenu = FindObjectOfType<MainMenu>();
        if (mainMenu != null)
        {
            mainMenu.ShowMainMenu();
        }
    }
}

