using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLibControl : BYSingletonMono<SpriteLibControl>
{
    public List<Sprite> sprites;
    private Dictionary<string, Sprite> dic = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Sprite e in sprites)
        {
            dic.Add(e.name, e);
        }
    }
    public Sprite GetSpriteByName(string name_)
    {
        return dic[name_];
    }
    
}
