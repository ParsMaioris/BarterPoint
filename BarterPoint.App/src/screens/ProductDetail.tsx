import React, {useState} from "react"
import
{
  View,
  Text,
  Image,
  FlatList,
  TouchableOpacity,
  Platform,
  ToastAndroid,
  Alert,
} from "react-native"
import {RouteProp, useNavigation} from "@react-navigation/native"
import {buttonStyles} from "./ButtonStyles"
import {useSelector, useDispatch} from "react-redux"
import {RootState, AppDispatch} from "./Data/Store"
import {addBid, fetchAllBids} from "./Data/Api/ApiService"
import {RootStackParamList} from "../navigation/navigationTypes"
import {StackNavigationProp} from "@react-navigation/stack"
import {Product} from "../models/Product"
import {productDetailStyles} from "./ProductDetailStyles"

type ProductDetailRouteProp = RouteProp<RootStackParamList, "ProductDetail">

interface ProductDetailProps
{
  route: ProductDetailRouteProp
}

const ProductDetail: React.FC<ProductDetailProps> = ({route}) =>
{
  const styles = productDetailStyles
  const userProducts = useSelector(
    (state: RootState) => state.products.userProducts
  ) as Product[]
  const {product} = route.params
  const [selectedItem, setSelectedItem] = useState<Product | null>(null)
  const navigation = useNavigation<StackNavigationProp<RootStackParamList>>()
  const dispatch = useDispatch<AppDispatch>()

  const handleTradeOffer = async () =>
  {
    if (selectedItem)
    {
      const bid = {
        product1Id: selectedItem.id,
        product2Id: product.id,
      }

      const resultAction = await dispatch(addBid(bid))

      if (addBid.fulfilled.match(resultAction))
      {
        await dispatch(fetchAllBids())

        if (Platform.OS === "android")
        {
          ToastAndroid.show("Trade offer made successfully!", ToastAndroid.SHORT)
        } else
        {
          Alert.alert("Success", "Trade offer made successfully!")
        }

        navigation.navigate("LandingPage")
      } else
      {
        const errorMessage = (resultAction.payload as string) || 'Failed to make trade offer.'
        if (Platform.OS === "android")
        {
          ToastAndroid.show(errorMessage, ToastAndroid.SHORT)
        } else
        {
          Alert.alert("Error", errorMessage)
        }
      }
    } else
    {
      if (Platform.OS === "android")
      {
        ToastAndroid.show("Please select an item to trade.", ToastAndroid.SHORT)
      } else
      {
        Alert.alert("Error", "Please select an item to trade.")
      }
    }
  }

  return (
    <View style={styles.container}>
      <Image source={{uri: product.image}} style={styles.productImage} />
      <Text style={styles.productName}>{product.name}</Text>
      <Text style={styles.productDescription}>{product.description}</Text>
      <Text style={styles.tradeFor}>Wants to trade for: {product.tradeFor}</Text>
      <Text style={styles.selectText}>Select an item to trade:</Text>
      <FlatList
        data={userProducts}
        keyExtractor={(item) => item.id}
        horizontal
        renderItem={({item}) => (
          <TouchableOpacity onPress={() => setSelectedItem(item)}>
            <View
              style={[
                styles.userItem,
                selectedItem && selectedItem.id === item.id ? styles.selectedItem : null,
              ]}
            >
              <Image source={{uri: item.image}} style={styles.userImage} />
              <Text style={styles.userName}>{item.name}</Text>
            </View>
          </TouchableOpacity>
        )}
      />
      <TouchableOpacity style={buttonStyles.button} onPress={handleTradeOffer}>
        <Text style={buttonStyles.buttonText}>Make Trade Offer</Text>
      </TouchableOpacity>
    </View>
  )
}

export default ProductDetail