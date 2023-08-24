using Game.Player.Gun;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Game.Player
{
	public class Player : MonoBehaviourPun
	{
		[SerializeField] private Camera _playerCamera;
		[SerializeField] private PlayerFlip _playerFlip;
		[SerializeField] private Animator _playerAnimator;
		[SerializeField] private BulletPool _bulletPool;
		[SerializeField] private float _speed;
		[SerializeField] private TextMeshProUGUI _playerNameText;
		private bool _isAllowedMove = true;
		private void Awake()
		{
			if (photonView.IsMine)
			{
				_playerCamera.gameObject.SetActive(true);
				_playerNameText.text = PhotonNetwork.NickName;
			}
			else
			{
				_playerNameText.text = photonView.Owner.NickName;
			}
		}

		private void Update()
		{
			if(photonView.IsMine)
				PlayerInput();
		}

		private void PlayerInput()
		{
			if (_isAllowedMove)
			{
				Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
				transform.position += movement * (Time.deltaTime * _speed);
			}
			PlayerMove();
			if (Input.GetMouseButtonDown(0) && _playerAnimator.GetBool("IsMove") == false) 
				Shot();
			else if (Input.GetMouseButtonUp(0))
			{
				_playerAnimator.SetBool("IsShot", false);
				_isAllowedMove = true;
			}
		}

		private void PlayerMove()
		{
			if (Input.GetKeyDown(KeyCode.D) && _playerAnimator.GetBool("IsShot") == false)
			{
				_playerAnimator.SetBool("IsMove", true);
				_playerFlip.SetFlip(false);
			}

			if (Input.GetKeyDown(KeyCode.A) && _playerAnimator.GetBool("IsShot") == false)
			{
				_playerAnimator.SetBool("IsMove", true);
				_playerFlip.SetFlip(true);
			}
			else if (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.D)))
				_playerAnimator.SetBool("IsMove", false);
		}

		private void Shot()
		{
			if (_playerFlip.SpriteRenderer.flipX == false)
			{
			GameObject bulletFromPool = _bulletPool.GetBulletFromPool(_bulletPool.SpawnPointRight);
			}
			if (_playerFlip.SpriteRenderer.flipX)
			{
				GameObject bulletFromPool = _bulletPool.GetBulletFromPool(_bulletPool.SpawnPointLeft);
				bulletFromPool.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
			}
			_playerAnimator.SetBool("IsShot", true);
			_isAllowedMove = false;
		}
	}
}