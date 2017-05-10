using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attackers;

public class AttackerSpawner : MonoBehaviour
{
	public GameObject[] AttackerPrefabs;
	[Tooltip("Maximum time between each respawn of each attacker type.")]
	public List<float> MaxAttackerSpawnIntervals;
	public float MinAttackerSpawnIntervals = 1;

	[Tooltip("Interval between each respawn speed increase in second.")]
	[Range(1, 1000)]
	public float RespawnSpeedIncreaseInterval = 30;

	[Tooltip("Percentage of next respawn interval that will be dropped to next respawn speed increase.")]
	[Range(0.01f, 1)]
	public float NextRespawnSpeedMultiflier = .9f;

	private Transform[] _lanes;
	private List<float> _nextSpawnTimes;
	private float _nextRespawnSpeedIncreaseTime;
	 
	// Use this for initialization
	void Awake ()
	{
		_lanes = GameObject.FindGameObjectsWithTag("Lane").Select(l => l.transform).ToArray();
		MaxAttackerSpawnIntervals = MaxAttackerSpawnIntervals.ToList();
		_nextSpawnTimes = MaxAttackerSpawnIntervals.Select(t => Time.time).ToList();
		_nextRespawnSpeedIncreaseTime = Time.time + RespawnSpeedIncreaseInterval;
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (var i = 0; i < AttackerPrefabs.Length; i++)
		{
			// Respawn each type of attacker
			if (Time.time > _nextSpawnTimes[i])
			{
				var lane = _lanes[Random.Range(0, _lanes.Length)];
				var attacker = Instantiate(AttackerPrefabs[i], lane.position, Quaternion.identity) as GameObject;
				attacker.transform.parent = lane;

				// Set time for next respawn of this attacker type
				_nextSpawnTimes[i] += Random.Range(MinAttackerSpawnIntervals, MaxAttackerSpawnIntervals[i]);
			}
		}
		
		if (Time.time > _nextRespawnSpeedIncreaseTime)
		{
			// Increase respawn speed
			MaxAttackerSpawnIntervals = MaxAttackerSpawnIntervals.Select(t => t * NextRespawnSpeedMultiflier).ToList();

			// Set time for next respawn speed increase
			_nextRespawnSpeedIncreaseTime += RespawnSpeedIncreaseInterval;
		}
	}
}
