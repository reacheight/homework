using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game over script
/// </summary>
public class GameOverScript : MonoBehaviour
{
    /// <summary>
    /// Menu buttons
    /// </summary>
    private Button[] buttons;

    /// <summary>
    /// Awake
    /// </summary>
    public void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        HideButtons();
    }

    /// <summary>
    /// Hide buttons
    /// </summary>
    public void HideButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Show buttons
    /// </summary>
    public void ShowButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Restart game
    /// </summary>
    public void RestartGame()
    {
        Application.LoadLevel("Stage");
    }
}
