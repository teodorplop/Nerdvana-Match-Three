using UnityEngine;

/// <summary>
/// Represents a single cell
/// </summary>
public class Cell : MonoBehaviour
{
	/// <summary>
	/// What type does this cell have? <br/>
	/// SerializeField - modifiable from the inspector
	/// </summary>
	[SerializeField] private string m_Type;
	
	/// <summary>
	/// Reference to the object that represents the highlight of the cell <br/>
	/// SerializeField - modifiable from the inspector
	/// </summary>
	[SerializeField] private GameObject m_Highlight;

	private int m_Row;
	private int m_Column;

	/// <summary>
	/// What type does this cell have?
	/// </summary>
	public string Type => m_Type;
	
	/// <summary>
	/// Row this cell is on
	/// </summary>
	public int Row => m_Row;
	
	/// <summary>
	/// Column this cell is on
	/// </summary>
	public int Column => m_Column;

	/// <summary>
	/// Sets the row and the column of this cell. Will also change the name of the object, for convenience.
	/// </summary>
	public void SetMatrixPosition(int row, int column)
	{
		name = $"{nameof(Cell)}({row}, {column})";
		m_Row = row;
		m_Column = column;
	}

	/// <summary>
	/// Enables or disables the highlight on the object
	/// </summary>
	public void Highlight(bool isHighlighted)
	{
		if (m_Highlight != null)
		{
			m_Highlight.SetActive(isHighlighted);
		}
	}
}
