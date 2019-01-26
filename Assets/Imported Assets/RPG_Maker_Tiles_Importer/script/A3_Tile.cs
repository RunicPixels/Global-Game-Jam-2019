using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class A3_Tile : A2_Tile
{
    /// <summary>
    /// force to show the left side border of the tile
    /// </summary>
    [SerializeField]
    private int left_side = -1;

    /// <summary>
    /// force to show the right side border of the tile
    /// </summary>
    [SerializeField]
    private int right_side = -1;

#if UNITY_EDITOR
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        if (position.x == 6 && position.y == 1)
        {
            int a = 0;
            a++;
        }

        tilemap.RefreshTile(position);

        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                if (x == 0 && y == 0) continue;
                Vector3Int tmp_pos = position + new Vector3Int(x, y, 0);
                A3_Tile tmp_Tile = tilemap.GetTile(tmp_pos) as A3_Tile;
                if (tmp_Tile != null //&& tmp_Tile.id_tile_type == this.id_tile_type
                    )
                    tilemap.RefreshTile(tmp_pos);
            }
        }

        Vector3Int tmp_pos1 = position;
        while (true)
        {
            tmp_pos1 += Vector3Int.up;
            A3_Tile tmp_Tile = tilemap.GetTile(tmp_pos1) as A3_Tile;
            if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
            {
                tilemap.RefreshTile(tmp_pos1 + Vector3Int.right);
                tilemap.RefreshTile(tmp_pos1 + Vector3Int.left);
                tilemap.RefreshTile(tmp_pos1);
            }
            else {
                break;
            }
        }

        tmp_pos1 = position;
        while (true)
        {
            tmp_pos1 += Vector3Int.down;
            A3_Tile tmp_Tile = tilemap.GetTile(tmp_pos1) as A3_Tile;
            if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
            {
                tilemap.RefreshTile(tmp_pos1 + Vector3Int.right);
                tilemap.RefreshTile(tmp_pos1 + Vector3Int.left);
                tilemap.RefreshTile(tmp_pos1);
            }
            else {
                break;
            }
        }
    }
