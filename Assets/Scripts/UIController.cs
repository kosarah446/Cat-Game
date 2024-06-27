using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
		public GameObject leaderboardPrefab;

		public GameObject PickupsParent;
		private int totalKittens = 0;

		// Variable to keep track of collected "PickUp" objects.
		private int totalCount = 0;
		private int tempTotalCount = 0;
		private int kittensLeftCount = 0;
		public TextMeshProUGUI scoreText;

		// Variable to keep track of collected "PickUp" objects.
		private int oldChainCount = 0;
		public int chainCount = 0;
		public TextMeshProUGUI chainText;
		private float chainTextTimer = 0;
		private bool onWinTriggered = false;

		public UnityEvent onWinEvent;

		public void DisplayLeaderboard()
    {
        Debug.Log("Displaying Leaderboard");
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(leaderboardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

		void Start()
		{
				Debug.Log("Start leaderboard");
				string val = PlayerStats.PlayerName;
				Debug.Log(val);
			

				// Get the total number of kittens in the scene
				totalKittens = PickupsParent.transform.childCount;
				kittensLeftCount = totalKittens;
				//Debug.Log("total K");
				//Debug.Log(totalKittens);


				// Update the count display.
				SetCountText();
				chainText.enabled = false;
		}

		private void Update()
		{
				// Update the count display.
				if (chainCount != oldChainCount) {
						SetCountText();
						oldChainCount = chainCount;
				}
				if (Time.time - chainTextTimer > 1) {
						submitKittens();
				}
		}

		void OnWin()
		{
			if(onWinTriggered == false)
			{
				Debug.Log("IOn WIIN");
				Time.timeScale = 0.0f;
				onWinTriggered = true;
				scoreText.enabled = false;
				onWinEvent.Invoke();
				DisplayLeaderboard();
				
		}

		}

		void SetCountText()
		{
				chainTextTimer = Time.time;
				chainText.text = chainCount.ToString() + " CHAIN!";
				chainText.fontSize = 20 + chainCount * 10;
				chainText.enabled = true;

				// Update the count text with the current count.
				tempTotalCount = totalCount + chainCount;
				kittensLeftCount = totalKittens - tempTotalCount;
				scoreText.text = "Kittens left: " + kittensLeftCount.ToString();
		}

		void submitKittens()
		{
				chainText.enabled = false;
				totalCount = totalCount + chainCount;
				chainCount = 0;
				oldChainCount = 0;
				// Update the count text with the current count.
				//countText.text = "Kittens saved: " + totalCount.ToString();
				if (totalCount >= totalKittens) {
						OnWin();
				}
		}

}
