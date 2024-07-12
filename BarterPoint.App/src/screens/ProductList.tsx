import React, {useState} from "react"
import
{
  View,
  Text,
  FlatList,
  StyleSheet,
  Image,
  TouchableOpacity,
} from "react-native"
import {useSelector} from "react-redux"
import useProductList from "./useProductList"
import {RootState} from "../redux/Store"
import {Product} from "../models/Product"
import CategoryFilter from "./CategoryFilter"
import {ProductCategory} from "../models/ProductCategory"

interface ProductListProps
{
  showCategoryFilter?: boolean
  allowNavigation?: boolean
}

const ProductList: React.FC<ProductListProps> = ({showCategoryFilter = false, allowNavigation = true}) =>
{
  const products = useSelector((state: RootState) => state.products.products) as Product[]
  const {handlePress} = useProductList()
  const [selectedCategory, setSelectedCategory] = useState<ProductCategory | null>(null)

  const filteredProducts = selectedCategory === null
    ? products
    : products.filter(product => product.category === selectedCategory)

  return (
    <View style={styles.container}>
      {showCategoryFilter && (
        <CategoryFilter
          selectedCategory={selectedCategory}
          onSelectCategory={setSelectedCategory}
        />
      )}
      <FlatList
        data={filteredProducts}
        keyExtractor={(item) => item.id}
        renderItem={({item}) => (
          <TouchableOpacity
            onPress={() => allowNavigation && handlePress(item)}
          >
            <View style={styles.productItem}>
              <Image source={{uri: item.image}} style={styles.productImage} />
              <View style={styles.productInfo}>
                <Text style={styles.productName}>{item.name}</Text>
                <Text style={styles.productDescription}>{item.description}</Text>
              </View>
            </View>
          </TouchableOpacity>
        )}
      />
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    padding: 20,
  },
  productItem: {
    flexDirection: "row",
    alignItems: "center",
    padding: 15,
    borderBottomWidth: 1,
    borderBottomColor: "#ccc",
  },
  productImage: {
    width: 50,
    height: 50,
    borderRadius: 25,
    marginRight: 15,
  },
  productInfo: {
    flex: 1,
  },
  productName: {
    fontSize: 18,
    fontWeight: "bold",
  },
  productDescription: {
    fontSize: 14,
    color: "#666",
  },
})

export default ProductList