using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
// https://stackoverflow.com/questions/42244755/load-image-from-stream-streamreader-to-image-or-rawimage-component

public class CallCLIPGAN : MonoBehaviour
{
    public UnityEngine.UI.RawImage DisplayImage;

    public void UpdateImage(string clipInput) {
        DisplayImage.texture = GetImage(clipInput);
        // DisplayImage.color = Color.magenta;
    }
    Texture2D GetImage(string clipInput) {
        string awsUrl = "localhost";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}:5000/getImage?clip_input=\"{1}\"", awsUrl, clipInput));
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
