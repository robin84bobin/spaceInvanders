using System;
using UnityEngine;
using Data;

public class WeaponModel
{
	public event Action<Vector3, BulletData> OnShot = delegate {};

	private WeaponData _data;
	private Vector3 _direction;

	public WeaponModel (WeaponData data)
	{
		_data = data;
	}

	public void SetDirection(Vector3 direction)
	{
		_direction = direction;
	}

	public void Shot()
	{
		OnShot (_data.bulletSpeed * _direction, _data.bullet);
	}
}


