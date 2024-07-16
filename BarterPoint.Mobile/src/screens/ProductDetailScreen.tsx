import React, {useState, useEffect} from 'react'
import {View, Text, Image, FlatList, TouchableOpacity, Platform, ToastAndroid, Alert, StyleSheet} from 'react-native'
import {RouteProp, useNavigation} from '@react-navigation/native'
import {useSelector, useDispatch} from 'react-redux'
import {Ionicons} from '@expo/vector-icons'
import {buttonStyles} from '../styles/common/ButtonStyles'
import {RootState, AppDispatch} from '../redux/Store'
import {addBid, fetchAllBids, addFavorite, removeFavorite} from '../api/ApiService'
import {RootStackParamList} from '../navigation/navigationTypes'
import {StackNavigationProp} from '@react-navigation/stack'
import {Product} from '../models/Product'
import {ProductDetailScreenStyles} from '../styles/specific/ProductDetailScreenStyles'

type ProductDetailScreenRouteProp = RouteProp<RootStackParamList, 'ProductDetailScreen'>

interface ProductDetailScreenProps
{
  route: ProductDetailScreenRouteProp
}

const ProductDetailScreen: React.FC<ProductDetailScreenProps> = ({route}) =>
{
  const styles = ProductDetailScreenStyles
  const userProducts = useSelector((state: RootState) => state.products.userProducts) as Product[]
  const {product} = route.params
  const [selectedItem, setSelectedItem] = useState<Product | null>(null)
  const [isFavorited, setIsFavorited] = useState(false)
  const navigation = useNavigation<StackNavigationProp<RootStackParamList>>()
  const dispatch = useDispatch<AppDispatch>()
  const userId = useSelector((state: RootState) => state.users.userId)
  const favorites = useSelector((state: RootState) => state.users.favorites)

  useEffect(() =>
  {
    const favorited = favorites.some(favorite => favorite.productId === product.id)
    setIsFavorited(favorited)
  }, [favorites, product.id])

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

        if (Platform.OS === 'android')
        {
          ToastAndroid.show('Trade offer made successfully!', ToastAndroid.SHORT)
        } else
        {
          Alert.alert('Success', 'Trade offer made successfully!')
        }

        navigation.navigate('LandingPageScreen')
      } else
      {
        const errorMessage = (resultAction.payload as string) || 'Failed to make trade offer.'
        if (Platform.OS === 'android')
        {
          ToastAndroid.show(errorMessage, ToastAndroid.SHORT)
        } else
        {
          Alert.alert('Error', errorMessage)
        }
      }
    } else
    {
      if (Platform.OS === 'android')
      {
        ToastAndroid.show('Please select an item to trade.', ToastAndroid.SHORT)
      } else
      {
        Alert.alert('Error', 'Please select an item to trade.')
      }
    }
  }

  const handleToggleFavorite = async () =>
  {
    if (userId)
    {
      if (isFavorited)
      {
        const resultAction = await dispatch(removeFavorite({userId, productId: product.id}))

        if (removeFavorite.fulfilled.match(resultAction))
        {
          setIsFavorited(false)
          if (Platform.OS === 'android')
          {
            ToastAndroid.show('Product removed from favorites!', ToastAndroid.SHORT)
          } else
          {
            Alert.alert('Success', 'Product removed from favorites!')
          }
        } else
        {
          const errorMessage = (resultAction.payload as string) || 'Failed to remove product from favorites.'
          if (Platform.OS === 'android')
          {
            ToastAndroid.show(errorMessage, ToastAndroid.SHORT)
          } else
          {
            Alert.alert('Error', errorMessage)
          }
        }
      } else
      {
        const resultAction = await dispatch(addFavorite({userId, productId: product.id}))

        if (addFavorite.fulfilled.match(resultAction))
        {
          setIsFavorited(true)
          if (Platform.OS === 'android')
          {
            ToastAndroid.show('Product added to favorites!', ToastAndroid.SHORT)
          } else
          {
            Alert.alert('Success', 'Product added to favorites!')
          }
        } else
        {
          const errorMessage = (resultAction.payload as string) || 'Failed to add product to favorites.'
          if (Platform.OS === 'android')
          {
            ToastAndroid.show(errorMessage, ToastAndroid.SHORT)
          } else
          {
            Alert.alert('Error', errorMessage)
          }
        }
      }
    }
  }

  return (
    <View style={styles.container}>
      <Image source={{uri: product.image}} style={styles.productImage} />
      <View style={styles.header}>
        <Text style={styles.productName}>{product.name}</Text>
        <TouchableOpacity onPress={handleToggleFavorite}>
          <Ionicons name={isFavorited ? "heart" : "heart-outline"} size={24} color={isFavorited ? "red" : "#8B4513"} />
        </TouchableOpacity>
      </View>
      <Text style={styles.productDescription}>{product.description}</Text>
      <Text style={styles.tradeFor}>Wants to trade for: {product.tradeFor}</Text>
      <Text style={styles.selectText}>Select an item to trade:</Text>
      <FlatList
        data={userProducts}
        keyExtractor={(item) => item.id}
        horizontal
        renderItem={({item}) => (
          <TouchableOpacity onPress={() => setSelectedItem(item)}>
            <View style={[styles.userItem, selectedItem && selectedItem.id === item.id ? styles.selectedItem : null]}>
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

export default ProductDetailScreen