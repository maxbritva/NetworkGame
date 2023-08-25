using System;
using Game.GameManager;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Player
{
	public class PlayerHealth : MonoBehaviourPun
	{
		[SerializeField] private Image _fillImage;
		[SerializeField] private PhotonView _photonView;
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private SpriteRenderer _spriteRendered;
		[SerializeField] private BoxCollider2D _boxCollider2D;
		[SerializeField] private GameObject _playerCanvas;
		[SerializeField] private Player _player;
		[Inject] private EndGame _endGame;
		[Inject] private GameManager.GameManager _gameManager;
		private float _health = 1f;
		public float Health => _health;

		[PunRPC]
		public void HealthBarUpdate(float damage)
		{
			if (damage <= 0)
				throw new IndexOutOfRangeException("Damage value must be > than 0");
			_fillImage.fillAmount -= damage;
			_health = _fillImage.fillAmount;
			CheckHealth();
		}

		private void CheckHealth()
		{
			if (photonView.IsMine == false || _health <= 0 == false) return;
			_gameManager.SetMainCamera(true);
			gameObject.SetActive(false);
			_photonView.RPC("Death", RpcTarget.AllBuffered);
			_player.DisableInputs(true);
		}
		
		[PunRPC]
		public void Death()
		{
			PlayerChangeState(0, false, false, false);
			_endGame.Initialize();
		}

		private void PlayerChangeState(int gravity, bool colliderValue, bool spriteRendererValue, bool canvasVisible)
		{
			_rigidbody.gravityScale = gravity;
			_boxCollider2D.enabled = colliderValue;
			_spriteRendered.enabled = spriteRendererValue;
			_playerCanvas.SetActive(canvasVisible);
		}

		[PunRPC]
		public void Revive()
		{
			PlayerChangeState(1, true, true, true);
			_player.DisableInputs(false);
			_fillImage.fillAmount = 1f;
			_health = 1f;
		}
	}
}