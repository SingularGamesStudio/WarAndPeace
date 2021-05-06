using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaballBlock:MonoBehaviour
{
    public string myName;
    public List<Metaball2D> all;
    public Vector3 pos;
    public float speed;
    public float colorSpeed;
    public Color color;
    public int newsize;
    System.Random rnd = new System.Random();
    public Color newColor;

	private void Update()
	{
        if (all.Count != newsize)
            Resize();
		if (color != newColor) {
            color = new Color(Mathf.Lerp(color.r, newColor.r, colorSpeed * Time.deltaTime), Mathf.Lerp(color.g, newColor.g, colorSpeed * Time.deltaTime), Mathf.Lerp(color.b, newColor.b, colorSpeed * Time.deltaTime));
            foreach(Metaball2D z in all) {
                z.color = color;
			}
        }
        foreach (Metaball2D z in all) {
            z.rb.velocity = (pos - z.transform.position).normalized * speed;
        }
    }
    public void Resize()
	{
        int x = newsize;
		while (all.Count > x) {
            Destroy(all[all.Count - 1].gameObject);
            all.RemoveAt(all.Count - 1);
		}
        Vector3 newpos = pos;
        if (all.Count == 0)
            newpos = pos;
        else {
            foreach (Metaball2D z in all) {
                newpos = new Vector3(newpos.x+z.transform.position.x, newpos.y + z.transform.position.y, 0);
            }
            newpos = new Vector3(newpos.x/all.Count, newpos.y/all.Count, 0);
        }
		while (all.Count < x) {
            GameObject g = Instantiate(EventsListHolder._e.MetaballInstance);
            Metaball2D mb = g.GetComponent<Metaball2D>();
            Vector3 dir = Vector3.up*mb.GetRadius();
            dir = Quaternion.AngleAxis(rnd.Next(0, 360), Vector3.forward) * dir;
            g.transform.position = newpos+dir;
            mb.color = color;
            all.Add(mb);
		}
	}
}
