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

    private void OnValidate()
    {
        // Set up instructions text
        if (instructionsText != null)
        {
            instructionsText.text = GetInstructionsText();
        }
    }

    private void Start()
    {
        // Set up instructions text
        if (instructionsText != null)
        {
            instructionsText.text = GetInstructionsText();
        }
    }

    /// <summary>
    /// Get formatted instructions text
    ///
    /// if your confused by the '!' it does something fun in the DSEG fonts, instead of a super tiny space,
    /// it makes the space the size of one character, making it more readable and actually like a segment display
    /// where a space is indicated by a whole digit being unlit I think -- JGN
    /// </summary>
    private string GetInstructionsText()
    {
        return @"
<size=48><b>OBJECTIVE</b></size>

Reach!the!exit!door!before!time!runs!out

<size=48><b>CONTROLS</b></size>

•!Use!<b>WASD</b>!or!<b>Arrow!Keys</b>!to!move
•!Navigate!through!the!level!to!find!the!exit
•!Avoid!touching!the!death!walls(red!lines)

<size=48><b>GAMEPLAY</b></size>

•!Each!level!has!a!time!limit
•!If!time!runs!out,!you'll!need!to!restart
•!Touching!walls!will!instantly!restart!the!level
•!Complete!levels!to!unlock!the!next!one

<size=48><b>LEVELS</b></size>

•!<b>Tutorial:</b>!Learn!the!basics
•!<b>Easy:</b>!Simple!navigation!challenge
•!<b>Intermediate:</b>!More!complex!paths
•!<b>Hard:</b>!Tight!time!limits!and!tricky!routes
•!<b>Extra!Hard:</b>!Ultimate!challenge

<size=48><b>TIPS</b></size>

•!Plan!your!route!before!starting
•!Watch!the!timer!-!it!turns!red!when!time!is!low
•!Practice!makes!perfect
•!Collectibles!may!appear!in!some!levels

";
    }
}

