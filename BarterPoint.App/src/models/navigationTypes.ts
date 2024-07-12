import {RouteProp} from "@react-navigation/native"
import {StackNavigationProp} from "@react-navigation/stack"
import {Product} from "../screens/Data/Models/Product"

export interface Offer
{
  productOffered: string
  productRequested: string
}

export type RouteParams = {
  newOffer?: Offer
}

export type RootStackParamList = {
  ProductDetail: {product: Product}
  ProductList: undefined
  SignIn: undefined
  CreateAccount: undefined
  LandingPage: undefined
  PostItem: undefined
  Profile: undefined
  UploadSightingImageForm: {
    photoUri: string
  }
  ProductsPage: undefined
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

export type ProductDetailScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "ProductDetail"
>

export type ProductDetailScreenRouteProp = RouteProp<
  RootStackParamList,
  "ProductDetail"
>

export type BidsScreenNavigationProp = StackNavigationProp<
  RootStackParamList,
  "BidScreen"
>
