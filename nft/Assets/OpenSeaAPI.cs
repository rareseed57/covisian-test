using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OpenSeaAPI : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private Animator popup;

    private GameObject _nftTitle;
    private GameObject _nftCollection;
    private GameObject _nftId;

    private void Start()
    {
        _nftTitle = GameObject.FindWithTag("NFTtitleGUI");
        _nftCollection = GameObject.FindWithTag("NFTcollectionGUI");
        _nftId = GameObject.FindWithTag("NFTidGUI");

        StartCoroutine(FetchNft());
    }

    private IEnumerator FetchNft()
    {
        string url = "https://api.opensea.io/v2/collection/3dspirits/nfts?limit=1";
        UnityWebRequest request;

        do
        {
            request = UnityWebRequest.Get(url);
            request.SetRequestHeader("accept", "application/json");
            request.SetRequestHeader("X-API-KEY", "73f9305e34af46339e397cf6eb9437cd");

            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Request failed: " + request.error);
                yield return new WaitForSeconds(5);
            }
        } while (request.result != UnityWebRequest.Result.Success);
        

        OpenSea info = JsonUtility.FromJson<OpenSea>(request.downloadHandler.text);

        _nftTitle.GetComponent<TextMeshProUGUI>().text = info.nfts[0].name;
        _nftCollection.GetComponent<TextMeshProUGUI>().text = info.nfts[0].collection;
        _nftId.GetComponent<TextMeshProUGUI>().text = info.nfts[0].identifier;
        
        yield return StartCoroutine(DownloadImage(info.nfts[0].image_url));
    }

    private IEnumerator DownloadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Image download failed: " + www.error);
            yield break;
        }

        Texture texture = DownloadHandlerTexture.GetContent(www);
        img.texture = texture;

        Popup();
    }

    public void Popup()
    {
        popup.enabled = true;
    }
}