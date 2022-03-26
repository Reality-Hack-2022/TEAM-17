using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
// https://stackoverflow.com/questions/42244755/load-image-from-stream-streamreader-to-image-or-rawimage-component

public class CallCLIPGAN : MonoBehaviour
{
    public UnityEngine.UI.RawImage DisplayImage;
    public GPT3Interface GPT3Interface;
    private int numImagesPerRequest = 28;
    private int sessionNum;
    private int folderNum;
    private int fileNum;

    void Start() {
        sessionNum = GPT3Interface.session;
        folderNum = 0;
        fileNum = 0;
    }

    void Update() {
        try
        {
            Texture2D texture = GetImage(sessionNum, folderNum, fileNum);
            DisplayImage.texture = texture;
            Debug.Log("Received image " + folderNum + ", progress " + fileNum);
            fileNum++;
            if (fileNum >= numImagesPerRequest) {
                Debug.Log("Completed image " + folderNum);
                fileNum = 0;
                folderNum++;
            }
        }
        catch (System.Exception)
        {
            
        }
    }
    Texture2D GetImage(int sessionNum, int folderNumber, int fileNumber) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://imagination-machine.s3.us-east-2.amazonaws.com/Session_{0}/Image{1}/progress_{2}.png", sessionNum, folderNumber, fileNumber));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        byte[] imageData = null;
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            using (var memStream = new MemoryStream())
            {
                var buffer = new byte[512];
                var bytesRead = default(int);
                while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                    memStream.Write(buffer, 0, bytesRead);
                imageData = memStream.ToArray();
            }
        }
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);
        return texture;
    }
}