#endif

    protected override byte Compute_Neighbours(Vector3Int pos, ITilemap map)
    {
        int res = 0;
        int tmp_right_side = -1;
        int tmp_left_side = -1;

        Vector3Int tmp_pos = pos;
        A3_Tile tmp_Tile = null;

        while (true)
        {
            tmp_pos = tmp_pos + Vector3Int.up; //top
            tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;

            if (tmp_Tile == null || tmp_Tile.id_tile_type != this.id_tile_type)
            {
                tmp_pos += Vector3Int.down;

                A3_Tile tmp_Tile_r = map.GetTile(tmp_pos + Vector3Int.right) as A3_Tile; //right
                A3_Tile tmp_Tile_ru = map.GetTile(tmp_pos + Vector3Int.right + Vector3Int.up) as A3_Tile; //right-up
                if ((tmp_Tile_r != null && tmp_Tile_r.id_tile_type == this.id_tile_type) &&
                    (tmp_Tile_ru == null || tmp_Tile_ru.id_tile_type != this.id_tile_type) && tmp_right_side == -1)
                    tmp_right_side = 0;
                else
                    tmp_right_side = 1;

                A3_Tile tmp_Tile_l = map.GetTile(tmp_pos + Vector3Int.left) as A3_Tile; //left
                A3_Tile tmp_Tile_lu = map.GetTile(tmp_pos + Vector3Int.left + Vector3Int.up) as A3_Tile; //left-up
                if ((tmp_Tile_l != null && tmp_Tile_l.id_tile_type == this.id_tile_type) &&
                    (tmp_Tile_lu == null || tmp_Tile_lu.id_tile_type != this.id_tile_type) && tmp_left_side == -1)
                    tmp_left_side = 0;
                else
                    tmp_left_side = 1;
                break;
            }
        }

        tmp_pos = pos + Vector3Int.right + Vector3Int.up;
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile; //right-up
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type && tmp_right_side == -1)
        {
            tmp_right_side = 0;
        }

        tmp_pos = pos + Vector3Int.left + Vector3Int.up;
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile; //left-up
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type && tmp_left_side == -1)
        {
            tmp_left_side = 0;
        }

        tmp_pos = pos + Vector3Int.up; //up
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
        {
            res |= (1 << 1);
        }

        tmp_pos = pos + Vector3Int.down; //down
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
        {
            res |= (1 << 6);
        }

        if (tmp_right_side == -1)
        {
            tmp_pos = pos + Vector3Int.right; //right
            tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
            if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
            {
                res |= (1 << 3);
            }
        }
        else
        {
            res |= (tmp_right_side == 1 ? 0 : 1 << 3);
        }

        if (tmp_left_side == -1)
        {
            tmp_pos = pos + Vector3Int.left; //left
            tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
            if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
            {
                res |= (1 << 4);
            }
        }
        else
        {
            res |= (tmp_left_side == 1 ? 0 : 1 << 4);
        }
        return (byte)res;
    }

    protected byte Compute_Neighbours1(Vector3Int pos, ITilemap map)
    { 
        int res = 0;
        right_side = -1;
        left_side = -1;
        int tmp_right_side = -1;
        int tmp_left_side = -1;

        Vector3Int tmp_pos = pos;
        A3_Tile tmp_Tile = null;

        //while (true)
        //{
        //    tmp_pos = tmp_pos + Vector3Int.up; //top
        //    tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;

        //    if(tmp_Tile == null || tmp_Tile.id_tile_type != this.id_tile_type)
        //    {
        //        tmp_pos += Vector3Int.down;
        //        Debug.Log(tmp_pos);

        //        A3_Tile tmp_Tile_r = map.GetTile(tmp_pos + Vector3Int.right) as A3_Tile; //right
        //        if (tmp_Tile_r != null && tmp_Tile_r.id_tile_type == this.id_tile_type)
        //            right_side = -1;
        //        //right_side = !(tmp_Tile_r != null && tmp_Tile_r.id_tile_type == this.id_tile_type);

        //        //right_side = !((tmp_Tile_r != null && tmp_Tile_r.id_tile_type == this.id_tile_type) &&
        //        //    !(tmp_Tile_ru != null && tmp_Tile_ru.id_tile_type == this.id_tile_type));

        //        A3_Tile tmp_Tile_l = map.GetTile(tmp_pos + Vector3Int.left) as A3_Tile; //left
        //        if (tmp_Tile_l != null && tmp_Tile_l.id_tile_type == this.id_tile_type)
        //            left_side = -1;
        //        //left_side = !(tmp_Tile_l != null && tmp_Tile_l.id_tile_type == this.id_tile_type);

        //        //A3_Tile tmp_Tile_lu = map.GetTile(tmp_pos + Vector3Int.left + Vector3Int.up) as A3_Tile; //left-up
        //        break;
        //    }               
        //}

        tmp_pos = pos + Vector3Int.right + Vector3Int.up;
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile; //right-up
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
        {
            tmp_right_side = 0;
        }

        tmp_pos = pos + Vector3Int.left + Vector3Int.up;
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile; //left-up
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
        {
            tmp_left_side = 0;
        }
        
        tmp_pos = pos + Vector3Int.up; //up
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
        {
            //Debug.Log(string.Format("{2} {0} {1}", tmp_Tile.left_side, tmp_Tile.right_side, tmp_pos));
            if (tmp_Tile.left_side != -1)
            {
                tmp_left_side = tmp_Tile.left_side;
            }

            if (tmp_Tile.right_side != -1)
            {
                tmp_right_side = tmp_Tile.right_side;
            }
            res |= (1 << 1);
        }

        tmp_pos = pos + Vector3Int.down; //down
        tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
        if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
        {
            res |= (1 << 6);
        }

        if (tmp_right_side == -1)
        {
            tmp_pos = pos + Vector3Int.right; //right
            tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
            if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
            {
                res |= (1 << 3);
                //else if(right_side == true) res |= (1 << 3);
            }
        }
        else
        {
            res |= (tmp_right_side == 1 ? 0 : 1 << 3);
        }

        if (tmp_left_side == -1)
        {
            tmp_pos = pos + Vector3Int.left; //left
            tmp_Tile = map.GetTile(tmp_pos) as A3_Tile;
            if (tmp_Tile != null && tmp_Tile.id_tile_type == this.id_tile_type)
            {
                res |= (1 << 4);
                // else if (left_side == 1) res |= (1 << 4);
            }
        }
        else
        {
            res |= (tmp_left_side == 1 ? 0 : 1 << 4);
        }
        right_side = tmp_right_side;
        left_side = tmp_left_side;
        return (byte)res;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(A3_Tile))]
public class A3_Editor : Editor
{
    private A3_Tile tile { get { return (target as A3_Tile); } }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        float oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 210;

        EditorGUI.BeginChangeCheck();
        tile.preview = (Sprite)EditorGUILayout.ObjectField("Preview", tile.preview, typeof(Sprite), false, null);

        if (EditorGUI.EndChangeCheck())
            EditorUtility.SetDirty(tile);

        EditorGUIUtility.labelWidth = oldLabelWidth;
    }
}
#endif