using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
	public class DisconnectManager : MonoBehaviourPunCallbacks
	{
		[SerializeField] private GameObject _disconnectUI;
		[SerializeField] private GameObject _menuButton;
		[SerializeField] private GameObject _reconnectButton;
		[SerializeField] private TextMeshProUGUI _status;

		private void Awake() => DontDestroyOnLoad(gameObject);
		private void Update()
		{
			if (Application.internetReachability != NetworkReachability.NotReachable) return;
			if (SceneManager.GetActiveScene().buildIndex == 0)
			{
				SetActiveUI(false, true, true);
				_status.text = "Lost connection. Please try to reconnect";
			}

			if (SceneManager.GetActiveScene().buildIndex == 1)
			{
				SetActiveUI(true, false, true);
				_status.text = "Lost connection. Please try to reconnect in the main menu";
			}
		}
		public override void OnConnectedToMaster()
		{
			if (_disconnectUI.activeSelf) 
				SetActiveUI(false, false, false);
		}

		public void TryConnectButton() => PhotonNetwork.ConnectUsingSettings();
		public void GoMenuButton() => PhotonNetwork.LoadLevel(0);

		private void SetActiveUI(bool menuValue, bool reconnect, bool ui)
		{
			_menuButton.SetActive(menuValue);
			_reconnectButton.SetActive(reconnect);
			_disconnectUI.SetActive(ui);
		}
	}
} 