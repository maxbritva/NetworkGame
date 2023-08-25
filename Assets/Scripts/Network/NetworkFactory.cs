using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Network
{
	public class NetworkFactory :IFactory<string, GameObject>
	{
		[Inject] private DiContainer _diContainer = default;
		public GameObject Create(string param)
		{
			float randomXPosition = Random.Range(-5f, 5f);
			GameObject playerGameObject = PhotonNetwork.Instantiate(param, Vector3.zero, quaternion.identity,0);
			playerGameObject.gameObject.transform.position = new Vector2(
				playerGameObject.gameObject.transform.position.x * randomXPosition,
				playerGameObject.gameObject.transform.position.y);
			_diContainer.InjectGameObject(playerGameObject);
			return playerGameObject;
		}
	}
}