using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Network
{
	public class MenuManager : MonoBehaviourPunCallbacks
	{
		[Header("Screens")]
		[SerializeField] private GameObject _connectScreen;
		[SerializeField] private GameObject _userNameScreen;
		
		[Header("Buttons")]
		[SerializeField] private Button _createUSerNameButton;
		
		[Header("InputFields")]
		[SerializeField] private TMP_InputField _userNameInput;
		[SerializeField] private TMP_InputField _createRoomInput;
		[SerializeField] private TMP_InputField _joinRoomInput;
		
		private void Awake() => PhotonNetwork.ConnectUsingSettings();

		public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby(TypedLobby.Default);

		public override void OnJoinedLobby()
		{
			Debug.Log("ConnectedToLobby");
			ShowScreen(true,false);
			_createUSerNameButton.interactable = false;
		}

		public override void OnJoinedRoom() => PhotonNetwork.LoadLevel(1);

		#region UIMethods
		public void OnCreateNameButton()
		{
			PhotonNetwork.NickName = _userNameInput.text;
			ShowScreen(false,true);
		}

		public void OnNameFieldChanged() => _createUSerNameButton.interactable = _userNameInput.text.Length >= 2;
		
		public void OnCreateRoomButton()
		{
			PhotonNetwork.CreateRoom(_createRoomInput.text, new RoomOptions() {MaxPlayers = 2}, null);
		}
		public void OnJoinRoomButton()
		{
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = 2;
			PhotonNetwork.JoinOrCreateRoom(_joinRoomInput.text, roomOptions, TypedLobby.Default);
		}
		private void ShowScreen(bool nameScreen, bool connectScreen)
		{
			_userNameScreen.SetActive(nameScreen);
			_connectScreen.SetActive(connectScreen);
		}

		#endregion
	}
}