using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
// using System.Object.Amazon.Runtime.AmazonServiceClient.Amazon.S3.AmazonS3Client;
// using Amazon.S3;
// using Amazon.S3.Model;
// using 
public class DisplayS3 : MonoBehaviour
{
    /*
    private const string awsBucketName = "imagination-machine";
    private const string awsAccessKey = "AKIA4HUARN52S6RT2DCI";
    private const string awsSecretKey = "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";
    private string awsURLBaseVirtual = "";
 
    void Start()
    {
        awsURLBaseVirtual = "https://" +
           awsBucketName +
           ".s3.amazonaws.com/";

        // MemoryStream imageString = GetImage("neuromancer.webp");
    }

    // async MemoryStream GetImage(string keyName) {
        
    //     GetObjectResponse response = await client.GetObjectAsync(bucketName, keyName);
    //     MemoryStream memoryStream = new MemoryStream();

    //     using (Stream responseStream = response.ResponseStream)
    //     {
    //         responseStream.CopyTo(memoryStream);
    //     }

    //     return memoryStream;

    // }

    // public FileStream GetImage(string keyName)
    // {
    //     using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast2))
    //     {
    //         GetObjectRequest request = new GetObjectRequest
    //         {
    //             BucketName = "imagination-machine",
    //             Key = "neuromancer.webp"
    //         };

    //         using (GetObjectResponse response = client.GetObject(request))
    //         using (Stream responseStream = response.ResponseStream)
    //         using (StreamReader reader = new StreamReader(responseStream))
    //         {
    //             // The following outputs the content of my text file:
    //             // Console.WriteLine(reader.ReadToEnd());
    //             // Do some magic to return content as a stream
    //         }
    //     }
    // }




    private void GetObject()
    {
        string S3BucketName = "imagination-machine";
        string SampleFileName = "neuromancer.webp";
        // ResultText.text = string.Format("fetching {0} from bucket {1}", SampleFileName, S3BucketName);
        var Client = new AmazonS3Client("AKIA4HUARN52S6RT2DCI", "zBy/WmCJ61MjgVzkOwA9Dg/Com9IhyvtwjWbVAQC", "us-east-2");
        AmazonS3Client Client.GetObjectAsync(S3BucketName, SampleFileName, (responseObj) =>
        {
            byte[] data = null;
            var response = responseObj.Response;
            Stream input = response.ResponseStream;

            if (response.ResponseStream != null)
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    data = ms.ToArray();
                }

                //Display Image
                displayTexture.texture = bytesToTexture2D(data);
            }
        });
    }



    // public void UploadFileToAWS3(string FileName, string FilePath)
    // {
        
    //     string currentAWS3Date =  
    //         System.DateTime.UtcNow.ToString(
    //             "ddd, dd MMM yyyy HH:mm:ss ") +
    //             "GMT";
    //     string canonicalString =
    //         "PUT\n\n\n\nx-amz-date:" +
    //         currentAWS3Date + "\n/" +
    //         awsBucketName + "/" + FileName;
    //     UTF8Encoding encode = new UTF8Encoding();
    //     HMACSHA1 signature = new HMACSHA1();
    //     signature.Key = encode.GetBytes(awsSecretKey);
    //     byte[] bytes = encode.GetBytes(canonicalString);
    //     byte[] moreBytes = signature.ComputeHash(bytes);
    //     string encodedCanonical = Convert.ToBase64String(moreBytes);
    //     string aws3Header = "AWS " +
    //         awsAccessKey + ":" +
    //         encodedCanonical;
    //     string URL3 = awsURLBaseVirtual + FileName;
    //     WebRequest requestS3 = 
    //        (HttpWebRequest)WebRequest.Create(URL3);
    //     requestS3.Headers.Add("Authorization", aws3Header);
    //     requestS3.Headers.Add("x-amz-date", currentAWS3Date);
    //     // byte[] fileRawBytes = File.ReadAllBytes(FilePath);
    //     // requestS3.ContentLength = fileRawBytes.Length;

    //     requestS3.Method = "GET";
    //     Stream S3Stream = requestS3.GetRequestStream();
    //     S3Stream.Read(fileRawBytes, 0, fileRawBytes.Length);
    //     // Debug.Log("Sent bytes: " +
    //     //     requestS3.ContentLength +
    //     //     ", for file: " +
    //     //     FileName);
    //     S3Stream.Close();
    // }
    */
}



// Maybe try: https://csharp.hotexamples.com/examples/Amazon.S3/AmazonS3Client/GetObjectAsync/php-amazons3client-getobjectasync-method-examples.html