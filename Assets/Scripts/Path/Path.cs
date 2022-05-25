using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Path System welches man einen Namen ID und eine Node Länge zuweisen kann
/// </summary>
[System.Serializable]
public class Path
{
    // Rein optional für Unity damit ein Path ein Namen bekommt
    [SerializeField]
    private string m_name = "Pathname"; // Unity gibt eine Meldung aber der String der nicht genutzt wird dient für den Entwickler rein optisch
    [SerializeField]
    private int m_pathID = 0;
    [SerializeField]
    private List<Transform> m_pathNodes = new List<Transform>();

    // Übergebe die Position vom jeweiligen PathNodes
    public Vector3 GetNodePositionByIndex(int _index)
    {
        return m_pathNodes[_index].position;
    }

    public int GetPathID()
    {
        return m_pathID;
    }

    public int GetThePathLength()
    {
        return m_pathNodes.Count;
    }
}
