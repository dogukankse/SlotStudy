using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
	public class CoinParticle : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _particleSystem;
		private ParticleSystem.Burst _burst;

		private void Awake()
		{
			_burst = _particleSystem.emission.GetBurst(0);
		}

		public void Play(int burst, UnityAction onComplete)
		{
			float duration = _particleSystem.main.duration;
			_burst.count = new ParticleSystem.MinMaxCurve(burst);
			_particleSystem.emission.SetBurst(0, _burst);
			_particleSystem.Play();
			DOVirtual.DelayedCall(duration, () => onComplete());
		}
	}
}