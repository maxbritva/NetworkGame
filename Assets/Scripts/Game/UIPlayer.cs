using Game.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game
{
	public class UIPlayer : MonoBehaviour
	{
		[SerializeField] private GameObject _controllerUI;
		[SerializeField] private Joystick _joystick;
		[SerializeField] private TextMeshProUGUI _coinsText;
		private GameManager.GameManager _gameManager;
		private Player.Player _localPlayer;
		public Joystick Joystick => _joystick;

		public void ShowControls(bool value)
		{
			_controllerUI.SetActive(value);
			if (_gameManager.LocalPlayer != null)
				_localPlayer = _gameManager.LocalPlayer;
		}

		public void ButtonJump()
		{
			PlayerJump playerJump = _localPlayer.GetComponent<PlayerJump>();
			if(playerJump.IsGrounded)
				playerJump.Jump();
		}

		public void ButtonFire() => _localPlayer.TryShot();
		public void SetCoinsUi(int value) => _coinsText.text = "COINS: " + value;

		[Inject] private void Container(GameManager.GameManager gameManager) => _gameManager = gameManager;
	}
}