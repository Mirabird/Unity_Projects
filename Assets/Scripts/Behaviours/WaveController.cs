﻿using System;
using Netologia;
using Netologia.Systems;
using Netologia.TowerDefence.Behaviors;
using Netologia.TowerDefence.Settings;
using UnityEngine;
using Zenject;

namespace Behaviours
{
	public class WaveController : MonoBehaviour
	{
		private UnitSystem _units;				//injected
		private WavePresetSettings _settings;   //injected
		private InterfaceController _controller;

        private (int Wave, int Pack, int Unit) _data;
		
		[SerializeField]
		private Transform[] _paths;
		[SerializeField]
		private Transform _spawner;

		public event Action OnLastWaveEnded;
		
		public float Delay { get; private set; }
		public bool InWave { get; private set; }
		public (int Wave, int Pack, int Unit) Data => _data;
		public int WaveCount => _settings.Count;

		public Vector3[] GetPath()
		{
			var path = new Vector3[_paths.Length];
			for (int i = 0, iMax = path.Length; i < iMax; i++)
				path[i] = _paths[i].position;
			return path;
		}

		private void Update()
		{
			//delaying
			if (Delay > 0)
			{//тут есть потеря кадра
				Delay -= TimeManager.DeltaTime;
				return;
			}

			if (InWave)
				RespawnUnit();
			//start new wave
			else
				InWave = true;
		}

        private void RespawnUnit()
        {
            var wave = _settings[_data.Wave];
            var pack = wave.Packs[_data.Pack];

            // Получаем юнита из пула
            var unit = _units[pack.Prefab].Get;

            // Проверяем, успешно ли получен юнит
            if (unit != null)
            {
                unit.Respawn(pack.Preset.Preset, _spawner.position);
                _data.Unit++;

                // Проверяем, остались ли юниты в паке
                if (_data.Unit < pack.Count)
                {
                    Delay = pack.SpawnDelay;
                    return;
                }
            }

            // Если юнит не был создан, продолжаем с остальными юнитами
            if (_data.Unit >= pack.Count || unit == null)
            {
                // Переходим к следующему паку
                _data.Pack++;
                _data.Unit = 0;

                // Проверяем, остались ли пакеты в волне
                if (_data.Pack < _settings[_data.Wave].Packs.Length)
                {
                    Delay = wave.Packs[_data.Pack].SpawnDelay;
                    return;
                }

                // Переходим к следующей волне
                _data.Wave++;
                _data.Pack = 0;

                // Проверяем, остались ли волны
                if (_data.Wave < _settings.Count)
                {
                    Delay = _settings[_data.Wave].StartDelay;
                    InWave = false;
                }
                // Завершаем игру
                else
                {
                    enabled = false;
                    OnLastWaveEnded?.Invoke();
                }
            }
        }

        private void Awake()
			=> Delay = _settings[_data.Wave].StartDelay;

		private void OnDrawGizmos()
		{
			if (_paths is not null && _paths.Length > 2)
			{
				Gizmos.color = Color.cyan;
				var prev = _paths[0].position;
				for (int i = 1, iMax = _paths.Length; i < iMax; i++)
				{
					var curr = _paths[i].position;
					Gizmos.DrawLine(prev, curr);
					prev = curr;
				}
			}
		}

		[Inject]
		private void Construct(UnitSystem units, WavePresetSettings settings)
			=> (_units, _settings) = (units, settings);
	}
}