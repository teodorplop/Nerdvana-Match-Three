using UnityEngine;

/// <summary>
/// Handles user input
/// </summary>
public class InputHandler : MonoBehaviour
{
	/// <summary>
	/// Reference to the camera, used to transform from screen point to world point
	/// </summary>
	private Camera m_Camera;
	
	/// <summary>
	/// Reference to the matrix, so we can let the matrix know when a cell was clicked
	/// </summary>
	private Matrix m_Matrix;

	/// <summary>
	/// In awake, we get our dependencies: camera and matrix
	/// </summary>
	private void Awake()
	{
		m_Camera = Camera.main;
		m_Matrix = FindObjectOfType<Matrix>();
	}

	/// <summary>
	/// Update method, called every frame
	/// </summary>
	private void Update()
	{
		// If the first button of the mouse was clicked this frame
		if (Input.GetMouseButtonDown(0))
		{
			// We get the mouse position and we handle the click
			HandleMouseDown(Input.mousePosition);
		}
	}

	/// <summary>
	/// Represents a single cell
	/// </summary>
	private void HandleMouseDown(Vector2 mousePosition)
	{
		// Transforms mouse position to world position
		Vector2 worldPosition = m_Camera.ScreenToWorldPoint(mousePosition);
		// Sends a ray at the world position
		RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
		
		Cell cellClicked = null;
		
		if (hit.collider)
		{
			// If the ray hit something, we get the Cell component of what we hit
			cellClicked = hit.collider.GetComponent<Cell>();
		}

		// We tell the matrix that a certain cell was clicked
		m_Matrix.HandleCellClicked(cellClicked);
	}
}
