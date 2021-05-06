using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Events))]
public class EventsInspector : Editor
{
    public int index = 0; 
    string[] allTypes = { "Добавить персонажа", "Перемещение", "Изменить цвет", "Изменить размер", "Смешать персонажей", "Изменить скорость"};
    public bool creatingEvent = false;
    string[] st_characters;
    public Events myTarget = null;
    public override void OnInspectorGUI()
    {
        myTarget = (Events)target;
        if (EventsListHolder._e == null)
            myTarget.gameObject.GetComponent<EventsListHolder>().Init();
        if (!creatingEvent) {
            index = EditorGUILayout.Popup(index, allTypes);
            if (GUILayout.Button("Добавить событие")) {
                creatingEvent = true;
                Init();
            }
        } else {
            GUILayout.Label(allTypes[index]);
            GUILayout.Space(5);
            List<string> characters = new List<string>();
            foreach(MyEvent z in EventsListHolder._e.myEvents) {
                if (z.type == "Добавить персонажа")
                    characters.Add(z.target);
            }
            st_characters = new string[characters.Count];
            for (int i = 0; i < characters.Count; i++)
                st_characters[i] = characters[i];
            if (characters.Count > 0 || index==0) {
                if (index == 0) {
                    InitAddCharacter();
                } else if (index == 1) {
                    InitMove();
                } else if (index == 2) {
                    InitColor();
                } else if (index == 3) {
                    InitSize();
                } else if (index == 4) {
                    InitMerge();
                } else if (index == 5) {
                    InitSpeed();
				}
            } else {
                Debug.LogError("No Characters Created");
                creatingEvent = false;
            }
		}

        GUILayout.Space(10);
        if (GUILayout.Button("Упорядочить по времени")) {
            EventsListHolder._e.myEvents.Sort(Events.Comparison);
        }
    }
    string name_get = "";
    string merge_get = "";
    int size_get = 3;
    float speed_get = 1.1f;
    Color color_get = new Color();
    Object g = null;
    Transform pos_get = null;
    int time_get = 0;
    int time1_get = 0;
    string caption_get = "";
    int temp = 0;
    int temp1 = 0;
    void Init() {
        name_get = "";
        merge_get = "";
        caption_get = "";
        size_get = 3;
        color_get = new Color();
        g = null;
        pos_get = null;
        time_get = 0;
        time1_get = 0;
        temp = 0;
        temp1 = 0;
        speed_get = 1.1f;
    }
    void InitAddCharacter()
	{
        name_get = EditorGUILayout.TextField("Имя : ", name_get);
        g = EditorGUILayout.ObjectField("Позиция : ", g, typeof(GameObject), true);
        if(g!=null)
            pos_get = ((GameObject)g).transform;
        color_get = EditorGUILayout.ColorField(color_get);
        size_get = EditorGUILayout.IntField("Размер : ", size_get);
        speed_get = EditorGUILayout.FloatField("Скорость : ", speed_get);
        time_get = EditorGUILayout.IntField("Время (мс) : ", time_get);
        caption_get = EditorGUILayout.TextArea(caption_get);
        if (GUILayout.Button("Добавить")) {
            creatingEvent = false;
            EventsListHolder._e.myEvents.Add(new MyEvent(name_get, "Добавить персонажа", time_get, "", color_get, pos_get, size_get, speed_get, caption_get));
        }
        if (GUILayout.Button("Отмена")) {
            creatingEvent = false;
        }
    }
    void InitMove()
    {
        temp = EditorGUILayout.Popup(temp, st_characters);
        name_get = st_characters[temp];
        g = EditorGUILayout.ObjectField("Позиция : ", g, typeof(GameObject), true);
        if (g != null)
            pos_get = ((GameObject)g).transform;
        time_get = EditorGUILayout.IntField("Время начала движения (мс) : ", time_get);
        caption_get = EditorGUILayout.TextArea(caption_get);
        if (GUILayout.Button("Добавить")) {
            creatingEvent = false;
            EventsListHolder._e.myEvents.Add(new MyEvent(name_get, "Перемещение", time_get, "", color_get, pos_get, size_get, speed_get, caption_get));
        }
        if (GUILayout.Button("Отмена")) {
            creatingEvent = false;
        }
    }
    void InitColor()
    {
        temp = EditorGUILayout.Popup(temp, st_characters);
        name_get = st_characters[temp];
        color_get = EditorGUILayout.ColorField(color_get);
        time_get = EditorGUILayout.IntField("Время (мс) : ", time_get);
        caption_get = EditorGUILayout.TextArea(caption_get);
        if (GUILayout.Button("Добавить")) {
            creatingEvent = false;
            EventsListHolder._e.myEvents.Add(new MyEvent(name_get, "Изменить цвет", time_get, "", color_get, pos_get, size_get, speed_get, caption_get));
        }
        if (GUILayout.Button("Отмена")) {
            creatingEvent = false;
        }
    }
    void InitSize()
    {
        temp = EditorGUILayout.Popup(temp, st_characters);
        name_get = st_characters[temp];
        size_get = EditorGUILayout.IntField("Размер : ", size_get);
        time_get = EditorGUILayout.IntField("Время (мс) : ", time_get);
        caption_get = EditorGUILayout.TextArea(caption_get);
        if (GUILayout.Button("Добавить")) {
            creatingEvent = false;
            EventsListHolder._e.myEvents.Add(new MyEvent(name_get, "Изменить размер", time_get, "", color_get, pos_get, size_get, speed_get, caption_get));
        }
        if (GUILayout.Button("Отмена")) {
            creatingEvent = false;
        }
    }
    void InitMerge()
    {
        temp = EditorGUILayout.Popup(temp, st_characters);
        name_get = st_characters[temp];
        temp1 = EditorGUILayout.Popup(temp1, st_characters);
        merge_get = st_characters[temp1];
        time_get = EditorGUILayout.IntField("Время начала (мс) : ", time_get);
        time1_get = EditorGUILayout.IntField("Длительность (мс) : ", time1_get);
        caption_get = EditorGUILayout.TextArea(caption_get);
        if (GUILayout.Button("Добавить")) {
            Color last1 = Color.black;
            foreach(MyEvent z in EventsListHolder._e.myEvents) {
                if(z.target==name_get && (z.type== "Добавить персонажа" || z.type== "Изменить цвет")) {
                    last1 = z.recolor;
				}
			}
            Color last2 = Color.black;
            foreach (MyEvent z in EventsListHolder._e.myEvents) {
                if (z.target == merge_get && (z.type == "Добавить персонажа" || z.type == "Изменить цвет")) {
                    last2 = z.recolor;
                }
            }
            Color new0 = new Color((last1.r + last2.r) / 2, (last1.g + last2.g) / 2, (last1.b + last2.b) / 2);
            creatingEvent = false;
            EventsListHolder._e.myEvents.Add(new MyEvent(name_get, "Изменить цвет", time_get, "", new0, pos_get, size_get, speed_get, caption_get));
            EventsListHolder._e.myEvents.Add(new MyEvent(merge_get, "Изменить цвет", time_get, "", new0, pos_get, size_get, speed_get, ""));
            EventsListHolder._e.myEvents.Add(new MyEvent(name_get, "Изменить цвет", time_get+time1_get, "", last1, pos_get, size_get, speed_get, ""));
            EventsListHolder._e.myEvents.Add(new MyEvent(merge_get, "Изменить цвет", time_get + time1_get, "", last2, pos_get, size_get, speed_get, ""));
        }
        if (GUILayout.Button("Отмена")) {
            creatingEvent = false;
        }
    }
    void InitSpeed()
    {
        temp = EditorGUILayout.Popup(temp, st_characters);
        name_get = st_characters[temp];
        speed_get = EditorGUILayout.FloatField("Скорость : ", speed_get);
        time_get = EditorGUILayout.IntField("Время (мс) : ", time_get);
        caption_get = EditorGUILayout.TextArea(caption_get);
        if (GUILayout.Button("Добавить")) {
            creatingEvent = false;
            EventsListHolder._e.myEvents.Add(new MyEvent(name_get, "Изменить размер", time_get, "", color_get, pos_get, size_get, speed_get, caption_get));
        }
        if (GUILayout.Button("Отмена")) {
            creatingEvent = false;
        }
    }
}
