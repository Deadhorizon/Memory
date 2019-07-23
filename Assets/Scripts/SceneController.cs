using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	// parameters of offsets
	public const int gridColls = 4;
	public const int gridRows = 2;
	public const float offsetX = 2f;
	public const float offsetY = 2.5f;
	// Serialized parameters
	[SerializeField]
	private MemoryCard originalcard;
	[SerializeField]
	private Sprite[] images;
	[SerializeField]
	private TextMesh scorelabel;
	//Parameters for scoring
	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int _score = 0;
	public bool canReveal { get { return _secondRevealed == null; } }
		public void CardRevealed(MemoryCard card)
	{
		if (_firstRevealed == null) _firstRevealed = card;
		else {

			_secondRevealed = card;
			StartCoroutine(CheckMatch());
		}
	}
	private IEnumerator CheckMatch()
	{
		if (_firstRevealed.id ==_secondRevealed.id)
		{
			_score++;
			scorelabel.text = "Score: " + _score;
		}
		else {

			yield return new WaitForSeconds(0.5f);
			_firstRevealed.Unreveal();
			_secondRevealed.Unreveal();
		}
			_firstRevealed = null;
			_secondRevealed = null;
		
	}
	private int[] Shufflenumbers(int[] numbers)
	{
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++)
		{
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
			return newArray;
		
	}
	public void Restart()
	{
		Application.LoadLevel("SampleScene");
	}
	void Start()
	{
		Vector3 startPos = originalcard.transform.position;
		int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3};
		numbers = Shufflenumbers(numbers);
		for (int i = 0; i < gridColls; i++)
		{
			for (int j = 0; j < gridRows; j++)
			{
				MemoryCard card;
				if (i == 0 && j == 0)
				{
					card = originalcard;
				}
				else
				{
					card = Instantiate(originalcard) as MemoryCard;
				}
				int index = j * gridColls + i;
				int id = numbers[index];
				card.SetCard(id, images[id]);
				float posX = (offsetX * i) + startPos.x;
				float posY = -(offsetY * j) + startPos.y;
				card.transform.position = new Vector3(posX, posY, 0);
			}
		}
	}
}

