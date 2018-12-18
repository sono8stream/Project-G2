using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <参考>http://kameske027.php.xdomain.jp/analysis_woditor.php
/// マップデータは上から下に書き込まれている
/// </summary>

public class WolfMapImporter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapdata">mapdata.bytes</param>
    /// <returns>int[mapHeight,mapWidth][layerCnt(3)]</returns>
    public int[,][] ExportMapData(TextAsset rawData)
    {
        Debug.Log(rawData.bytes.Length);
        byte[] bytes = rawData.bytes;

        int mapWidth = IntFromBytes(bytes, 38);
        int mapHeight = IntFromBytes(bytes, 42);
        long mapSize = mapWidth * mapHeight;
        var mapdata = new int[mapHeight, mapWidth][];
        long offset = 50;

        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHeight; h++)
            {
                mapdata[h, w] = new int[3];
                for (int l = 0; l < 3; l++)
                {
                    long tempOffset = offset + mapSize * l * 4;

                    mapdata[h, w][l] = IntFromBytes(bytes, tempOffset);
                }
                offset += 4;
            }
        }

        Debug.Log(mapWidth + " " + mapHeight);
        return mapdata;
    }

    int IntFromBytes(byte[] data, long offset, bool littleEndian = true)
    {
        if (data.Length <= offset + 4) return 0;

        int val = 0;
        for (int i = 0; i < 4; i++)
        {
            val += data[offset + i] << (i * 8);
        }
        return val;
    }
}