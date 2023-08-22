using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OpenSeaAPI : MonoBehaviour
{
    [SerializeField] private RawImage img;

    void Start()
    {
        FetchNft();
    }

    private void FetchNft()
    {
        var url = "https://api.opensea.io/v2/collection/untitled-collection-4266549728/nfts?limit=1";
        
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
        request.Headers.Add("20", "application/json");
        request.Headers.Add("X-API-KEY", "73f9305e34af46339e397cf6eb9437cd");
        
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        OpenSea info = JsonUtility.FromJson<OpenSea>(json);

        StartCoroutine(DownloadImage(info.nfts[0].image_url));
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        Texture texture = DownloadHandlerTexture.GetContent(www);
        img.texture = texture;
    }
}
