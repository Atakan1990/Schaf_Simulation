using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Signleton Klasse die die Verwaltung der Path Listen übernimmt
/// wird benutzt um dem jeweiligen Agent den richtigen Path per ID zu geben
/// </summary>
public class PathDatabase : MonoBehaviour
{
    // Singleton (static)
    public static PathDatabase m_instance;

    // Liste für Alle Pfade die jeweils für sich alle Nodes beinhalten
    [SerializeField]
    private List<Path> m_paths = new List<Path>();

    private void Awake()
    {
        // Wenn noch keine Referenz vorhanden ist
        if (m_instance == null)
        {
            // dann Referenz auf sich selbst zuweisen
            m_instance = this;
        }
        // ansonsten neuen überflüssigen gameObject löschen
        else
        {
            Destroy(gameObject);
        }
    }

    // Gib die Path Liste mit der jeweiligen ID zurück
    public Path GetPathByID(int _id)
    {
        foreach (Path path in m_paths)
        {
            if (path.GetPathID() == _id)
            {
                return path;
            }
        }
        return null;
    }
}
