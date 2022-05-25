using UnityEngine;

/// <summary>
/// �berpr�ft per RayCast ob der Boden getroffen wurde wenn ja 
/// wird der neue Wert von Hit Position auf das Objekt zugewiesen
/// </summary>
public class DropOnGround : MonoBehaviour
{
    /// <summary>
    /// Jedes mal wenn das Objekt angeschaltet wird oder generiert wird wird dieser Teil ausgef�hrt
    /// da die �berpr�fung nach Awake und Start passiert ist ein OnEnable n�tig
    /// Der RayCast wird urspr�nglich von der Y-Achse+50 nach unten ausgef�hrt mit einer max. L�nge von 100
    /// </summary>
    private void OnEnable()
    {
        if (Physics.Raycast(this.transform.position + Vector3.up * 50, Vector3.down, out RaycastHit hit, 100f))
        {
            this.transform.position = hit.point;
        }
    }
}
