using System.ComponentModel.DataAnnotations;
using BlazorInputFile;

namespace eShopAsp.BlazorShared.Models;

public class CatalogItem 
{
    public int Id { get; set; }
    public int CatalogTypeId { get; set; }
    public string CatalogType { get; set;} = "NotSet";

    public int CatalogBrandId  { get; set; }
    public string CatalogBrand { get; set; } = "NotSet";

    [Required(ErrorMessage = "The Name field is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; }

    [RegularExpression(@"^\d+(\.\d{0,2})*S", ErrorMessage = "The field Price must be a positive number with maximum two decimals.")]
    [Range(0.01, 1000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public string PictureUri { get; set; }
    public string PictureBase64 { get; set; }
    public string PictureName { get; set; }

    private const int ImageMaximumBytes = 512000;

    public static string? IsvValidImage(string pictureName, string pictureBase64)
    {
        if (string.IsNullOrEmpty(pictureBase64)) return "File not found";
        var fileData  = Convert.FromBase64String(pictureBase64);

        if (fileData.Length <= 0) return "File length is 0";
        if (fileData.Length > ImageMaximumBytes) return "Maximum size is 512kB";
        if (!IsExtensionMethod(pictureName)) return "File is not image";
        return null;
    }

    public static async Task<string> DataToBase64(IFileListEntry fileItem)
    {
        using (var reader = new StreamReader(fileItem.Data))
        {
            using (var memStream = new MemoryStream())
            {
                await reader.BaseStream.CopyToAsync(memStream);
                var fileData = memStream.ToArray();
                var endcodedBase64 = Convert.ToBase64String(fileData);
                return endcodedBase64;
            }
        }
    }

    private static bool IsExtensionMethod(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
    }
}