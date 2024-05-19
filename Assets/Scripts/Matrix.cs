using System.Collections;
using UnityEngine;

/// <summary>
/// Represents a matrix of cells
/// </summary>
[RequireComponent(typeof(MatrixSpawner))]
public class Matrix : MonoBehaviour
{
	/// <summary>
	/// How many cells need to be in line for a match
	/// </summary>
    [SerializeField] private int m_MatchCount = 3;

	/// <summary>
	/// Duration of an animation, in seconds
	/// </summary>
    [SerializeField] private float m_AnimationDuration = 0.5f;

    /// <summary>
    /// Reference to the matrix spawner. We'll use it to spawn cells in a matrix formation.
    /// </summary>
    private MatrixSpawner m_MatrixSpawner;
	
	/// <summary>
	/// Reference to the score object. We'll use it to update the score
	/// </summary>
	private Score m_Score;

	/// <summary>
	/// Matrix of cells
	/// </summary>
	private Cell[,] m_Cells;
	
	/// <summary>
	/// Currently selected cell
	/// </summary>
	private Cell m_SelectedCell;

	private int m_AnimationsRunning;

	/// <summary>
	/// Awake method, called before Start
	/// </summary>
	private void Awake()
	{
		m_MatrixSpawner = GetComponent<MatrixSpawner>();
		m_Score = FindObjectOfType<Score>();
	}

	/// <summary>
	/// Start method
	/// </summary>
	private void Start()
	{
		SpawnMatrix();

		// At the start of the game, we check for any matches and run their animations
		StartCoroutine(RunMatchThreeAnimation());
	}

	/// <summary>
	/// Method that spawns the entire matrix
	/// </summary>
	private void SpawnMatrix()
	{
		m_Cells = m_MatrixSpawner.SpawnRandomMatrix();
	}

	/// <summary>
	/// Handles what happens when you tap a cell
	/// </summary>
	public void HandleCellClicked(Cell cell)
	{
		if (m_AnimationsRunning > 0)
			// If there is an animation running, don't handle any clicks
			return;

		if (cell == null)
		{
			// If no cell was clicked, we deselect the currently selected one
			SelectCell(null);
			return;
		}

		if (m_SelectedCell == null || !AreNeighbours(m_SelectedCell, cell))
		{
			// If we don't have a selected cell
			// OR
			// If we have one, but we cannot swap it with this one
			
			// We simply select the new cell
			SelectCell(cell);
		}
		else
		{
			// We start animations to swap & match
			StartCoroutine(SwapCellsAnimation(cell));
		}
	}

	/// <summary>
	/// Swap m_SelectedCell with cell
	/// </summary>
	private IEnumerator SwapCellsAnimation(Cell cell)
	{
		// Mark that we have an animation running
		m_AnimationsRunning++;

		// Swap cells
        SwapCells(m_SelectedCell, cell);

		// Wait some time
        yield return new WaitForSeconds(m_AnimationDuration);

        if (CheckMatchThree())
        {
			// If there's a match, run all animations
            yield return StartCoroutine(RunMatchThreeAnimation());
        }
        else
        {
            // Swap cells back
            SwapCells(m_SelectedCell, cell);
			// Deselect cell
            SelectCell(null);
			// Wait some time
            yield return new WaitForSeconds(m_AnimationDuration);
        }

		// Mark that we finished the animation
		m_AnimationsRunning--;
    }

	/// <summary>
	/// Run all match animations, sequencially
	/// </summary>
	private IEnumerator RunMatchThreeAnimation()
	{
        // Mark that we have an animation running
        ++m_AnimationsRunning;

        // If we still have a match
        while (CheckMatchThree())
        {
			// Wait a bit
            yield return new WaitForSeconds(m_AnimationDuration);

			// Destroy all cells marked for destruction
            for (int i = 0; i < m_Cells.GetLength(0); ++i)
                for (int j = 0; j < m_Cells.GetLength(1); ++j)
                    m_Cells[i, j].Destroy(m_AnimationDuration);

			// Wait a bit
            yield return new WaitForSeconds(m_AnimationDuration);

            int i1;
            for (int j = 0; j < m_Cells.GetLength(1); ++j)
            {
				// For each column
				// Bring cells down
                i1 = 0;
                for (int i = 0; i < m_Cells.GetLength(0); ++i)
                    if (m_Cells[i, j] != null)
                    {
                        if (i != i1)
                        {
                            m_Cells[i1, j] = m_Cells[i, j];
                            m_Cells[i, j] = null;

							m_Cells[i1, j].MoveTo(m_MatrixSpawner.ComputePosition(i1, j), m_AnimationDuration);
                            m_Cells[i1, j].SetMatrixPosition(i1, j);
                        }
                        i1++;
                    }
            }

			// Wait a bit
            yield return new WaitForSeconds(m_AnimationDuration);

			// Spawn all cells that have been destroyed
            for (int i = 0; i < m_Cells.GetLength(0); ++i)
                for (int j = 0; j < m_Cells.GetLength(1); ++j)
                    if (m_Cells[i, j] == null)
                        m_Cells[i, j] = m_MatrixSpawner.SpawnRandomCell(i, j);

			// Wait a bit
			yield return new WaitForSeconds(m_AnimationDuration);

            // Increase score
            m_Score.Increase();
        }

        // Mark that we finished the animation
        --m_AnimationsRunning;
    }

	/// <summary>
	/// Are the given cells neighbours?
	/// </summary>	
	private bool AreNeighbours(Cell cell1, Cell cell2)
	{
		return Mathf.Abs(cell1.Row - cell2.Row) + Mathf.Abs(cell1.Column - cell2.Column) == 1;
	}

	/// <summary>
	/// Checks for matches and marks for destruction all cells that are part of a match
	/// </summary>
	private bool CheckMatchThree()
	{
		bool hasMatch = false;

		for (int i = 0; i < m_Cells.GetLength(0); ++i)
			for (int j = 0; j < m_Cells.GetLength(1); ++j)
			{
				// Check up
				int k = i;
				while (k < m_Cells.GetLength(0) && m_Cells[k, j].Type == m_Cells[i, j].Type)
					++k;
				if (k - i >= m_MatchCount)
				{
					hasMatch = true;

					--k;
                    while (k >= i)
					{
						m_Cells[k, j].MarkForDestruction();
						--k;
					}
				}

				// Check right
				k = j;
				while (k < m_Cells.GetLength(1) && m_Cells[i, k].Type == m_Cells[i, j].Type)
					++k;
				if (k - j >= m_MatchCount)
				{
					hasMatch = true;

					--k;
                    while (k >= j)
                    {
                        m_Cells[i, k].MarkForDestruction();
                        --k;
                    }
                }
			}

		return hasMatch;
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
		cell1.MoveTo(m_MatrixSpawner.ComputePosition(cell2.Row, cell2.Column), m_AnimationDuration);
        cell2.MoveTo(m_MatrixSpawner.ComputePosition(cell1.Row, cell1.Column), m_AnimationDuration);
		
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
