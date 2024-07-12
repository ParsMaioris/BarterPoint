import {StackNavigationProp} from "@react-navigation/stack"
import {Product} from "./Data/Models/Product"
import {useNavigation} from "@react-navigation/native"
import {RootStackParamList} from "models/navigationTypes"

type ProductListNavigationProp = StackNavigationProp<RootStackParamList, 'ProductDetail'>

const useProductList = () =>
{
  const navigation = useNavigation<ProductListNavigationProp>()

  const handlePress = (item: Product) =>
  {
    navigation.navigate("ProductDetail", {product: item})
  }

  return {
    handlePress,
  }
}

export default useProductList
