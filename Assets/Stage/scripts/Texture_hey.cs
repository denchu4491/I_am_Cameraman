using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEditor;

public class Texture_hey : MonoBehaviour {


    string path1;
    Image image;
    static Texture2D texture;
    


    void Start () {

       
        
        image = this.GetComponent<Image>();
        path1 = Application.persistentDataPath;
        texture = PngToTex2D(path1 + "\\takepicture.png");
        // Texture -> Spriteに変換する
        Sprite texture_sprite = Sprite.Create(texture, new Rect(0, 0, 1920, 1200), Vector2.zero);
        image.sprite = texture_sprite;
        


    }

    static public void Save() {
        
            if(EditorUtility.DisplayDialog("撮影した写真を保存しますか？", "写真を保存したい場合は「はい」を、保存したくない場合は「いいえ」を選択して下さい", "はい", "いいえ")){
                Savepng();
            }
        
    }



    Texture2D PngToTex2D(string path) {
        BinaryReader bin = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read));
        byte[] rb = bin.ReadBytes((int)bin.BaseStream.Length);
        bin.Close();
        int pos = 16, width = 0, height = 0;
        for (int i = 0; i < 4; i++) width = width * 256 + rb[pos++];
        for (int i = 0; i < 4; i++) height = height * 256 + rb[pos++];
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(rb);
        return texture;
    }

    static void Savepng() {

        var filepath = EditorUtility.SaveFilePanel("Save as", "", "takepicture", "png");

        if (!string.IsNullOrEmpty(filepath)) {
            
            //  ファイルの書き込み
            byte[] bytes = texture.EncodeToPNG();
            if (bytes != null) {
                Debug.Log("Try to Write png ");
                Debug.Log(@filepath);
                File.WriteAllBytes(@filepath , bytes);
                Debug.Log("Success Write png ");
            } else Debug.Log("Save file failed");
        }
    }

    
}
