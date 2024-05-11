using UnityEngine;

/// <summary>
/// Responsible for spawning cells in a matrix formation
/// </summary>
[RequireComponent(typeof(CellSpawner))]
public class MatrixSpawner : MonoBehaviour
{
	[SerializeField] private int m_NoRows;
	[SerializeField] private int m_NoColumns;
	[SerializeField] private float m_CellSize;
	[SerializeField] private float m_Offset;

	private CellSpawner m_CellSpawner;
	private Vector2 m_BottomLeftPosition;

	private void Awake()
	{
		m_CellSpawner = GetComponent<CellSpawner>();
		m_BottomLeftPosition = ComputeBottomLeftPosition(m_NoRows, m_NoColumns);
	}

	/// <summary>
	/// Spawns random cells in a matrix format
	/// </summary>
	public Cell[,] SpawnRandomMatrix()
	{
		Cell[,] matrix = new Cell[m_NoRows, m_NoColumns];
		for (int row = 0; row < m_NoRows; ++row)
			for (int column = 0; column < m_NoColumns; ++column)
				matrix[row, column] = SpawnRandomCell(row, column);

		Debug.Log($"Spawned {m_NoRows} rows and {m_NoColumns} columns.");

		return matrix;
	}
	
	/// <summary>
	/// Spawns one random cell at a given position in the matrix
	/// </summary>	
	public Cell SpawnRandomCell(int row, int column) 
	{
		Cell cell = m_CellSpawner.SpawnRandomCell(ComputePosition(row, column), m_CellSize);	
		cell.SetMatrixPosition(row, column);
		
		return cell;
	}
	
	/// <summary>
	/// Computes the world position of a given matrix position
	/// </summary>	
	public Vector2 ComputePosition(int row, int column)
	{
		// Quick maths
		return m_BottomLeftPosition + new Vector2(column * (m_CellSize + m_Offset), row * (m_CellSize + m_Offset));
	}

	/// <summary>
	/// Computes the bottom left position of a cell in the matrix
	/// </summary>	
	private Vector2 ComputeBottomLeftPosition(int noRows, int noColumns)
	{
		return new Vector2(-ComputeStartPosition(noColumns), -ComputeStartPosition(noRows));
	}
	
	private float ComputeStartPosition(int count)
	{
		// Quick maths
		float position = 0.5f * (count - 1) * (m_Offset + m_CellSize);
		if (count % 2 == 0)
			position += 0.5f * (m_Offset + m_CellSize);

		return position;
	}
}
