using System.Collections;
using Game.Player.Gun;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Player
{
	public class Player : MonoBehaviourPun
	{
		[SerializeField] private PlayerFlip _playerFlip;
		[SerializeField] private Camera _playerCamera;
		[SerializeField] private Animator _playerAnimator;
		[SerializeField] private BulletPool _bulletPool;
		[SerializeField] private float _speed;
		[SerializeField] private TextMeshProUGUI _playerNameText;
		[Inject] private UIPlayer _uiPlayer;
		private bool _isAllowedMove = true;
		private bool _isDisableInputs;
		private bool _isMe;
		private bool _isReadyForShot = true;
		private int _coinsCollected;
		private string _coinsCollectedString;
		public string CoinsCollectedString => _coinsCollectedString;
		public TextMeshProUGUI PlayerNameText => _playerNameText;

		private void Awake()
		{
			if (photonView.IsMine)
			{
				_isMe = true;
				_playerCamera.gameObject.SetActive(true);
				_playerNameText.text = PhotonNetwork.NickName;
			}
			else
				_playerNameText.text = photonView.Owner.NickName;
		}

		private void Update()
		{
			if(photonView.IsMine && _isDisableInputs == false)
				PlayerInput();
		}

		public void DisableInputs(bool value) => _isDisableInputs = value;
		public bool SetIsMe(bool value) => _isMe = value;
		private void PlayerInput()
		{
			if (_isAllowedMove)
				transform.position += new Vector3(_uiPlayer.Joystick.Horizontal, 0) * (Time.deltaTime * _speed);
			PlayerMove();
		}

		private void PlayerMove()
		{
			if (_uiPlayer.Joystick.Horizontal == 0) _playerAnimator.SetBool("IsMove", false);
				
			if (_uiPlayer.Joystick.Horizontal > 0 && _playerAnimator.GetBool("IsShot") == false) 
				_playerFlip.SetFlip(false);
			if(_uiPlayer.Joystick.Horizontal is > 0 or < 0) 
				_playerAnimator.SetBool("IsMove", true);
			if (_uiPlayer.Joystick.Horizontal < 0 && _playerAnimator.GetBool("IsShot") == false)
				_playerFlip.SetFlip(true);
		}

		public void TryShot()
		{
			if (_playerAnimator.GetBool("IsMove") == false) 
				StartCoroutine(Shot());
		}

		private IEnumerator Shot()
		{
			if(gameObject.activeSelf== false)
				yield break;
			if(_isReadyForShot == false)
				yield break;
			if (_playerFlip.SpriteRenderer.flipX == false) 
				_bulletPool.GetBulletFromPool(_bulletPool.SpawnPointRight);
			if (_playerFlip.SpriteRenderer.flipX)
			{
				GameObject bulletFromPool = _bulletPool.GetBulletFromPool(_bulletPool.SpawnPointLeft);
				bulletFromPool.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
			}
			_isReadyForShot = false;
			ShotState(true,false);
			yield return new WaitForSeconds(1f);
			ShotState(false,true);
			_isReadyForShot = true;
		}

		private void ShotState(bool shotAnimationValue, bool isAllowedMove)
		{
			_playerAnimator.SetBool("IsShot", shotAnimationValue);
			_isAllowedMove = isAllowedMove;
		}

		public void PickupCoin()
		{
			_coinsCollected++;
			_coinsCollectedString = _coinsCollected.ToString();
			_uiPlayer.SetCoinsUi(_coinsCollected);
		}
		
		public string SendCoinsValue() => _coinsCollected.ToString();
	}
}