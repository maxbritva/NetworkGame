using System.Collections.Generic;
using Network;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.GameManager
{
	public class GameManager : MonoBehaviourPun
	{
		[SerializeField] private GameObject _playerPrefab;
		[SerializeField] private Canvas _canvas;
		[SerializeField] private Camera _sceneCamera;
		[SerializeField] private TextMeshProUGUI _pingText;
		[SerializeField] private GameObject _leaveScreen;
		private Player.Player _localPlayer;
		private UIPlayer _uiPlayer;
		private NetworkFactory _networkFactory;
		public Player.Player LocalPlayer => _localPlayer;
		
		private void Awake() => _canvas.gameObject.SetActive(true);
		private void Update() => _pingText.text = "Ping: " + PhotonNetwork.GetPing();

		public void SpawnPlayer()
		{
			GameObject player = _networkFactory.Create(_playerPrefab.name);
			if(player.GetComponent<Player.Player>().SetIsMe(true))
				_localPlayer = player.GetComponent<Player.Player>();
			_canvas.gameObject.SetActive(false);
			_uiPlayer.ShowControls(true);
			SetMainCamera(false);
			_localPlayer.DisableInputs(false);
			_localPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
		}

		public void SetMainCamera(bool value) => _sceneCamera.gameObject.SetActive(value);

		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom();
			PhotonNetwork.LoadLevel(0);
		}

		public void ToggleLeaveScreen() => _leaveScreen.SetActive(!_leaveScreen.activeSelf);
		[Inject] private void Construct(NetworkFactory networkFactory, UIPlayer uiPlayer)
		{
			_networkFactory = networkFactory;
			_uiPlayer = uiPlayer;
		}
	}
}