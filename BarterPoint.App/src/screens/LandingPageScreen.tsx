import React, {useEffect} from "react"
import {View, Text, Image, StyleSheet} from "react-native"
import LandingPageButtons from "./LandingPageButtons"
import {mockAppLogo} from "./Mock"
import {useDispatch, useSelector} from "react-redux"
import {AppDispatch, RootState} from "../redux/Store"
import {fetchProductsNotOwnedByUser, fetchProductsByOwner, fetchAllBids} from "../api/ApiService"
import ProductListScreen from "./ProductListScreen"

const LandingPageScreen: React.FC = () =>
{
  const dispatch = useDispatch<AppDispatch>()
  const status = useSelector((state: RootState) => state.products.status)
  const error = useSelector((state: RootState) => state.products.error)
  const currentUser = useSelector((state: RootState) => state.users.currentUser)

  useEffect(() =>
  {
    if (currentUser?.id)
    {
      dispatch(fetchProductsNotOwnedByUser(currentUser.id))
      dispatch(fetchProductsByOwner(currentUser.id))
      dispatch(fetchAllBids())
    }
  }, [dispatch, currentUser])

  if (status === "loading")
  {
    return <Text>Loading...</Text>
  }

  if (status === "failed")
  {
    return <Text>Error: {error}</Text>
  }

  return (
    <View style={styles.container}>
      <View style={styles.heroSection}>
        <Image
          source={{
            uri: mockAppLogo,
          }}
          style={styles.heroImage}
        />
        <Text style={styles.tagline}>
          Discover Amazing Items to Barter or Trade
        </Text>
        <Text style={styles.description}>
          Join our community and start trading your items with others. It's
          simple, fun, and rewarding!
        </Text>
      </View>
      <ProductListScreen />
      <View style={styles.buttonContainer}>
        <LandingPageButtons />
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f5f5f5",
  },
  heroSection: {
    alignItems: "center",
    padding: 20,
    backgroundColor: "#ffffff",
    borderBottomWidth: 1,
    borderBottomColor: "#dddddd",
    marginBottom: 10,
  },
  heroImage: {
    width: 200,
    height: 200,
    resizeMode: "cover",
    borderRadius: 100,
    marginBottom: 10,
  },
  tagline: {
    fontSize: 24,
    fontWeight: "bold",
    color: "#333333",
    textAlign: "center",
    marginVertical: 10,
  },
  description: {
    fontSize: 16,
    color: "#666666",
    textAlign: "center",
    paddingHorizontal: 20,
  },
  row: {
    flex: 1,
    justifyContent: "space-between",
    padding: 2,
  },
  noItemsText: {
    textAlign: "center",
    color: "#999999",
    marginTop: 20,
  },
  buttonContainer: {
    padding: 10,
  },
})

export default LandingPageScreen