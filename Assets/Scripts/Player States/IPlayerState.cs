using UnityEngine;
using System.Collections;

public interface IPlayerState{

	void UpdateState();

	void  OnCollisionEnter2D (Collision2D coll);

	void vulnerableStatePattern();

	void invincibleStatePattern();

	IEnumerator invincibleTime();
}
