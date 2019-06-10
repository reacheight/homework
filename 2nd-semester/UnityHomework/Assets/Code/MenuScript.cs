using UnityEngine;

/// <summary>
/// Menu script
/// </summary>
public class MenuScript : MonoBehaviour
{
    /// <summary>
    /// Start game menu
    /// </summary>
    public void StartGame()
    {
        Application.LoadLevel("Stage");
    }
}
