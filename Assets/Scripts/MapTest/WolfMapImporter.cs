using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0x00000000~ :00が10バイト並ぶ
/// 0x0000000A~ :マップデータを表す識別子"WOLFM"(5byte)
/// 0x0000000F:	未調査データ。0x64 0x65 0x82 などが含まれていることがある(19byte)
/// 0x00000022:	マップチップID。4バイト整数型
/// 0x00000026: マップ幅。4バイト整数型
/// 0x0000002A: マップ高さ。4バイト整数型
/// 0x0000002E: イベント数
/// 0x00000032: マップ幅* 高さ*4 マップデータ
/// 4バイト整数型でマップチップIDが並ぶ
/// ここから始まらないこともある模様
/// 0レイヤー目、(0,0)の位置から開始し、レイヤーごとにまとまって記述
/// 
/// オートタイルのidについて
/// 3byte目がオートタイル番号、1,2byte目でオートタイルの位置を指定している？
/// 
/// マップデータの後ろ EOFまで イベントデータ
/// </summary>

public class WolfMapImporter : MonoBehaviour {

    int[] ExportMap()
    {

    }
}
