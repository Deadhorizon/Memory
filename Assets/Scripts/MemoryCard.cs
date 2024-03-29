﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
	private int _id;
	public int id
	{
		get
		{
			return _id;
		}
	}
	public void SetCard(int id, Sprite image)
	{
		_id = id;
		GetComponent<SpriteRenderer>().sprite = image;
	}

	[SerializeField]
	private GameObject cardback;
	[SerializeField]
	private SceneController controller;
	public void OnMouseDown()
	{
		if (cardback.activeSelf && controller.canReveal)
		{
			cardback.SetActive(false);
			controller.CardRevealed(this);
		}
	}
	public void Unreveal()
	{
		cardback.SetActive(true);
	}
}

