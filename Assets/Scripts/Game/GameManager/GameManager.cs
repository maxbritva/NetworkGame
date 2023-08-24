using System;
using Photon.Pun;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.GameManager
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private GameObject _playerPrefab;
		[SerializeField] private Canvas _canvas;
		[SerializeField] private Camera _sceneCamera;
		[SerializeField] private TextMeshProUGUI _pingText;

		private void Awake() => _canvas.gameObject.SetActive(true);

		private void Update() => _pingText.text = "Ping: " + PhotonNetwork.GetPing();

		public void SpawnPlayer()
		{
			float randomXPosition = Random.Range(-5f, 5f);
			PhotonNetwork.Instantiate(_playerPrefab.name, new Vector2(
				_playerPrefab.transform.position.x * randomXPosition,
				_playerPrefab.transform.position.y), quaternion.identity,0);
			_canvas.gameObject.SetActive(false);
			_sceneCamera.gameObject.SetActive(false);
		}
	}
}