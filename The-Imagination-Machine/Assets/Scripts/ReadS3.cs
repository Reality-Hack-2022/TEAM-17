using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
// https://stackoverflow.com/questions/42244755/load-image-from-stream-streamreader-to-image-or-rawimage-component
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class ReadS3 : MonoBehaviour
{
    /*public UnityEngine.UI.RawImage DisplayImage;

    public void UpdateImage(string imgPath) {
        // DisplayImage.texture = SendAmasonS3Request(imgPath);
        // DisplayImage.texture = GetImage(clipInput);
        // DisplayImage.color = Color.magenta;
    }  

    static readonly string BUCKET = "imagination-machine";

    static readonly string AWS_ACCESS_KEY_ID ="AKIA4HUARN52S6RT2DCI";
    static readonly string AWS_SECRET_ACCESS_KEY ="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";

    static readonly string AWS_S3_URL_BASE_VIRTUAL_HOSTED = "https://"+BUCKET+".s3.amazonaws.com/";
    static readonly string AWS_S3_URL_BASE_PATH_HOSTED = "https://s3.amazonaws.com/"+BUCKET+"/";
    static readonly string AWS_S3_URL = AWS_S3_URL_BASE_VIRTUAL_HOSTED;

    void SendAmasonS3Request(string itemName)
    {
        Hashtable headers = new Hashtable();

        string dateString =
            System.DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT";
        headers.Add("x-amz-date", dateString);
        Debug.Log("Date: " + dateString);

        string canonicalString = "GET\n\n\n\nx-amz-date:" + dateString + "\n/" + BUCKET + "/" + itemName;

        // now encode the canonical string
        var ae = new System.Text.UTF8Encoding();
        // create a hashing object
        HMACSHA1 signature = new HMACSHA1();
        // secretId is the hash key
        signature.Key = ae.GetBytes(AWS_SECRET_ACCESS_KEY);
        byte[] bytes  = ae.GetBytes(canonicalString);
        byte[] moreBytes = signature.ComputeHash(bytes);
        // convert the hash byte array into a base64 encoding
        string encodedCanonical = System.Convert.ToBase64String(moreBytes);

        // finally, this is the Authorization header.
        headers.Add("Authorization", "AWS " + AWS_ACCESS_KEY_ID + ":" + encodedCanonical);

        // The URL, either PATH_HOSTED or VIRTUAL_HOSTED, plus the item path in the bucket 
        string url = AWS_S3_URL + itemName;

        // Setup the request url to be sent to Amazon
        WWW www = new WWW(url, null, headers);

        // Send the request in this coroutine so as not to wait busily
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // Check for errors
        if(www.error == null)
        {
            ParseResponse(www.text, www.url);
        }
        else
        {
            Debug.Log("WWW Error: "+ www.error+" for URL: "+www.url);
            ProcessAmazonS3Error(www);
        }
    }

    // Texture2D GetImage(string clipInput) {

    //     GetObjectRequest request = new GetObjectRequest();  
    //     request.WithBucketName("imagination-machine");  
    //     request.WithKey("zBy/WmCJ61MjgVzkOwA9Dg/Com9IhyvtwjWbVAQC");  
    //     GetObjectResponse response = client.GetObject(request);  
    //     StreamReader reader = new StreamReader(response.ResponseStream);  
    //     string response = reader.ReadToEnd();

    //     // string awsUrl = "localhost";
    //     // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}:5000/getImage?clip_input=\"{1}\"", awsUrl, clipInput));
    //     // HttpWebResponse response = (HttpWebResponse)request.GetResponse();

    //     byte[] imageData = null;
    //     using (StreamReader reader = new StreamReader(response.GetResponseStream()))
    //     {
    //         using (var memStream = new MemoryStream())
    //         {
    //             var buffer = new byte[512];
    //             var bytesRead = default(int);
    //             while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
    //                 memStream.Write(buffer, 0, bytesRead);
    //             imageData = memStream.ToArray();
    //         }
    //     }
    //     Texture2D texture = new Texture2D(2, 2);
    //     texture.LoadImage(imageData);
    //     return texture;
    // }
    */
}
