import React, {useState} from "react"
import {useDispatch, useSelector} from "react-redux"
import {addProduct, fetchProductsByOwner} from "../api/ApiService"
import {mockPostProducts} from "./Mock"
import
{
  TextInput,
  View,
  StyleSheet,
  Text,
  TouchableOpacity,
  Platform,
  ToastAndroid,
  Alert,
} from "react-native"
import {buttonStyles} from "../styles/common/ButtonStyles"
import {useNavigation} from "@react-navigation/native"
import {LandingPageScreenScreenNavigationProp} from "../navigation/navigationTypes"
import {ProductCategory} from "../models/ProductCategory"
import {AddProductRequest} from "../api/models/AddProductRequest"
import {ProductCategoryUtil} from "../utils/ProductCategoryUtil"
import {RootState} from "../redux/Store"

const PostItemScreen: React.FC = () =>
{
  const navigation = useNavigation<LandingPageScreenScreenNavigationProp>()
  const dispatch = useDispatch<any>()
  const [name, setName] = useState<string>("")
  const [description, setDescription] = useState<string>("")
  const [image, setImage] = useState<string>("")
  const [tradeFor, setTradeFor] = useState<string>("")
  const [category, setCategory] = useState<ProductCategory | null>(null)
  const [condition, setCondition] = useState<"New" | "Used" | "Refurbished">("New")
  const [location, setLocation] = useState<string>("")
  const [dimensions, setDimensions] = useState<{width: number, height: number, depth: number, weight: number}>({
    width: 0,
    height: 0,
    depth: 0,
    weight: 0,
  })
  const ownerId = useSelector((state: RootState) => state.users.currentUser)?.id as string

  const handleAddItem = async () =>
  {
    const newItem: AddProductRequest = {
      name,
      description,
      image,
      tradeFor,
      category: category ? ProductCategoryUtil.getCategoryId(category).toString() : ProductCategoryUtil.getCategoryId(ProductCategory.Other).toString(),
      condition,
      location,
      ownerId,
      dimensionsWidth: dimensions.width,
      dimensionsHeight: dimensions.height,
      dimensionsDepth: dimensions.depth,
      dimensionsWeight: dimensions.weight,
      dateListed: new Date().toISOString(),
    }

    const resultAction = await dispatch(addProduct(newItem))

    if (addProduct.fulfilled.match(resultAction))
    {
      await dispatch(fetchProductsByOwner(ownerId))
      if (Platform.OS === "android")
      {
        ToastAndroid.show("Item posted successfully!", ToastAndroid.SHORT)
      } else
      {
        Alert.alert("Success", "Item posted successfully!")
      }
      navigation.navigate("LandingPageScreen")
    } else
    {
      const errorMessage = (resultAction.payload as string) || "Failed to post item."
      if (Platform.OS === "android")
      {
        ToastAndroid.show(errorMessage, ToastAndroid.SHORT)
      } else
      {
        Alert.alert("Error", errorMessage)
      }
    }
  }

  const handleAutoGenerate = () =>
  {
    const randomIndex = Math.floor(Math.random() * mockPostProducts.length)
    const randomItem = mockPostProducts[randomIndex]
    setName(randomItem.name)
    setDescription(randomItem.description)
    setImage(randomItem.image)
    setTradeFor(randomItem.tradeFor)
    setCategory(randomItem.category)
    setCondition(randomItem.condition)
    setLocation(randomItem.location)
    setDimensions(randomItem.dimensions ?? {width: 0, height: 0, depth: 0, weight: 0})
  }

  return (
    <View style={styles.container}>
      <TextInput
        style={styles.input}
        placeholder="Name"
        value={name}
        onChangeText={setName}
        placeholderTextColor="#999"
      />
      <TextInput
        style={styles.input}
        placeholder="Description"
        value={description}
        onChangeText={setDescription}
        placeholderTextColor="#999"
      />
      <TextInput
        style={styles.input}
        placeholder="Image URL"
        value={image}
        onChangeText={setImage}
        placeholderTextColor="#999"
      />
      <TextInput
        style={styles.input}
        placeholder="Trade For"
        value={tradeFor}
        onChangeText={setTradeFor}
        placeholderTextColor="#999"
      />
      <View style={styles.buttonContainer}>
        <TouchableOpacity style={buttonStyles.button} onPress={handleAddItem}>
          <Text style={buttonStyles.buttonText}>Post Item</Text>
        </TouchableOpacity>
        <TouchableOpacity style={buttonStyles.button} onPress={handleAutoGenerate}>
          <Text style={buttonStyles.buttonText}>Auto Generate</Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: "#F5F5F5",
  },
  title: {
    fontSize: 24,
    fontWeight: "600",
    marginBottom: 20,
    color: "#333",
  },
  input: {
    height: 50,
    borderColor: "#DDD",
    borderWidth: 1,
    borderRadius: 8,
    marginBottom: 15,
    paddingHorizontal: 15,
    backgroundColor: "#FFF",
    shadowColor: "#000",
    shadowOffset: {width: 0, height: 2},
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 2,
  },
  buttonContainer: {
    marginTop: 20,
    flexDirection: "row",
    justifyContent: "space-between",
  },
})

export default PostItemScreen