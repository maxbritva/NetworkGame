using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Game.Player.Gun
{
	public class BulletPool : MonoBehaviour
	{
		[SerializeField] private GameObject _bulletPrefab;
		[SerializeField] private List<GameObject> _poolBullet = new List<GameObject>();
		[SerializeField] private Transform _spawnPointRight;
		[SerializeField] private Transform _spawnPointLeft;

		public Transform SpawnPointRight => _spawnPointRight;
		public Transform SpawnPointLeft => _spawnPointLeft;

		private void Start() => CreateBullet(_spawnPointRight);

		private GameObject CreateBullet(Transform point)
		{
			GameObject newBall = PhotonNetwork.Instantiate(_bulletPrefab.name, new Vector3(
				point.position.x,point.position.y), Quaternion.identity,0);
			newBall.transform.SetParent(transform);
			newBall.gameObject.SetActive(false);
			_poolBullet.Add(newBall);
			return newBall;
		}

		public GameObject GetBulletFromPool(Transform point)
		{
			for (int i = 0; i < _poolBullet.Count; i++) 
			{
				if (_poolBullet[i].gameObject.activeInHierarchy == false)
				{
					_poolBullet[i].gameObject.transform.position = point.position;
					_poolBullet[i].gameObject.SetActive(true);
					return _poolBullet[i]; 
				}
			}
			GameObject ball = CreateBullet(point);
			ball.gameObject.SetActive(true);
			return ball; 
		}
	}
}