import {ProductCategory} from "./ProductCategory"

export interface Product
{
  id: string
  name: string
  image: string
  description: string
  tradeFor: string
  category: ProductCategory
  condition: 'New' | 'Used' | 'Refurbished'
  location: string
  ownerId: string
  dimensions?: {width: number; height: number; depth: number; weight: number}
  dateListed: string
}