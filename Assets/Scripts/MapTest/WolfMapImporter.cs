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
        byte[] bytes = rawData.bytes;

        int mapWidth = ByteHelper.IntFromBytes(bytes, 38);
        int mapHeight = ByteHelper.IntFromBytes(bytes, 42);
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

                    mapdata[h, w][l] = ByteHelper.IntFromBytes(bytes, tempOffset);
                }
                offset += 4;
            }
        }

        Debug.Log(mapWidth + " " + mapHeight);
        return mapdata;
    }
}