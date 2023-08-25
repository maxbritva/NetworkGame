using UnityEngine;

namespace Game.Player
{
	public class PlayerJump : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private float _jumpForce;
		private bool _isGrounded;
		public bool IsGrounded => _isGrounded;

		private void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.TryGetComponent(out Ground ground)) 
				_isGrounded = true;
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out Ground ground)) 
				_isGrounded = false;
		}

		public void Jump() => _rigidbody2D.AddForce(new Vector2(0,_jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
	}
}