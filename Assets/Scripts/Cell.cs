using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private string m_Type;
    [SerializeField] private GameObject m_Highlight;

    public string Type => m_Type;
    public int Row { get; private set; }
    public int Column { get; private set; }

    public void SetMatrixPosition(int row, int column)
    {
        name = $"{nameof(Cell)}({row}, {column})";
        Row = row;
        Column = column;
    }

    public void Highlight(bool isHighlighted)
    {
        if (m_Highlight != null)
        {
            m_Highlight.SetActive(isHighlighted);
        }
    }
}
