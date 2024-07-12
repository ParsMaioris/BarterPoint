import {RouteProp} from "@react-navigation/native"
import {StackNavigationProp} from "@react-navigation/stack"
import {Product} from "../models/Product"

export interface Offer
{
  productOffered: string
  productRequested: string
}

export type RouteParams = {
  newOffer?: Offer
}

export type RootStackParamList = {
  ProductDetailScreen: {product: Product}
  ProductListScreen: undefined
  SignIn: undefined
  CreateAccount: undefined
  LandingPage: undefined
  PostItem: undefined
  Profile: undefined
  UploadSightingImageForm: {
    photoUri: string
  }
  ProductsScreen: undefined
  BidScreen: undefined
}

export type SignInScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "SignIn"
>
export type SignUpScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "CreateAccount"
>
export type LandingPageScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "LandingPage"
>
export type PostItemScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "PostItem"
>

export type UploadSightingImageFormNavigationProp = StackNavigationProp<
  RootStackParamList,
  "UploadSightingImageForm"
>

export type ProfileScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "Profile"
>

export type ProductDetailScreenScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "ProductDetailScreen"
>

export type ProductDetailScreenScreenRouteProp = RouteProp<
  RootStackParamList,
  "ProductDetailScreen"
>

export type BidsScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "BidScreen"
>
