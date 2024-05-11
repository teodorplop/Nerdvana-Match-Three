using UnityEngine;

/// <summary>
/// Responsible for spawning a single cell 
/// </summary>
public class CellSpawner : MonoBehaviour
{
	/// <summary>
	/// Array of all the prefabs we can spawn
	/// </summary>
	[SerializeField] private Cell[] m_CellPrefabs;
	
	/// <summary>
	/// Spawns a random cell with a given size at a given world position
	/// </summary>
	public Cell SpawnRandomCell(Vector2 position, float size)
	{
		if (m_CellPrefabs == null || m_CellPrefabs.Length == 0) 
		{
			// We didn't setup any prefabs, something went wrong
			Debug.LogError("No prefabs setup. Nothing to spawn!");
			return null;
		}
		
		// Choose a random prefab
		Cell prefab = m_CellPrefabs[Random.Range(0, m_CellPrefabs.Length)];
		
		// Instantiate a copy of it in the world
		Cell copy = Instantiate(prefab, position, Quaternion.identity, transform);
		copy.transform.localScale *= size;
		
		// Return the copy
		return copy;
	}
}
