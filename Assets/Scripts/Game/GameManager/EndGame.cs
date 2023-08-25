using Game.Player;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameManager
{
	public class EndGame : MonoBehaviourPunCallbacks
	{
		[SerializeField] private Image _endGameUI;
		[SerializeField] private TextMeshProUGUI _text;
	
		private Player.Player _winner;
		private int _winnerCoins;
		public void Initialize()
		{
			GameObject[] findGameObjectWithTag = GameObject.FindGameObjectsWithTag("Winner");
			for (int i = 0; i < findGameObjectWithTag.Length; i++)
			{
				if (findGameObjectWithTag[i].gameObject.GetComponent<PlayerHealth>().Health >0)
					_winner = findGameObjectWithTag[i].GetComponent<Player.Player>();
			}
			//_winnerCoins = _winner.CoinsCollected;
			SetUIVisible(true);
			ShowCoins(); 
		}

		[PunRPC]
		private void ShowCoins() => _text.text = $"PLAYER: {_winner.PlayerNameText.text} WIN THE GAME! AND COLLECTED: {_winner.CoinsCollectedString}";

		private void SetUIVisible(bool value) => _endGameUI.gameObject.SetActive(value);
		
	}
}