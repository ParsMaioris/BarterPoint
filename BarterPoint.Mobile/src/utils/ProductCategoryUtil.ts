import {ProductCategory} from "../models/ProductCategory"

export class ProductCategoryUtil
{
    private static categoryMap: {[key in ProductCategory]: number} = {
        [ProductCategory.Electronics]: 1,
        [ProductCategory.Furniture]: 2,
        [ProductCategory.Clothing]: 3,
        [ProductCategory.Books]: 4,
        [ProductCategory.Toys]: 5,
        [ProductCategory.Tools]: 6,
        [ProductCategory.Appliances]: 7,
        [ProductCategory.SportsEquipment]: 8,
        [ProductCategory.MusicalInstruments]: 9,
        [ProductCategory.Art]: 10,
        [ProductCategory.Jewelry]: 11,
        [ProductCategory.Collectibles]: 12,
        [ProductCategory.Automotive]: 13,
        [ProductCategory.Gardening]: 14,
        [ProductCategory.OfficeSupplies]: 15,
        [ProductCategory.PetSupplies]: 16,
        [ProductCategory.HealthAndBeauty]: 17,
        [ProductCategory.HomeDecor]: 18,
        [ProductCategory.OutdoorGear]: 19,
        [ProductCategory.Other]: 20,
    };

    public static getCategoryId(category: ProductCategory): number
    {
        return this.categoryMap[category]
    }
}