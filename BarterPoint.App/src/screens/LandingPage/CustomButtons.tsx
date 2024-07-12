import React from "react"
import {View, Text, TouchableOpacity, StyleSheet} from "react-native"
import {useNavigation} from "@react-navigation/native"

import {LandingPageScreenNavigationProp} from "models/navigationTypes"
import {buttonStyles} from "../ButtonStyles"

const CustomButtons: React.FC = () =>
{
  const navigation = useNavigation<LandingPageScreenNavigationProp>()

  const goToProfile = () => navigation.navigate("Profile")
  const goToBids = () => navigation.navigate("BidScreen")
  const goToProductList = () => navigation.navigate("ProductsPage")

  return (
    <View style={styles.buttonContainer}>
      <TouchableOpacity style={buttonStyles.button} onPress={goToProfile}>
        <Text style={buttonStyles.buttonText}>Profile</Text>
      </TouchableOpacity>
      <TouchableOpacity style={buttonStyles.button} onPress={goToProductList}>
        <Text style={buttonStyles.buttonText}>Products</Text>
      </TouchableOpacity>
      <TouchableOpacity style={buttonStyles.button} onPress={goToBids}>
        <Text style={buttonStyles.buttonText}>Bids</Text>
      </TouchableOpacity>
    </View>
  )
}

const styles = StyleSheet.create({
  buttonContainer: {
    flexDirection: "row",
    justifyContent: "space-around",
    marginTop: 20,
  },
})

export default CustomButtons
