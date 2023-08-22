// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System;
using System.Collections.Generic;

[Serializable]
public class Nft
{
    public string identifier;
    public string collection;
    public string contract;
    public string token_standard;
    public string name;
    public object description;
    public string image_url;
    public object metadata_url;
    public DateTime created_at;
    public DateTime updated_at;
    public bool is_disabled;
    public bool is_nsfw;
}

[Serializable]
public class OpenSea
{
    public List<Nft> nfts;
}

