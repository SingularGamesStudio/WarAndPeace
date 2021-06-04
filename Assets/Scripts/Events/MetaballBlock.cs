using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MetaballBlock:MonoBehaviour
{
    public string myName;
    public List<Metaball2D> all;
    public Vector3 pos;
    public float speed;
    const float basespeed = 1.1f;
    public float colorSpeed;
    public Color color;
    const float mindir = 0.5f;
    public int newsize;
    System.Random rnd = new System.Random();
    public Color newColor;
    public RectTransform caption = null;
    public GameObject caption_base;
    public RectTransform canv;
	private void Awake()
	{
        init();
	}
	public void init()
	{
        canv = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }
	private void Update()
	{
        
        if (all.Count != newsize)
            Resize();
        if (caption != null) {
            Vector3 mediumpos = Vector3.zero;
            foreach (Metaball2D z in all) {
                mediumpos += z.transform.position;
            }
            mediumpos = new Vector3(mediumpos.x / all.Count, mediumpos.y / all.Count, 0);
            mediumpos = Camera.main.WorldToScreenPoint(mediumpos);
            mediumpos = new Vector3(mediumpos.x - canv.rect.width / 2, mediumpos.y - canv.rect.height / 2, 0);
            caption.localPosition = mediumpos;
		    if (color != newColor) {
                color = new Color(Mathf.Lerp(color.r, newColor.r, colorSpeed * Time.deltaTime), Mathf.Lerp(color.g, newColor.g, colorSpeed * Time.deltaTime), Mathf.Lerp(color.b, newColor.b, colorSpeed * Time.deltaTime));
                foreach(Metaball2D z in all) {
                    z.color = color;
			    }
            }
            foreach (Metaball2D z in all) {
                if((pos - z.transform.position).magnitude<mindir)
                    z.rb.velocity = (pos - z.transform.position).normalized * basespeed;
                else z.rb.velocity = (pos - z.transform.position).normalized * speed;
            }
		}
    }
    public void Resize()
	{
        int x = newsize;
        if (x == 0) {
            if (caption != null)
                Destroy(caption.gameObject);
        } else if(caption==null){
            GameObject g = Instantiate(caption_base);
            g.transform.SetParent(canv.transform);
            g.GetComponent<Text>().text = myName;
            caption = g.GetComponent<RectTransform>();
            caption.SetParent(UIController.ierarhy.transform);
        }
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
