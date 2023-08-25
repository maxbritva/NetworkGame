using Game.GameManager;
using Network;
using UnityEngine;
using Zenject;

namespace Game.DI
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private GameManager.GameManager _gameManager;
		[SerializeField] private UIPlayer _uiPlayer;
		[SerializeField] private EndGame _endGame;
		public override void InstallBindings()
		{
			Container.Bind<GameManager.GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
			Container.Bind<EndGame>().FromInstance(_endGame).AsSingle().NonLazy();
			Container.Bind<NetworkFactory>().FromNew().AsSingle().NonLazy();
			Container.Bind<UIPlayer>().FromInstance(_uiPlayer).AsSingle().NonLazy();

		}
	}
}