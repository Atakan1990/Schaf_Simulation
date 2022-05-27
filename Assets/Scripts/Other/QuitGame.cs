using UnityEngine;

/// <summary>
/// Sorgt dafür das dass Programm per Tastatur oder per Maus Button im Canvas beendet werden kann
/// </summary>
public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// Wenn der Spieler die ESC Taste auf der Tastatur drückt wird das Programm beendet
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Version 2: Beendet das Programm
    /// Wird bei ein Button im Canvas benutzt
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
