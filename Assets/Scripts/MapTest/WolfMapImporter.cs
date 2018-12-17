using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <参考>http://kameske027.php.xdomain.jp/analysis_woditor.php
/// マップデータは上から下に書き込まれている
/// </summary>

public class WolfMapImporter : MonoBehaviour
{
    [SerializeField]
    TextAsset rawdata;

    private void Start()
    {
        ExportMapdata(rawdata);
    }

    Texture2D RenderMap(int[,][] mapdata)
    {
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapdata">mapdata.bytes</param>
    /// <returns>int[mapHeight,mapWidth][layerCnt(3)]</returns>
    int[,][] ExportMapdata(TextAsset rawdata)
    {
        Debug.Log(rawdata.bytes.Length);
        byte[] bytes = rawdata.bytes;

        int mapWidth = IntFromBynary(bytes, 38);
        int mapHeight = IntFromBynary(bytes, 42);
        long mapSize = mapWidth * mapHeight;
        var mapdata = new int[mapHeight, mapWidth][];
        long offset = 50;

        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHeight; h++)
            {
                mapdata[h, w] = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    long tempoffset = offset + mapSize * i;
                    mapdata[h, w][i] = IntFromBynary(bytes, tempoffset);
                }
            }
        }

        Debug.Log(mapWidth+" "+mapHeight);
        return mapdata;
    }

    int IntFromBynary(byte[] data, long offset, bool littleEndian = true)
    {
        if (data.Length <= offset + 4) return 0;

        int val = 0;
        for(int i = 0; i < 4; i++)
        {
            val += data[offset + i] << (i * 8);
        }
        return val;
    }
}