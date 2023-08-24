using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Game.Player.Gun
{
	public class Bullet : MonoBehaviourPun
	{
		[SerializeField] private float _moveSpeed = 8f;
		[SerializeField] private PhotonView _photonView;
		private WaitForSeconds _hideTime = new WaitForSeconds(2f);
		private bool _movingDirection;

		private void Update()
		{
			if (_movingDirection == false)
				transform.Translate(Vector2.right * (_moveSpeed * Time.deltaTime));
			else
				transform.Translate(Vector2.left * (_moveSpeed * Time.deltaTime));
		}

		private IEnumerator HideBullet()
		{
			yield return _hideTime;
			_photonView.RPC("Hide", RpcTarget.AllBuffered);
		}

		[PunRPC]
		private void Hide() => gameObject.SetActive(false);
		
		[PunRPC]
		private void ChangeDirection() => _movingDirection = true;

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (photonView.IsMine == false)
				return;
			PhotonView target = col.GetComponent<PhotonView>();
			if (target != null && (target.IsMine == false || target.IsRoomView))
				_photonView.RPC("Hide", RpcTarget.AllBuffered);
		}
	}
}