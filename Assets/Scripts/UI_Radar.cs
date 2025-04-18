using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Radar : MaskableGraphic
{
    private float size = 0;
    private int sides = 6;
    // 顶点距离
    public List<float> vertDist = new List<float>(6);
    private float rotation = Mathf.PI;
    protected override void Start()
    {
        vertDist.Clear();
        if(vertDist == null) vertDist = new List<float>();
        if (vertDist.Count != 0)
        {
            for (int i = 0; i < vertDist.Count; i++)
            {
                vertDist[i] = 1f;
            }
        }
    }

    private void Update()
    {
        // 根据当前物体的宽高去适配尺寸
        size = rectTransform.rect.width;
        if (rectTransform.rect.width > rectTransform.rect.height)
            size = rectTransform.rect.height;
        else
            size = rectTransform.rect.width;
    }

    private bool ConvertData(HeroData hero, out List<float> temp)
    {
        temp = new List<float>();
        temp.Add(hero.averageDamage);
        temp.Add(hero.damageConversion);
        temp.Add(hero.damagepercentage);
        temp.Add(hero.fieldAverageKill);
        temp.Add(hero.Viability);
        temp.Add(hero.economiCproportion);
        temp.Add(hero.contrapositionEconomicDifference);
        temp.Add(hero.proportionOfInjuries);
        return temp != null;
    }

    public void RefreshVerDist(HeroData hero)
    {
        if (ConvertData(hero, out var temp))
        {
            vertDist.Clear();
            vertDist = temp;
            sides = vertDist.Count;
            // 设置Layout布局，Vertices顶点和Material材质为Dirty；
            // 当一个Canvas被标记为包含需要被rebatch的几何图形，那这个Canvas被认为dirty，简单来说就是图形会重新绘制
            SetVerticesDirty();
        }
    }
    
    //绘制函数，一开始就会调用这个函数去绘制图形
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (vertDist == null || vertDist.Count == 0) return;
        // degress：度， size：边数， vertSize：顶点数， adaptPos：适配位置， dist： 距离
        float degress = 360f / sides;
        int vertSize = sides + 1;
        float adpaptPos = -rectTransform.pivot.x * size;
        Vector2 pre = Vector2.zero;
        Vector2 poszero = Vector2.zero;
        if(vertDist.Count < vertSize) vertDist.Add(vertDist[0]);
        vertDist[vertDist.Count - 1] = vertDist[0];
        for (int i = 0; i < vertDist.Count; i++)
        {
            float dist = adpaptPos * vertDist[i];
            // Deg2Rad：把角度值转换为弧度值
            float rad = Mathf.Deg2Rad * i * degress;
//            float rad = Mathf.Deg2Rad * i * degress + rotation;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            Vector2 pos0 = pre;
            Vector2 pos1 = new Vector2(dist * sin, dist * cos);
            pre = pos1;
            Debug.LogFormat($"顶点1 {pos0}，顶点2 {pos1}");
            vh.AddUIVertexQuad(SetVertex(new[] {pos0, pos1, poszero, poszero}));
        }
    }

    private UIVertex[] SetVertex(Vector2[] vertices)
    {
        UIVertex[] uiVertices = new UIVertex[4];
        Vector2[] uvs = new Vector2[]{new Vector2(0, 1), new Vector2(1, 1),new Vector2(1, 0),new Vector2(0, 0)};
        for (int i = 0; i < vertices.Length; i++)
        {
            var vert = UIVertex.simpleVert;
            vert.color = color;
            vert.position = vertices[i];
            vert.uv0 = uvs[i];
            uiVertices[i] = vert;
        }

        return uiVertices;
    }

    protected override void OnDestroy()
    {
        vertDist.Clear();
        vertDist = null;
    }
}
