using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
// https://stackoverflow.com/questions/42244755/load-image-from-stream-streamreader-to-image-or-rawimage-component

public class CallCLIPGAN : MonoBehaviour
{
    public UnityEngine.UI.RawImage DisplayImage;
    private int numImagesPerRequest = 15;

    void Start() {
        // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://imagination-machine.s3.us-east-2.amazonaws.com/first/cyberpunk/progress_15.png"));
        // HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        // Debug.Log(response);
    }

    public void UpdateImage(string sessionName, string folderName) {
        // DisplayImage.texture = GetImage(sessionName, folderName);
        // DisplayImage.color = Color.magenta;
        Texture2D[] textures = new Texture2D[numImagesPerRequest];
        int nextTexture = 0;
        while (nextTexture < numImagesPerRequest) {
            while (textures[nextTexture] != null) {
                try
                {
                    textures[nextTexture] = GetImage(sessionName, folderName, nextTexture);
                }
                catch (System.Exception)
                {
                    System.Threading.Thread.Sleep(100);
                } 
            }
            DisplayImage.texture = textures[nextTexture];
            nextTexture++;
        }
    }
    Texture2D GetImage(string sessionName, string folderName, int fileNumber) {
        // string fileName = "localhost";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://imagination-machine.s3.us-east-2.amazonaws.com/{0}/{1}/progress_{2}.png", sessionName, folderName, fileNumber));
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
