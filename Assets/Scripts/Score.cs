using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for modifying the score text
/// </summary>
public class Score : MonoBehaviour
{
	/// <summary>
	/// The number of points the score starts with <br/>
	/// SerializeField - modifiable from the inspector
	/// </summary>
	[SerializeField] private int m_StartingScore;
	
	/// <summary>
	/// The number of points added every time there's a match <br/>
	/// SerializeField - modifiable from the inspector
	/// </summary>
	[SerializeField] private int m_IncreaseAmount;
	
	private TMP_Text m_TextComponent;
	private int m_Score;
	
	void Awake() 
	{
		m_TextComponent = GetComponent<TMP_Text>();
	}
	
	void Start() 
	{
		SetScore(0);
	}
	
	/// <summary>
	/// Increases the score
	/// </summary>
	public void Increase()
	{
		SetScore(m_Score + m_IncreaseAmount);
	}
	
	/// <summary>
	/// Sets the score text to a given number
	/// </summary>
	private void SetScore(int number) 
	{
		m_Score = number;
		m_TextComponent.text = $"Score: {number}";
	}
}
