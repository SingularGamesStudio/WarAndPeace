using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MyEvent
{
    
    public string ShownName;
    public string caption;
    public string type;
    public string target;
    public int time;
    public string merge;
    public Color recolor;
    public float newSpeed;
    public Vector3 move;
    public int resize;
    public MyEvent() { }
    public MyEvent(string _target, string _type, int _time, string _merge, Color _col, Transform _move, int _sz, float _speed, string _caption)
	{
        ShownName = _type+"|"+_target;
        target = _target;
        type = _type;
        time = _time;
        merge = _merge;
        recolor = _col;
        if (_move != null)
            move = _move.position;
        resize = _sz;
        newSpeed = _speed;
        caption = _caption;
	}
}
