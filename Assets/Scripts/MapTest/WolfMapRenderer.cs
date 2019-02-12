using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniWolf
{
    public class WolfMapRenderer : MonoBehaviour
    {
        [SerializeField]
        TextAsset rawTileData;
        [SerializeField]
        TextAsset rawMapData;
        [SerializeField]
        Texture2D nullAutoChip;

        Texture2D mapChip;
        Texture2D[] autoChips;

        // Use this for initialization
        void Start()
        {
            var tiler = new WolfTileImporter();
            List<Texture2D> autoChipList;
            tiler.ExportTileData(rawTileData, out mapChip, out autoChipList);
            autoChipList.Insert(0, nullAutoChip);
            autoChips = autoChipList.ToArray();

            var importer = new WolfMapImporter();
            Sprite mapSprite = RenderMap(importer.ExportMapData(rawMapData));
            GetComponent<SpriteRenderer>().sprite = mapSprite;
        }

        // Update is called once per frame
        void Update()
        {

        }

        Sprite RenderMap(int[,][] mapData)
        {
            int width = mapData.GetLength(1);
            int height = mapData.GetLength(0);
            int layerCnt = mapData[0, 0].Length;
            int cellPix = mapChip.width / 8;

            int pixWidth = width * cellPix;
            int pixHeight = height * cellPix;

            Texture2D texture
                = InitialTexture(pixWidth, pixHeight, Color.clear);

            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int l = 0; l < layerCnt; l++)
                    {
                        Color[] colors = GetChipSprite(mapData[h, w][l], cellPix);
                        int x = w * cellPix;
                        int y = (height - h - 1) * cellPix;
                        RenderChip(ref texture, x, y, cellPix, colors);
                    }
                }
            }
            texture.Apply();

            Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, pixWidth, pixHeight),
                Vector2.one * 0.5f, cellPix);
            return sprite;
        }

        Texture2D InitialTexture(int width, int height, Color initialColor)
        {
            Texture2D texture = new Texture2D(width, height);
            Color[] colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = initialColor;
            }
            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }

        Color[] GetChipSprite(int chipID, int cellPix)
        {
            if (chipID >= 100000)
            {
                return GetAutoChipColors(chipID, cellPix);
            }
            else
            {
                int x = (chipID % 8) * cellPix;
                int y = mapChip.height - (chipID / 8) * cellPix - cellPix;
                return GetMapChipColors(x, y, cellPix);
            }
        }

        Color[] GetAutoChipColors(int id, int cellPix)
        {
            int index = id / 100000 - 1;
            int upLeft = (id / 1000) % 10;
            int upRight = (id / 100) % 10;
            int downLeft = (id / 10) % 10;
            int downRight = id % 10;

            Color[] colors = new Color[cellPix * cellPix];
            for (int i = 0; i < colors.Length / 4; i++)
            {
                int x = i % (cellPix / 2);
                int y = i / (cellPix / 2);
                colors[x + y * cellPix]
                    = autoChips[index].GetPixel(x, (4 - downLeft) * cellPix + y);
                colors[x + cellPix / 2 + y * cellPix]
                    = autoChips[index].GetPixel(
                        x + cellPix / 2, (4 - downRight) * cellPix + y);
                colors[x + y * cellPix + cellPix * cellPix / 2]
                    = autoChips[index].GetPixel(
                        x, (4 - upLeft) * cellPix + cellPix / 2 + y);
                colors[x + cellPix / 2 + y * cellPix + cellPix * cellPix / 2]
                    = autoChips[index].GetPixel(
                        x + cellPix / 2, (4 - upRight) * cellPix + cellPix / 2 + y);
            }
            return colors;
        }

        Color[] GetMapChipColors(int x, int y, int cellPix)
        {
            return mapChip.GetPixels(x, y, cellPix, cellPix);
        }

        void RenderChip(ref Texture2D texture, int x, int y,
            int cellPix, Color[] colors)
        {
            for (int i = 0; i < cellPix; i++)
            {
                for (int j = 0; j < cellPix; j++)
                {
                    if (colors[i + j * cellPix].a == 0) continue;

                    texture.SetPixel(x + i, y + j, colors[i + j * cellPix]);
                }
            }
        }
    }
}