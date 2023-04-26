using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Camera m_Camera;
    private Matrix m_Matrix;

    private void Awake()
    {
        m_Camera = Camera.main;
        m_Matrix = FindObjectOfType<Matrix>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown(Input.mousePosition);
        }
    }

    private void HandleMouseDown(Vector2 mousePosition)
    {
        Vector2 worldPosition = m_Camera.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        Cell cellClicked = null;

        if (hit.collider)
        {
            cellClicked = hit.collider.GetComponent<Cell>();
        }

        m_Matrix.HandleCellClicked(cellClicked);
    }
}
