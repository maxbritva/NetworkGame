using Photon.Pun;
using UnityEngine;

namespace Game.Player
{
	public class PlayerFlip : MonoBehaviourPun
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private PhotonView _photonView;

		public SpriteRenderer SpriteRenderer => _spriteRenderer;

		public void SetFlip(bool value)
		{
			_spriteRenderer.flipX = value;
			_photonView.RPC("FlipSpriteRight",RpcTarget.AllBuffered, value);
		}

		[PunRPC]
		private void FlipSpriteRight(bool value) => _spriteRenderer.flipX = value;
	}
}