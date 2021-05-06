using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class EventsListHolder : MonoBehaviour
{
	public List<MyEvent> myEvents;
	public static EventsListHolder _e = null;
	public GameObject MetaballInstance;
	public GameObject EmptyBlock;
	private void Awake()
	{
		_e = this;
	}
	public void Init()
	{
		_e = this;
	}
}
