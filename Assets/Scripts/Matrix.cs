using System;
using UnityEngine;

[RequireComponent(typeof(MatrixSpawner))]
public class Matrix : MonoBehaviour
{
    private MatrixSpawner m_MatrixSpawner;

    private Cell[,] m_Cells;
    private Cell m_SelectedCell;

    private void Awake()
    {
        m_MatrixSpawner = GetComponent<MatrixSpawner>();
    }

    private void Start()
    {
        SpawnMatrix();
    }

    private void SpawnMatrix()
    {
        m_Cells = m_MatrixSpawner.Spawn();
    }

    public void HandleCellClicked(Cell cell)
    {
        if (cell == null)
        {
            SelectCell(null);
            return;
        }

        if (m_SelectedCell == null || !CanSwap(m_SelectedCell, cell))
        {
            SelectCell(cell);
        }
        else
        {
            SwapCells(m_SelectedCell, cell);
            SelectCell(null);
        }
    }

    private bool CanSwap(Cell cell1, Cell cell2)
    {
        if (!AreNeighbours(cell1, cell2))
            return false;

        return CheckMatchThree();
    }

    private bool AreNeighbours(Cell cell1, Cell cell2)
    {
        return Mathf.Abs(cell1.Row - cell2.Row) + Mathf.Abs(cell1.Column - cell2.Column) == 1;
    }

    private bool CheckMatchThree()
    {
        // TODO: Real check here
        return true;
    }

    private void SwapCells(Cell cell1, Cell cell2)
    {
        m_Cells[cell1.Row, cell1.Column] = cell2;
        m_Cells[cell2.Row, cell2.Column] = cell1;

        m_MatrixSpawner.Swap(cell1, cell2);
    }

    private void SelectCell(Cell cell)
    {
        if (m_SelectedCell != null)
        {
            Debug.Log($"Deselecting {m_SelectedCell.name}", m_SelectedCell);
            m_SelectedCell.Highlight(false);
        }

        m_SelectedCell = cell;

        if (m_SelectedCell != null)
        {
            Debug.Log($"Selecting {m_SelectedCell.name}", m_SelectedCell);
            m_SelectedCell.Highlight(true);
        }
    }
}
