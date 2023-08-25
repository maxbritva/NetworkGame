using Photon.Pun;
using UnityEngine;

namespace Game.Coin
{
	public class Coin : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!col.gameObject.TryGetComponent(out Player.Player player)) return;
			if (player.TryGetComponent(out PhotonView photonView))
				if (photonView.IsMine)
					player.PickupCoin();
			Destroy(gameObject);
		}
	}
}