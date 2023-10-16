using eShopAsp.Core.Entities;
using eShopAsp.Core.Interfaces.Services;

namespace eShopAsp.Core.Services;

public class UriComposer : IUriComposer
{
    private readonly CatalogSettings _catalogSettings;
    public UriComposer(CatalogSettings catalogSettings) => _catalogSettings = catalogSettings;

    public string ComposePicUri(string uriTemplate)
        => uriTemplate.Replace("http://catalogbaseurltobereplaced", _catalogSettings.CatalogBaseUrl);
}