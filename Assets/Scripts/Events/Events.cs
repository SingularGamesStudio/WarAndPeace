﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
	public static int Comparison(MyEvent x, MyEvent y)
	{
		if (x.time == y.time)
			return 0;
		if (x.time < y.time)
			return -1;
		return 1;
	}
	List<MetaballBlock> players = new List<MetaballBlock>();
	public int time;
	List<MyEvent> all;
	int now = 0;
	Slider slider;
	Text timer;
	bool warping = false;
	bool warp = false;
	private void Start()
	{
		warping = false;
		time = 0;
		slider = GameObject.Find("Slider").GetComponent<Slider>();
		timer = GameObject.Find("Timer").GetComponent<Text>();
		all = EventsListHolder._e.myEvents;
		all.Sort(Comparison);
		slider.maxValue = all[all.Count - 1].time+5000;
	}
	bool changedbythis = false;
	private void Update()
	{
		time += (int)(Time.deltaTime * 1000);
		changedbythis = true;
		if(!warping)
			slider.value = time;
		changedbythis = false;
		timer.text = time.ToString() + " ms";
		while (now < all.Count && all[now].time <= time) {
			if(all[now].type== "Добавить персонажа") {
				GameObject g = Instantiate(EventsListHolder._e.EmptyBlock);
				MetaballBlock b = g.GetComponent<MetaballBlock>();
				b.myName = all[now].target;
				b.name = all[now].target;
				b.pos = all[now].move;
				b.color = all[now].recolor;
				b.newColor = b.color;
				b.speed = all[now].newSpeed;
				b.newsize = all[now].resize;
				players.Add(b);
			} else if(all[now].type== "Перемещение") {
				foreach(MetaballBlock b in players) {
					if (b.myName == all[now].target) {
						b.pos = all[now].move;
						break;
					}
				}
			} else if(all[now].type == "Изменить цвет") {
				foreach (MetaballBlock b in players) {
					if (b.myName == all[now].target) {
						b.newColor = all[now].recolor;
						break;
					}
				}
			} else if (all[now].type == "Изменить размер") {
				foreach (MetaballBlock b in players) {
					if (b.myName == all[now].target) {
						b.newsize = all[now].resize;
						break;
					}
				}
			} else if(all[now].type=="Изменить скорость") {
				foreach (MetaballBlock b in players) {
					if (b.myName == all[now].target) {
						b.speed = all[now].newSpeed;
						break;
					}
				}
			}
			now++;
		}
		if (warp) {
			foreach (MetaballBlock z in players)
				z.Resize();
		}
		warp = false;
	}
	string last = "";
	public void sliderChanged()
	{
		if (!changedbythis) {
			warping = true;
			
		}
		last = "";
		foreach (MyEvent e in all) {
			if (e.time <= slider.value && e.caption != "") {
				last = "Последнее событие: " + e.caption + " (" + e.time.ToString() + " ms)";
			}
		}
		UIController._ui.TextChange(last);
	}
	public void TimeTravel()
	{
		warping = false;
		time = 0;
		foreach (MyEvent e in all) {
			if (e.time <= slider.value && e.caption != "") {
				time = e.time-500;
			}
		}
		warp = true;
		now = 0;
		foreach(MetaballBlock z in players) {
			foreach(Metaball2D z1 in z.all) {
				Destroy(z1.gameObject);
			}
			Destroy(z.gameObject);
		}
		UIController._ui.TextRelease(last);
		players.Clear();
	}
}
