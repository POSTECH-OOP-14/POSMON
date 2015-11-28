using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour {

    private Dictionary<string, List<Component>> listener = new Dictionary<string, List<Component>>();


    public void add(Component send, string notification_name)
    {
        if (!listener.ContainsKey(notification_name))
            listener.Add(notification_name, new List<Component>());

        listener[notification_name].Add(send);
    }

    public void remove(Component send, string notification_name)
    {
        if (!listener.ContainsKey(notification_name))
            return;
        for(int i = listener[notification_name].Count -1; i >= 0; i--)
        {
            if (listener[notification_name][i].GetInstanceID() == send.GetInstanceID())
                listener[notification_name].RemoveAt(i);
        }
    }

    public void post(Component send, string name)
    {
        if (!listener.ContainsKey(name))
            return;

        foreach (Component listen in listener[name])
            listen.SendMessage(name, send, SendMessageOptions.DontRequireReceiver);
    }

    public void remove_useless()
    {
        Dictionary<string, List<Component>> tmp_l = new Dictionary<string, List<Component>>();

        foreach(KeyValuePair<string,List<Component>> item in listener)
        {
            for(int i = item.Value.Count - 1; i >= 0; i--)
            {
                if (item.Value[i] == null)
                    item.Value.RemoveAt(i);
            }

            if (item.Value.Count > 0)
                tmp_l.Add(item.Key, item.Value);
        }

        listener = tmp_l;

    }
    void loaded_level()
    {
        remove_useless();
    }
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
