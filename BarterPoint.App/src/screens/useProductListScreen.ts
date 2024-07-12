import {StackNavigationProp} from "@react-navigation/stack"
import {Product} from "../models/Product"
import {useNavigation} from "@react-navigation/native"
import {RootStackParamList} from "../navigation/navigationTypes"

type ProductListScreenNavigationProp = StackNavigationProp<RootStackParamList, 'ProductDetail'>

const useProductListScreen = () =>
{
  const navigation = useNavigation<ProductListScreenNavigationProp>()

  const handlePress = (item: Product) =>
  {
    navigation.navigate("ProductDetail", {product: item})
  }

  return {
    handlePress,
  }
}

export default useProductListScreen
