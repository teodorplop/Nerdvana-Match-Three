using UnityEngine;

/// <summary>
/// Represents a matrix of cells
/// </summary>
[RequireComponent(typeof(MatrixSpawner))]
public class Matrix : MonoBehaviour
{
	private MatrixSpawner m_MatrixSpawner;

	private Cell[,] m_Cells;
	private Cell m_SelectedCell;

	/// <summary>
	/// Awake method, called before Start
	/// </summary>
	private void Awake()
	{
		m_MatrixSpawner = GetComponent<MatrixSpawner>();
	}

	/// <summary>
	/// Awake method, called before Start
	/// </summary>
	private void Start()
	{
		SpawnMatrix();
	}

	private void SpawnMatrix()
	{
		m_Cells = m_MatrixSpawner.SpawnRandomMatrix();
	}

	/// <summary>
	/// Handles what happens when you tap a cell
	/// </summary>
	public void HandleCellClicked(Cell cell)
	{
		if (cell == null)
		{
			// If no cell was clicked, we deselect the currently selected one
			SelectCell(null);
			return;
		}

		if (m_SelectedCell == null || !CanSwap(m_SelectedCell, cell))
		{
			// If we don't have a selected cell
			// OR
			// If we have one, but we cannot swap it with this one
			
			// We simply select the new cell
			SelectCell(cell);
		}
		else
		{
			// We swap the cells
			SwapCells(m_SelectedCell, cell);
			// And deselect
			SelectCell(null);
			
			// TODO:
			// Since we swapped, we might have to delete those cells and bring in new ones from the top
		}
	}

	/// <summary>
	/// Can we swap these two cells?
	/// </summary>	
	private bool CanSwap(Cell cell1, Cell cell2)
	{
		return AreNeighbours(cell1, cell2) && CheckMatchThree(cell1, cell2);
	}

	/// <summary>
	/// Are the given cells neighbours?
	/// </summary>	
	private bool AreNeighbours(Cell cell1, Cell cell2)
	{
		return Mathf.Abs(cell1.Row - cell2.Row) + Mathf.Abs(cell1.Column - cell2.Column) == 1;
	}

	private bool CheckMatchThree(Cell cell1, Cell cell2)
	{
		// TODO: 
		// We received neighbouring cell1 and cell2, which we are about to swap
		// We should only swap if the operation actually makes a three match
		return true;
	}

	/// <summary>
	/// Swaps two cells
	/// </summary>
	private void SwapCells(Cell cell1, Cell cell2)
	{
		// Swap inside matrix
		m_Cells[cell1.Row, cell1.Column] = cell2;
		m_Cells[cell2.Row, cell2.Column] = cell1;

		// Swap world positions
		cell1.transform.localPosition = m_MatrixSpawner.ComputePosition(cell2.Row, cell2.Column);
		cell2.transform.localPosition = m_MatrixSpawner.ComputePosition(cell1.Row, cell1.Column);
		
		// Swap cell component matrix positions
		int cell1Row, cell1Column;
		cell1Row = cell1.Row;
		cell1Column = cell1.Column;
		cell1.SetMatrixPosition(cell2.Row, cell2.Column);
		cell2.SetMatrixPosition(cell1Row, cell1Column);
	}

	/// <summary>
	/// Selects a new cell, and deselects the previous one
	/// </summary>
	private void SelectCell(Cell cell)
	{
		if (m_SelectedCell != null)
		{
			// If we already have a cell selected, we want to deselect it
			Debug.Log($"Deselecting {m_SelectedCell.name}", m_SelectedCell);
			m_SelectedCell.Highlight(false);
		}

		// Select the new cell
		m_SelectedCell = cell;

		if (m_SelectedCell != null)
		{
			// Highlight it
			Debug.Log($"Selecting {m_SelectedCell.name}", m_SelectedCell);
			m_SelectedCell.Highlight(true);
		}
	}
}
