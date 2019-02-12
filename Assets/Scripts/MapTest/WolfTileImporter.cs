using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文字コードはshift-jis
/// ↓データ構造
/// ~ 0x0E : ヘッダ？
/// 0x0F: タイル設定名の文字数+1(4byte リトルエンディアン)
/// 0x13~: タイル設定名
/// 00
/// ベースマップチップのパス名の文字数+1
/// ベースマップチップのパス
/// [
/// オートタイルへのパス名の文字数+1(4byte リトルエンディアン)
/// オートタイルへのパス 00 ...
/// ]
/// の15回繰り返し
/// FF タイル数(4byte リトルエンディアン)，オートタイル(16個分)含む
/// タイル数分タグ番号設定(タイル数*1byte)
/// FF タイル数
/// 進行設定(タイル数*4byte)
/// 
/// 末尾はCF
/// タイルデータは4byteごと
/// 通行許可設定
/// ○→00 00 00 00
/// ×→0F 00 00 00
/// ☆→10 00 00 00
/// △→00 01 00 00
/// ↓→00 02 00 00 (↓追加時は加算)
/// □→40 00 00 00
/// ×1/4について
/// 右下: 21 00 00 00 左下: 22 00 00 00 右上: 24 00 00 00 左上: 28 00 00 00
/// 重複時は1byte目の1ケタ目が加算される 例) 上二つ: 2C 00 00 00
/// ☆と重複した場合は 3X 00 00 00となる 
/// 
/// 通行方向設定
/// .の位置によって変化
/// 下: 01 00 00 00 左: 02 00 00 00 右: 04 00 00 00 上: 08 00 00 00
/// それぞれ重複すると加算される
/// 1/4と通行方向設定は重複しない(1/4が優先される)
/// 
/// カウンター設定
/// ON→80 00 00 00を加算 
/// 
/// </summary>

namespace UniWolf
{
    public class WolfTileImporter
    {
        Encoding encoding;

        public WolfTileImporter()
        {
            encoding = Encoding.GetEncoding("Shift_JIS");
        }

        public int[] ExportTileData(TextAsset rawData,
            out Texture2D mapChip, out List<Texture2D> autoChipList)
        {
            byte[] bytes = rawData.bytes;
            long offset = 15;

            LoadBaseTileName(bytes, ref offset);
            mapChip = LoadBaseTileSet(bytes, ref offset);
            autoChipList = LoadAutochipList(bytes, ref offset);

            return null;
            //var tileData = new int[][];
        }

        void LoadBaseTileName(byte[] rawBytes, ref long offset)
        {
            int nameLength = rawBytes.ToInt(ref offset) - 1;
            string tileName = encoding.GetString(
                rawBytes.GetRange(ref offset, nameLength));
            offset++;
        }

        Texture2D LoadBaseTileSet(byte[] rawBytes, ref long offset)
        {
            int chipNameLength = rawBytes.ToInt(ref offset) - 1;
            string chipPath = encoding.GetString(
                    rawBytes.GetRange(ref offset, chipNameLength));
            offset++;
            chipPath = chipPath.Replace(".png", "");
            Texture2D mapChip = Resources.Load<Texture2D>(chipPath);
            return mapChip;
        }

        List<Texture2D> LoadAutochipList(byte[] rawBytes, ref long offset)
        {
            List<Texture2D> autoChipList = new List<Texture2D>();
            for (int i = 0; i < 15; i++)// Read auto chips
            {
                int autoNameLength = rawBytes.ToInt(ref offset) - 1;
                if (autoNameLength > 0)
                {
                    string autoPath = encoding.GetString(
                        rawBytes.GetRange(ref offset, autoNameLength));
                    autoPath = autoPath.Replace(".png", "");
                    offset++;
                    autoChipList.Add(Resources.Load<Texture2D>(autoPath));
                }
            }
            return autoChipList;
        }

        int[] ImportTagData(byte[] rawBytes, long offset)
        {
            int size = rawBytes.ToInt(ref offset);
            return null;
        }
    }
}